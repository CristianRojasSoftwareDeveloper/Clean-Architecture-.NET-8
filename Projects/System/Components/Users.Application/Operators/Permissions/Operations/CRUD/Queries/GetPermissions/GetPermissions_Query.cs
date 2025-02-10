using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Queries.GetPermissions;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Permissions.Operations.CRUD.Queries.GetPermissions {

    /// <summary>
    /// Consulta para obtener todos los permisos.
    /// </summary>
    [RequiredPermissions([SystemPermissions.GetPermissions, SystemPermissions.GetEntities])]
    [AssociatedOperationHandlerFactory(typeof(IPermissionOperationHandlerFactory))]
    public class GetPermissions_Query : IGetPermissions_Query {

        public bool EnableTracking { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para obtener todos los permisos.
        /// </summary>
        public GetPermissions_Query (bool enableTracking = false) => EnableTracking = enableTracking;

    }

}