namespace SharedKernel.Application.Models.Abstractions.Attributes {

    /// <summary>
    /// Atributo que identifica un método creador encargado de crear instancias de manejadores de operaciones.
    /// Este atributo almacena metadata sobre el tipo de operación que el manejador procesará.
    /// </summary>
    /// <remarks>
    /// Este atributo se utiliza exclusivamente en métodos que actúan como factories para crear
    /// instancias de manejadores de operaciones específicas. El tipo de operación almacenado
    /// sirve como metadata que puede ser consultada en tiempo de ejecución para identificar
    /// qué tipo de operación está diseñado para procesar el manejador.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public class OperationHandlerCreatorAttribute (Type operationType) : Attribute {

        /// <summary>
        /// Obtiene el tipo de operación que el manejador procesará.
        /// </summary>
        /// <value>
        /// Tipo que representa la operación específica que el manejador está diseñado para procesar.
        /// Este valor se establece durante la construcción del atributo y es inmutable.
        /// </value>
        /// <exception cref="ArgumentNullException">Se produce cuando se intenta asignar un valor nulo al tipo de operación.</exception>
        public Type OperationType { get; } = operationType ?? throw new ArgumentNullException(nameof(operationType));

    }

}