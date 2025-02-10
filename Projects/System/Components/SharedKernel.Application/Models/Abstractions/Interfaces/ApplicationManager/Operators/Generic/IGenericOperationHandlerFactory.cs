using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.AddEntity;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.UpdateEntity;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic {

    /// <summary>
    /// Interfaz que define las operaciones básicas CRUD compartidas por todos los operadores genéricos.
    /// Proporciona métodos factory para crear manejadores especializados en operaciones específicas.
    /// </summary>
    /// <typeparam name="EntityType">Tipo de entidad sobre la que opera el operador.</typeparam>
    public interface IGenericOperationHandlerFactory<EntityType> : IOperationHandlerFactory where EntityType : IGenericEntity {

        #region Queries (Consultas)

        /// <summary>
        /// Crea un manejador para obtener una entidad específica por su identificador.
        /// Este manejador implementa la lógica de consulta para recuperar una única entidad.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        /// <returns>Instancia de <see cref="IGetEntityByID_QueryHandler{EntityType}"/> configurada para el tipo de entidad especificado.</returns>
        IGetEntityByID_QueryHandler<EntityType> Create_GetEntityByID_Handler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para obtener una lista de todas las entidades del tipo especificado.
        /// Este manejador implementa la lógica de consulta para recuperar múltiples entidades.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        /// <returns>Instancia de <see cref="IGetEntities_QueryHandler{EntityType}"/> configurada para el tipo de entidad especificado.</returns>
        IGetEntities_QueryHandler<EntityType> Create_GetEntities_Handler (IUnitOfWork unitOfWork);

        #endregion

        #region Commands (Comandos)

        /// <summary>
        /// Crea un manejador para agregar una nueva entidad al repositorio.
        /// Este manejador implementa la lógica de creación y persistencia de nuevas entidades.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        /// <returns>Instancia de <see cref="IAddEntity_CommandHandler{EntityType}"/> configurada para el tipo de entidad especificado.</returns>
        IAddEntity_CommandHandler<EntityType> Create_AddEntity_Handler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para actualizar una entidad existente en el repositorio.
        /// Este manejador implementa la lógica de actualización de entidades existentes.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        /// <returns>Instancia de <see cref="IUpdateEntity_CommandHandler{EntityType}"/> configurada para el tipo de entidad especificado.</returns>
        IUpdateEntity_CommandHandler<EntityType> Create_UpdateEntity_Handler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para eliminar una entidad del repositorio mediante su identificador.
        /// Este manejador implementa la lógica de eliminación de entidades.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        /// <returns>Instancia de <see cref="IDeleteEntityByID_CommandHandler{EntityType}"/> configurada para el tipo de entidad especificado.</returns>
        IDeleteEntityByID_CommandHandler<EntityType> Create_DeleteEntityByID_Handler (IUnitOfWork unitOfWork);

        #endregion

    }

}