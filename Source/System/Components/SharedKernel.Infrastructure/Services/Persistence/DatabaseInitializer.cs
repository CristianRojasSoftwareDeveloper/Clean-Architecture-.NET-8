
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
﻿using SharedKernel.Infrastructure.Configurations;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;
using System.Collections.Concurrent;

namespace SharedKernel.Infrastructure.Services.Persistence {

    /// <summary>
    /// Proporciona métodos para inicializar la base de datos de forma centralizada y evitar la ejecución reiterada de la operación «EnsureCreated».
    /// </summary>
    /// <remarks>
    /// Debido a que el contexto de la base de datos depende de la configuración suministrada en el constructor de «ApplicationManager», no es posible
    /// ejecutar la inicialización de forma estática sin contar con dicha referencia. Esta clase permite que la operación «EnsureCreated» se realice
    /// únicamente una vez por cada configuración, evitando su ejecución en cada creación de una instancia de «ApplicationManager».
    /// 
    /// Para identificar de manera única cada base de datos se utiliza una clave basada en la configuración: si se utiliza un contexto en memoria, se emplea
    /// la clave «InMemory»; de lo contrario, se utiliza la cadena de conexión de PostgreSQL definida en «configuration.ConnectionStrings.PostgreSQL».
    /// </remarks>
    public static class DatabaseInitializer {
        /// <summary>
        /// Almacena las claves de las bases de datos que ya han sido inicializadas.
        /// </summary>
        /// <remarks>
        /// Se utiliza un diccionario concurrente «_initializedDatabases» para garantizar que la inicialización se ejecute una sola vez por base de datos,
        /// incluso en escenarios de alta concurrencia.
        /// </remarks>
        private static readonly ConcurrentDictionary<string, bool> _initializedDatabases = new();

        /// <summary>
        /// Asegura que la base de datos correspondiente a la configuración proporcionada se encuentre creada.
        /// </summary>
        /// <param name="configuration">La configuración de la aplicación, la cual define el tipo de contexto de base de datos a utilizar.</param>
        /// <remarks>
        /// <para>
        /// El método determina una clave única basada en la configuración: si «configuration.InMemoryDbContext» es <c>true</c>, se utiliza la clave «InMemory»;
        /// de lo contrario, se utiliza la cadena de conexión especificada en «configuration.ConnectionStrings.PostgreSQL».
        /// </para>
        /// <para>
        /// Si la base de datos aún no ha sido inicializada (es decir, la clave no se encuentra en el diccionario «_initializedDatabases»), se crea una
        /// instancia temporal del contexto correspondiente («InMemory_DbContext» o «PostgreSQL_DbContext») para ejecutar el método «EnsureCreated».
        /// Una vez completada la inicialización, se registra la clave en el diccionario para evitar futuras ejecuciones de la operación.
        /// </para>
        /// <para>
        /// Este enfoque es fundamental para evitar la ejecución reiterada de «EnsureCreated» en cada creación de «ApplicationManager», 
        /// ya que el contexto de base de datos depende de la configuración proporcionada en tiempo de ejecución.
        /// </para>
        /// </remarks>
        public static void EnsureCreated (ApplicationConfiguration configuration) {
            // Define la clave según si se utiliza un contexto en memoria o PostgreSQL.
            string key = configuration.InMemoryDbContext
                ? "InMemory"
                : configuration.ConnectionStrings.PostgreSQL;

            // Verifica si la base de datos ya fue inicializada.
            if (!_initializedDatabases.ContainsKey(key)) {
                // Bloqueo para asegurar que la inicialización se realice solo una vez por clave.
                lock (_initializedDatabases) {
                    if (!_initializedDatabases.ContainsKey(key)) {
                        // Crea una instancia temporal del contexto para ejecutar «EnsureCreated».
                        using ApplicationDbContext dbContext = configuration.InMemoryDbContext
                            ? new InMemory_DbContext()
                            : new PostgreSQL_DbContext(configuration.ConnectionStrings.PostgreSQL);

                        // Inicializa la base de datos.
                        dbContext.Database.EnsureCreated();

                        // Registra en el diccionario que la base ya fue inicializada.
                        _initializedDatabases.TryAdd(key, true);
                    }
                }
            }
        }
    }

}