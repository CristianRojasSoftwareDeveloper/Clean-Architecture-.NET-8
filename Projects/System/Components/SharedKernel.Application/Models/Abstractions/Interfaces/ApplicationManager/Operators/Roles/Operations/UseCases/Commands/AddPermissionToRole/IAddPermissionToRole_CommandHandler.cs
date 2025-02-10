using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.UseCases.Commands.AddPermissionToRole {

    public interface IAddPermissionToRole_CommandHandler : IOperationHandler<IAddPermissionToRole_Command, PermissionAssignedToRole> { }

}