using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.RemoveRoleFromUser {

    public interface IRemoveRoleFromUser_CommandHandler : IOperationHandler<IRemoveRoleFromUser_Command, RoleAssignedToUser> { }

}