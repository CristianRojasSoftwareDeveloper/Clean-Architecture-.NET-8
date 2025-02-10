using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Queries.GetPermissionByID;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Permissions.Operations.CRUD.Queries.GetPermissionByID {

    /// <summary>
    /// Consulta para obtener un permiso por su ID.
    /// </summary>
    [RequiredPermissions([SystemPermissions.GetPermissionByID, SystemPermissions.GetEntityByID])]
    [AssociatedOperationHandlerFactory(typeof(IPermissionOperationHandlerFactory))]
    public class GetPermissionByID_Query : IGetPermissionByID_Query {

        /// <summary>
        /// Obtiene el ID del permiso a recuperar.
        /// </summary>
        public int ID { get; }

        public bool EnableTracking { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la consulta con el ID del permiso especificado.
        /// </summary>
        /// <param name="permissionID">El ID del permiso a recuperar.</param>
        public GetPermissionByID_Query (int permissionID, bool enableTracking = false) {
            ID = permissionID;
            EnableTracking = enableTracking;
        }

    }

}