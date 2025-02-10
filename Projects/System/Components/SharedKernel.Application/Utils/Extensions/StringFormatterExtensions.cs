
namespace SharedKernel.Application.Utils.Extensions {

    public static class StringFormatterExtensions {

        /// <summary>
        /// Retorna la cadena original seguida por un salto de línea y una cantidad de guiones largos igual a la cantidad de caracteres de la cadena.
        /// </summary>
        /// <param name="value">La cadena original.</param>
        /// <returns>La cadena original seguida por un salto de línea y guiones largos.</returns>
        public static string Underline (this string value) {
            // Verificar si el valor es nulo o vacío
            if (string.IsNullOrWhiteSpace(value))
                return value;

            value = value.Trim();

            // Crear una cadena de guiones largos con la misma longitud
            string underline = new string('═', value.Length);

            // Combinar la cadena original, un salto de línea y la cadena de guiones largos
            return $"{value}\n{underline}";
        }

        public static string AddLeftMargin (this string value, int marginSize) =>
            string.Join('\n', value.Split('\n').Select(line => $"{new string(' ', marginSize)}{line}"));

    }

}