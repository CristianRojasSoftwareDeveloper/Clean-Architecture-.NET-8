using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.AddRoleToUser {

    public interface IAddRoleToUser_CommandHandler : IOperationHandler<IAddRoleToUser_Command, RoleAssignedToUser> { }

}