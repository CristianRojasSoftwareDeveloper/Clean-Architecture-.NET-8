namespace SharedKernel.Domain.Utils.Extensions {

    /// <summary>
    /// Clase de extensiones que provee métodos para formatear valores nulos de distintos tipos.
    /// </summary>
    public static class ValueFormatterExtensions {

        /// <summary>
        /// Método de extensión para formatear valores de cadena nulos, devolviendo un texto predeterminado si el valor es null.
        /// </summary>
        /// <param name="value">Valor de tipo string a formatear.</param>
        /// <returns>Cadena formateada o «No especificado» si el valor es null.</returns>
        public static string FormatStringValue (this string? value) =>
            value ?? "No especificado";

        /// <summary>
        /// Método de extensión para formatear valores de fecha nulos, devolviendo un texto predeterminado si el valor es null.
        /// </summary>
        /// <param name="date">Valor de tipo DateTime? a formatear.</param>
        /// <returns>Cadena formateada con el formato «yyyy-MM-dd HH:mm:ss» o «No especificada» si el valor es null.</returns>
        public static string FormatDateTime (this DateTime? date) =>
            date?.ToString("yyyy-MM-dd HH:mm:ss") ?? "No especificada";

    }

}