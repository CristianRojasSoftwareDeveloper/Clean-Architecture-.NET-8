using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.UseCases.Queries.GetPermissionsByRoleID {

    public interface IGetPermissionsByRoleID_QueryHandler : IOperationHandler<IGetPermissionsByRoleID_Query, List<Permission>> { }

}