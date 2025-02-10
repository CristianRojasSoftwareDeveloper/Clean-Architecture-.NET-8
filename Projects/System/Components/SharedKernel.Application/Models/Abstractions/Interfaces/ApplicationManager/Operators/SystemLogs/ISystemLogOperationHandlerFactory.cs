using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands.AddSystemLog;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands.UpdateSystemLog;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.UseCases.Queries.GetSystemLogsByUserID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.SystemLogs;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs {

    /// <summary>
    /// Interfaz que define operaciones para la gestión de registros en el sistema.
    /// </summary>
    public interface ISystemLogOperationHandlerFactory : IOperationHandlerFactory {

        #region Queries (Consultas)

        /// <summary>
        /// Crea un manejador para obtener un log del sistema por su ID.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IGetEntityByID_QueryHandler{SystemLog}"/>.</returns>
        IGetEntityByID_QueryHandler<SystemLog> Create_GetSystemLogByID_QueryHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para obtener todos los logs del sistema.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IGetEntities_QueryHandler{SystemLog}"/>.</returns>
        IGetEntities_QueryHandler<SystemLog> Create_GetSystemLogs_QueryHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para obtener los logs del sistema asociados a un usuario.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IGetSystemLogsByUserID_QueryHandler"/>.</returns>
        IGetSystemLogsByUserID_QueryHandler Create_GetSystemLogsByUserID_QueryHandler (IUnitOfWork unitOfWork);

        #endregion

        #region Commands (Comandos)

        /// <summary>
        /// Crea un manejador para agregar un log del sistema.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IAddSystemLog_CommandHandler"/>.</returns>
        IAddSystemLog_CommandHandler Create_AddSystemLog_CommandHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para actualizar un log del sistema existente.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IUpdateSystemLog_CommandHandler"/>.</returns>
        IUpdateSystemLog_CommandHandler Create_UpdateSystemLog_CommandHandler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para eliminar un log del sistema por su ID.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia.</param>
        /// <returns>Instancia de <see cref="IDeleteEntityByID_CommandHandler{SystemLog}"/>.</returns>
        IDeleteEntityByID_CommandHandler<SystemLog> Create_DeleteSystemLogByID_CommandHandler (IUnitOfWork unitOfWork);

        #endregion

    }

}