using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Commands.AddPermissionToRole;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Roles.Operations.UseCases.Commands.AddPermissionToRole {

    /// <summary>
    /// Manejador para el comando de agregar un permiso a un rol.
    /// </summary>
    public class AddPermissionToRole_CommandHandler : IAddPermissionToRole_CommandHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador del comando para agregar un permiso a un rol.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public AddPermissionToRole_CommandHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja el comando para agregar un permiso a un rol de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando para agregar el permiso.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el permiso de rol agregado.</returns>
        public async Task<PermissionAssignedToRole> Handle (IAddPermissionToRole_Command command) {

            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el identificador del rol es válido y existe en el repositorio de roles
            if (command.RoleID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.RoleID), "El identificador del rol de usuario no es válido"));
            else if ((await _unitOfWork.RoleRepository.GetRoleByID(command.RoleID)) == null)
                validationErrors.Add(ValidationError.Create(nameof(command.RoleID), $"No se ha encontrado ningún rol de usuario con el identificador {command.RoleID}."));

            // Verificar si el identificador del permiso es válido y existe en el repositorio de permisos
            if (command.PermissionID == default)
                validationErrors.Add(ValidationError.Create(nameof(command.PermissionID), "El identificador del permiso de usuario no es válido"));
            else if ((await _unitOfWork.PermissionRepository.GetPermissionByID(command.PermissionID)) == null)
                validationErrors.Add(ValidationError.Create(nameof(command.PermissionID), $"No se ha encontrado ningún permiso de usuario con el identificador {command.PermissionID}."));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Crear el nuevo permiso de rol y guardarlo en el repositorio
            var newPermissionAssignedToRole = new PermissionAssignedToRole {
                RoleID = command.RoleID,
                PermissionID = command.PermissionID
            };

            return await _unitOfWork.PermissionAssignedToRoleRepository.AddPermissionAssignedToRole(newPermissionAssignedToRole);

        }

    }

}