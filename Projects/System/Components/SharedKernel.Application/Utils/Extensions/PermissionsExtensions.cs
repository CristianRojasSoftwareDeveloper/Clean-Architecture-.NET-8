using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace SharedKernel.Application.Utils.Extensions {

    public static class PermissionsExtensions {

        public static bool HasPermission (this IEnumerable<SystemPermissions> permissions, SystemPermissions permission) =>
            permissions != null && permissions.Contains(permission);

    }

}