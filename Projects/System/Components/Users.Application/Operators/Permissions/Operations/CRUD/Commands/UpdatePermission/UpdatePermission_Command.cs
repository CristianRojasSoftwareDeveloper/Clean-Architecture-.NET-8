using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions.Operations.CRUD.Commands.UpdatePermission;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.UpdateEntity;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Permissions.Operations.CRUD.Commands.UpdatePermission {

    /// <summary>
    /// Comando para actualizar un permiso de sistema existente.
    /// </summary>
    /// <remarks>
    /// Inicializa una nueva instancia del comando para actualizar un permiso de sistema.
    /// </remarks>
    /// <param name="permissionUpdate">Actualización del permiso de sistema.</param>
    [RequiredPermissions([SystemPermissions.UpdatePermission, SystemPermissions.UpdateEntity])]
    [AssociatedOperationHandlerFactory(typeof(IPermissionOperationHandlerFactory))]
    public class UpdatePermission_Command (Partial<Permission> permissionUpdate) : UpdateEntity_Command<Permission>(permissionUpdate), IUpdatePermission_Command { }

}