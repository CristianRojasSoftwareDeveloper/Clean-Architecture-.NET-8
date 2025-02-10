using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands.AddSystemLog;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.SystemLogs;

namespace SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Commands.AddSystemLog {

    /// <summary>
    /// Manejador para el comando de agregar un nuevo log de sistema.
    /// </summary>
    public class AddSystemLog_CommandHandler : IAddSystemLog_CommandHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos de agregar un nuevo log de sistema.
        /// </summary>
        /// <param name="systemLogRepository">Repositorio de logs de sistema.</param>
        public AddSystemLog_CommandHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja de forma asíncrona el comando para agregar un nuevo log de sistema.
        /// </summary>
        /// <param name="command">Comando para agregar un nuevo log de sistema.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el log de sistema agregado.</returns>
        /// <exception cref="ApplicationError">Se lanza si el comando o el log de sistema son nulos.</exception>
        /// <exception cref="AggregateError">Se lanza si hay errores de validación.</exception>
        public Task<SystemLog> Handle (IAddSystemLog_Command command) {
            // Verificar si el comando es nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verificar si el log de sistema es nulo
            if (command.Entity == null)
                throw BadRequestError.Create("El log de sistema no puede ser nulo");

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si las propiedades de Entity contienen valores no vacíos y válidos.
            if (command.Entity.LogLevel == null)
                validationErrors.Add(ValidationError.Create(nameof(command.Entity.LogLevel), "El nivel de severidad del registro no puede ser nulo"));
            if (string.IsNullOrWhiteSpace(command.Entity.Source))
                validationErrors.Add(ValidationError.Create(nameof(command.Entity.Source), "El origen del registro no puede ser nulo o vacío"));
            if (string.IsNullOrWhiteSpace(command.Entity.Message))
                validationErrors.Add(ValidationError.Create(nameof(command.Entity.Message), "El mensaje del registro no puede ser nulo o vacío"));
            if (command.Entity.UserID != null && command.Entity.UserID.Value <= 0)
                validationErrors.Add(ValidationError.Create(nameof(command.Entity.UserID), $"No es posible asociar el registro del sistema con un identificador de usuario negativo [{command.Entity.UserID}]."));

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Agregar el log de sistema de forma asíncrona y devolverlo
            return _unitOfWork.SystemLogRepository.AddSystemLog(command.Entity);
        }

    }

}