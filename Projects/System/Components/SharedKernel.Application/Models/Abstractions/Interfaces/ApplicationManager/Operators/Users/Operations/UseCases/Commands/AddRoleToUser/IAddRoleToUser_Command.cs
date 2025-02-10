using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.AddRoleToUser {

    public interface IAddRoleToUser_Command : IOperation {

        int RoleID { get; }

        int UserID { get; }

    }

}