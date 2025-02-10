using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID {

    /// <summary>
    /// Manejador para el comando de eliminación de una entidad por su ID.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad a eliminar.</typeparam>
    public class DeleteEntityByID_CommandHandler<EntityType> : IDeleteEntityByID_CommandHandler<EntityType> where EntityType : IGenericEntity {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador de comandos de eliminación de entidades por ID.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public DeleteEntityByID_CommandHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;


        /// <summary>
        /// Maneja el comando de eliminación de una entidad por su ID de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando que contiene el ID de la entidad a eliminar.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con un booleano que indica si la eliminación fue exitosa (true) o no (false).</returns>
        public Task<EntityType> Handle (IDeleteEntityByID_Command command) =>
            _unitOfWork.GetGenericRepository<EntityType>().DeleteEntityByID(command.ID);

    }

}