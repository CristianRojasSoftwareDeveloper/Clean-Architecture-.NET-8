using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.UseCases.Queries.GetPermissionsByRoleID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Permissions.Operations.UseCases.Queries.GetPermissionsByRoleID {

    /// <summary>
    /// Manejador para la consulta de obtención de permisos por ID de rol.
    /// </summary>
    public class GetPermissionsByRoleID_QueryHandler : IGetPermissionsByRoleID_QueryHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador de consulta.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public GetPermissionsByRoleID_QueryHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja la consulta de obtención de permisos de manera asíncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el ID del rol.</param>
        /// <returns>Una tarea que representa la operación asíncrona y contiene una lista de permisos asociados al rol especificado.</returns>
        public async Task<List<Permission>> Handle (IGetPermissionsByRoleID_Query query) {
            var permissionAssignedToRoles = await _unitOfWork.PermissionAssignedToRoleRepository.GetPermissionAssignedToRolesByRoleID(query.RoleID, query.EnableTracking);
            var permissions = permissionAssignedToRoles.Select(permissionAssignedToRole => permissionAssignedToRole.Permission).ToList();
            return permissions;
        }

    }

}