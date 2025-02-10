using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands.UpdateSystemLog;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.UpdateEntity;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Entities.SystemLogs;

namespace SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Commands.UpdateSystemLog {

    /// <summary>
    /// Comando para actualizar un registro de sistema existente.
    /// </summary>
    /// <remarks>
    /// Inicializa una nueva instancia del comando para actualizar un registro de sistema.
    /// </remarks>
    /// <param name="systemLogUpdate">Actualización del registro de sistema.</param>
    [RequiredPermissions([SystemPermissions.UpdateSystemLog, SystemPermissions.UpdateEntity])]
    [AssociatedOperationHandlerFactory(typeof(ISystemLogOperationHandlerFactory))]
    public class UpdateSystemLog_Command (Partial<SystemLog> systemLogUpdate) : UpdateEntity_Command<SystemLog>(systemLogUpdate), IUpdateSystemLog_Command { }

}