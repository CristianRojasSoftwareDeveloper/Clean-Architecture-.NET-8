using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace SharedKernel.Application.Models.Abstractions.Attributes {

    [AttributeUsage(AttributeTargets.Class)]
    public class RequiredPermissionsAttribute (params SystemPermissions[] permissions) : Attribute {

        public IEnumerable<SystemPermissions> Permissions { get; } = permissions.Length > 0 ? permissions : [SystemPermissions.None];

    }

}