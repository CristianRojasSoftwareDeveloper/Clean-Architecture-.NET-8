// Importa las abstracciones de errores del dominio compartido.
using SharedKernel.Application.Models.Abstractions.Errors;
// Importa la interfaz del comando para activar usuario.
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.ActivateUser;
// Importa la interfaz para la unidad de trabajo del servicio de persistencia de datos.
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
// Importa la entidad «User» del dominio.
using SharedKernel.Domain.Models.Entities.Users;

namespace Users.Application.Operators.Users.Operations.UseCases.Commands.ActivateUser {

    /// <summary>
    /// Define el manejador para el comando para activar un usuario.
    /// </summary>
    public class ActivateUserByID_CommandHandler : IActivateUserByID_CommandHandler {

        /// <summary>
        /// Almacena la unidad de trabajo del servicio de persistencia de datos.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork; // «IUnitOfWork»: Unidad de trabajo para operaciones de persistencia.

        /// <summary>
        /// Inicializa una nueva instancia del manejador para el comando para activar un usuario.
        /// </summary>
        /// <param name="unitOfWork">Inyecta la unidad de trabajo del servicio de persistencia de datos («IUnitOfWork»).</param>
        public ActivateUserByID_CommandHandler (IUnitOfWork unitOfWork) =>
            // Asigna la instancia inyectada de «IUnitOfWork» al campo privado.
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja la activación de un usuario según el comando recibido.
        /// </summary>
        /// <param name="command">Comando que contiene el identificador del usuario a activar.</param>
        /// <returns>Devuelve la entidad «User» actualizada.</returns>
        public async Task<User> Handle (IActivateUserByID_Command command) {

            // Verifica si el comando es nulo para prevenir errores de referencia.
            if (command == null)
                // Lanza un error de solicitud incorrecta («BadRequestError») indicando que el comando no puede ser nulo.
                throw BadRequestError.Create("El comando no puede ser nulo.");

            // Verifica si el identificador del usuario es igual a cero (valor no válido).
            if (command.UserID == 0)
                // Lanza un error de solicitud incorrecta indicando que el identificador del usuario es inválido.
                throw BadRequestError.Create("El identificador del usuario por activar no es válido.");

            // Intenta obtener el usuario existente mediante el repositorio y la unidad de trabajo.
            var existingUser = await _unitOfWork.UserRepository.GetUserByID(command.UserID, true) ??
                // Si no se encuentra el usuario, lanza un error de no encontrado («NotFoundError»).
                throw NotFoundError.Create($"No ha sido encontrado el usuario con identificador «{command.UserID}».");

            // Verifica si el usuario ya se encuentra activado.
            if (existingUser.IsActive.HasValue && existingUser.IsActive.Value)
                // Lanza un error de solicitud incorrecta indicando que el usuario ya está activado.
                throw BadRequestError.Create($"El usuario con identificador «{command.UserID}» ya se encuentra activado.");

            // Actualiza el estado «IsActive» del usuario a «true» para marcarlo como activado.
            existingUser.IsActive = true;

            // Persiste la actualización parcial del usuario, especificando únicamente el campo «IsActive», luego retorna el usuario actualizado.
            return await _unitOfWork.UserRepository.UpdateUser(existingUser.AsPartial(user => user.IsActive));

        }

    }

}