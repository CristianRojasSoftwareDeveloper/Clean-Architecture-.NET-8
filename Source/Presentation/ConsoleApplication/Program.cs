
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

using API;
using ConsoleApplication.Layers;
using ConsoleApplication.Layers.Enumerations;
using ConsoleApplication.Utils;
using Microsoft.Extensions.Configuration;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Utils.Extensions;
using SharedKernel.Infrastructure.Configurations;
using System.Diagnostics;

namespace ConsoleApplication;

internal class Program {

    /// <summary>
    /// Punto de entrada principal de la aplicación de consola.
    /// </summary>
    /// <param name="args">Argumentos de la línea de comandos.</param>
    internal static async Task Main (string[] args) {

        // Crear una instancia de ConfigurationBuilder para construir la configuración de la aplicación
        IConfiguration configuration = new ConfigurationBuilder()
            // Establece la ruta base para buscar archivos de configuración. En este caso, usamos la ruta donde se ejecuta la aplicación.
            .SetBasePath(AppContext.BaseDirectory)
            // Agrega el archivo JSON llamado "appsettings.json" como una fuente de configuración.
            // - `optional: false`: Indica que el archivo es obligatorio. Si no se encuentra, se lanzará una excepción.
            // - `reloadOnChange: true`: Permite que los cambios en el archivo se reflejen automáticamente en la configuración durante la ejecución de la aplicación.
            .AddJsonFile("app.settings.json", optional: false, reloadOnChange: true)
            // Agrega las variables de entorno del sistema operativo como fuente de configuración.
            // Esto es útil para sobrescribir configuraciones del archivo JSON o proporcionar valores sensibles
            // (como cadenas de conexión o claves API) de manera segura en entornos de despliegue.
            .AddEnvironmentVariables()
            // Construye el objeto de configuración (`IConfiguration`) con las fuentes previamente definidas.
            .Build();

        // 2. Inicialización de la configuración en la capa de consola utilizando el wrapper «ConsoleApplicationConfiguration».
        // Este método retorna directamente la configuración inicializada, permitiendo un estilo funcional.
        var applicationConfiguration = ConsoleApplicationConfiguration.Initialize(configuration);

        // Stopwatch para medir el tiempo de ejecución
        Stopwatch stopwatch = new();

        try {

            stopwatch.Start();

            #region Inicialización de servicios de la aplicación
            Printer.PrintLine($"\n{"Inicializando los servicios de la aplicación".Underline()}");

            // Instancia el administrador de la aplicación
            IApplicationManager applicationManager = new ApplicationManager(applicationConfiguration);
            Printer.PrintLine("- ApplicationManager inicializado exitosamente.");

            IAuthService authService = applicationManager.AuthService;
            Printer.PrintLine("- Servicio de autenticación inicializado exitosamente.");

            using var persistenceService = applicationManager.UnitOfWork;
            Printer.PrintLine("- Servicio de persistencia inicializado exitosamente.");
            #endregion

            var systemLayerToTest = SystemLayers.API;

            switch (systemLayerToTest) {

                case SystemLayers.Services:
                    await Main_Services_Layer.ExecuteTestFlow(authService, persistenceService);
                    break;

                case SystemLayers.Operators:
                    await Main_Operators_Layer.ExecuteTestFlow(applicationManager);
                    break;

                case SystemLayers.API:
                    await Main_API_Layer.ExecuteTestFlow(applicationManager);
                    break;

                default:
                    Printer.PrintLine($"La capa especificada «{systemLayerToTest}» no es válida.");
                    break;

            }

        } catch (Exception ex) {

            Printer.PrintLine($"\n{"Error:".Underline()}");
            Printer.PrintLine(ex.Message);
            throw;

        } finally {

            stopwatch.Stop();
            Printer.PrintLine($"\nTiempo de ejecución: {stopwatch.Elapsed.AsFormattedTime()}");

        }

    }

}