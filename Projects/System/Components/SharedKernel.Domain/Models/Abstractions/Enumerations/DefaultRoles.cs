using SharedKernel.Domain.Models.Abstractions.Attributes;

namespace SharedKernel.Domain.Models.Abstractions.Enumerations {

    /// <summary>
    /// Enumeración que contiene todos los roles de usuario disponibles en el sistema.
    /// </summary>
    public enum DefaultRoles {

        /// <summary>
        /// Rol de administrador del sistema con acceso total.
        /// </summary>
        [Role("Rol de administrador del sistema con acceso total", permissions: [
            SystemPermissions.AddEntity,
            SystemPermissions.GetEntities,
            SystemPermissions.GetEntityByID,
            SystemPermissions.UpdateEntity,
            SystemPermissions.DeleteEntityByID,
            SystemPermissions.AddRoleToUser,
            SystemPermissions.RemoveRoleFromUser,
            SystemPermissions.AddPermissionToRole,
            SystemPermissions.RemovePermissionFromRole,
            SystemPermissions.AuthenticateUser
        ])]
        SystemAdministrator = 1,

        /// <summary>
        /// Rol de administrador de usuarios con capacidad de gestionar cuentas.
        /// </summary>
        [Role("Rol de administrador de usuarios con capacidad de gestionar cuentas", permissions: [
            SystemPermissions.GetEntities,
            SystemPermissions.GetEntityByID,
            SystemPermissions.RegisterUser,
            SystemPermissions.AddRoleToUser,
            SystemPermissions.RemoveRoleFromUser
        ])]
        UserAdministrator = 2,

        /// <summary>
        /// Rol de usuario estándar del sistema.
        /// </summary>
        [Role("Rol de usuario estándar del sistema", permissions: [
            SystemPermissions.GetUserByID,
            SystemPermissions.UpdateUser,
            SystemPermissions.DeleteUserByID,
            SystemPermissions.GetUserByToken,
            SystemPermissions.GetUserByUsername
        ])]
        User = 3,

        /// <summary>
        /// Rol de invitado con acceso limitado al sistema.
        /// </summary>
        [Role("Rol de usuario invitado (Usuario no autenticado)", permissions: [
            SystemPermissions.RegisterUser,
            SystemPermissions.AuthenticateUser
        ])]
        Guest = 4

    }

}