#region GPL v3 License Header

/*
 * Clean Architecture - .NET 8
 * Copyright (C) 2025 Cristian Rojas Arredondo
 *
 * Author / Contact:
 *   Cristian Rojas Arredondo «cristian.rojas.software.developer@gmail.com»
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

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts {

    /// <summary>
    /// Implementación específica de «DbContext» para pruebas en memoria.
    /// Proporciona una base de datos en memoria para pruebas unitarias y desarrollo, sin necesidad de disponer de una base de datos real.
    /// </summary>
    /// <param name="databaseName">
    /// Nombre que identifica la instancia de la base de datos en memoria. Si no se especifica, se utiliza «"InMemoryDb"» por defecto.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Se lanza cuando «databaseName» es nulo, vacío o contiene únicamente espacios en blanco.
    /// </exception>
    public class InMemory_DbContext (string databaseName = "InMemoryDb") : ApplicationDbContext(CreateConfiguration(databaseName)) {

        /// <summary>
        /// Crea la configuración necesaria para la base de datos en memoria.
        /// </summary>
        /// <param name="databaseName">
        /// Nombre que identifica la instancia de la base de datos en memoria.
        /// </param>
        /// <returns>
        /// Opciones de configuración específicas para la base de datos en memoria.
        /// </returns>
        private static DbContextOptions<InMemory_DbContext> CreateConfiguration (string databaseName) {
            if (string.IsNullOrWhiteSpace(databaseName))
                throw new ArgumentNullException(nameof(databaseName), "El nombre de la base de datos en memoria no puede estar vacío.");
            return new DbContextOptionsBuilder<InMemory_DbContext>().UseInMemoryDatabase(databaseName).Options;
        }

    }

}