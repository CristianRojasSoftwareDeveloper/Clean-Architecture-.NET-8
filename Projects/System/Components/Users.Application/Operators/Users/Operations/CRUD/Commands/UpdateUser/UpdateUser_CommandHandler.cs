using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Commands.UpdateUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Utils.Validators;
using SharedKernel.Domain.Models.Entities.Users;

namespace Users.Application.Operators.Users.Operations.CRUD.Commands.UpdateUser {

    /// <summary>
    /// Manejador para el comando de actualización de un usuario.
    /// </summary>
    public class UpdateUser_CommandHandler : IUpdateUser_CommandHandler {

        private readonly IAuthService _authService;

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUser_CommandHandler (IAuthService authService, IUnitOfWork unitOfWork) {
            _authService = authService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Maneja el comando de actualización de un usuario de forma asíncrona.
        /// </summary>
        /// <param name="command">El comando de actualización de usuario.</param>
        /// <returns>El usuario actualizado.</returns>
        public async Task<User> Handle (IUpdateUser_Command command) {

            // Verifica si el comando es nulo.
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo.");

            // Verifica si la instancia Partial<User> es nula.
            if (command.EntityUpdate == null)
                throw BadRequestError.Create("La actualización del usuario no puede ser nula.");

            var userUpdate = command.EntityUpdate;

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Valida el identificador del usuario.
            if (!userUpdate.ID.HasValue || (int) userUpdate.ID == default)
                validationErrors.Add(ValidationError.Create(nameof(User.ID), "El identificador del usuario no es válido."));

            // Si está presente, valida el nombre de usuario.
            var usernameProperty = nameof(User.Username);
            if (userUpdate.Properties.TryGetValue(usernameProperty, out var usernameValue)) {
                var username = usernameValue as string;
                if (string.IsNullOrWhiteSpace(username))
                    validationErrors.Add(ValidationError.Create(usernameProperty, "El nombre de usuario no puede estar vacío."));
                else if (await _unitOfWork.UserRepository.GetUserByUsername(username) != null)
                    validationErrors.Add(ValidationError.Create(usernameProperty, $"El nombre de usuario «{username}» ya existe."));
            }

            // Si está presente, valida el nombre de pila del usuario.
            var nameProperty = nameof(User.Name);
            if (userUpdate.Properties.TryGetValue(nameProperty, out var nameValue) && string.IsNullOrWhiteSpace(nameValue as string))
                validationErrors.Add(ValidationError.Create(nameProperty, "El nombre de pila del usuario no puede estar vacío."));

            // Si está presente, valida el formato del correo electrónico.
            var emailProperty = nameof(User.Email);
            if (userUpdate.Properties.TryGetValue(emailProperty, out var emailValue)) {
                var email = emailValue as string;
                if (string.IsNullOrWhiteSpace(email))
                    validationErrors.Add(ValidationError.Create(emailProperty, "El email del usuario no puede estar vacío."));
                else if (!EmailValidator.IsValidEmail(email))
                    validationErrors.Add(ValidationError.Create(emailProperty, "El formato del email no es válido."));
            }

            // Si está presente, valida y encripta la contraseña del usuario.
            var passwordProperty = nameof(User.Password);
            if (userUpdate.Properties.TryGetValue(passwordProperty, out var passwordValue)) {
                var password = passwordValue as string;
                if (string.IsNullOrWhiteSpace(password))
                    validationErrors.Add(ValidationError.Create(passwordProperty, "La contraseña del usuario no puede estar vacía."));
                else // Si la contraseña no es nula o vacía se encripta y reasigna.
                    userUpdate.Properties[passwordProperty] = _authService.HashPassword(password);
            }

            // Si hay errores de validación lanza un «AggregateError».
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Ejecuta la actualización del usuario de forma asíncrona.
            return await _unitOfWork.UserRepository.UpdateUser(userUpdate);

        }

    }

}