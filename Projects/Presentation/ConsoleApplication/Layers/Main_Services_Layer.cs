using ConsoleApplication.Utils;
using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Utils.Extensions;
using SharedKernel.Domain.Models.Entities.Users;
using SharedKernel.Domain.Models.Entities.Users.Authorizations;
using System.Net;
using System.Text.RegularExpressions;

namespace ConsoleApplication.Layers {

    internal class Main_Services_Layer {

        #region Flujo de operaciones de prueba de la capa de servicios del sistema (también llamada capa de infraestructura en Arquitectura Limpia)
        /// <summary>
        /// Punto de entrada principal de la aplicación de consola.
        /// </summary>
        /// <param name="args">Argumentos de la línea de comandos.</param>
        internal static async Task ExecuteTestFlow (IAuthService authService, IPersistenceService persistenceService) {

            Printer.PrintLine($"\n{"Iniciando el flujo de operaciones de prueba de la capa de servicios del sistema (también llamada capa de infraestructura en Arquitectura Limpia)".Underline()}");

            #region Paso 1.1: Registrar un nuevo usuario
            Printer.PrintLine($"\n{"1.1. Registrando un nuevo usuario [persistenceService.UserRepository.AddUser(newUser)]".Underline()}");

            const string decryptedPassword = "secret-key";
            var newUser = new User {
                Username = "CristianDeveloper",
                Password = decryptedPassword,
                Name = "Cristian Rojas",
                Email = "cristian.rojas.software.developer@gmail.com"
            };

            // Lista para almacenar los errores de validación
            var validationErrors = new List<ApplicationError>();

            // Verificar si el nombre de usuario es nulo o vacío
            if (string.IsNullOrWhiteSpace(newUser.Username))
                validationErrors.Add(ValidationError.Create(nameof(newUser.Username), "El nombre de usuario no puede ser nulo o vacío"));
            else if (persistenceService.UserRepository.GetUserByUsername(newUser.Username) != null)
                validationErrors.Add(ValidationError.Create(nameof(newUser.Username), $"El nombre de usuario '{newUser.Username}' ya existe"));

            // Verificar si el nombre es nulo o vacío
            if (string.IsNullOrWhiteSpace(newUser.Name))
                validationErrors.Add(ValidationError.Create(nameof(newUser.Name), "El nombre de pila del usuario no puede ser nulo o vacío"));

            // Verificar si el correo es nulo o vacío, o si tiene formato válido
            if (string.IsNullOrWhiteSpace(newUser.Email))
                validationErrors.Add(ValidationError.Create(nameof(newUser.Email), "El email no puede ser nulo o vacío"));
            else if (!Regex.IsMatch(newUser.Email, @"^[a-zA-Z][a-zA-Z0-9_.]*[a-zA-Z]@[a-zA-Z]+\.[a-zA-Z]{2,}$"))
                validationErrors.Add(ValidationError.Create(nameof(newUser.Email), "El formato del email no es válido"));

            // Verificar si la contraseña es nula o vacía
            if (string.IsNullOrWhiteSpace(newUser.Password))
                validationErrors.Add(ValidationError.Create(nameof(newUser.Password), "La contraseña del usuario no puede ser nula o vacía"));
            else
                // Hashear la contraseña del usuario y asignarla al campo EncryptedPassword del usuario
                newUser.Password = authService.HashPassword(newUser.Password);

            // Si hay errores de validación, lanzar un AggregateError
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Agregar el usuario al repositorio y obtener el usuario registrado
            var registeredUser = await persistenceService.UserRepository.AddUser(newUser);
            // Verificar si el usuario registrado es nulo
            if (registeredUser == null)
                // Lanzar un Error de aplicación si ocurrió un Error durante el registro del usuario
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, $"Ha ocurrido un Error mientras se registraba al usuario '{newUser.Username}'");
            else {
                // Imprime el resultado de la operación de registro de usuario.
                Printer.PrintLine($"El usuario '{registeredUser.Username}' ha sido registrado exitosamente");
                registeredUser.PrintUser();
            }
            //#endregion

