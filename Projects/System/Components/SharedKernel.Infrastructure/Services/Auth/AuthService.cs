#region GPL v3 License Header

/*
 * Clean Architecture - .NET 8
 * Copyright (C) 2025 Cristian Rojas Arredondo
 *
 * Author / Contact:
 *   Cristian Rojas Arredondo «cristian.rojas.software.developer@gmail.com»
 *
 * A full copy of the GNU GPL v3 is provided in the root of this project in the "LICENSE" file.
 * Additionally, you can view the license at <https://www.gnu.org/licenses/gpl-3.0.html>.
 */

#region «English version» GPL v3 License Information 
/*
 * This file is part of Clean Architecture - .NET 8.
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <https://www.gnu.org/licenses/gpl-3.0.html>.
 * 
 * Note: In the event of any discrepancy between translations, this version (English) shall prevail.
 */
#endregion

#region «Spanish version» GPL v3 License Information
/*
 * Este archivo es parte de Clean Architecture - .NET 8.
 *
 * Este programa es software libre: puede redistribuirlo y/o modificarlo
 * bajo los términos de la Licencia Pública General de GNU publicada por
 * la Free Software Foundation, ya sea la versión 3 de la Licencia o
 * (a su elección) cualquier versión posterior.
 *
 * Este programa se distribuye con la esperanza de que sea útil,
 * pero SIN NINGUNA GARANTÍA, incluso sin la garantía implícita de
 * COMERCIABILIDAD o IDONEIDAD PARA UN PROPÓSITO PARTICULAR.
 * Consulte la Licencia Pública General de GNU para más detalles.
 *
 * Usted debería haber recibido una copia de la Licencia Pública General de GNU
 * junto con este programa. De no ser así, véase <https://www.gnu.org/licenses/gpl-3.0.html>.
 * 
 * Nota: En caso de cualquier discrepancia entre las traducciones, la versión en inglés prevalecerá.
 */
#endregion

#endregion

