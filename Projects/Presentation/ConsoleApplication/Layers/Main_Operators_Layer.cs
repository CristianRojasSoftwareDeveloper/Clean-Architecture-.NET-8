using ConsoleApplication.Utils;
using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager;
using SharedKernel.Application.Utils.Extensions;
using SharedKernel.Domain.Models.Entities.Users;
using System.Net;
using Users.Application.Operators.Users.Operations.CRUD.Commands.DeleteUserByID;
using Users.Application.Operators.Users.Operations.CRUD.Commands.RegisterUser;
using Users.Application.Operators.Users.Operations.CRUD.Commands.UpdateUser;
using Users.Application.Operators.Users.Operations.CRUD.Queries.GetUserByID;
using Users.Application.Operators.Users.Operations.CRUD.Queries.GetUsers;
using Users.Application.Operators.Users.Operations.UseCases.Queries.AuthenticateUser;

namespace ConsoleApplication.Layers {

    internal class Main_Operators_Layer {

        #region Flujo de operaciones de prueba de la capa de operadores del sistema (también llamada capa de aplicación/casos de uso en Arquitectura Limpia)
        /// <summary>
        /// Punto de entrada principal de la aplicación de consola.
        /// </summary>
        /// <param name="args">Argumentos de la línea de comandos.</param>
        internal static async Task ExecuteTestFlow (IApplicationManager applicationManager) {

            User? newUser, registeredUser, foundUser, updatedUser, deletedUser;
            List<User> usersAfterUpdate, remainingUsers;

            Printer.PrintLine($"\n{"Iniciando el flujo de operaciones de prueba de la capa de operadores del sistema (también llamada capa de aplicación/casos de uso en Arquitectura Limpia)".Underline()}");

            #region Paso 1: Registrar un nuevo usuario
            Printer.PrintLine($"\n{"1. Registrando un nuevo usuario [applicationManager.UserOperationHandlerFactory.RegisterUser(new RegisterUser_Command(newUser, roleAssignedToUser))]".Underline()}");

            newUser = new User {
                Username = "CristianDeveloper",
                Password = "secret-key",
                Name = "Cristian Rojas",
                Email = "cristian.rojas.software.developer@gmail.com"
            };

            using (var unitOfWork = applicationManager.UnitOfWork)
                registeredUser = await applicationManager.UserOperationHandlerFactory.Create_RegisterUser_CommandHandler(unitOfWork).Handle(new RegisterUser_Command(newUser));
            if (registeredUser == null)
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, "La respuesta a la operación de registro de usuario en la base de datos no es válida.");
            Printer.PrintLine($"Se ha registrado el usuario «{registeredUser.Username}» exitosamente.");
            registeredUser.PrintUser();
            #endregion

            #region Paso 2: Autenticar el usuario registrado
            Printer.PrintLine($"\n{"2. Autenticando usuario [applicationManager.UserOperationHandlerFactory.AuthenticateUser(new AuthenticateUser_Query(newUser.Username, newUser.Password))]".Underline()}");

            var username = "CristianDeveloper";
            var password = "secret-key";
            Printer.PrintLine($"\n- Usuario: {username}\n- Contraseña: {password}");

            string accessToken;
            using (var unitOfWork = applicationManager.UnitOfWork)
                accessToken = await applicationManager.UserOperationHandlerFactory.Create_AuthenticateUser_QueryHandler(unitOfWork).Handle(new AuthenticateUser_Query(newUser.Username, newUser.Password));

            Printer.PrintLine($"\n- Token: {accessToken}");
            Printer.PrintLine($"\n- Token desencriptado:\n{applicationManager.AuthService.ValidateToken(accessToken)}");
            #endregion

            #region Paso 3: Consultar el usuario registrado según su ID
            Printer.PrintLine($"\n{"3. Consultando el usuario registrado por su ID [applicationManager.UserOperationHandlerFactory.GetUserByID(new GetUserByID_Query((int) registeredUser.ID!))]".Underline()}");
            using (var unitOfWork = applicationManager.UnitOfWork)
                foundUser = await applicationManager.UserOperationHandlerFactory.Create_GetUserByID_QueryHandler(unitOfWork).Handle(new GetUserByID_Query((int) registeredUser.ID!, true));
            if (foundUser != null) {
                Printer.PrintLine("Usuario encontrado:");
                foundUser.PrintUser();
            } else
                Printer.PrintLine($"No se encontró ningún usuario con ID: {(int) registeredUser.ID}");
            #endregion

