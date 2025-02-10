using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Queries.GetRolesByUserID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Roles.Operations.UseCases.Queries.GetRolesByUserID {

    /// <summary>
    /// Manejador para la consulta de obtención de roles de usuario por su ID.
    /// </summary>
    public class GetRolesByUserID_QueryHandler : IGetRolesByUserID_QueryHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador de la consulta para obtener roles de usuario por su ID.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public GetRolesByUserID_QueryHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja la consulta para obtener los roles de usuario de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta para obtener los roles de usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona con una lista de roles asociados al usuario.</returns>
        public async Task<List<Role>> Handle (IGetRolesByUserID_Query query) {
            var rolesAssignedToUser = await _unitOfWork.RoleAssignedToUserRepository.GetRolesAssignedToUserByUserID(query.UserID, query.EnableTracking);
            var roles = rolesAssignedToUser.Select(roleAssignedToUser => roleAssignedToUser.Role).ToList();
            return roles;
        }

    }

}