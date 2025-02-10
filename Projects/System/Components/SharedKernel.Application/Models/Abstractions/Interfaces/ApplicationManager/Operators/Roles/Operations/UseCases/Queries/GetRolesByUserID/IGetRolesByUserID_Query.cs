using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Queries.GetRolesByUserID {

    public interface IGetRolesByUserID_Query : IOperation {

        int UserID { get; }

        bool EnableTracking { get; }

    }

}