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

using SharedKernel.Application.Models.Abstractions.Errors;
using System.Net;

namespace SharedKernel.Application.Models.Abstractions.Operations {

    /// <summary>
    /// Clase base no genérica que encapsula los resultados de las operaciones en la aplicación.
    /// Proporciona información sobre el éxito de la operación, un mensaje de estado y posibles errores.
    /// </summary>
    public class Response {

        /// <summary>
        /// Mensaje que describe el resultado de la operación.
        /// </summary>
        public string StatusMessage { get; }

        /// <summary>
        /// Error de la aplicación, si la operación falló.
        /// </summary>
        public ApplicationError? ApplicationError { get; }

        /// <summary>
        /// Constructor protegido para la clase Response y sus clases derivadas.
        /// </summary>
        /// <param name="statusMessage">Mensaje de estado de la operación.</param>
        /// <param name="applicationError">Error de la aplicación si la operación falló.</param>
        protected Response (string statusMessage, ApplicationError? applicationError = null) {
            StatusMessage = statusMessage;
            ApplicationError = applicationError;
        }

        /// <summary>
        /// Crea un resultado exitoso con un mensaje de estado opcional.
        /// </summary>
        /// <param name="statusMessage">Mensaje de estado de la operación, por defecto "Operación exitosa".</param>
        /// <returns>Un objeto Response que indica éxito.</returns>
        public static Response Success (string statusMessage = "Operación exitosa") =>
            new(statusMessage);

        /// <summary>
        /// Crea un resultado fallido a partir de un error de aplicación.
        /// </summary>
        /// <param name="applicationError">Error de la aplicación que causó la falla.</param>
        /// <returns>Un objeto Response que indica fallo.</returns>
        public static Response Failure (ApplicationError applicationError) =>
            new(applicationError.Message, applicationError);

        /// <summary>
        /// Crea un resultado fallido con un código de error personalizado.
        /// </summary>
        /// <param name="errorCode">Código de error HTTP.</param>
        /// <param name="statusMessage">Mensaje de estado de la operación, por defecto "Operación fallida".</param>
        /// <param name="innerException">Excepción interna opcional.</param>
        /// <returns>Un objeto Response que indica fallo con un código de error personalizado.</returns>
        public static Response Failure (HttpStatusCode errorCode, string statusMessage = "Operación fallida", Exception? innerException = null) =>
            Failure(ApplicationError.Create(errorCode, statusMessage, innerException));

        /// <summary>
        /// Indica si la operación fue exitosa.
        /// </summary>
        public bool IsSuccess => ApplicationError == null;

    }

    /// <summary>
    /// Clase genérica derivada de Response que encapsula los resultados de las operaciones,
    /// incluyendo un dato específico de tipo GenericBodyType.
    /// </summary>
    /// <typeparam name="GenericBodyType">Tipo de datos esperado en el resultado.</typeparam>
    public class Response<GenericBodyType> : Response {

        /// <summary>
        /// Datos resultantes de la operación, si fue exitosa.
        /// </summary>
        public GenericBodyType? Body { get; }

        /// <summary>
        /// Constructor privado para crear un resultado exitoso con datos.
        /// </summary>
        /// <param name="body">Datos resultantes de la operación.</param>
        /// <param name="statusMessage">Mensaje de estado de la operación, por defecto "Operación exitosa".</param>
        private Response (GenericBodyType body, string statusMessage) : base(statusMessage) =>
            Body = body;

        /// <summary>
        /// Constructor privado para crear un resultado fallido.
        /// </summary>
        /// <param name="applicationError">Error de la aplicación que causó la falla.</param>
        private Response (ApplicationError applicationError) : base(applicationError.Message, applicationError) =>
            Body = default; // No hay datos si la operación falló.

        /// <summary>
        /// Crea un resultado exitoso con datos.
        /// </summary>
        /// <param name="body">Datos resultantes de la operación.</param>
        /// <param name="statusMessage">Mensaje de estado de la operación, por defecto "Operación exitosa".</param>
        /// <returns>Un objeto Response que indica éxito con datos.</returns>
        public static Response<GenericBodyType> Success (GenericBodyType body, string statusMessage = "Operación exitosa") =>
            new(body, statusMessage);

        /// <summary>
        /// Crea un resultado fallido a partir de un error de aplicación.
        /// </summary>
        /// <param name="applicationError">Error de la aplicación que causó la falla.</param>
        /// <returns>Un objeto Response que indica fallo.</returns>
        public static new Response<GenericBodyType> Failure (ApplicationError applicationError) =>
            new(applicationError);

        /// <summary>
        /// Crea un resultado fallido con un código de error personalizado.
        /// </summary>
        /// <param name="errorCode">Código de error HTTP.</param>
        /// <param name="statusMessage">Mensaje de estado de la operación, por defecto "Operación fallida".</param>
        /// <param name="innerException">Excepción interna opcional.</param>
        /// <returns>Un objeto Response que indica fallo con un código de error personalizado.</returns>
        public static new Response<GenericBodyType> Failure (HttpStatusCode errorCode, string statusMessage = "Operación fallida", Exception? innerException = null) =>
            Failure(ApplicationError.Create(errorCode, statusMessage, innerException));

    }

}