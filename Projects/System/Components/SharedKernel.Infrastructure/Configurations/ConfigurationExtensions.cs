using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedKernel.Infrastructure.Configurations {

    /// <summary>
    /// Métodos de extensión para facilitar la integración de la configuración en aplicaciones que
    /// utilizan inyección de dependencias, como ASP.NET Core o Azure Functions.
    /// </summary>
    public static class ConfigurationExtensions {

        /// <summary>
        /// Agrega la configuración de la aplicación al contenedor de servicios.
        /// Este método:
        ///  - Enlaza la fuente de configuración a la clase de opciones «ApplicationConfigurationOptions».
        ///  - Registra «ApplicationConfiguration» como Singleton en el contenedor de servicios.
        /// 
        /// Nota: Esta configuración «core» está pensada para ser utilizada en aplicaciones donde se dispone de
        /// un módulo de inyección de dependencias que se encarga de gestionar el ciclo de vida de las dependencias.
        /// </summary>
        /// <param name="services">Colección de servicios donde se registrarán las dependencias.</param>
        /// <param name="configuration">Fuente de configuración de la aplicación.</param>
        /// <returns>La misma colección de servicios, permitiendo encadenar otras llamadas.</returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza si «configuration» es nula, ya que se requiere una fuente de configuración válida.
        /// </exception>
        public static IServiceCollection AddApplicationConfiguration (this IServiceCollection services, IConfiguration configuration) {
            // Verifica que la fuente de configuración no sea nula.
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration), "El objeto de configuración no puede ser nulo.");

            // Enlaza la fuente de configuración con la clase de opciones «ApplicationConfigurationOptions».
            // Si alguna propiedad no se encuentra en la fuente, se utilizará el valor por defecto definido en la clase.
            services.Configure<ApplicationConfigurationOptions>(configuration);

            // Registra «ApplicationConfiguration» como Singleton para que sea una única instancia en la aplicación.
            services.AddSingleton<ApplicationConfiguration>();

            // Retorna la colección de servicios modificada.
            return services;
        }

    }

}