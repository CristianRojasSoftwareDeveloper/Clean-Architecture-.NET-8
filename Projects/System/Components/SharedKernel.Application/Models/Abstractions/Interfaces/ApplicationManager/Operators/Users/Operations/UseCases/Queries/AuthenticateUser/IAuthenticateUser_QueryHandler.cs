using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Queries.AuthenticateUser {

    public interface IAuthenticateUser_QueryHandler : IOperationHandler<IAuthenticateUser_Query, string> { }

}