            //#region Paso 1.2: Agregar roles al usuario registrado
            Printer.PrintLine($"\n{"1.2. Agregando roles al usuario registrado [persistenceService.RoleAssignedToUserRepository.AddRoleAssignedToUser(new RolesAssignedToUser((int) registeredUser.ID!, roleID))]".Underline()}");

            // DefaultRoles por defecto: [ 1=Administrator, 2=Moderator, 3=Entity, 4=Guest ]

            // Agrega el rol [ 1 = Administrator ] al usuario registrado
            var roleID = 1;
            var role = await persistenceService.RoleRepository.GetRoleByID(roleID);
            var roleAssignedToUser = persistenceService.RoleAssignedToUserRepository.AddRoleAssignedToUser(new RoleAssignedToUser {
                UserID = (int) registeredUser.ID!,
                RoleID = (int) role.ID!
            });
            if (roleAssignedToUser == null)
                Printer.PrintLine($"- Ha ocurrido un Error mientras se asignaba el rol '{role.Name}' al usuario '{registeredUser.Username}'");
            else
                Printer.PrintLine($"- El rol '{role.Name}' cuyo ID es [{role.ID}] ha sido asignado al usuario '{registeredUser.Username}' exitosamente");

            // Agrega el rol [ 2 = Moderator ] al usuario registrado
            roleID = 2;
            role = await persistenceService.RoleRepository.GetRoleByID(roleID);
            roleAssignedToUser = persistenceService.RoleAssignedToUserRepository.AddRoleAssignedToUser(new RoleAssignedToUser {
                UserID = (int) registeredUser.ID!,
                RoleID = (int) role.ID!
            });
            if (roleAssignedToUser == null)
                Printer.PrintLine($"- Ha ocurrido un Error mientras se asignaba el rol '{role.Name}' al usuario '{registeredUser.Username}'");
            else
                Printer.PrintLine($"- El rol '{role.Name}' cuyo ID es [{role.ID}] ha sido asignado al usuario '{registeredUser.Username}' exitosamente");

            // Agrega el rol [ 3 = Entity ] al usuario registrado
            roleID = 3;
            role = await persistenceService.RoleRepository.GetRoleByID(roleID);
            roleAssignedToUser = persistenceService.RoleAssignedToUserRepository.AddRoleAssignedToUser(new RoleAssignedToUser {
                UserID = (int) registeredUser.ID!,
                RoleID = (int) role.ID!
            });
            if (roleAssignedToUser == null)
                Printer.PrintLine($"- Ha ocurrido un Error mientras se asignaba el rol '{role.Name}' al usuario '{registeredUser.Username}'");
            else
                Printer.PrintLine($"- El rol '{role.Name}' cuyo ID es [{role.ID}] ha sido asignado al usuario '{registeredUser.Username}' exitosamente");

            registeredUser.PrintUser();
            #endregion

            #region Paso 2: Autenticar el usuario registrado
            Printer.PrintLine($"\n{"2. Autenticando el usuario registrado [authService.VerifyPassword(password, userByUsername.EncryptedPassword!)]".Underline()}");
            Printer.PrintLine($"- Usuario: {registeredUser.Username}");
            Printer.PrintLine($"- Contraseña: {registeredUser.Password}");
            var userByUsername = await persistenceService.UserRepository.GetUserByUsername(registeredUser.Username!);
            if (userByUsername == null)
                throw new UnauthorizedAccessException($"No se ha encontrado ningún usuario cuyo nombre de usuario sea: {registeredUser.Username}");
            else if (!authService.VerifyPassword(decryptedPassword, userByUsername.Password!))
                throw new UnauthorizedAccessException("La contraseña es incorrecta");

            var accessToken = authService.GenerateToken(userByUsername);

