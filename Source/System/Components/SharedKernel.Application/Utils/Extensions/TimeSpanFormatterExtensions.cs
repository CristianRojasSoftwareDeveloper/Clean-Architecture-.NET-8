
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

namespace SharedKernel.Application.Utils.Extensions {

    /// <summary>
    /// Proporciona métodos de extensión para formatear y representar valores de tiempo.
    /// </summary>
    public static class TimeSpanFormatterExtensions {

        /// <summary>
        /// Convierte un valor de tiempo representado como un «TimeSpan» a una representación legible,
        /// descomponiéndolo en horas, minutos, segundos y milisegundos según corresponda.
        /// </summary>
        /// <param name="timeSpan">El «TimeSpan» que se desea formatear.</param>
        /// <returns>
        /// Una cadena formateada que representa el tiempo desglosado en horas, minutos,
        /// segundos y milisegundos, con pluralización automática.
        /// </returns>
        public static string AsFormattedTime (this TimeSpan timeSpan) {
            // Validación: Asegurarse de que el valor no sea negativo.
            if (timeSpan < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(timeSpan), "El valor no puede ser negativo.");

            // Obtener los componentes individuales de tiempo.
            int hours = timeSpan.Hours;
            int minutes = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;
            int milliseconds = timeSpan.Milliseconds;

            // Crear una lista para almacenar las partes del formato de tiempo.
            var parts = new List<string>();

            // Agregar la representación de horas, si corresponde.
            if (hours > 0)
                parts.Add($"{hours} {(hours == 1 ? "hora" : "horas")}");

            // Agregar la representación de minutos, si corresponde.
            if (minutes > 0)
                parts.Add($"{minutes} {(minutes == 1 ? "minuto" : "minutos")}");

            // Agregar la representación de segundos, si corresponde.
            if (seconds > 0)
                parts.Add($"{seconds} {(seconds == 1 ? "segundo" : "segundos")}");

            // Agregar la representación de milisegundos, siempre que haya restante o no se haya agregado nada más.
            if (milliseconds > 0 || parts.Count == 0)
                parts.Add($"{milliseconds} {(milliseconds == 1 ? "milisegundo" : "milisegundos")}");

            // Combinar todas las partes con una coma y devolver el resultado.
            return string.Join(", ", parts);
        }

    }

}