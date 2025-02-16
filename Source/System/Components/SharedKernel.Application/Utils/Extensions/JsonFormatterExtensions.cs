
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

using System.Text.Json;
using System.Text.Json.Serialization;

namespace SharedKernel.Application.Utils.Extensions {

    /// <summary>
    /// Proporciona métodos de extensión para convertir objetos en cadenas JSON formateadas y legibles.
    /// Útil para depuración, registro y exportación de datos.
    /// </summary>
    public static class JsonFormatterExtensions {

        /// Opciones predeterminadas de serialización para JSON.
        /// Estas opciones aseguran una salida legible y evitan problemas con referencias circulares.
        private static JsonSerializerOptions DefaultSerializerOptions { get; } = new() {
            WriteIndented = true, // Formatea el JSON con indentación para facilitar su lectura.
            PropertyNamingPolicy = null, // Mantiene los nombres originales de las propiedades sin convertirlos a camelCase.
            ReferenceHandler = ReferenceHandler.IgnoreCycles // Ignora referencias circulares al usar metadatos JSON.
        };

        /// <summary>
        /// Convierte un objeto en una cadena JSON legible.
        /// </summary>
        /// <param name="complexObject">Objeto que se desea serializar.</param>
        /// <returns>Cadena JSON que representa al objeto.</returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza si el objeto proporcionado es nulo.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Se lanza si ocurre un error durante la serialización.
        /// </exception>
        public static string AsJSON (this object complexObject) {
            if (complexObject == null)
                // Validación para evitar argumentos nulos.
                throw new ArgumentNullException(nameof(complexObject), "El objeto proporcionado no puede ser nulo. Verifique que la entrada sea válida antes de llamar a este método.");
            try {
                // Serializa el objeto con las opciones configuradas y devuelve el resultado.
                return JsonSerializer.Serialize(complexObject, DefaultSerializerOptions);
            } catch (ArgumentNullException ex) {
                // Proporciona un mensaje específico si ocurre un ArgumentNullException.
                throw new ArgumentNullException($"No se puede procesar el objeto [{nameof(complexObject)}] proporcionado porque es nulo. Asegúrese de que la instancia sea válida.", ex);
            } catch (JsonException ex) {
                // Proporciona un mensaje detallado si ocurre un error de serialización JSON.
                throw new InvalidOperationException($"Ha ocurrido un error al intentar serializar el objeto del tipo '{complexObject.GetType().Name}'. " +
                    "Verifique que todas las propiedades sean serializables y no contengan referencias circulares no manejadas.", ex);
            } catch (Exception ex) {
                // Maneja cualquier otra excepción no anticipada.
                throw new InvalidOperationException($"Ha ocurrido un error inesperado al intentar serializar el objeto del tipo '{complexObject.GetType().Name}'. " +
                    "Revise los detalles en la excepción interna para identificar el problema.", ex);
            }
        }

    }

}