using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Queries.GetRoleByID;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Roles.Operations.CRUD.Queries.GetRoleByID {

    /// <summary>
    /// Consulta para obtener un rol por su ID.
    /// </summary>
    [RequiredPermissions([SystemPermissions.GetRoleByID, SystemPermissions.GetEntityByID])]
    [AssociatedOperationHandlerFactory(typeof(IRoleOperationHandlerFactory))]
    public class GetRoleByID_Query : IGetRoleByID_Query {

        /// <summary>
        /// Obtiene el ID del rol a consultar.
        /// </summary>
        public int ID { get; }

        public bool EnableTracking { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta con el ID del rol especificado.
        /// </summary>
        /// <param name="roleID">El ID del rol a recuperar.</param>
        public GetRoleByID_Query (int roleID, bool enableTracking = false) {
            ID = roleID;
            EnableTracking = enableTracking;
        }

    }

}
