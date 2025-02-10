using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Queries.GetUsers;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Users.Operations.CRUD.Queries.GetUsers {

    /// <summary>
    /// Consulta para obtener todos los usuarios.
    /// </summary>
    [RequiredPermissions([SystemPermissions.GetUsers, SystemPermissions.GetEntities])]
    [AssociatedOperationHandlerFactory(typeof(IUserOperationHandlerFactory))]
    public class GetUsers_Query : IGetUsers_Query {

        public bool EnableTracking { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para obtener todos los usuarios.
        /// </summary>
        public GetUsers_Query (bool enableTracking = false) => EnableTracking = enableTracking;

    }

}