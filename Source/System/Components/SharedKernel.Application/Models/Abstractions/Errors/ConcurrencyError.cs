
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

using System.Net;

namespace SharedKernel.Application.Models.Abstractions.Errors {

    /// <summary>
    /// Representa un error ocurrido por un conflicto producido durante la ejecución concurrente de dos o más operaciones.
    /// Este error es marcado con el código 500 InternalServerError.
    /// </summary>
    public class ConcurrencyError : ApplicationError {

        /// <summary>
        /// Constructor de la clase ConcurrencyError.
        /// </summary>
        /// <param name="message">Mensaje de error.</param>
        /// <param name="innerException">Excepción interna [opcional].</param>
        private ConcurrencyError (string message, Exception? innerException = null) : base(HttpStatusCode.InternalServerError, message, innerException) { }

        /// <summary>
        /// Método estático para crear una instancia de ConcurrencyError.
        /// </summary>
        /// <param name="message">Mensaje de error.</param>
        /// <param name="innerException">Excepción interna [opcional].</param>
        /// <returns>Una nueva instancia de ConcurrencyError.</returns>
        public static ConcurrencyError Create (string message, Exception? innerException = null) => new(message, innerException);

    }

}