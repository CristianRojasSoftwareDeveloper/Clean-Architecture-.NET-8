
#region GPL v3 License Header
/*
 * Clean Architecture - .NET 8
 * Copyright (C) 2025 Cristian Rojas Arredondo
 * 
 * Author / Contact:
 *   Cristian Rojas Arredondo «cristian.rojas.software.engineer@gmail.com»
 * 
 * A full copy of the GNU GPL v3 is provided in the root of this project in the "LICENSE" file.
 * Additionally, you can view the license at <https://www.gnu.org/licenses/gpl-3.0.html>.
 */

#region «English version» GPL v3 License Information 
/*
 * This file is part of Clean Architecture - .NET 8.
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <https://www.gnu.org/licenses/gpl-3.0.html>.
 * 
 * Note: In the event of any discrepancy between translations, this version (English) shall prevail.
 */
#endregion

#region «Spanish version» GPL v3 License Information
/*
 * Este archivo es parte de Clean Architecture - .NET 8.
 * 
 * Este programa es software libre: puede redistribuirlo y/o modificarlo
 * bajo los términos de la Licencia Pública General de GNU publicada por
 * la Free Software Foundation, ya sea la versión 3 de la Licencia o
 * (a su elección) cualquier versión posterior.
 * 
 * Este programa se distribuye con la esperanza de que sea útil,
 * pero SIN NINGUNA GARANTÍA, incluso sin la garantía implícita de
 * COMERCIABILIDAD o IDONEIDAD PARA UN PROPÓSITO PARTICULAR.
 * Consulte la Licencia Pública General de GNU para más detalles.
 * 
 * Usted debería haber recibido una copia de la Licencia Pública General de GNU
 * junto con este programa. De no ser así, véase <https://www.gnu.org/licenses/gpl-3.0.html>.
 * 
 * Nota: En caso de cualquier discrepancia entre las traducciones, la versión en inglés prevalecerá.
 */
#endregion

#endregion

using SharedKernel.Application.Models.Abstractions.Attributes;
using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Permissions;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Roles;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.SystemLogs;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Application.Utils.Extensions;
using SharedKernel.Domain.Models.Abstractions.Enumerations;
using SharedKernel.Domain.Models.Entities.SystemLogs;
using SharedKernel.Domain.Models.Entities.Users;
using SharedKernel.Infrastructure.Configurations;
using SharedKernel.Infrastructure.Services.Auth;
using SharedKernel.Infrastructure.Services.Persistence;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using SystemLogs.Application.Operators.SystemLogs;
using SystemLogs.Application.Operators.SystemLogs.Operations.CRUD.Commands.AddSystemLog;
using SystemLogs.Infrastructure.Services.Persistence.Entity_Framework.Repositories;
using Users.Application.Operators.Permissions;
using Users.Application.Operators.Roles;
using Users.Application.Operators.Users;
using Users.Application.Operators.Users.Operations.CRUD.Commands.RegisterUser;
using Users.Application.Operators.Users.Operations.UseCases.Queries.AuthenticateUser;
using Users.Infrastructure.Services.Persistence.Entity_Framework.Repositories;
using Users.Infrastructure.Services.Persistence.Entity_Framework.Repositories.Authorizations;

namespace API {

    /// <summary>
    /// Coordina la ejecución de operaciones y gestiona los servicios principales del sistema.
    /// </summary>
    public class ApplicationManager : IApplicationManager {

        #region Campos Privados

        /// <summary>
        /// Almacena la configuración de la aplicación.
        /// </summary>
        private readonly ApplicationConfiguration _applicationConfiguration;

        /// <summary>
        /// Asocia tipos de OperationHandlerFactory (implementaciones de «IOperationHandlerFactory») a sus propiedades correspondientes.
        /// Se utiliza para resolver dinámicamente el OperationHandlerFactory adecuado para cada operación.
        /// </summary>
        private static readonly Dictionary<Type, PropertyInfo> _operationHandlerFactories = typeof(ApplicationManager).GetProperties()
            .Where(property => typeof(IOperationHandlerFactory).IsAssignableFrom(property.PropertyType))
            .ToDictionary(property => property.PropertyType);