            #region Paso 4: Actualizar el usuario registrado
            if (foundUser != null) {
                Printer.PrintLine($"\n{"4. Actualizando el usuario registrado [applicationManager.UserOperationHandlerFactory.UpdateUser(new UpdateUser_Command(new Entity { ID = foundUser.ID, Name = \"Cristian Rojas Arredondo\" }))]".Underline()}");
                using (var unitOfWork = applicationManager.UnitOfWork)
                    updatedUser = await applicationManager.UserOperationHandlerFactory.Create_UpdateUser_CommandHandler(unitOfWork).Handle(new UpdateUser_Command(new User { ID = foundUser.ID, Name = "Cristian Rojas Arredondo" }.AsPartial(user => user.Name)));
                if (updatedUser != null) {
                    Printer.PrintLine("Usuario actualizado exitosamente:");
                    updatedUser.PrintUser();
                } else
                    Printer.PrintLine($"No se pudo actualizar el usuario con ID: {foundUser.ID}");
            }
            #endregion

            #region Paso 5: Consultar todos los usuarios después de la actualización del usuario registrado
            Printer.PrintLine($"\n{"5. Consultando todos los usuarios después de la actualización [applicationManager.UserOperationHandlerFactory.GetUsers()]".Underline()}");
            using (var unitOfWork = applicationManager.UnitOfWork)
                usersAfterUpdate = await applicationManager.UserOperationHandlerFactory.Create_GetUsers_QueryHandler(unitOfWork).Handle(new GetUsers_Query());
            if (usersAfterUpdate?.Count > 0) {
                Printer.PrintLine("Lista de usuarios:");
                foreach (var user in usersAfterUpdate)
                    user.PrintUser();
            } else
                Printer.PrintLine("No hay usuarios en la base de datos.");
            #endregion

            #region Paso 6: Eliminar el usuario registrado según su ID
            Printer.PrintLine($"\n{"6. Eliminando el usuario registrado según su ID [applicationManager.UserOperationHandlerFactory.DeleteUserByID(new DeleteUserByID_Command((int) registeredUser.ID))]".Underline()}");
            try {
                using (var unitOfWork = applicationManager.UnitOfWork)
                    deletedUser = await applicationManager.UserOperationHandlerFactory.Create_DeleteUserByID_CommandHandler(unitOfWork).Handle(new DeleteUserByID_Command((int) registeredUser.ID));
                Printer.PrintLine($"El usuario cuyo ID es [{deletedUser.ID}] ha sido eliminado exitosamente");
            } catch (Exception) {
                throw ApplicationError.Create(HttpStatusCode.InternalServerError, $"Ha ocurrido un error mientras se eliminaba el usuario cuyo ID es: {registeredUser.ID}");
            }
            #endregion

            #region Paso 7: Consultar todos los usuarios nuevamente después de la eliminación del usuario registrado
            Printer.PrintLine($"\n{"7. Consultando todos los usuarios después de la eliminación [applicationManager.UserOperationHandlerFactory.GetUsers()]".Underline()}");
            using (var unitOfWork = applicationManager.UnitOfWork)
                remainingUsers = await applicationManager.UserOperationHandlerFactory.Create_GetUsers_QueryHandler(unitOfWork).Handle(new GetUsers_Query());
            if (remainingUsers?.Count > 0) {
                Printer.PrintLine("Lista de usuarios después de la eliminación:");
                foreach (var user in remainingUsers)
                    user.PrintUser();
            } else
                Printer.PrintLine("No hay usuarios en la base de datos.");
            #endregion

            Printer.PrintLine($"\n{"Ha finalizado el flujo de operaciones de prueba de la capa de operadores del sistema (también llamada capa de aplicación/casos de uso en Arquitectura Limpia)".Underline()}");

        }
        #endregion

    }

}