using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.UseCases.Queries.GetPermissionsByRoleID {

    public interface IGetPermissionsByRoleID_Query : IOperation {

        int RoleID { get; }

        bool EnableTracking { get; }

    }

}