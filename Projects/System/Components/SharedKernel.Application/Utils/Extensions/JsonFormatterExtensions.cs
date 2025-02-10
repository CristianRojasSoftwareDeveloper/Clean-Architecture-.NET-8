using System.Text.Json;
using System.Text.Json.Serialization;

namespace SharedKernel.Application.Utils.Extensions {

    /// <summary>
    /// Proporciona métodos de extensión para convertir objetos en cadenas JSON formateadas y legibles.
    /// Útil para depuración, registro y exportación de datos.
    /// </summary>
    public static class JsonFormatterExtensions {

        /// Opciones predeterminadas de serialización para JSON.
        /// Estas opciones aseguran una salida legible y evitan problemas con referencias circulares.
        private static JsonSerializerOptions DefaultSerializerOptions { get; } = new() {
            WriteIndented = true, // Formatea el JSON con indentación para facilitar su lectura.
            PropertyNamingPolicy = null, // Mantiene los nombres originales de las propiedades sin convertirlos a camelCase.
            ReferenceHandler = ReferenceHandler.IgnoreCycles // Ignora referencias circulares al usar metadatos JSON.
        };

        /// <summary>
        /// Convierte un objeto en una cadena JSON legible.
        /// </summary>
        /// <param name="complexObject">Objeto que se desea serializar.</param>
        /// <returns>Cadena JSON que representa al objeto.</returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza si el objeto proporcionado es nulo.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se lanza si ocurre un error durante la serialización.
        /// </exception>
        public static string AsJSON (this object complexObject) {
            if (complexObject == null)
                // Validación para evitar argumentos nulos.
                throw new ArgumentNullException(nameof(complexObject), "El objeto proporcionado no puede ser nulo. Verifique que la entrada sea válida antes de llamar a este método.");
            try {
                // Serializa el objeto con las opciones configuradas y devuelve el resultado.
                return JsonSerializer.Serialize(complexObject, DefaultSerializerOptions);
            } catch (ArgumentNullException ex) {
                // Proporciona un mensaje específico si ocurre un ArgumentNullException.
                throw new ArgumentNullException($"No se puede procesar el objeto [{nameof(complexObject)}] proporcionado porque es nulo. Asegúrese de que la instancia sea válida.", ex);
            } catch (JsonException ex) {
                // Proporciona un mensaje detallado si ocurre un error de serialización JSON.
                throw new InvalidOperationException($"Ha ocurrido un error al intentar serializar el objeto del tipo '{complexObject.GetType().Name}'. " +
                    "Verifique que todas las propiedades sean serializables y no contengan referencias circulares no manejadas.", ex);
            } catch (Exception ex) {
                // Maneja cualquier otra excepción no anticipada.
                throw new InvalidOperationException($"Ha ocurrido un error inesperado al intentar serializar el objeto del tipo '{complexObject.GetType().Name}'. " +
                    "Revise los detalles en la excepción interna para identificar el problema.", ex);
            }
        }

    }

}