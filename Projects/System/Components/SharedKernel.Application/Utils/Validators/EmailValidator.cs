using System.Net.Mail;

namespace SharedKernel.Application.Utils.Validators {

    /// <summary>
    /// Clase estática que proporciona métodos para validar direcciones de correo electrónico.
    /// </summary>
    public static class EmailValidator {

        /// <summary>
        /// Valida el formato de una dirección de correo electrónico.
        /// </summary>
        /// <param name="email">La dirección de correo electrónico a validar.</param>
        /// <returns>True si la dirección de correo electrónico tiene un formato válido, False en caso contrario.</returns>
        public static bool IsValidEmail (string email) {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try {
                // Intenta crear un objeto MailAddress con la dirección de correo electrónico proporcionada.
                // Si la dirección no es válida, se lanzará una excepción FormatException.
                _ = new MailAddress(email);
                return true;
            } catch (FormatException) {
                return false;
            }
        }

    }

}