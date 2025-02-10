using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntities {

    /// <summary>
    /// Manejador para la consulta de obtención de todas las entidades genéricas.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que se obtendrá.</typeparam>
    public class GetEntities_QueryHandler<EntityType> : IGetEntities_QueryHandler<EntityType> where EntityType : IGenericEntity {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador de consultas de entidades.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public GetEntities_QueryHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;


        /// <summary>
        /// Maneja la consulta para obtener todas las entidades de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta de obtención de entidades.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con una lista de entidades como resultado.</returns>
        public Task<List<EntityType>> Handle (IGetEntities_Query query) =>
            _unitOfWork.GetGenericRepository<EntityType>().GetEntities(query.EnableTracking);

    }

}