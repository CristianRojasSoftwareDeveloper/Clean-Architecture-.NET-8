using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs.Operations.UseCases.Queries.GetSystemLogsByUserID {

    public interface IGetSystemLogsByUserID_Query : IOperation {

        int UserID { get; }

        bool EnableTracking { get; }

    }

}