            Printer.PrintLine($"\n- Token: {accessToken}");
            Printer.PrintLine($"\n- Token desencriptado:\n{authService.ValidateToken(accessToken)}");
            #endregion

            #region Paso 3: Consultar el usuario registrado según su ID
            Printer.PrintLine($"\n{"3. Consultando el usuario registrado según su ID [persistenceService.UserRepository.GetUserByID((int) userByUsername.ID!)]".Underline()}");
            var foundUser = await persistenceService.UserRepository.GetUserByID((int) userByUsername.ID!);
            if (foundUser == null)
                throw NotFoundError.Create(userByUsername.GetType().Name, (int) userByUsername.ID!);
            else {
                Printer.PrintLine("Usuario encontrado:");
                foundUser.PrintUser();
            }
            #endregion

            #region Paso 4: Actualizar el usuario registrado
            Printer.PrintLine($"\n{"4. Actualizando el usuario registrado [persistenceService.UserRepository.UpdateUser(new Entity { ID = foundUser.ID, Name = $\"{foundUser.Name} Arredondo\" })]".Underline()}");
            var updatedUser = await persistenceService.UserRepository.UpdateUser(new User { ID = foundUser.ID, Name = $"{foundUser.Name} Arredondo" }.AsPartial(user => user.Name));
            if (updatedUser == null)
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, $"Ha ocurrido un Error mientras se actualizaba el usuario cuyo ID es: {foundUser.ID}");
            else {
                Printer.PrintLine("Usuario actualizado exitosamente:");
                updatedUser.PrintUser();
            }
            #endregion

            #region Paso 5: Consultar todos los usuarios después de la actualización del usuario registrado
            Printer.PrintLine($"\n{"5. Consultando todos los usuarios después de la actualización del usuario registrado [persistenceService.UserRepository.GetUsers()]".Underline()}");
            var usersAfterUpdate = await persistenceService.UserRepository.GetUsers();
            if (usersAfterUpdate == null || usersAfterUpdate.Count == 0)
                Printer.PrintLine("No hay usuarios en la base de datos");
            else {
                Printer.PrintLine("Lista de usuarios después de la actualización del usuario registrado:");
                foreach (var user in usersAfterUpdate)
                    user.PrintUser();
            }
            #endregion

            #region Paso 6: Eliminar el usuario registrado según su ID 
            Printer.PrintLine($"\n{"6. Eliminando el usuario registrado según su ID [persistenceService.UserRepository.DeleteUserByID((int) updatedUser.ID!)]".Underline()}");
            try {
                var deletedUser = await persistenceService.UserRepository.DeleteUserByID((int) updatedUser.ID!);
                Printer.PrintLine($"El usuario cuyo ID es [{deletedUser.ID}] ha sido eliminado exitosamente");
            } catch (Exception) {
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, $"Ha ocurrido un Error mientras se eliminaba el usuario cuyo ID es: {updatedUser.ID}");
            }
            #endregion

            #region Paso 7: Consultar todos los usuarios nuevamente después de la eliminación del usuario registrado
            Printer.PrintLine($"\n{"7. Consultando todos los usuarios nuevamente después de la eliminación del usuario registrado [persistenceService.UserRepository.GetUsers()]".Underline()}");
            var remainingUsers = await persistenceService.UserRepository.GetUsers();
            if (remainingUsers == null || remainingUsers.Count == 0)
                Printer.PrintLine("No hay usuarios en la base de datos");
            else {
                Printer.PrintLine("Lista de usuarios después de la eliminación del usuario registrado:");
                foreach (var user in remainingUsers)
                    user.PrintUser();
            }
            #endregion

            Printer.PrintLine($"\n{"Ha finalizado el flujo de operaciones de prueba de la capa de servicios del sistema (también llamada capa de infraestructura en Arquitectura Limpia)".Underline()}");

        }
        #endregion

    }

}