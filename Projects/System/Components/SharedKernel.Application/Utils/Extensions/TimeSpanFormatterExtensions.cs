namespace SharedKernel.Application.Utils.Extensions {

    /// <summary>
    /// Proporciona métodos de extensión para formatear y representar valores de tiempo.
    /// </summary>
    public static class TimeSpanFormatterExtensions {

        /// <summary>
        /// Convierte un valor de tiempo representado como un «TimeSpan» a una representación legible,
        /// descomponiéndolo en horas, minutos, segundos y milisegundos según corresponda.
        /// </summary>
        /// <param name="timeSpan">El «TimeSpan» que se desea formatear.</param>
        /// <returns>
        /// Una cadena formateada que representa el tiempo desglosado en horas, minutos,
        /// segundos y milisegundos, con pluralización automática.
        /// </returns>
        public static string AsFormattedTime (this TimeSpan timeSpan) {
            // Validación: Asegurarse de que el valor no sea negativo.
            if (timeSpan < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(timeSpan), "El valor no puede ser negativo.");

            // Obtener los componentes individuales de tiempo.
            int hours = timeSpan.Hours;
            int minutes = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;
            int milliseconds = timeSpan.Milliseconds;

            // Crear una lista para almacenar las partes del formato de tiempo.
            var parts = new List<string>();

            // Agregar la representación de horas, si corresponde.
            if (hours > 0)
                parts.Add($"{hours} {(hours == 1 ? "hora" : "horas")}");

            // Agregar la representación de minutos, si corresponde.
            if (minutes > 0)
                parts.Add($"{minutes} {(minutes == 1 ? "minuto" : "minutos")}");

            // Agregar la representación de segundos, si corresponde.
            if (seconds > 0)
                parts.Add($"{seconds} {(seconds == 1 ? "segundo" : "segundos")}");

            // Agregar la representación de milisegundos, siempre que haya restante o no se haya agregado nada más.
            if (milliseconds > 0 || parts.Count == 0)
                parts.Add($"{milliseconds} {(milliseconds == 1 ? "milisegundo" : "milisegundos")}");

            // Combinar todas las partes con una coma y devolver el resultado.
            return string.Join(", ", parts);
        }

    }

}