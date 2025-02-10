using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntityByID {

    /// <summary>
    /// Manejador para la consulta de obtención de una entidad genérica por su ID.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que se obtendrá.</typeparam>
    public class GetEntityByID_QueryHandler<EntityType> : IGetEntityByID_QueryHandler<EntityType> where EntityType : IGenericEntity {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador de consultas de entidades por ID.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public GetEntityByID_QueryHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja la consulta para obtener una entidad por su ID de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta de obtención de entidad por ID.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con la entidad obtenida o null si no se encuentra.</returns>
        public Task<EntityType> Handle (IGetEntityByID_Query query) =>
            _unitOfWork.GetGenericRepository<EntityType>().GetEntityByID(query.ID, query.EnableTracking);

    }

}