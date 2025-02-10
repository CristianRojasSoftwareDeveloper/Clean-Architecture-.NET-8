using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands.DeleteSystemLogByID;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Commands.DeleteSystemLogByID {

    /// <summary>
    /// Comando para eliminar un log del sistema por su ID.
    /// </summary>
    [RequiredPermissions([SystemPermissions.DeleteSystemLogByID, SystemPermissions.DeleteEntityByID])]
    [AssociatedOperationHandlerFactory(typeof(ISystemLogOperationHandlerFactory))]
    public class DeleteSystemLogByID_Command : IDeleteSystemLogByID_Command {

        /// <summary>
        /// Obtiene el ID del registro de sistema que se va a eliminar.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Inicializa una nueva instancia del comando con el ID del registro de sistema especificado.
        /// </summary>
        /// <param name="systemLogID">El ID del registro de sistema que se va a eliminar.</param>
        public DeleteSystemLogByID_Command (int systemLogID) {
            ID = systemLogID;
        }

    }

}