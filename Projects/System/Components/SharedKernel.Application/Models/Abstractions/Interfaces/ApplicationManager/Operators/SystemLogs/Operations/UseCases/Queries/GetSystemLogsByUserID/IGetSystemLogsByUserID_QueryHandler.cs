using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Domain.Models.Entities.SystemLogs;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.UseCases.Queries.GetSystemLogsByUserID {

    public interface IGetSystemLogsByUserID_QueryHandler : IOperationHandler<IGetSystemLogsByUserID_Query, List<SystemLog>> { }

}