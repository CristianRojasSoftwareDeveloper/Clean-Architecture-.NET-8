using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.UpdateEntity;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.UpdateEntity {

    /// <summary>
    /// Manejador para el comando de actualización de una entidad genérica.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que se actualizará.</typeparam>
    public class UpdateEntity_CommandHandler<EntityType> : IUpdateEntity_CommandHandler<EntityType> where EntityType : IGenericEntity {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos de actualización de entidades.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public UpdateEntity_CommandHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;


        /// <summary>
        /// Maneja el comando de actualización de una entidad genérica de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando de actualización de entidad.</param>
        /// <returns>Una tarea que representa la operación asincrónica, con la entidad actualizada o null si no se encontró.</returns>
        public Task<EntityType> Handle (IUpdateEntity_Command<EntityType> command) =>
            _unitOfWork.GetGenericRepository<EntityType>().UpdateEntity(command.EntityUpdate);

    }

}