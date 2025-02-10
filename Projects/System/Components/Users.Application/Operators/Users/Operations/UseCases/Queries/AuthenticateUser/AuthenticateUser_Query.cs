using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Queries.AuthenticateUser;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Users.Operations.UseCases.Queries.AuthenticateUser {

    /// <summary>
    /// Consulta para autenticar a un usuario.
    /// </summary>
    [RequiredPermissions(SystemPermissions.AuthenticateUser)]
    [AssociatedOperationHandlerFactory(typeof(IUserOperationHandlerFactory))]
    public class AuthenticateUser_Query : IAuthenticateUser_Query {

        /// <summary>
        /// Obtiene o establece el nombre de usuario.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Obtiene o establece la contraseña del usuario.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para autenticar a un usuario.
        /// </summary>
        /// <param name="username">El nombre de usuario.</param>
        /// <param name="password">La contraseña del usuario.</param>
        public AuthenticateUser_Query (string username, string password) {
            Username = username;
            Password = password;
        }

    }

}