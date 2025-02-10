using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Commands.RemovePermissionFromRole {
    public interface IRemovePermissionFromRole_Command : IOperation {
        int PermissionID { get; }
        int RoleID { get; }
    }
}