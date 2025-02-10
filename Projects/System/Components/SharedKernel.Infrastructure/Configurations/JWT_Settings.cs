namespace SharedKernel.Infrastructure.Configurations {

    /// <summary>
    /// Clase que encapsula la configuración para JSON Web Tokens (JWT).
    /// Se definen propiedades como la clave, emisor, destinatario y el tiempo de expiración.
    /// </summary>
    public class JWT_Settings {

        /// <summary>
        /// Clave de seguridad para firmar el JWT.
        /// Valor por defecto: «2BYTlejp9Bk20qEyHvzfyReyHBTMkgnJvkVCbqko/SQ=».
        /// </summary>
        public string Key { get; set; } = "2BYTlejp9Bk20qEyHvzfyReyHBTMkgnJvkVCbqko/SQ=";

        /// <summary>
        /// Emisor del JWT.
        /// Valor por defecto: «http://localhost/».
        /// </summary>
        public string Issuer { get; set; } = "http://localhost/";

        /// <summary>
        /// Destinatario del JWT.
        /// Valor por defecto: «http://localhost/».
        /// </summary>
        public string Audience { get; set; } = "http://localhost/";

        /// <summary>
        /// Tiempo de expiración del JWT en minutos.
        /// Valor por defecto: 60.
        /// </summary>
        public int ExpiryMinutes { get; set; } = 60;

    }

}