using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Queries.GetRoles;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Roles.Operations.CRUD.Queries.GetRoles {

    /// <summary>
    /// Consulta para obtener todos los roles.
    /// </summary>
    [RequiredPermissions([SystemPermissions.GetRoles, SystemPermissions.GetEntities])]
    [AssociatedOperationHandlerFactory(typeof(IRoleOperationHandlerFactory))]
    public class GetRoles_Query : IGetRoles_Query {

        public bool EnableTracking { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta para obtener todos los roles.
        /// </summary>
        public GetRoles_Query (bool enableTracking = false) => EnableTracking = enableTracking;

    }

}