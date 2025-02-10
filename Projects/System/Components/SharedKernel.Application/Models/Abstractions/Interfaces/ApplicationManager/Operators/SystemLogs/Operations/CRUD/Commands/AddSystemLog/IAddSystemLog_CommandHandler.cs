using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Domain.Models.Entities.SystemLogs;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.CRUD.Commands.AddSystemLog {

    public interface IAddSystemLog_CommandHandler : IOperationHandler<IAddSystemLog_Command, SystemLog> { }

}