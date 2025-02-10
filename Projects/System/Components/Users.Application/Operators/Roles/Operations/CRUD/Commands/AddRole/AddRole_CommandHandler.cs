using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Commands.AddRole;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Roles.Operations.CRUD.Commands.AddRole {

    /// <summary>
    /// Manejador del comando <see cref="AddRole_Command"/> que agrega un nuevo rol.
    /// </summary>
    public class AddRole_CommandHandler : IAddRole_CommandHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador con el repositorio de roles especificado.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public AddRole_CommandHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja el comando de manera asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene la información del rol a agregar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el rol agregado.</returns>
        /// <exception cref="ApplicationError">Se lanza si el comando o el rol son nulos, o si hay errores de validación.</exception>
        public async Task<Role> Handle (IAddRole_Command command) {
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            if (command.Entity == null)
                throw BadRequestError.Create("El rol no puede ser nulo");

            var validationErrors = new List<ApplicationError>();

            if (string.IsNullOrWhiteSpace(command.Entity.Name))
                validationErrors.Add(ValidationError.Create(nameof(command.Entity.Name), "El nombre del rol no puede ser nulo o vacío"));
            else if (await _unitOfWork.RoleRepository.FirstOrDefault(role => role.Name!.Equals(command.Entity.Name)) != null)
                validationErrors.Add(ValidationError.Create(nameof(command.Entity.Name), $"El nombre del rol '{command.Entity.Name}' ya existe"));

            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            return await _unitOfWork.RoleRepository.AddRole(command.Entity);
        }

    }

}