using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Queries.GetUserByUsername;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users;

namespace Users.Application.Operators.Users.Operations.UseCases.Queries.GetUserByUsername {

    /// <summary>
    /// Manejador para la consulta de obtención de un usuario por su nombre de usuario.
    /// </summary>
    public class GetUserByUsername_QueryHandler : IGetUserByUsername_QueryHandler {

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GetUserByUsername_QueryHandler"/>.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public GetUserByUsername_QueryHandler (IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja la consulta de obtención de un usuario por su nombre de usuario de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta de obtención de usuario por su nombre de usuario.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el usuario obtenido.</returns>
        public Task<User?> Handle (IGetUserByUsername_Query query) =>
            _unitOfWork.UserRepository.GetUserByUsername(query.Username, query.EnableTracking);

    }

}