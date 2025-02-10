using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic;

namespace SharedKernel.Application.Models.Abstractions.Attributes {

    /// <summary>
    /// «AssociatedOperationHandlerFactoryAttribute» es un atributo que establece la asociación entre una operación y el 
    /// operador responsable de procesarla. Permite definir de forma declarativa qué interfaz de operador se encargará de 
    /// manejar cada operación específica en el sistema, facilitando el enrutamiento automático sin recurrir a búsquedas 
    /// reflexivas.
    /// </summary>
    /// <remarks>
    /// Este atributo se utiliza para:
    /// <list type="bullet">
    ///   <item>
    ///     <description>Identificar el operador responsable de una operación sin necesidad de búsquedas reflexivas.</description>
    ///   </item>
    ///   <item>
    ///     <description>Establecer una relación directa entre operaciones y sus manejadores.</description>
    ///   </item>
    ///   <item>
    ///     <description>Validar en tiempo de compilación la existencia del operador asociado.</description>
    ///   </item>
    /// </list>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public class AssociatedOperationHandlerFactoryAttribute : Attribute {

        /// <summary>
        /// Obtiene el tipo de la fábrica de operadores asociada que se utilizará para procesar la operación.
        /// Este tipo debe implementar la interfaz «IOperationHandlerFactory».
        /// </summary>
        public Type OperationHandlerFactoryType { get; }

        /// <summary>
        /// Inicializa una nueva instancia del atributo «AssociatedOperationHandlerFactoryAttribute».
        /// </summary>
        /// <param name="operationHandlerFactoryType">
        /// Tipo que representa la fábrica de operadores. Debe implementar la interfaz «IOperationHandlerFactory».
        /// </param>
        /// <exception cref="ArgumentException">
        /// Se lanza cuando el tipo proporcionado no implementa la interfaz «IOperationHandlerFactory».
        /// </exception>
        public AssociatedOperationHandlerFactoryAttribute (Type operationHandlerFactoryType) {
            // Validar que el tipo proporcionado implemente la interfaz «IOperationHandlerFactory»
            if (!typeof(IOperationHandlerFactory).IsAssignableFrom(operationHandlerFactoryType))
                throw new ArgumentException("El tipo proporcionado debe implementar la interfaz «IOperationHandlerFactory»", nameof(operationHandlerFactoryType));
            OperationHandlerFactoryType = operationHandlerFactoryType;
        }

    }

}