using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Queries.GetUserByUsername;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Users.Operations.UseCases.Queries.GetUserByUsername {

    /// <summary>
    /// Consulta para obtener un usuario por su nombre de usuario.
    /// </summary>
    [RequiredPermissions([SystemPermissions.GetUserByUsername, SystemPermissions.GetEntities])]
    [AssociatedOperationHandlerFactory(typeof(IUserOperationHandlerFactory))]
    public class GetUserByUsername_Query : IGetUserByUsername_Query {

        /// <summary>
        /// Obtiene el nombre de usuario del usuario.
        /// </summary>
        public string Username { get; }

        public bool EnableTracking { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GetUserByUsername_Query"/>.
        /// </summary>
        /// <param name="username">El nombre de usuario del usuario a obtener.</param>
        public GetUserByUsername_Query (string username, bool enableTracking = false) {
            Username = username;
            EnableTracking = enableTracking;
        }

    }

}