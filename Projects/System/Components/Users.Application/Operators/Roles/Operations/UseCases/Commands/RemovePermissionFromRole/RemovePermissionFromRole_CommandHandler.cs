using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Commands.RemovePermissionFromRole;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Roles.Operations.UseCases.Commands.RemovePermissionFromRole {

    /// <summary>
    /// Manejador para el comando de eliminación de un permiso de un rol.
    /// </summary>
    public class RemovePermissionFromRole_CommandHandler : IRemovePermissionFromRole_CommandHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos de eliminación de permisos de roles.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public RemovePermissionFromRole_CommandHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja el comando para eliminar un permiso de un rol de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando para eliminar el permiso.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un valor booleano que indica si se eliminó exitosamente.</returns>
        public async Task<PermissionAssignedToRole> Handle (IRemovePermissionFromRole_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del rol es válido y existe en el repositorio de roles
            if (command.RoleID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.RoleID), "El identificador del rol de usuario no es válido"));

            // Verificar si el identificador del permiso es válido y existe en el repositorio de permisos
            if (command.PermissionID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.PermissionID), "El identificador del permiso de usuario no es válido"));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);
            else {
                var permissionAssignedToRole = await _unitOfWork.PermissionAssignedToRoleRepository.GetPermissionAssignedToRoleByForeignKeys(command.RoleID, command.PermissionID);
                if (permissionAssignedToRole == null)
                    throw NotFoundError.Create("PermissionAssignedToRole");
                else
                    return await _unitOfWork.PermissionAssignedToRoleRepository.DeletePermissionAssignedToRoleByID((int) permissionAssignedToRole.ID!);
            }

        }

    }

}