using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands.AddSystemLog;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Entities.SystemLogs;

namespace SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Commands.AddSystemLog {

    /// <summary>
    /// Comando para agregar un nuevo log del sistema.
    /// </summary>
    [RequiredPermissions([SystemPermissions.AddSystemLog, SystemPermissions.AddEntity])]
    [AssociatedOperationHandlerFactory(typeof(ISystemLogOperationHandlerFactory))]
    public class AddSystemLog_Command : IAddSystemLog_Command {

        /// <summary>
        /// Obtiene el registro de sistema que se va a agregar.
        /// </summary>
        public SystemLog Entity { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando con el registro de sistema especificado.
        /// </summary>
        /// <param name="systemLog">El registro de sistema a agregar.</param>
        public AddSystemLog_Command (SystemLog systemLog) {
            Entity = systemLog;
        }

    }

}