
#region GPL v3 License Header
/*
 * Clean Architecture - .NET 8
 * Copyright (C) 2025 Cristian Rojas Arredondo
 * 
 * Author / Contact:
 *   Cristian Rojas Arredondo «cristian.rojas.software.engineer@gmail.com»
 * 
 * A full copy of the GNU GPL v3 is provided in the root of this project in the "LICENSE" file.
 * Additionally, you can view the license at <https://www.gnu.org/licenses/gpl-3.0.html>.
 */

#region «English version» GPL v3 License Information 
/*
 * This file is part of Clean Architecture - .NET 8.
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <https://www.gnu.org/licenses/gpl-3.0.html>.
 * 
 * Note: In the event of any discrepancy between translations, this version (English) shall prevail.
 */
#endregion

#region «Spanish version» GPL v3 License Information
/*
 * Este archivo es parte de Clean Architecture - .NET 8.
 * 
 * Este programa es software libre: puede redistribuirlo y/o modificarlo
 * bajo los términos de la Licencia Pública General de GNU publicada por
 * la Free Software Foundation, ya sea la versión 3 de la Licencia o
 * (a su elección) cualquier versión posterior.
 * 
 * Este programa se distribuye con la esperanza de que sea útil,
 * pero SIN NINGUNA GARANTÍA, incluso sin la garantía implícita de
 * COMERCIABILIDAD o IDONEIDAD PARA UN PROPÓSITO PARTICULAR.
 * Consulte la Licencia Pública General de GNU para más detalles.
 * 
 * Usted debería haber recibido una copia de la Licencia Pública General de GNU
 * junto con este programa. De no ser así, véase <https://www.gnu.org/licenses/gpl-3.0.html>.
 * 
 * Nota: En caso de cualquier discrepancia entre las traducciones, la versión en inglés prevalecerá.
 */
#endregion

#endregion

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