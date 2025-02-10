using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.RemoveRoleFromUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Users.Operations.UseCases.Commands.RemoveRoleFromUser {

    /// <summary>
    /// Manejador para el comando de eliminar un rol de un usuario.
    /// </summary>
    public class RemoveRoleFromUser_CommandHandler : IRemoveRoleFromUser_CommandHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRoleFromUser_CommandHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        public async Task<RoleAssignedToUser> Handle (IRemoveRoleFromUser_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del usuario es válido
            if (command.UserID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.UserID), "El identificador del usuario no es válido"));

            // Verificar si el identificador del rol de usuario es válido
            if (command.RoleID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.RoleID), "El identificador del rol de usuario no es válido"));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);
            else {
                var roleAssignedToUser = await _unitOfWork.RoleAssignedToUserRepository.GetRoleAssignedToUserByForeignKeys(command.UserID, command.RoleID);
                if (roleAssignedToUser == null)
                    throw NotFoundError.Create("RolesAssignedToUser");
                else
                    return await _unitOfWork.RoleAssignedToUserRepository.DeleteRoleAssignedToUserByID((int) roleAssignedToUser.ID!);
            }

        }

    }

}