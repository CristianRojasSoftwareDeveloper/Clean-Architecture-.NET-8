using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Queries.GetUserByUsername {

    public interface IGetUserByUsername_Query : IOperation {

        string Username { get; }

        bool EnableTracking { get; }

    }

}