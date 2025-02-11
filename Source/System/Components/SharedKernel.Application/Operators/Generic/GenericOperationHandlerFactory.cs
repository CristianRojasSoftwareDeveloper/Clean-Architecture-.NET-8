#region GPL v3 License Header

/*
 * Clean Architecture - .NET 8
 * Copyright (C) 2025 Cristian Rojas Arredondo
 *
 * Author / Contact:
 *   Cristian Rojas Arredondo «cristian.rojas.software.developer@gmail.com»
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
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.AddEntity;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.UpdateEntity;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.AddEntity;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Commands.UpdateEntity;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Domain.Models.Abstractions.Interfaces;
using System.Collections.Concurrent;
using System.Data;
using System.Net;
using System.Reflection;

namespace SharedKernel.Application.Operators.Generic {

    /// <summary>
    /// Implementación abstracta de operaciones genéricas CRUD y manejo de entidades.
    /// Proporciona una base para operadores que trabajan con entidades genéricas.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad con la que se realizarán las operaciones. Debe implementar IGenericEntity.</typeparam>
    /// <remarks>
    /// Esta clase abstracta implementa IGenericOperationHandlerFactory y proporciona:
    /// - Manejo automático de operaciones mediante reflexión
    /// - Validación de permisos para operaciones
    /// - Operaciones CRUD básicas para entidades
    /// - Caché de manejadores para mejor rendimiento
    /// </remarks>
    public abstract class GenericOperationHandlerFactory<EntityType> : IGenericOperationHandlerFactory<EntityType> where EntityType : IGenericEntity {

        /// <summary>
        /// Caché concurrente de factorías de manejadores de operaciones registradas para diferentes tipos de operadores.
        /// </summary>
        /// <remarks>
        /// Se utiliza un «ConcurrentDictionary» para almacenar las referencias a los métodos factoría de cada operador.
        /// Esto garantiza la seguridad en entornos multihilo, evitando condiciones de carrera y problemas de concurrencia 
        /// al acceder a la caché desde múltiples hilos.
        /// </remarks>
        private static readonly ConcurrentDictionary<Type, Dictionary<Type, MethodInfo>> _operationHandlerFactoriesCache = new();

        /// <summary>
        /// Registra los métodos factoría de manejadores de operaciones para un tipo específico de operador.
        /// </summary>
        /// <param name="operatorType">El tipo de operador para el cual se registrarán las factorías de manejadores de operaciones.</param>
        /// <returns>
        /// Un diccionario que mapea tipos de operaciones a sus métodos de factoría de manejadores de operaciones.
        /// </returns>
        /// <remarks>
        /// Este método utiliza reflexión para identificar los métodos de instancia marcados con el atributo 
        /// «OperationHandlerCreatorAttribute», los cuales actúan como factorías de «OperationHandler»s.
        /// 
        /// Una vez identificados, los métodos se almacenan en un diccionario que mapea el tipo de operación con su 
        /// respectiva factoría de «OperationHandler»s. Posteriormente, este diccionario puede ser reutilizado 
        /// para instanciar nuevos «OperationHandler»s dinámicamente según sea necesario.
        /// </remarks>
        private static Dictionary<Type, MethodInfo> RegisterOperationHandlerCreators (Type operatorType) {

            // Se obtienen todos los métodos públicos de instancia que cuenten con el atributo «OperationHandlerCreatorAttribute».
            var methodsWithAttribute = operatorType
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Select(methodInfo => new {
                    Info = methodInfo,
                    CreatorAttribute = methodInfo.GetCustomAttribute<OperationHandlerCreatorAttribute>()
                })
                .Where(method => method.CreatorAttribute is not null);

            // Se proyecta la información necesaria y se valida que cada «OperationType» implemente la interface «IOperation».
            var validatedMethods = methodsWithAttribute
                .Select(method => {
                    // Extrae el tipo de operación desde el atributo de forma local.
                    var operationType = method.CreatorAttribute!.OperationType;
                    return new {
                        OperationType = operationType,
                        Info = method.Info,
                        IsValid = typeof(IOperation).IsAssignableFrom(operationType)
                    };
                })
                .ToList();

            // Se recopilan todos los errores encontrados en la validación.
            var validationErrors = validatedMethods
                .Where(method => !method.IsValid)
                .Select(method =>
                    ApplicationError.Create(
                        HttpStatusCode.InternalServerError,
                        $"El tipo de operación «{method.OperationType.Name}» no implementa la interface «IOperation»."
                    )
                )
                .ToList();

            // Si se encontraron errores, se lanza una excepción agregada con todos ellos.
            if (validationErrors.Count > 0)
                throw AggregateError.Create(validationErrors);

            // Se crea el diccionario de métodos válidos, mapeando cada «OperationType» a su método factoría.
            var operationHandlerCreators = validatedMethods
                .Where(method => method.IsValid)
                .ToDictionary(method => method.OperationType, method => method.Info);

            // Se retorna el diccionario de creadores de «OperationHandler»s registrados.
            return operationHandlerCreators;

        }

        /// <summary>
        /// Obtiene las factorías de manejadores de operaciones registradas para el tipo actual del operador.
        /// </summary>
        /// <returns>
        /// Un diccionario que mapea tipos de operaciones a sus métodos de factoría de manejadores de operaciones.
        /// </returns>
        /// <remarks>
        /// Este método implementa un mecanismo de caché para optimizar el rendimiento, utilizando un 
        /// «ConcurrentDictionary» para garantizar la seguridad en entornos multihilo. 
        /// - Si las factorías de «OperationHandler's» ya están registradas, se devuelven directamente desde la caché.
        /// - Si no existen en la caché, se registran mediante el método «RegisterOperationHandlerCreators()», 
        ///   y luego se almacenan en caché para futuras solicitudes.
        /// </remarks>
        private Dictionary<Type, MethodInfo> GetOperationHandlerCreators () =>
            _operationHandlerFactoriesCache.GetOrAdd(GetType(), RegisterOperationHandlerCreators);

        /// <summary>
        /// Obtiene el método factoría (factory) encargado de crear el manejador de operaciones para un tipo de operación específico.
        /// </summary>
        /// <param name="operationType">
        /// El <b>Type</b> que representa el tipo de operación para el cual se desea obtener el método factoría.
        /// Este tipo <b>debe</b> implementar la interface «IOperation».
        /// </param>
        /// <returns>
        /// Un objeto de tipo <b>MethodInfo</b> que representa el método factoría registrado para la operación indicada.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Este método interno trabaja directamente con el tipo <b>Type</b> para simplificar la lógica de búsqueda en la
        /// caché de métodos factoría. Se consulta un diccionario que mapea cada tipo de operación a su correspondiente
        /// método factory.
        /// </para>
        /// <para>
        /// Si el diccionario no contiene un método factory para el tipo proporcionado, se construye un mensaje de error
        /// que distingue entre dos casos:
        /// </para>
        /// <list type="bullet">
        ///   <item>
        ///     <description>
        ///       Si <b>operationType</b> implementa la interface «IOperation», se indica que el método factory no fue encontrado.
        ///     </description>
        ///   </item>
        ///   <item>
        ///     <description>
        ///       Si <b>operationType</b> no implementa la interface «IOperation», se informa que el tipo proporcionado no es válido.
        ///     </description>
        ///   </item>
        /// </list>
        /// <para>
        /// En cualquiera de estos casos, se lanza una excepción de tipo «BadRequestError» con el mensaje de error correspondiente.
        /// </para>
        /// </remarks>
        private MethodInfo GetOperationHandlerCreator (Type operationType) {
            // Se intenta recuperar el método factory asociado al tipo de operación desde la caché.
            if (GetOperationHandlerCreators().TryGetValue(operationType, out var operationHandlerFactory))
                return operationHandlerFactory;
            // Se construye un mensaje de error en función de si el tipo implementa o no la interface «IOperation».
            // Se utiliza pattern matching para extraer el valor y verificar si es asignable a «IOperation».
            var errorMessage = operationType switch {
                // Se verifica que el tipo, asignado a la variable «operationType»,
                // implemente o sea asignable a la interfaz «IOperation».
                _ when typeof(IOperation).IsAssignableFrom(operationType)
                    => $"El método factoría del manejador de la operación «{operationType.Name}» no fue encontrado entre los métodos factoría registrados en «{GetType().Name}».",
                // Caso por defecto: si el tipo no implementa «IOperation».
                _ => $"El tipo subyacente del argumento «{nameof(operationType)}» debe implementar la interface «IOperation»."
            };
            // Se lanza una excepción de tipo «BadRequestError» con el mensaje detallado.
            throw BadRequestError.Create(errorMessage);
        }

        /// <summary>
        /// Obtiene el método factoría del manejador de operaciones para el tipo de operación especificado mediante un parámetro genérico.
        /// </summary>
        /// <typeparam name="OperationType">
        /// Especifica el tipo de operación para el cual se desea obtener el método factory.
        /// Este tipo debe implementar la interface «IOperation», lo cual se verifica en tiempo de compilación.
        /// </typeparam>
        /// <returns>
        /// Un objeto de tipo <b>MethodInfo</b> que representa el método factoría registrado para el tipo de operación indicado.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Este método público aprovecha las restricciones genéricas para garantizar que el parámetro de tipo cumpla
        /// con la interface «IOperation», lo que aporta seguridad de tipos en tiempo de compilación.
        /// </para>
        /// <para>
        /// Internamente, este método delega la obtención del método factory al método privado «GetOperationHandlerCreator(Type operationType)»,
        /// pasando como argumento el resultado de <b>typeof(OperationType)</b>. De esta forma, se simplifica la implementación interna
        /// al trabajar exclusivamente con el tipo <b>Type</b>.
        /// </para>
        /// </remarks>
        public MethodInfo GetOperationHandlerCreator<OperationType> () where OperationType : IOperation =>
            GetOperationHandlerCreator(typeof(OperationType));

        #region Operation Execution

        /// <inheritdoc/>
        //public async Task<ResultType> ExecuteHandler<OperationType, ResultType> (OperationType operation) where OperationType : IOperation {

        //    // Obtiene los manejadores registrados para este tipo de operador.
        //    var handlers = GetHandlers();

        //    var operationType = operation.GetType();
        //    var operationInterfaces = operationType.GetInterfaces();
        //    var immediateOperationInterface = operationInterfaces.FirstOrDefault(@interface => @interface.Name.Equals($"I{operationType.Name}")) ??
        //        throw new InvalidOperationException($"La interface de la operación «{operation.GetType()}» no ha sido encontrada.");

        //    // Intenta obtener el manejador correspondiente para el tipo de operación.
        //    if (!handlers.TryGetValue(immediateOperationInterface, out var handler))
        //        // Si no se encuentra un manejador, lanza una excepción.
        //        throw new InvalidOperationException($"No hay un manejador asíncrono registrado para la operación «{operation.GetType()}».");

        //    // Invoca el manejador correspondiente y espera su resultado.
        //    return await (Task<ResultType>) handler.Invoke(this, [operation])!;
        //}

        #endregion

        #region Operaciones CRUD asíncronas

        #region Queries (Consultas)

        /// <inheritdoc />
        public IGetEntityByID_QueryHandler<EntityType> Create_GetEntityByID_Handler (IUnitOfWork unitOfWork) => new GetEntityByID_QueryHandler<EntityType>(unitOfWork);

        /// <inheritdoc />
        public IGetEntities_QueryHandler<EntityType> Create_GetEntities_Handler (IUnitOfWork unitOfWork) => new GetEntities_QueryHandler<EntityType>(unitOfWork);

        #endregion

        #region Commands (Comandos)

        /// <inheritdoc />
        public IAddEntity_CommandHandler<EntityType> Create_AddEntity_Handler (IUnitOfWork unitOfWork) => new AddEntity_CommandHandler<EntityType>(unitOfWork);

        /// <inheritdoc />
        public IUpdateEntity_CommandHandler<EntityType> Create_UpdateEntity_Handler (IUnitOfWork unitOfWork) => new UpdateEntity_CommandHandler<EntityType>(unitOfWork);

        /// <inheritdoc />
        public IDeleteEntityByID_CommandHandler<EntityType> Create_DeleteEntityByID_Handler (IUnitOfWork unitOfWork) => new DeleteEntityByID_CommandHandler<EntityType>(unitOfWork);

        #endregion

        #endregion

    }

}