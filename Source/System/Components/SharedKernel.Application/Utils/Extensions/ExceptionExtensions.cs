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

using System.Text;

namespace SharedKernel.Application.Utils.Extensions {

    public static class ExceptionExtensions {

        private const string Separator = "--------------------------------------------------";

        /// <summary>
        /// Método de extensión para obtener todos los detalles de una excepción, incluyendo excepciones internas 
        /// y excepciones de tipo AggregateException, de manera estructurada y formateada.
        /// </summary>
        /// <param name="exception">La excepción que se desea analizar.</param>
        /// <returns>Una cadena con todos los detalles de la excepción y sus excepciones internas.</returns>
        public static string GetFormattedDetails (this Exception exception) {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception), "La excepción no puede ser nula.");
            var builder = new StringBuilder();
            builder.AppendLine("Detalles de la excepción:");
            builder.AppendLine(Separator);
            AppendExceptionDetails(exception, builder);
            return builder.ToString();
        }

        /// <summary>
        /// Agrega los detalles de una excepción y sus excepciones internas al StringBuilder.
        /// Maneja recursivamente AggregateException y excepciones anidadas.
        /// </summary>
        /// <param name="exception">La excepción que se desea procesar.</param>
        /// <param name="builder">El StringBuilder donde se almacenan los detalles.</param>
        /// <param name="level">El nivel actual de la excepción (para rastrear profundidad).</param>
        private static void AppendExceptionDetails (Exception exception, StringBuilder builder, int level = 0) {
            // Agregar información básica de la excepción
            AddExceptionInfo(exception, builder, level);
            // Manejar excepciones internas
            switch (exception) {
                case AggregateException aggregateException:
                    foreach (var innerException in aggregateException.InnerExceptions)
                        AppendExceptionDetails(innerException, builder, level + 1);
                    break;
                case { InnerException: not null }:
                    AppendExceptionDetails(exception.InnerException, builder, level + 1);
                    break;
            }
        }

        /// <summary>
        /// Agrega información detallada de una excepción al StringBuilder.
        /// </summary>
        /// <param name="exception">La excepción que se desea procesar.</param>
        /// <param name="builder">El StringBuilder donde se almacenan los detalles.</param>
        /// <param name="level">El nivel actual de la excepción (para rastrear profundidad).</param>
        private static void AddExceptionInfo (Exception exception, StringBuilder builder, int level) {
            string indent = new string(' ', level * 2); // Indentación basada en el nivel
            builder.AppendLine($"{indent}Nivel: {level}");
            builder.AppendLine($"{indent}Tipo: {exception.GetType().FullName}");
            builder.AppendLine($"{indent}Mensaje: {exception.Message}");
            builder.AppendLine($"{indent}Origen: {exception.Source ?? "No especificado"}");
            builder.AppendLine($"{indent}Método: {exception.TargetSite?.ToString() ?? "No disponible"}");
            builder.AppendLine($"{indent}Pila de llamadas: {exception.StackTrace ?? "No disponible"}");
            if (exception is AggregateException aggregateException)
                builder.AppendLine($"{indent}Excepciones internas: {aggregateException.InnerExceptions.Count}");
            builder.AppendLine(Separator);
        }

    }

}