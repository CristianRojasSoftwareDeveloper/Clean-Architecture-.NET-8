using Microsoft.Extensions.Options;

namespace SharedKernel.Infrastructure.Configurations {

    /// <summary>
    /// Clase que representa la configuración central de la aplicación.
    /// Esta configuración «core» se utiliza en escenarios con módulos de inyección de dependencias,
    /// tales como ASP.NET Core o Azure Functions, donde el ciclo de vida de las dependencias se gestiona automáticamente.
    /// </summary>
    public class ApplicationConfiguration {

        /// <summary>
        /// Indica si la aplicación debe utilizar una base de datos en memoria.
        /// Si es «true», la aplicación usará un proveedor de base de datos en memoria en lugar de una base de datos persistente.
        /// Esto es especialmente útil en pruebas unitarias o entornos de desarrollo controlados.
        /// </summary>
        public bool InMemoryDbContext { get; }

        /// <summary>
        /// Conjunto de cadenas de conexión a las bases de datos.
        /// </summary>
        public ConnectionStrings ConnectionStrings { get; }

        /// <summary>
        /// Configuración relacionada con JSON Web Tokens (JWT).
        /// </summary>
        public JWT_Settings JWT_Settings { get; }

        /// <summary>
        /// Constructor que recibe las opciones de configuración encapsuladas en «IOptions<ApplicationConfigurationOptions>».
        /// Se asegura que, si alguna propiedad no está configurada en la fuente de datos, se utilice el valor por defecto.
        /// </summary>
        /// <param name="options">
        /// Objeto que contiene las opciones de configuración. 
        /// Se espera que este objeto provenga de un contenedor de inyección de dependencias.
        /// </param>
        public ApplicationConfiguration (IOptions<ApplicationConfigurationOptions> options) {

            // Se obtiene el objeto de opciones a partir del contenedor; 
            // si éste es nulo, se crea una nueva instancia de «ApplicationConfigurationOptions»
            // que contiene los valores por defecto.
            var configuration = options?.Value ?? new ApplicationConfigurationOptions();

            // Asigna la configuración de uso de base de datos en memoria.
            // Se toma el valor definido en la configuración o, si no se especifica, se usa «false» por defecto.
            InMemoryDbContext = configuration.InMemoryDbContext;

            // Asigna la configuración de las cadenas de conexión.
            // Si no se ha definido, se utiliza una nueva instancia de «ConnectionStrings»
            // que ya tiene un valor por defecto para la conexión a PostgreSQL.
            ConnectionStrings = configuration.ConnectionStrings ?? new ConnectionStrings();

            // Asigna la configuración para JWT.
            // Si la propiedad no está definida en la fuente, se usa el objeto predeterminado «JWT_Settings».
            JWT_Settings = configuration.JWT_Settings ?? new JWT_Settings();

        }

    }

}