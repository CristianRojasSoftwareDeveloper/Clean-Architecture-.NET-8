using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles.Operations.CRUD.Commands.UpdateRole;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.UpdateEntity;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;

namespace Users.Application.Operators.Roles.Operations.CRUD.Commands.UpdateRole {

    /// <summary>
    /// Comando para actualizar un rol de usuario existente.
    /// </summary>
    /// <remarks>
    /// Inicializa una nueva instancia del comando para actualizar un rol de usuario.
    /// </remarks>
    /// <param name="roleUpdate">Actualización del rol de usuario.</param>
    [RequiredPermissions([SystemPermissions.UpdateRole, SystemPermissions.UpdateEntity])]
    [AssociatedOperationHandlerFactory(typeof(IRoleOperationHandlerFactory))]
    public class UpdateRole_Command (Partial<Role> roleUpdate) : UpdateEntity_Command<Role>(roleUpdate), IUpdateRole_Command { }

}