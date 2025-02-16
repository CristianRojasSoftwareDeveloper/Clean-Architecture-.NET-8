
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

using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Models.Abstractions.Enumerations;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts {

    /// <summary>
    /// Implementación específica de «DbContext» para PostgreSQL.
    /// Configura y gestiona la conexión con una base de datos PostgreSQL, incluyendo el mapeo de enumeraciones y otras configuraciones específicas del proveedor.
    /// </summary>
    /// <param name="connectionString">
    /// Cadena de conexión a la base de datos PostgreSQL.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se lanza cuando «connectionString» es nula, vacía o contiene únicamente espacios en blanco.
    /// </exception>
    public class PostgreSQL_DbContext (string connectionString) : ApplicationDbContext(CreateConfiguration(connectionString)) {

        /// <summary>
        /// Crea la configuración necesaria para la conexión a PostgreSQL.
        /// </summary>
        /// <param name="connectionString">
        /// Cadena de conexión que especifica los parámetros para acceder a la base de datos PostgreSQL.
        /// </param>
        /// <returns>
        /// Opciones de configuración específicas para PostgreSQL, incluyendo el mapeo de enumeraciones.
        /// </returns>
        /// <remarks>
        /// Se configura el mapeo del enum «LogLevel» a la columna «log_level».
        /// </remarks>
        private static DbContextOptions<PostgreSQL_DbContext> CreateConfiguration (string connectionString) {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "La cadena de conexión no puede estar vacía.");
            return new DbContextOptionsBuilder<PostgreSQL_DbContext>().UseNpgsql(
                connectionString,
                npgsqlOptions => npgsqlOptions.MapEnum<LogLevel>("log_level")
            ).Options;
        }

        /// <summary>
        /// Constructor estático para configurar comportamientos globales específicos de PostgreSQL.
        /// </summary>
        /// <remarks>
        /// Habilita el comportamiento de marca de tiempo heredado para mantener la compatibilidad con versiones anteriores de PostgreSQL en el manejo de timestamps.
        /// </remarks>
        static PostgreSQL_DbContext () => AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    }

}