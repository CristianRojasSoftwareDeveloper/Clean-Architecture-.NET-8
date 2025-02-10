using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Domain.Models.Entities.Users;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth {

    /// <summary>
    /// Interfaz que define los métodos para la autenticación y generación de tokens JWT.
    /// </summary>
    public interface IAuthService {

        /// <summary>
        /// Método para hashear una contraseña utilizando el algoritmo BCrypt.
        /// </summary>
        /// <param name="password">Contraseña a hashear.</param>
        /// <returns>Hash de la contraseña.</returns>
        string HashPassword (string password);

        /// <summary>
        /// Método para verificar si una contraseña coincide con su hash utilizando el algoritmo BCrypt.
        /// </summary>
        /// <param name="password">Contraseña a verificar.</param>
        /// <param name="hashedPassword">Hash de la contraseña.</param>
        /// <returns>True si la contraseña es válida, False de lo contrario.</returns>
        bool VerifyPassword (string password, string hashedPassword);

        /// <summary>
        /// Método para generar un token JWT.
        /// </summary>
        /// <param name="user">Usuario asociado al token, incluyendo sus roles y permisos.</param>
        /// <returns>Token JWT generado.</returns>
        string GenerateToken (User user);

        /// <summary>
        /// Método para validar un token JWT y obtener los atributos en este.
        /// </summary>
        /// <param name="token">Token JWT a validar.</param>
        /// <returns>TokenClaims con los atributos extraídos del token.</returns>
        TokenClaims ValidateToken (string token);

    }

}