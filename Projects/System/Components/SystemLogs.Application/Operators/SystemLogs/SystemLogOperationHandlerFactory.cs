using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands.AddSystemLog;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands.DeleteSystemLogByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands.UpdateSystemLog;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Queries.GetSystemLogByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Queries.GetSystemLogs;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.UseCases.Queries.GetSystemLogsByUserID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Operators.Generic;
using SharedKernel.Domain.Models.Entities.SystemLogs;
using SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Commands.AddSystemLog;
using SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Commands.UpdateSystemLog;
using SystemLogs.Application.Operators.SystemLogs.Operations.UseCases.Queries.GetSystemLogsByUserID;

namespace SystemLogs.Application.Operators.SystemLogs {

    /// <summary>
    /// Implementación de la interfaz <see cref="ISystemLogOperationHandlerFactory"/>.
    /// Fábrica de manejadores de operaciones para registros del sistema.
    /// </summary>
    public class SystemLogOperationHandlerFactory : GenericOperationHandlerFactory<SystemLog>, ISystemLogOperationHandlerFactory {

        // Si fuera necesario inyectar otros servicios, se pueden agregar al constructor.
        //public SystemLogOperationHandlerFactory () : base() { }

        #region Queries (Consultas)

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IGetSystemLogByID_Query))]
        public IGetEntityByID_QueryHandler<SystemLog> Create_GetSystemLogByID_QueryHandler (IUnitOfWork unitOfWork) => Create_GetEntityByID_Handler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IGetSystemLogs_Query))]
        public IGetEntities_QueryHandler<SystemLog> Create_GetSystemLogs_QueryHandler (IUnitOfWork unitOfWork) => Create_GetEntities_Handler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IGetSystemLogsByUserID_Query))]
        public IGetSystemLogsByUserID_QueryHandler Create_GetSystemLogsByUserID_QueryHandler (IUnitOfWork unitOfWork) => new GetSystemLogsByUserID_QueryHandler(unitOfWork);

        #endregion

        #region Commands (Comandos)

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IAddSystemLog_Command))]
        public IAddSystemLog_CommandHandler Create_AddSystemLog_CommandHandler (IUnitOfWork unitOfWork) => new AddSystemLog_CommandHandler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IUpdateSystemLog_Command))]
        public IUpdateSystemLog_CommandHandler Create_UpdateSystemLog_CommandHandler (IUnitOfWork unitOfWork) => new UpdateSystemLog_CommandHandler(unitOfWork);

        /// <inheritdoc />
        [OperationHandlerCreator(typeof(IDeleteSystemLogByID_Command))]
        public IDeleteEntityByID_CommandHandler<SystemLog> Create_DeleteSystemLogByID_CommandHandler (IUnitOfWork unitOfWork) => Create_DeleteEntityByID_Handler(unitOfWork);

        #endregion

    }

}