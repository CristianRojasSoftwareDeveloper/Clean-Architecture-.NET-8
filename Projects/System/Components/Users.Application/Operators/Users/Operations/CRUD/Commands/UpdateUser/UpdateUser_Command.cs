using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Commands.UpdateUser;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.UpdateEntity;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Entities.Users;

namespace Users.Application.Operators.Users.Operations.CRUD.Commands.UpdateUser {

    /// <summary>
    /// Comando para actualizar un usuario existente.
    /// </summary>
    /// <remarks>
    /// Inicializa una nueva instancia del comando para actualizar un usuario.
    /// </remarks>
    /// <param name="userUpdate">Actualización del usuario.</param>
    [RequiredPermissions([SystemPermissions.UpdateUser, SystemPermissions.UpdateEntity])]
    [AssociatedOperationHandlerFactory(typeof(IUserOperationHandlerFactory))]
    public class UpdateUser_Command (Partial<User> userUpdate) : UpdateEntity_Command<User>(userUpdate), IUpdateUser_Command { }

}