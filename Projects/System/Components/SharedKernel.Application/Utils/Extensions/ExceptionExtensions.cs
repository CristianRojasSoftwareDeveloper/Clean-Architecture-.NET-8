using System.Text;

namespace SharedKernel.Application.Utils.Extensions {

    public static class ExceptionExtensions {

        private const string Separator = "--------------------------------------------------";

        /// <summary>
        /// Método de extensión para obtener todos los detalles de una excepción, incluyendo excepciones internas 
        /// y excepciones de tipo AggregateException, de manera estructurada y formateada.
        /// </summary>
        /// <param name="exception">La excepción que se desea analizar.</param>
        /// <returns>Una cadena con todos los detalles de la excepción y sus excepciones internas.</returns>
        public static string GetFormattedDetails (this Exception exception) {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception), "La excepción no puede ser nula.");
            var builder = new StringBuilder();
            builder.AppendLine("Detalles de la excepción:");
            builder.AppendLine(Separator);
            AppendExceptionDetails(exception, builder);
            return builder.ToString();
        }

        /// <summary>
        /// Agrega los detalles de una excepción y sus excepciones internas al StringBuilder.
        /// Maneja recursivamente AggregateException y excepciones anidadas.
        /// </summary>
        /// <param name="exception">La excepción que se desea procesar.</param>
        /// <param name="builder">El StringBuilder donde se almacenan los detalles.</param>
        /// <param name="level">El nivel actual de la excepción (para rastrear profundidad).</param>
        private static void AppendExceptionDetails (Exception exception, StringBuilder builder, int level = 0) {
            // Agregar información básica de la excepción
            AddExceptionInfo(exception, builder, level);
            // Manejar excepciones internas
            switch (exception) {
                case AggregateException aggregateException:
                    foreach (var innerException in aggregateException.InnerExceptions)
                        AppendExceptionDetails(innerException, builder, level + 1);
                    break;
                case { InnerException: not null }:
                    AppendExceptionDetails(exception.InnerException, builder, level + 1);
                    break;
            }
        }

        /// <summary>
        /// Agrega información detallada de una excepción al StringBuilder.
        /// </summary>
        /// <param name="exception">La excepción que se desea procesar.</param>
        /// <param name="builder">El StringBuilder donde se almacenan los detalles.</param>
        /// <param name="level">El nivel actual de la excepción (para rastrear profundidad).</param>
        private static void AddExceptionInfo (Exception exception, StringBuilder builder, int level) {
            string indent = new string(' ', level * 2); // Indentación basada en el nivel
            builder.AppendLine($"{indent}Nivel: {level}");
            builder.AppendLine($"{indent}Tipo: {exception.GetType().FullName}");
            builder.AppendLine($"{indent}Mensaje: {exception.Message}");
            builder.AppendLine($"{indent}Origen: {exception.Source ?? "No especificado"}");
            builder.AppendLine($"{indent}Método: {exception.TargetSite?.ToString() ?? "No disponible"}");
            builder.AppendLine($"{indent}Pila de llamadas: {exception.StackTrace ?? "No disponible"}");
            if (exception is AggregateException aggregateException)
                builder.AppendLine($"{indent}Excepciones internas: {aggregateException.InnerExceptions.Count}");
            builder.AppendLine(Separator);
        }

    }

}