using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Queries.GetUserByToken;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace Users.Application.Operators.Users.Operations.UseCases.Queries.GetUserByToken {

    /// <summary>
    /// Consulta para obtener un usuario mediante un token de autenticación.
    /// </summary>
    [RequiredPermissions([SystemPermissions.GetUserByToken, SystemPermissions.GetEntities])]
    [AssociatedOperationHandlerFactory(typeof(IUserOperationHandlerFactory))]
    public class GetUserByToken_Query : IGetUserByToken_Query {

        /// <summary>
        /// Obtiene o establece el token de autenticación.
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GetUserByToken_Query"/>.
        /// </summary>
        /// <param name="token">El token de autenticación.</param>
        public GetUserByToken_Query (string token) => Token = token;

    }

}