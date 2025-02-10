namespace SharedKernel.Application.Utils.Extensions {

    /// <summary>
    /// Clase que proporciona utilidades para la validación mediante reflexión.
    /// </summary>
    public static class TypeExtensions {

        /// <summary>
        /// Determina si un tipo es un tipo primitivo extendido.
        /// Un tipo primitivo extendido incluye:
        /// - Tipos primitivos nativos (int, bool, float, etc.).
        /// - Tipos adicionales: string, DateTime, Guid, decimal, y enumeraciones.
        /// - Versiones anulables de los tipos mencionados.
        /// </summary>
        /// <param name="type">Tipo a evaluar.</param>
        /// <returns>
        /// <c>true</c> si el tipo es un tipo primitivo extendido; de lo contrario, <c>false</c>.
        /// </returns>
        public static bool IsExtendedPrimitive (this Type type) {
            // Manejar tipos anulables (Nullable<GenericType>):
            // Si el tipo es anulable, obtenemos su tipo subyacente.
            if (Nullable.GetUnderlyingType(type) is Type underlyingType)
                type = underlyingType;

            // Verificar si el tipo pertenece a los tipos primitivos extendidos:
            return type.IsPrimitive ||          // Tipos primitivos nativos.
                   type.IsEnum ||              // Enumeraciones.
                   type == typeof(string) ||   // Cadenas de texto.
                   type == typeof(DateTime) || // Fechas y horas.
                   type == typeof(Guid) ||     // Identificadores únicos.
                   type == typeof(decimal);    // Números decimales de alta precisión.
        }

    }

}