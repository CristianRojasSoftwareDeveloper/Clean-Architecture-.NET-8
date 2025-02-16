
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