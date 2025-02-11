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

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SharedKernel.Infrastructure.Configurations {

    /// <summary>
    /// Gestor de configuración para aplicaciones de consola con garantías de inmutabilidad y seguridad.
    /// Implementa un patrón Singleton con control estricto de inicialización.
    /// </summary>
    public sealed class ConsoleApplicationConfiguration {

        // Indicador de estado de inicialización
        // Garantiza que solo se pueda inicializar una vez
        private static bool _isInitialized = false;

        // Instancia única de configuración 
        // Almacena la configuración con un tipo nullable para control preciso
        private static ApplicationConfiguration? _instance;

        // Objeto de bloqueo para sincronización thread-safe
        // Previene condiciones de carrera durante la inicialización
        private static readonly object _lock = new();

        // Constructor privado para prevenir instanciación externa
        // Refuerza el patrón Singleton
        private ConsoleApplicationConfiguration () { }

        /// <summary>
        /// Proporciona acceso a la instancia de configuración.
        /// Lanza una excepción si no se ha inicializado previamente.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Se lanza si se intenta acceder a la configuración sin inicialización previa.
        /// </exception>
        public static ApplicationConfiguration Instance {
            get {
                // Verificación de inicialización antes de acceder
                if (!_isInitialized)
                    throw new InvalidOperationException("La configuración debe inicializarse previamente mediante el método Initialize().");
                // Aserción de no nulidad tras verificación de inicialización
                return _instance!;
            }
        }

        /// <summary>
        /// Inicializa la configuración de manera segura y controlada.
        /// Solo puede ejecutarse una única vez en el ciclo de vida de la aplicación.
        /// </summary>
        /// <param name="configuration">
        /// Fuente de configuración opcional. 
        /// Si es nula, se utilizarán valores predeterminados.
        /// </param>
        /// <returns>La instancia de configuración inicializada.</returns>
        /// <exception cref="InvalidOperationException">
        /// Se lanza si se intenta inicializar más de una vez.
        /// </exception>
        public static ApplicationConfiguration Initialize (IConfiguration? configuration) {
            // Bloqueo para garantizar inicialización thread-safe
            lock (_lock) {
                // Previene múltiples inicializaciones
                if (_isInitialized)
                    throw new InvalidOperationException("La configuración ya ha sido inicializada. No se permiten reconfiguraciones.");
                // Construcción de opciones de configuración.
                var options = new ApplicationConfigurationOptions();
                // Enlace de configuración externa si está disponible.
                // Solo sobrescribe propiedades definidas, manteniendo valores por defecto.
                configuration?.Bind(options);
                // Creación de la instancia única de configuración.
                _instance = new ApplicationConfiguration(Options.Create(options));
                // Marcado de inicialización completa.
                _isInitialized = true;
                // Retorna la instancia de la configuración de la aplicación.
                return _instance;
            }
        }

    }

}