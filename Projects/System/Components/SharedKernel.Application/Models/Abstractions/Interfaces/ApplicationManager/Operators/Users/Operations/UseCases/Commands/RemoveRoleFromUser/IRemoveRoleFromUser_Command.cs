using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.RemoveRoleFromUser {
    public interface IRemoveRoleFromUser_Command : IOperation {
        int RoleID { get; }
        int UserID { get; }
    }
}