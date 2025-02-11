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

using System.Net;

namespace SharedKernel.Application.Models.Abstractions.Errors {

    /// <summary>
    /// Clase base para todos los errores de la aplicación.
    /// Incluye un código de error y un mensaje descriptivo.
    /// </summary>
    public class ApplicationError : Exception {

        /// <summary>
        /// Código de error HTTP asociado a este error.
        /// </summary>
        public HttpStatusCode ErrorCode { get; }

        /// <summary>
        /// Constructor protegido de la clase ApplicationError.
        /// </summary>
        /// <param name="errorCode">Código de error HTTP.</param>
        /// <param name="message">Mensaje descriptivo del error.</param>
        /// <param name="innerException">Excepción interna [opcional].</param>
        protected ApplicationError (HttpStatusCode errorCode, string message, Exception? innerException = null) : base(message, innerException) =>
            ErrorCode = errorCode;

        /// <summary>
        /// Crea un ApplicationError genérico para errores internos del servidor.
        /// </summary>
        /// <param name="innerException">Excepción interna [opcional].</param>
        /// <returns>Una nueva instancia de ApplicationError con un código de error 500.</returns>
        public static ApplicationError Create (Exception? innerException = null) => new(HttpStatusCode.InternalServerError, "Ha ocurrido un error interno en la aplicación", innerException);

        /// <summary>
        /// Crea una instancia de ApplicationError con un código y mensaje específicos.
        /// </summary>
        /// <param name="errorCode">Código de error HTTP.</param>
        /// <param name="message">Mensaje descriptivo del error.</param>
        /// <param name="innerException">Excepción interna opcional.</param>
        /// <returns>Una nueva instancia de ApplicationError.</returns>
        public static ApplicationError Create (HttpStatusCode errorCode, string message, Exception? innerException = null) => new(errorCode, message, innerException);

        /// <summary>
        /// Lanza una excepción basada en este ApplicationError.
        /// </summary>
        /// <returns>Una excepción que representa este error.</returns>
        public ApplicationError Throw () => throw this;

    }

}