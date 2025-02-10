using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.UseCases.Queries.GetSystemLogsByUserID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.SystemLogs;

namespace SystemLogs.Application.Operators.SystemLogs.Operations.UseCases.Queries.GetSystemLogsByUserID {

    /// <summary>
    /// Manejador para la consulta de obtención de logs del sistema asociados a un usuario por su ID.
    /// </summary>
    public class GetSystemLogsByUserID_QueryHandler : IGetSystemLogsByUserID_QueryHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador de consulta.
        /// </summary>
        /// <param name="systemLogRepository">El repositorio de logs del sistema.</param>
        public GetSystemLogsByUserID_QueryHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja la consulta de obtención de logs del sistema asociados a un usuario por su ID de manera asíncrona.
        /// </summary>
        /// <param name="query">La consulta que contiene el ID del usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona y contiene una lista de logs del sistema asociados al usuario especificado.</returns>
        public Task<List<SystemLog>> Handle (IGetSystemLogsByUserID_Query query) =>
            _unitOfWork.SystemLogRepository.GetSystemLogsByUserID(query.UserID, query.EnableTracking);

    }

}