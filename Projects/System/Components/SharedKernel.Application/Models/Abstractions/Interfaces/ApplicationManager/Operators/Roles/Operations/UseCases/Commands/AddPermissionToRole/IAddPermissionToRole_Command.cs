using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Commands.AddPermissionToRole {

    public interface IAddPermissionToRole_Command : IOperation {

        int PermissionID { get; }

        int RoleID { get; }

    }

}