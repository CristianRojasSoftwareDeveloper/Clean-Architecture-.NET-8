using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands.UpdateSystemLog;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.SystemLogs;

namespace SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Commands.UpdateSystemLog {

    /// <summary>
    /// Manejador para el comando de actualización de log de sistema.
    /// </summary>
    public class UpdateSystemLog_CommandHandler : IUpdateSystemLog_CommandHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSystemLog_CommandHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja el comando de actualización de un log de sistema de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el log de sistema a actualizar.</param>
        /// <returns>Una tarea que representa la operación asíncrona y contiene el log de sistema actualizado.</returns>
        public Task<SystemLog> Handle (IUpdateSystemLog_Command command) {

            // Verifica si el comando es nulo.
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo.");

            // Verifica si la instancia Partial<SystemLog> es nula.
            if (command.EntityUpdate == null)
                throw BadRequestError.Create("La actualización del registro del sistema no puede ser nula.");

            var systemLogUpdate = command.EntityUpdate;

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Valida el identificador del registro del sistema.
            if (!systemLogUpdate.ID.HasValue || (int) systemLogUpdate.ID == default)
                validationErrors.Add(ValidationError.Create(nameof(SystemLog.ID), "El identificador del registro del sistema no es válido."));

            // Verifica si el nombre del log de sistema no está vacío
            var sourceProperty = nameof(SystemLog.Source);
            if (systemLogUpdate.Properties.TryGetValue(sourceProperty, out var systemLogSourceValue) && string.IsNullOrWhiteSpace(systemLogSourceValue as string))
                validationErrors.Add(ValidationError.Create(sourceProperty, "El origen del registro del sistema no puede estar vacío."));

            var messageProperty = nameof(SystemLog.Message);
            if (systemLogUpdate.Properties.TryGetValue(messageProperty, out var systemLogMessageValue) && string.IsNullOrWhiteSpace(systemLogMessageValue as string))
                validationErrors.Add(ValidationError.Create(messageProperty, "El mensaje del registro del sistema no puede estar vacío."));

            var userIDProperty = nameof(SystemLog.UserID);
            if (systemLogUpdate.Properties.TryGetValue(userIDProperty, out var systemLogUserIDValue) && (systemLogUserIDValue != null && (int) systemLogUserIDValue <= 0))
                validationErrors.Add(ValidationError.Create(userIDProperty, $"No es posible asociar el registro del sistema con un identificador de usuario negativo «{(int) systemLogUserIDValue}»."));

            // Si hay errores de validación lanza un «AggregateError».
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Inicia y retorna la actualización del registro del sistema de forma asíncrona.
            return _unitOfWork.SystemLogRepository.UpdateSystemLog(systemLogUpdate);

        }

    }

}