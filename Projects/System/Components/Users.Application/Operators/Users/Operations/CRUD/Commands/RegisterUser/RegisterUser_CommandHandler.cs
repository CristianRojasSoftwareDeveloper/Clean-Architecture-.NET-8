using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.CRUD.Commands.RegisterUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Commands.AddRoleToUser;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Utils.Validators;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Entities.Users;
using System.Net;
using Users.Application.Operators.Users.Operations.UseCases.Commands.AddRoleToUser;

namespace Users.Application.Operators.Users.Operations.CRUD.Commands.RegisterUser {

    /// <summary>
    /// Define el manejador para el comando de registro de usuario.
    /// </summary>
    public class RegisterUser_CommandHandler : IRegisterUser_CommandHandler {

        /// <summary>
        /// Almacena el servicio de autenticación para el manejo de contraseñas («IAuthService»).
        /// </summary>
        private readonly IAuthService _authService;

        /// <summary>
        /// Almacena la unidad de trabajo del servicio de persistencia de datos («IUnitOfWork» : «IPersistenceService»).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Almacena el manejador del comando para asignar un rol a un usuario («IAddRoleToUser_CommandHandler»).
        /// </summary>
        private readonly IAddRoleToUser_CommandHandler _addRoleToUserCommandHandler;

        /// <summary>
        /// Inicializa una nueva instancia del manejador para el comando de registro de usuario.
        /// </summary>
        /// <param name="authService">Inyecta el servicio de autenticación para el manejo de contraseñas.</param>
        /// <param name="unitOfWork">Inyecta la unidad de trabajo del servicio de persistencia de datos («IUnitOfWork» : «IPersistenceService»).</param>
        /// <param name="addRoleToUserCommandHandler">Inyecta el manejador del comando para asignar un rol a un usuario.</param>
        public RegisterUser_CommandHandler (IAuthService authService, IUnitOfWork unitOfWork, IAddRoleToUser_CommandHandler addRoleToUserCommandHandler) {
            _authService = authService;
            _unitOfWork = unitOfWork;
            _addRoleToUserCommandHandler = addRoleToUserCommandHandler;
        }

        /// <summary>
        /// Ejecuta de forma asíncrona la operación de registro de usuario.
        /// </summary>
        /// <param name="command">Recibe el comando de registro de usuario que contiene el objeto «User» y la colección de identificadores de roles asociados.</param>
        /// <returns>Devuelve el usuario registrado.</returns>
        /// <exception cref="BadRequestError">Lanza error si el comando o el usuario son nulos.</exception>
        /// <exception cref="AggregateError">Lanza error si se detectan errores de validación en los datos del usuario.</exception>
        /// <exception cref="ApplicationError">Lanza error si ocurre un fallo durante la operación de persistencia.</exception>
        public async Task<User> Handle (IRegisterUser_Command command) {

            // Verifica que el comando no sea nulo
            if (command == null)
                throw BadRequestError.Create("El comando no puede ser nulo");

            // Verifica que el usuario contenido en el comando no sea nulo
            if (command.Entity == null)
                throw BadRequestError.Create("El usuario no puede ser nulo");

            // Obtiene el objeto «User» desde el comando
            var user = command.Entity;

            // Crea la lista para acumular los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verifica que «user.Username» no sea nulo o vacío
            if (string.IsNullOrWhiteSpace(user.Username))
                validationErrors.Add(ValidationError.Create(nameof(user.Username), "El nombre de usuario no puede ser nulo o vacío"));
            else if (await _unitOfWork.UserRepository.GetUserByUsername(user.Username) != null)
                validationErrors.Add(ValidationError.Create(nameof(user.Username), $"El nombre de usuario «{user.Username}» ya existe"));

            // Verifica que «user.Name» no sea nulo o vacío
            if (string.IsNullOrWhiteSpace(user.Name))
                validationErrors.Add(ValidationError.Create(nameof(user.Name), "El nombre de pila del usuario no puede ser nulo o vacío"));

            // Verifica que «user.Email» no sea nulo o vacío y que tenga un formato válido
            if (string.IsNullOrWhiteSpace(user.Email))
                validationErrors.Add(ValidationError.Create(nameof(user.Email), "El email no puede ser nulo o vacío"));
            else if (!EmailValidator.IsValidEmail(user.Email))
                validationErrors.Add(ValidationError.Create(nameof(user.Email), "El formato del email no es válido"));

            // Verifica que «user.Password» no sea nulo o vacío y encripta la contraseña si es válida
            if (string.IsNullOrWhiteSpace(user.Password))
                validationErrors.Add(ValidationError.Create(nameof(user.Password), "La contraseña del usuario no puede estar vacía."));
            else
                user.Password = _authService.HashPassword(user.Password);

            // Lanza un «AggregateError» si se detectaron errores de validación
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            try {

                //// Inicia una nueva transacción en la unidad de trabajo para garantizar la atomicidad de las operaciones.
                //await _unitOfWork.BeginTransactionAsync();

                // Intenta agregar el usuario al repositorio y obtiene la instancia del usuario registrado.
                var registeredUser = await _unitOfWork.UserRepository.AddUser(user)
                    ?? throw ApplicationError.Create(HttpStatusCode.InternalServerError, $"Ha ocurrido un error mientras se registraba al usuario «{user.Username}».");

                // Verifica que el usuario registrado tenga un ID válido (no nulo y distinto de cero).
                if (registeredUser.ID is null or 0)
                    throw ApplicationError.Create(HttpStatusCode.InternalServerError, $"El usuario «{user.Username}» fue registrado, pero el ID asignado no es válido.");

                // Agrega el rol predeterminado «User» al usuario recién registrado.
                await _addRoleToUserCommandHandler.Handle(new AddRoleToUser_Command((int) registeredUser.ID, (int) DefaultRoles.User));

                //// Confirma la transacción si todas las operaciones se completaron exitosamente, asegurando la persistencia de los cambios.
                //await _unitOfWork.CommitTransactionAsync();

                // Devuelve la instancia del usuario registrado con sus modificaciones aplicadas.
                return registeredUser;

            } catch {

                //// En caso de que ocurra una excepción en cualquier punto del bloque, se revierte la transacción
                //// para evitar la persistencia de datos inconsistentes.
                //await _unitOfWork.RollbackTransactionAsync();

                // Relanza la excepción para que la capa superior pueda manejarla adecuadamente.
                throw;

            }

        }

    }

}