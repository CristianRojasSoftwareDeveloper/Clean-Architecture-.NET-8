namespace SharedKernel.Infrastructure.Configurations {

    /// <summary>
    /// Clase que encapsula las opciones de configuración de la aplicación.
    /// Se definen valores por defecto para cada propiedad, que serán utilizados
    /// en caso de que la fuente de configuración no provea un valor explícito.
    /// </summary>
    public class ApplicationConfigurationOptions {

        /// <summary>
        /// Conjunto de cadenas de conexión a bases de datos.
        /// Se inicializa con valores predeterminados.
        /// </summary>
        public ConnectionStrings ConnectionStrings { get; set; } = new();

        /// <summary>
        /// Configuración para JSON Web Tokens (JWT).
        /// Se inicializa con valores predeterminados.
        /// </summary>
        public JWT_Settings JWT_Settings { get; set; } = new();

        /// <summary>
        /// Indica si la aplicación debe utilizar una base de datos en memoria.
        /// Valor por defecto: «true», lo que significa que se usará una base de datos en memoria.
        /// Si es «false», se utilizará un proveedor de base de datos persistente, se requerirá una cadena de conexión en «ConnectionStrings».
        /// </summary>
        public bool InMemoryDbContext { get; set; } = true;

    }

}