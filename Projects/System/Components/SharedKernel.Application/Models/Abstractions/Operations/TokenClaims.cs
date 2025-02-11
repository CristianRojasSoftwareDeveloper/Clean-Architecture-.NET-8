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

using SharedKernel.Domain.Models.Abstractions.Enumerations;
using System.Text;

namespace SharedKernel.Application.Models.Abstractions.Operations {

    /// <summary>
    /// Clase que representa los claims de un token de autenticación.
    /// </summary>
    public class TokenClaims {

        /// <summary>
        /// Identificador único del usuario al que pertenecen los claims.
        /// </summary>
        public int UserID { get; }

        /// <summary>
        /// Nombre de usuario del usuario al que pertenecen los claims.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Correo electrónico del usuario al que pertenecen los claims.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Colección de nombres de roles a los que pertenece el usuario.
        /// </summary>
        public IEnumerable<string> Roles { get; set; }

        /// <summary>
        /// Colección de permisos que posee el usuario.
        /// </summary>
        public IEnumerable<SystemPermissions> Permissions { get; set; }

        /// <summary>
        /// Constructor para la clase TokenClaims.
        /// </summary>
        /// <param name="userID">Identificador único del usuario.</param>
        /// <param name="username">Nombre de usuario del usuario.</param>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="roles">Colección de nombres de roles. Si es nula, se inicializa como lista vacía.</param>
        /// <param name="permissions">Colección de permisos de aplicación. Si es nula, se inicializa como lista vacía.</param>
        public TokenClaims (int userID, string username, string email, IEnumerable<string> roles, IEnumerable<SystemPermissions> permissions) {
            UserID = userID;
            Username = username;
            Email = email;
            Roles = roles;
            Permissions = permissions;
        }

        /// <summary>
        /// Crea un conjunto de tokenClaims para un usuario invitado.
        /// </summary>
        /// <returns>Claims de usuario invitado.</returns>
        public static TokenClaims CreateGuestTokenClaims () => new(
            // ID 0 representa un usuario invitado sin identificador real en el sistema
            userID: 0,
            // Nombre de usuario genérico "Guest" para usuarios no autenticados
            username: "Guest",
            // Email vacío ya que un usuario invitado no tiene correo asociado
            email: string.Empty,
            // Asigna solo el rol básico de "Guest" 
            roles: ["Guest"],
            // Otorga permisos mínimos: solo registrarse y autenticarse
            permissions: [SystemPermissions.RegisterUser, SystemPermissions.AuthenticateUser]
        );

        /// <summary>
        /// Devuelve una representación en cadena de la instancia de TokenClaims.
        /// </summary>
        /// <returns>Una cadena que representa la instancia de TokenClaims.</returns>
        public override string ToString () {

            // Función auxiliar para formatear valores posiblemente nulos
            static string FormatNullableValue<T> (T? value, string defaultValue = "No especificado") => value?.ToString() ?? defaultValue;

            var sb = new StringBuilder("TokenClaims:\n");

            sb.AppendLine($"\tUserID: {FormatNullableValue(UserID)}");
            sb.AppendLine($"\tUsername: {FormatNullableValue(Username)}");
            sb.AppendLine($"\tEmail: {FormatNullableValue(Email)}");

            // Formatear roles (nombres dinámicos)
            string rolesStr = Roles.Any() ? $"[ {string.Join(", ", Roles)} ]" : "No asignados";
            sb.AppendLine($"\tRoles: {rolesStr}");

            // Formatear permisos (enumeración estática)
            string permissionsStr = Permissions.Any() ? $"[ {string.Join(", ", Permissions.Select(p => p.ToString()))} ]" : "No asignados";
            sb.AppendLine($"\tPermissions: {permissionsStr}");

            return sb.ToString();

        }

    }

}