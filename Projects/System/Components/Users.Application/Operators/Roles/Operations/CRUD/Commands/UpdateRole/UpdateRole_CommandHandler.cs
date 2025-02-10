using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Commands.UpdateRole;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Roles.Operations.CRUD.Commands.UpdateRole {

    /// <summary>
    /// Manejador para el comando de actualización de un rol.
    /// </summary>
    public class UpdateRole_CommandHandler : IUpdateRole_CommandHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos de actualización de roles.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public UpdateRole_CommandHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja el comando de actualización de un rol de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando de actualización de rol.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el rol actualizado.</returns>
        public async Task<Role> Handle (IUpdateRole_Command command) {

            // Verifica si el comando es nulo.
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo.");

            // Verifica si la instancia Partial<Role> es nula.
            if (command.EntityUpdate == null)
                throw BadRequestError.Create("La actualización del rol de usuario no puede ser nula.");

            var roleUpdate = command.EntityUpdate;

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Valida el identificador del rol de usuario.
            if (!roleUpdate.ID.HasValue || (int) roleUpdate.ID == default)
                validationErrors.Add(ValidationError.Create(nameof(Role.ID), "El identificador del rol de usuario no es válido."));

            // Si está presente, valida el nombre del rol de usuario.
            var nameProperty = nameof(Role.Name);
            if (roleUpdate.Properties.TryGetValue(nameProperty, out var nameValue)) {
                var name = nameValue as string;
                if (string.IsNullOrWhiteSpace(name))
                    validationErrors.Add(ValidationError.Create(nameProperty, "El nombre del rol de usuario no puede estar vacío."));
                else if (await _unitOfWork.RoleRepository.FirstOrDefault(role => role.Name!.Equals(name)) != null)
                    validationErrors.Add(ValidationError.Create(nameProperty, $"El nombre del rol de usuario «{name}» ya existe."));
            }

            // Si hay errores de validación lanza un «AggregateError».
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Ejecuta la actualización del rol de usuario de forma asíncrona.
            return await _unitOfWork.RoleRepository.UpdateRole(roleUpdate);

        }

    }

}