using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Commands.UpdatePermission;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Permissions.Operations.CRUD.Commands.UpdatePermission {

    /// <summary>
    /// Manejador para el comando de actualización de permiso.
    /// </summary>
    public class UpdatePermission_CommandHandler : IUpdatePermission_CommandHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePermission_CommandHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        ///// <summary>
        ///// Maneja el comando de actualización de un permiso de forma asíncrona.
        ///// </summary>
        ///// <param name="command">El comando que contiene la actualización del permiso.</param>
        ///// <returns>Una tarea que representa la operación asíncrona y contiene el permiso actualizado.</returns>
        public async Task<Permission> Handle (IUpdatePermission_Command command) {

            // Verifica si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo.");

            // Verifica si la instancia Partial<Permission> es nula
            if (command.EntityUpdate == null)
                throw BadRequestError.Create("La actualización del permiso de usuario no puede ser nula.");

            var permissionUpdate = command.EntityUpdate;

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Valida el identificador del permiso.
            if (!permissionUpdate.ID.HasValue || (int) permissionUpdate.ID == default)
                validationErrors.Add(ValidationError.Create(nameof(Permission.ID), "El identificador del permiso de usuario no es válido."));

            // Si está presente, valida el nombre del permiso de usuario.
            var nameProperty = nameof(Permission.Name);
            if (permissionUpdate.Properties.TryGetValue(nameProperty, out var nameValue)) {
                var name = nameValue as string;
                if (string.IsNullOrWhiteSpace(name))
                    validationErrors.Add(ValidationError.Create(nameProperty, "El nombre del permiso de usuario no puede estar vacío."));
                else if (await _unitOfWork.PermissionRepository.FirstOrDefault(permission => permission.Name!.Equals(name)) != null)
                    validationErrors.Add(ValidationError.Create(nameProperty, $"El nombre del permiso de usuario «{name}» ya existe."));
            }

            // Si hay errores de validación lanza un «AggregateError».
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Ejecuta la actualización del permiso de usuario de forma asíncrona.
            return await _unitOfWork.PermissionRepository.UpdatePermission(permissionUpdate);

        }

    }

}