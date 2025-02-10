using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Commands.RemovePermissionFromRole {

    public interface IRemovePermissionFromRole_CommandHandler : IOperationHandler<IRemovePermissionFromRole_Command, PermissionAssignedToRole> { }

}