using Microsoft.IdentityModel.Tokens;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Entities.Users;
using SharedKernel.Infrastructure.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SharedKernel.Infrastructure.Services.Auth {

    /// <summary>
    /// Implementación del servicio de autenticación que proporciona funcionalidades para la gestión de contraseñas y tokens JWT.
    /// </summary>
    public class AuthService : IAuthService {

        private readonly JWT_Settings _jwtSettings;
        private const string CLAIM_DELIMITER = " | ";

        /// <summary>
        /// Inicializa una nueva instancia del servicio de autenticación.
        /// </summary>
        /// <param name="jwtSettings">Configuración para la generación y validación de tokens JWT.</param>
        /// <exception cref="ArgumentNullException">Se lanza cuando jwtSettings es nulo.</exception>
        public AuthService (JWT_Settings jwtSettings) {
            _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
        }

        /// <summary>
        /// Genera un hash seguro de una contraseña utilizando el algoritmo BCrypt.
        /// </summary>
        /// <param name="password">Contraseña en texto plano a hashear.</param>
        /// <returns>Hash de la contraseña generado con BCrypt.</returns>
        /// <exception cref="ArgumentException">Se lanza cuando la contraseña está vacía o es nula.</exception>
        public string HashPassword (string password) {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede estar vacía.", nameof(password));
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Verifica si una contraseña coincide con su hash utilizando BCrypt.
        /// </summary>
        /// <param name="password">Contraseña en texto plano a verificar.</param>
        /// <param name="hashedPassword">Hash de la contraseña almacenado.</param>
        /// <returns>True si la contraseña coincide con el hash, False en caso contrario.</returns>
        /// <exception cref="ArgumentException">Se lanza cuando alguno de los parámetros está vacío o es nulo.</exception>
        public bool VerifyPassword (string password, string hashedPassword) {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede estar vacía.", nameof(password));
            if (string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentException("El hash de la contraseña no puede estar vacío.", nameof(hashedPassword));
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        /// <summary>
        /// Genera un token JWT para un usuario, incluyendo sus roles y permisos.
        /// </summary>
        /// <param name="user">Usuario para el cual se generará el token, debe incluir sus roles y permisos.</param>
        /// <returns>Token JWT generado como string.</returns>
        /// <exception cref="ArgumentNullException">Se lanza cuando el usuario o sus roles son nulos.</exception>
        /// <exception cref="InvalidOperationException">Se lanza cuando hay un error en la generación del token.</exception>
        public string GenerateToken (User user) {
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(user.RolesAssignedToUser, nameof(user.RolesAssignedToUser));
            try {
                var claims = BuildUserClaims(user);
                var token = CreateSecurityToken(claims);
                return new JwtSecurityTokenHandler().WriteToken(token);
            } catch (Exception ex) {
                throw new InvalidOperationException("Error al generar el token JWT", ex);
            }
        }

        /// <summary>
        /// Valida un token JWT y extrae sus claims.
        /// </summary>
        /// <param name="token">Token JWT a validar.</param>
        /// <returns>Objeto TokenClaims con la información extraída del token.</returns>
        /// <exception cref="SecurityTokenException">Se lanza cuando el token es inválido o ha expirado.</exception>
        public TokenClaims ValidateToken (string token) {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("El token no puede estar vacío.", nameof(token));
            try {
                var principal = ValidateTokenAndGetPrincipal(token);
                return ExtractTokenClaims(principal.Claims);
            } catch (SecurityTokenExpiredException) {
                throw new SecurityTokenException("El token JWT ha expirado");
            } catch (SecurityTokenException ex) {
                throw new SecurityTokenException("El token JWT no es válido", ex);
            } catch (Exception ex) {
                throw new SecurityTokenException("Ha ocurrido un error durante la validación del token JWT", ex);
            }
        }

        #region Métodos privados auxiliares

        /// <summary>
        /// Construye la lista de claims para un usuario.
        /// </summary>
        private static List<Claim> BuildUserClaims (User user) {
            var claims = new List<Claim> {
                new ("ID", user.ID.ToString()!),
                new ("Username", user.Username ?? throw new InvalidOperationException("El nombre de usuario no puede ser nulo")),
                new ("Email", user.Email ?? throw new InvalidOperationException("El email no puede ser nulo"))
            };

            // Extrae los nombres de los roles de un usuario.
            var roles = user.RolesAssignedToUser.Select(roleAssignedToUser => roleAssignedToUser.Role!.Name!);
            // Agrega los roles del usuario como un único claim en el token.
            claims.Add(new Claim("Roles", string.Join(CLAIM_DELIMITER, roles)));

            // Extraer y agregar permisos como un único claim
            var permissions = ExtractUserPermissions(user);
            claims.Add(new Claim("SystemPermissions", string.Join(CLAIM_DELIMITER, permissions)));

            return claims;
        }

        /// <summary>
        /// Extrae los permisos de un usuario.
        /// </summary>
        private static IEnumerable<SystemPermissions> ExtractUserPermissions (User user) {
            return user.RolesAssignedToUser
                .Where(ur => ur?.Role?.PermissionAssignedToRoles != null)
                .SelectMany(ur => ur.Role!.PermissionAssignedToRoles!)
                .Where(rp => rp?.Permission != null)
                .Select(rp => Enum.Parse<SystemPermissions>(rp.Permission!.Name!))
                .Distinct();
        }

        /// <summary>
        /// Crea un token de seguridad JWT.
        /// </summary>
        private SecurityToken CreateSecurityToken (IEnumerable<Claim> claims) {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            return new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
        }

        /// <summary>
        /// Valida un token JWT y obtiene su ClaimsPrincipal.
        /// </summary>
        private ClaimsPrincipal ValidateTokenAndGetPrincipal (string token) {

            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var validationParameters = new TokenValidationParameters {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out var validatedToken);
            if (validatedToken is not JwtSecurityToken jwtToken || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("El algoritmo de firma del token no es válido");

            return principal;

        }

        /// <summary>
        /// Extrae los claims de un token y los convierte en un objeto TokenClaims.
        /// </summary>
        /// <exception cref="SecurityTokenException">
        /// Se lanza cuando:
        /// - Faltan claims obligatorios (ID, Username, Email, Roles, SystemPermissions)
        /// - El ID no es un número válido
        /// - Los roles o permisos están vacíos
        /// - Hay permisos con formato inválido
        /// </exception>
        private static TokenClaims ExtractTokenClaims (IEnumerable<Claim> claims) {

            // Validar claims básicos obligatorios
            var userIdClaim = claims.FirstOrDefault(c => c.Type == "ID")
                ?? throw new SecurityTokenException("El token no contiene el claim obligatorio «ID»");
            // Validar formato de ID
            if (!int.TryParse(userIdClaim.Value, out int userId))
                throw new SecurityTokenException("El claim «ID» no contiene un número válido");

            var usernameClaim = claims.FirstOrDefault(c => c.Type == "Username")
                ?? throw new SecurityTokenException("El token no contiene el claim obligatorio «Username»");

            var emailClaim = claims.FirstOrDefault(claim => claim.Type == "Email")
                ?? throw new SecurityTokenException("El token no contiene el claim obligatorio «Email»");

            // Extraer y validar roles
            var rolesClaim = claims.FirstOrDefault(claim => claim.Type == "Roles")
                ?? throw new SecurityTokenException("El token no contiene el claim obligatorio «Roles»");

            var roles = rolesClaim.Value.Split(CLAIM_DELIMITER, StringSplitOptions.RemoveEmptyEntries);
            if (roles.Length == 0)
                throw new SecurityTokenException("El token debe contener al menos un rol");

            // Extraer y validar permisos
            var permissionsClaim = claims.FirstOrDefault(claim => claim.Type == "SystemPermissions")
                ?? throw new SecurityTokenException("El token no contiene el claim obligatorio «SystemPermissions»");

            var permissions = ParsePermissionsClaim(permissionsClaim.Value);

            // Crear y retornar TokenClaims con todos los datos validados
            return new TokenClaims(
                userID: userId,
                username: usernameClaim.Value,
                email: emailClaim.Value,
                roles: roles,
                permissions: permissions
            );

        }

        /// <summary>
        /// Parsea y valida los permisos contenidos en un claim.
        /// </summary>
        /// <param name="permissionsValue">Valor del claim de permisos.</param>
        /// <returns>Colección de permisos parseados.</returns>
        /// <exception cref="SecurityTokenException">
        /// Se lanza cuando:
        /// - No hay permisos en el claim
        /// - Algún permiso no es válido
        /// - El formato de los permisos es inválido
        /// </exception>
        private static IEnumerable<SystemPermissions> ParsePermissionsClaim (string permissionsValue) {
            try {
                var permissions = permissionsValue
                    .Split(CLAIM_DELIMITER, StringSplitOptions.RemoveEmptyEntries)
                    .Select(permissionString =>
                        Enum.TryParse<SystemPermissions>(permissionString, out var permission) ? permission : throw new SecurityTokenException($"El permiso «{permissionString}» no es un valor válido de la enumeración SystemPermissions"));
                if (!permissions.Any())
                    throw new SecurityTokenException("El token debe contener al menos un permiso");
                return permissions;
            } catch (ArgumentException ex) {
                throw new SecurityTokenException("El token contiene permisos con formato inválido", ex);
            }
        }

        #endregion

    }

}