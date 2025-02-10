using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Queries.GetUserByToken;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users;

namespace Users.Application.Operators.Users.Operations.UseCases.Queries.GetUserByToken {

    /// <summary>
    /// Manejador para la consulta de obtención de usuario mediante un token de autenticación.
    /// </summary>
    public class GetUserByToken_QueryHandler : IGetUserByToken_QueryHandler {

        private readonly IAuthService _authService;

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GetUserByToken_QueryHandler"/>.
        /// </summary>
        /// <param name="authService">Servicio de autenticación.</param>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public GetUserByToken_QueryHandler (IAuthService authService, IUnitOfWork unitOfWork) {
            _authService = authService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Maneja la consulta de obtención de usuario mediante un token de autenticación de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta de obtención de usuario mediante un token de autenticación.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el usuario obtenido.</returns>
        public Task<User?> Handle (IGetUserByToken_Query query) {
            // Verificar que la consulta no sea nula
            ArgumentNullException.ThrowIfNull(query);

            // Verificar que el token no sea nulo o vacío
            if (string.IsNullOrWhiteSpace(query.Token))
                throw BadRequestError.Create("El token no puede ser nulo o vacío");

            // Validar el token y obtener los atributos
            var tokenClaims = _authService.ValidateToken(query.Token);

            // Verifica si el atributo de nombre de usuario está presente en el token.
            if (string.IsNullOrWhiteSpace(tokenClaims.Username))
                // Si el atributo de nombre de usuario no está presente en el token, se lanza un error.
                throw ValidationError.Create(nameof(tokenClaims.Username), "No se ha encontrado el atributo del nombre de usuario dentro del token");

            // Obtiene y retorna el usuario por su nombre de usuario.
            return _unitOfWork.UserRepository.GetUserByUsername(tokenClaims.Username);
        }

    }

}