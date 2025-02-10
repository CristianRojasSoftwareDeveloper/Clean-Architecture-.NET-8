using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Commands.AddPermission;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Permissions.Operations.CRUD.Commands.AddPermission {

    /// <summary>
    /// Manejador para el comando de agregar un nuevo permiso.
    /// </summary>
    public class AddPermission_CommandHandler : IAddPermission_CommandHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos de agregar un nuevo permiso.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public AddPermission_CommandHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja de forma asíncrona el comando para agregar un nuevo permiso.
        /// </summary>
        /// <param name="command">Comando para agregar un nuevo permiso.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el permiso agregado.</returns>
        /// <exception cref="ApplicationError">Se lanza si el comando o el permiso son nulos.</exception>
        /// <exception cref="AggregateError">Se lanza si hay errores de validación.</exception>
        public async Task<Permission> Handle (IAddPermission_Command command) {
            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el permiso es nulo
            if (command.Entity == null)
                throw BadRequestError.Create("El permiso no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el nombre del permiso es nulo o vacío
            if (string.IsNullOrWhiteSpace(command.Entity.Name))
                validationErrors.Add(ValidationError.Create(nameof(command.Entity.Name), "El nombre del permiso no puede ser nulo o vacío"));
            else if (await _unitOfWork.PermissionRepository.FirstOrDefault(permission => permission.Name!.Equals(command.Entity.Name)) != null)
                validationErrors.Add(ValidationError.Create(nameof(command.Entity.Name), $"El nombre del permiso '{command.Entity.Name}' ya existe"));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Agregar el permiso de forma asíncrona y devolverlo
            return await _unitOfWork.PermissionRepository.AddPermission(command.Entity);
        }

    }

}