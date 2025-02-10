using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Queries.AuthenticateUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;

namespace Users.Application.Operators.Users.Operations.UseCases.Queries.AuthenticateUser {

    /// <summary>
    /// Manejador para la consulta de autenticación de usuario.
    /// </summary>
    public class AuthenticateUser_QueryHandler : IAuthenticateUser_QueryHandler {

        private readonly IAuthService _authService;

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del manejador de la consulta de autenticación de usuario.
        /// </summary>
        /// <param name="authService">Servicio de autenticación utilizado para verificar las credenciales del usuario.</param>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public AuthenticateUser_QueryHandler (IAuthService authService, IUnitOfWork unitOfWork) {
            _authService = authService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Maneja la consulta de autenticación de usuario de forma asíncrona.
        /// </summary>
        /// <param name="authenticateUser_Query">La consulta de autenticación de usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona y el token de acceso generado si la autenticación es exitosa.</returns>
        public async Task<string> Handle (IAuthenticateUser_Query authenticateUser_Query) {
            var user = await _unitOfWork.UserRepository.GetUserByUsername(authenticateUser_Query.Username) ??
                throw new UnauthorizedAccessException($"No se ha encontrado el usuario con el nombre de usuario «{authenticateUser_Query.Username}»");

            if (!_authService.VerifyPassword(authenticateUser_Query.Password, user.Password!))
                throw new UnauthorizedAccessException("La contraseña es incorrecta");

            return _authService.GenerateToken(user);
        }

    }

}