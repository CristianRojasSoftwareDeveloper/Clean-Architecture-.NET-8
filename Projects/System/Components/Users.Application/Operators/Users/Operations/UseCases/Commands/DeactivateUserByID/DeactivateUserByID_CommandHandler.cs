// Importa las abstracciones de errores del dominio compartido.

using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.DeactivateUserByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users;
// Importa la interfaz del comando para desactivar usuario.
// Importa la interfaz para la unidad de trabajo del servicio de persistencia de datos.
// Importa la entidad «User» del dominio.

namespace Users.Application.Operators.Users.Operations.UseCases.Commands.DeactivateUserByID {

    /// <summary>
    /// Define el manejador para el comando para desactivar un usuario.
    /// </summary>
    public class DeactivateUserByID_CommandHandler : IDeactivateUserByID_CommandHandler {

        /// <summary>
        /// Almacena la unidad de trabajo del servicio de persistencia de datos.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork; // «IUnitOfWork»: Unidad de trabajo para operaciones de persistencia.

        /// <summary>
        /// Inicializa una nueva instancia del manejador para el comando para desactivar un usuario.
        /// </summary>
        /// <param name="unitOfWork">Inyecta la unidad de trabajo del servicio de persistencia de datos («IUnitOfWork»).</param>
        public DeactivateUserByID_CommandHandler (IUnitOfWork unitOfWork) =>
            // Asigna la instancia inyectada de «IUnitOfWork» al campo privado.
            _unitOfWork = unitOfWork;

        /// <summary>
        /// Maneja la desactivación de un usuario según el comando recibido.
        /// </summary>
        /// <param name="command">Comando que contiene el identificador del usuario a desactivar.</param>
        /// <returns>Devuelve la entidad «User» actualizada.</returns>
        public async Task<User> Handle (IDeactivateUserByID_Command command) {

            // Verifica si el comando es nulo para prevenir errores de referencia.
            if (command == null)
                // Lanza un error de solicitud incorrecta («BadRequestError») indicando que el comando no puede ser nulo.
                throw BadRequestError.Create("El comando no puede ser nulo.");

            // Verifica si el identificador del usuario es igual a cero (valor no válido).
            if (command.UserID == 0)
                // Lanza un error de solicitud incorrecta indicando que el identificador del usuario es inválido.
                throw BadRequestError.Create("El identificador del usuario por desactivar no es válido.");

            // Intenta obtener el usuario existente mediante el repositorio y la unidad de trabajo.
            var existingUser = await _unitOfWork.UserRepository.GetUserByID(command.UserID, true) ??
                // Si no se encuentra el usuario, lanza un error de no encontrado («NotFoundError»).
                throw NotFoundError.Create($"No ha sido encontrado el usuario con identificador «{command.UserID}».");

            // Verifica si el usuario ya se encuentra desactivado.
            if (existingUser.IsActive.HasValue && !existingUser.IsActive.Value)
                // Lanza un error de solicitud incorrecta indicando que el usuario ya está desactivado.
                throw BadRequestError.Create($"El usuario con identificador «{command.UserID}» ya se encuentra desactivado.");

            // Actualiza el estado «IsActive» del usuario a «false» para marcarlo como desactivado.
            existingUser.IsActive = false;

            // Persiste la actualización parcial del usuario, especificando únicamente el campo «IsActive», luego retorna el usuario actualizado.
            return await _unitOfWork.UserRepository.UpdateUser(existingUser.AsPartial(user => user.IsActive));

        }

    }

}