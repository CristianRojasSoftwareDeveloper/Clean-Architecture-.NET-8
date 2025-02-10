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