        #endregion

        #region Propiedades Públicas

        /// <summary>
        /// Obtiene el servicio de autenticación y autorización de usuarios.
        /// </summary>
        public IAuthService AuthService { get; }

        /// <summary>
        /// Obtiene el servicio de persistencia de datos, instanciado a partir de la colección de repositorios configurada.
        /// </summary>
        public IUnitOfWork UnitOfWork => new UnitOfWork_EntityFramework(DbContext, InitializeRepositoryCollection(DbContext));

        /// <summary>
        /// Obtiene el operador encargado de gestionar operaciones relacionadas con usuarios.
        /// </summary>
        public IUserOperationHandlerFactory UserOperationHandlerFactory { get; }

        /// <summary>
        /// Obtiene el operador encargado de gestionar operaciones relacionadas con roles.
        /// </summary>
        public IRoleOperationHandlerFactory RoleOperationHandlerFactory { get; }

        /// <summary>
        /// Obtiene el operador encargado de gestionar operaciones relacionadas con permisos.
        /// </summary>
        public IPermissionOperationHandlerFactory PermissionOperationHandlerFactory { get; }

        /// <summary>
        /// Obtiene el operador encargado de gestionar operaciones relacionadas con logs del sistema.
        /// </summary>
        public ISystemLogOperationHandlerFactory SystemLogOperationHandlerFactory { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Inicializa una nueva instancia de la clase «ApplicationManager».
        /// </summary>
        /// <param name="configuration">Establece la configuración de la aplicación.</param>
        public ApplicationManager (ApplicationConfiguration configuration) {
            // Asigna la configuración recibida.
            _applicationConfiguration = configuration;
            // Inicializa el servicio de autenticación utilizando los parámetros definidos en «JWT_Settings».
            AuthService = new AuthService(_applicationConfiguration.JWT_Settings);
            // Inicializa los operadores con las dependencias requeridas.
            UserOperationHandlerFactory = new UserOperationHandlerFactory(AuthService);
            RoleOperationHandlerFactory = new RoleOperationHandlerFactory();
            PermissionOperationHandlerFactory = new PermissionOperationHandlerFactory();
            SystemLogOperationHandlerFactory = new SystemLogOperationHandlerFactory();
            // Asegura que la base de datos se encuentre creada y disponible.
            DbContext.Database.EnsureCreated();
            // Asegura que la base de datos se encuentre creada y disponible, usando la clase auxiliar que evita reintentos de inicialización.
            DatabaseInitializer.EnsureCreated(_applicationConfiguration);
        }

        #endregion

        #region Métodos Privados

        #region Validación

        /// <summary>
        /// Valida el token de acceso y obtiene los reclamos asociados al usuario.
        /// </summary>
        /// <typeparam name="ResultType">
        /// Define el tipo del resultado esperado, usado para construir una respuesta en caso de error.
        /// </typeparam>
        /// <param name="authService">
        /// Recibe el servicio de autenticación de usuarios.
        /// </param>
        /// <param name="accessToken">
        /// Recibe el token de autenticación, que puede ser nulo, vacío o contener solo espacios.
        /// </param>
        /// <param name="tokenClaims">
        /// Retorna los reclamos obtenidos del token; si el token es inválido, se asignan credenciales de invitado.
        /// </param>
        /// <remarks>
        /// Valida el token mediante el servicio de autenticación. Lanza una excepción a través de <see cref="UnauthorizedError.Create"/>
        /// si ocurre algún error durante la validación.
        /// </remarks>
        private static void ValidateAccessToken<ResultType> (IAuthService authService, string? accessToken, out TokenClaims tokenClaims) {
            // Comprueba si el token es nulo, vacío o contiene solo espacios.
            if (string.IsNullOrWhiteSpace(accessToken)) {
                // Asigna credenciales de invitado.
                tokenClaims = TokenClaims.CreateGuestTokenClaims();
            } else {
                try {
                    // Valida el token usando el servicio de autenticación.
                    tokenClaims = authService.ValidateToken(accessToken);
                } catch (Exception ex) {
                    // Registra el error formateado en la salida de error.
                    Console.Error.WriteLine(ex.GetFormattedDetails());
                    // Asegura que tokenClaims quede inicializado con un valor predeterminado.
                    tokenClaims = default!;
                    // Lanza una excepción indicando que el token es inválido o ha expirado.
                    throw UnauthorizedError.Create("El token de acceso proporcionado no es válido o ha expirado.");
                }
            }
        }

        /// <summary>
        /// Valida que el usuario posea los permisos requeridos para ejecutar la operación.
        /// </summary>
        /// <typeparam name="OperationType">
        /// Especifica el tipo de la operación, que debe implementar <see cref="IOperation"/>.
        /// </typeparam>
        /// <param name="operation">
        /// Recibe la instancia de la operación a ejecutar.
        /// </param>
        /// <param name="userPermissions">
        /// Recibe el conjunto de permisos asociados al usuario.
        /// </param>
        /// <remarks>
        /// Extrae el atributo «RequiredPermissionsAttribute» de la operación y verifica si el usuario posee alguno de los permisos requeridos.
        /// Lanza una excepción mediante <see cref="UnauthorizedError.Create"/> si no se cumplen los permisos.
        /// </remarks>
        private static void ValidateOperationPermissions<OperationType> (OperationType operation, IEnumerable<SystemPermissions> userPermissions) where OperationType : IOperation {
            // Obtiene el tipo real de la operación.
            var operationType = operation.GetType();
            // Extrae el atributo que especifica los permisos requeridos.
            var requiredPermissions = operationType.GetCustomAttribute<RequiredPermissionsAttribute>()?.Permissions;
            // Permite la operación si no hay permisos requeridos o si el usuario tiene alguno de ellos.
            if (requiredPermissions == null || !requiredPermissions.Any() || requiredPermissions.Any(userPermissions.HasPermission))
                return;
            // Lanza una excepción si el usuario no cumple con los permisos requeridos.
            throw UnauthorizedError.Create(
                $"El usuario no tiene los permisos necesarios para ejecutar la operación {operationType.Name}. " +
                $"Permisos requeridos: {string.Join(", ", requiredPermissions)}"
            );
        }

        #endregion

        #region Configuración y Acceso a Recursos

        /// <summary>
        /// Obtiene el contexto de base de datos según la configuración de la aplicación.
        /// </summary>
        /// <remarks>
        /// Crea una instancia de <see cref="InMemory_DbContext"/> si se utiliza una base de datos en memoria;
        /// de lo contrario, crea una instancia de <see cref="PostgreSQL_DbContext"/> usando la cadena de conexión configurada.
        /// </remarks>
        private ApplicationDbContext DbContext => _applicationConfiguration.InMemoryDbContext switch {
            true => new InMemory_DbContext(),  // Se utiliza el contexto en memoria.
            false => new PostgreSQL_DbContext(_applicationConfiguration.ConnectionStrings.PostgreSQL)  // Se utiliza PostgreSQL.
        };

        /// <summary>
        /// Inicializa la colección de repositorios necesaria para la persistencia.
        /// </summary>
        /// <param name="applicationDbContext">Instancia del contexto de base de datos.</param>
        /// <returns>
        /// Devuelve una instancia de <see cref="RepositoryCollection"/> configurada con los repositorios para usuarios, roles, permisos, asignaciones y logs.
        /// </returns>
        /// <remarks>
        /// Se crean repositorios específicos para cada entidad necesaria en la aplicación.
        /// </remarks>
        private static RepositoryCollection InitializeRepositoryCollection (ApplicationDbContext applicationDbContext) => new(
            // Crea el repositorio para la entidad «User».
            new User_EntityFrameworkRepository(applicationDbContext),
            // Crea el repositorio para la entidad «Role».
            new Role_EntityFrameworkRepository(applicationDbContext),
            // Crea el repositorio para la entidad «Permission».
            new Permission_EntityFrameworkRepository(applicationDbContext),
            // Crea el repositorio para asignar roles a usuarios.
            new RoleAssignedToUser_EntityFrameworkRepository(applicationDbContext),
            // Crea el repositorio para asignar permisos a roles.
            new PermissionAssignedToRole_EntityFrameworkRepository(applicationDbContext),
            // Crea el repositorio para la entidad «SystemLog».
            new SystemLog_EntityFrameworkRepository(applicationDbContext)
        );

        #endregion

        #region Resolución y Procesamiento

        /// <summary>
        /// Obtiene la instancia del operador responsable de procesar la operación.
        /// </summary>
        /// <param name="operation">
        /// Recibe la instancia de la operación que se desea procesar.
        /// </param>
        /// <returns>
        /// Devuelve una instancia de <see cref="IOperationHandlerFactory"/> que se utilizará para manejar la operación.
        /// </returns>
        /// <remarks>
        /// Utiliza reflexión para extraer el atributo «AssociatedOperationHandlerFactoryAttribute» y resuelve el operador correspondiente del diccionario.
        /// Lanza una excepción si no se encuentra el operador adecuado.
        /// </remarks>
        private IOperationHandlerFactory GetAssociatedOperationHandlerFactory (IOperation operation) {
            // Obtiene el tipo de la operación.
            var operationType = operation.GetType();
            // Intenta obtener el atributo que indica el operador asociado.
            var associatedOperatorAttribute = operationType.GetCustomAttribute<AssociatedOperationHandlerFactoryAttribute>()
                ?? throw new InvalidOperationException($"La operación «{operationType.Name}» no tiene el atributo «AssociatedOperationHandlerFactory» definido.");
            // Extrae el tipo del operador indicado en el atributo.
            var operatorType = associatedOperatorAttribute.OperationHandlerFactoryType;
            // Busca la propiedad asociada en el diccionario; verifica que devuelva una instancia válida.
            if (!_operationHandlerFactories.TryGetValue(operatorType, out var operatorProperty) || operatorProperty.GetValue(this) is not IOperationHandlerFactory operatorInstance)
                // Lanza excepción si no se encuentra el operador.
                throw new InvalidOperationException($"No se encontró un operador del tipo «{operatorType.Name}».");
            // Retorna la instancia del operador.
            return operatorInstance;
        }

        /// <summary>
        /// Agrega detalles al log del sistema, incorporando información del token, de la operación ejecutada y de la respuesta obtenida.
        /// </summary>
        /// <typeparam name="OperationType">
        /// Especifica el tipo de la operación ejecutada.
        /// </typeparam>
        /// <typeparam name="ResultType">
        /// Especifica el tipo del resultado esperado de la operación.
        /// </typeparam>
        /// <param name="systemLog">
        /// Recibe la instancia de <see cref="SystemLog"/> que se actualizará con la información.
        /// </param>
        /// <param name="tokenClaims">
        /// Recibe los reclamos del usuario obtenidos del token.
        /// </param>
        /// <param name="operation">
        /// Recibe la operación que se ejecutó.
        /// </param>
        /// <param name="response">
        /// Recibe la respuesta resultante de la ejecución de la operación.
        /// </param>
        /// <remarks>
        /// Determina el «UserID» a registrar en función del tipo de operación, utilizando el ID del token o extrayéndolo del cuerpo de la respuesta.
        /// Actualiza el mensaje del log incluyendo el nombre de la operación y sus argumentos en formato JSON.
        /// </remarks>
        private void DetailSystemLog<OperationType, ResultType> (SystemLog systemLog, TokenClaims tokenClaims, OperationType operation, Response<ResultType> response) {

            // Asigna el «UserID» al log según el tipo de operación ejecutada.
            systemLog.UserID = operation switch {
                // Caso para operaciones de registro de usuario.
                RegisterUser_Command =>
                    tokenClaims?.UserID switch {
                        // Utiliza el ID del token si es válido.
                        > 0 => tokenClaims.UserID,
                        // Si el ID del token no es válido, extrae el ID del usuario registrado del cuerpo de la respuesta.
                        _ when response != null => response.Body switch {
                            User registeredUser => registeredUser.ID,
                            _ => throw new InvalidOperationException("El cuerpo de la respuesta no es del tipo esperado para «RegisterUser_Command».")
                        }
                    },
                // Caso para operaciones de autenticación de usuario.
                AuthenticateUser_Query =>
                    tokenClaims?.UserID switch {
                        // Utiliza el ID del token si es válido.
                        > 0 => tokenClaims.UserID,
                        // Si el ID del token no es válido, extrae el ID validando el token generado en el cuerpo de la respuesta.
                        _ when response != null => response.Body switch {
                            string generatedAccessToken => AuthService.ValidateToken(generatedAccessToken).UserID,
                            _ => throw new InvalidOperationException("El cuerpo de la respuesta no es del tipo esperado para «AuthenticateUser_Query».")
                        }
                    },
                // Por defecto, utiliza el ID obtenido del token.
                _ => tokenClaims.UserID
            };

            // Actualiza el mensaje del log agregando el nombre de la operación y sus argumentos en formato JSON.
            systemLog.Message = $"Operation » {typeof(OperationType).Name}\nArguments: {operation!.AsJSON()}\n{systemLog.Message}";

        }

        #endregion

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Ejecuta centralizadamente una operación, gestionando autenticación, permisos, ejecución, manejo de errores y registro de logs.
        /// </summary>
        /// <typeparam name="OperationType">
        /// Especifica el tipo de la operación a ejecutar. Debe implementar <see cref="IOperation"/>.
        /// </typeparam>
        /// <typeparam name="ResultType">
        /// Especifica el tipo del resultado esperado de la operación.
        /// </typeparam>
        /// <param name="operation">
        /// Recibe la instancia de la operación específica que se desea ejecutar.
        /// </param>
        /// <param name="accessToken">
        /// Recibe un token de autenticación opcional para validar los permisos del usuario.
        /// </param>
        /// <returns>
        /// Devuelve una instancia de <see cref="Response{ResultType}"/> que contiene el resultado de la operación o información sobre el error.
        /// </returns>
        /// <remarks>
        /// El método realiza los siguientes pasos:
        /// <list type="bullet">
        ///   <item><description>Valida que la operación no sea nula.</description></item>
        ///   <item><description>Valida el token de acceso y extrae los reclamos del usuario.</description></item>
        ///   <item><description>Verifica que el usuario tenga los permisos requeridos para la operación.</description></item>
        ///   <item><description>Resuelve el operador encargado de manejar la operación.</description></item>
        ///   <item><description>Crea la unidad de trabajo y ejecuta la operación de forma asíncrona.</description></item>
        ///   <item><description>Maneja los errores, registrándolos en el log.</description></item>
        ///   <item><description>Registra el log del sistema con detalles de la operación y su ejecución.</description></item>
        /// </list>
        /// </remarks>
        public async Task<Response<ResultType>> ExecuteOperation<OperationType, ResultType> (OperationType operation, string? accessToken = null) where OperationType : IOperation {

            // Si la operación es nula se rechaza la ejecución.
            if (operation == null)
                throw new ArgumentNullException(nameof(operation), "La operación no puede ser nula.");

            // Declara la variable que almacenará los reclamos (claims) obtenidos del token.
            TokenClaims tokenClaims = default!;

            // Declara la variable que almacenará el resultado final de la operación.
            Response<ResultType> result = default!;

            // Inicia un cronómetro para medir el tiempo total de ejecución de la operación.
            var stopwatch = Stopwatch.StartNew();

            // Crea una nueva instancia de SystemLog para registrar información sobre la ejecución de la operación.
            var systemLog = new SystemLog { Source = "ApplicationManager.ExecuteOperation" };

            try {

                // Realiza la validación del token de acceso y extrae la información del usuario.
                // Si el token es nulo, se configuran automáticamente credenciales de usuario invitado.
                // Si el token existe pero no es válido o ha expirado, se lanza una excepción de seguridad.
                ValidateAccessToken<ResultType>(AuthService, accessToken, out tokenClaims);

                // Verifica que el usuario tenga los permisos necesarios para ejecutar la operación.
                // Se utiliza la lista de permisos obtenida de los reclamos del token.
                ValidateOperationPermissions(operation, tokenClaims.Permissions);

                // Resuelve la fábrica de manejadores asociada a la operación mediante reflexión.
                var associatedOperationHandlerFactory = GetAssociatedOperationHandlerFactory(operation);
                // Obtiene el delegado que se encargará de crear dinámicamente el manejador para la operación.
                var operationHandlerCreator = associatedOperationHandlerFactory.GetOperationHandlerCreator<OperationType>();

                // Crea la unidad de trabajo utilizando el contexto y la colección de repositorios.
                using var unitOfWork = UnitOfWork;

                // Invoca el delegado para crear el manejador de la operación, pasando la unidad de trabajo como parámetro.
                var operationHandler = operationHandlerCreator.Invoke(associatedOperationHandlerFactory, [unitOfWork])!;

                // Verifica que el manejador creado sea del tipo esperado.
                if (operationHandler is IOperationHandler<OperationType, ResultType> typedOperationHandler) {
                    // Ejecuta la operación de forma asíncrona y almacena el resultado.
                    var operationResult = await typedOperationHandler.Handle(operation);
                    // Construye la respuesta:
                    // - Si se obtiene un resultado válido, se retorna una respuesta de éxito.
                    // - Si el resultado es nulo, se retorna una respuesta de éxito con un mensaje indicando que el resultado está vacío.
                    result = operationResult switch {
                        not null => Response<ResultType>.Success(operationResult),
                        _ => Response<ResultType>.Success(default(ResultType), statusMessage: $"La operación «{operation.GetType().Name}» ha obtenido un resultado vacío.")
                    };
                } else {
                    // Lanza una excepción si el manejador no es del tipo esperado.
                    throw new InvalidOperationException($"El manejador de operación no es del tipo esperado: {operationHandler.GetType().Name}");
                }

                // Configura el log estableciendo el nivel de información e indicando que la operación se ejecutó con éxito.
                systemLog.LogLevel = LogLevel.Information;
                // Establece el mensaje del registro del sistema con un mensaje de éxito.
                systemLog.Message = "La operación ha sido ejecutada exitosamente.".Underline();

            } catch (AggregateError ex) {
                // Recorre cada error del AggregateError y los muestra en la consola.
                foreach (var error in ex.Errors)
                    Console.WriteLine($"\n{error.ErrorCode}» {error.Message}\n");
                // Construye la respuesta de fallo a partir del error agregado.
                result = Response<ResultType>.Failure(ex);
            } catch (Exception ex) {
                // En caso de excepción, configura el registro del sistema para indicar error.
                systemLog.LogLevel = LogLevel.Error;
                // Actualiza el mensaje del log con detalles del error ocurrido.
                systemLog.Message = $"Ha ocurrido un error interno en el servidor durante la ejecución de la operación «{operation.GetType().Name}»:".Underline() + Environment.NewLine + ex.Message;
                // Construye la respuesta de fallo con el código de error 500 (Internal Server Error).
                result = Response<ResultType>.Failure(HttpStatusCode.InternalServerError, systemLog.Message, ex);
            } finally {
                // Detiene el cronómetro para obtener el tiempo total de ejecución.
                stopwatch.Stop();
                // Añade el tiempo de ejecución, formateado, al mensaje del log.
                systemLog.Message += Environment.NewLine + $"Tiempo de ejecución: {stopwatch.Elapsed.AsFormattedTime()}.".Underline();
                // Agrega detalles adicionales al log utilizando la información de la operación y la respuesta obtenida.
                DetailSystemLog(systemLog, tokenClaims, operation, result);
                // Crea la unidad de trabajo utilizando el contexto y la colección de repositorios.
                using var unitOfWork = UnitOfWork;
                // Registra el log del sistema de forma asíncrona.
                await SystemLogOperationHandlerFactory.Create_AddSystemLog_CommandHandler(unitOfWork).Handle(new AddSystemLog_Command(systemLog));
            }

            // Retorna el resultado final de la operación.
            return result;

        }

        #endregion

    }

}