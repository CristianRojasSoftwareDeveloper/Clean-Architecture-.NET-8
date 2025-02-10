using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Commands.RegisterUser;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Entities.Users;

namespace Users.Application.Operators.Users.Operations.CRUD.Commands.RegisterUser {

    /// <summary>
    /// Define el comando para registrar un nuevo usuario.
    /// </summary>
    /// <remarks>
    /// Este comando extiende la operación genérica de agregar entidad y encapsula los datos necesarios
    /// para el registro de un usuario. Se utiliza la nueva sintaxis de constructor primario para asignar
    /// el objeto «User».
    /// 
    /// Parámetros del constructor:
    /// <list type="bullet">
    ///   <item>
    ///     <description>[User] user: Entidad de usuario que se registrará en la base de datos.</description>
    ///   </item>
    /// </list>
    /// </remarks>
    [RequiredPermissions(SystemPermissions.RegisterUser, SystemPermissions.AddEntity)]
    [AssociatedOperationHandlerFactory(typeof(IUserOperationHandlerFactory))]
    public class RegisterUser_Command (User user) : IRegisterUser_Command {

        /// <summary>
        /// Obtiene el objeto «User» que se va a registrar.
        /// </summary>
        /// <remarks>
        /// Esta propiedad contiene la entidad de usuario que se registrará en la base de datos.
        /// </remarks>
        public User Entity { get; } = user;

    }

}