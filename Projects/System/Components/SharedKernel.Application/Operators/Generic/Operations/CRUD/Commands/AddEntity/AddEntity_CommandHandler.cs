using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.AddEntity;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.AddEntity {

    /// <summary>
    /// Manejador para el comando de creación de una nueva entidad genérica.
    /// </summary>
    public class AddEntity_CommandHandler<EntityType> : IAddEntity_CommandHandler<EntityType> where EntityType : IGenericEntity {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos AddEntity.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public AddEntity_CommandHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;


        /// <summary>
        /// Maneja el comando de creación de una nueva entidad de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando de creación de entidad.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con la entidad creada.</returns>
        public Task<EntityType> Handle (IAddEntity_Command<EntityType> command) =>
            _unitOfWork.GetGenericRepository<EntityType>().AddEntity(command.Entity);

    }

}