
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

using SharedKernel.Application.Models.Abstractions.Errors;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Users.Operations.UseCases.Queries.GetUserByToken;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Entities.Users;

namespace Users.Application.Operators.Users.Operations.UseCases.Queries.GetUserByToken {

    /// <summary>
    /// Manejador para la consulta de obtención de usuario mediante un token de autenticación.
    /// </summary>
    public class GetUserByToken_QueryHandler : IGetUserByToken_QueryHandler {

        private readonly IAuthService _authService;

        /// <summary>
        /// Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GetUserByToken_QueryHandler"/>.
        /// </summary>
        /// <param name="authService">Servicio de autenticación.</param>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        public GetUserByToken_QueryHandler (IAuthService authService, IUnitOfWork unitOfWork) {
            _authService = authService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Maneja la consulta de obtención de usuario mediante un token de autenticación de forma asíncrona.
        /// </summary>
        /// <param name="query">La consulta de obtención de usuario mediante un token de autenticación.</param>
        /// <returns>Una tarea que representa la operación asíncrona, con el usuario obtenido.</returns>
        public Task<User?> Handle (IGetUserByToken_Query query) {
            // Verificar que la consulta no sea nula
            ArgumentNullException.ThrowIfNull(query);

            // Verificar que el token no sea nulo o vacío
            if (string.IsNullOrWhiteSpace(query.Token))
                throw BadRequestError.Create("El token no puede ser nulo o vacío");

            // Validar el token y obtener los atributos
            var tokenClaims = _authService.ValidateToken(query.Token);

            // Verifica si el atributo de nombre de usuario está presente en el token.
            if (string.IsNullOrWhiteSpace(tokenClaims.Username))
                // Si el atributo de nombre de usuario no está presente en el token, se lanza un error.
                throw ValidationError.Create(nameof(tokenClaims.Username), "No se ha encontrado el atributo del nombre de usuario dentro del token");

            // Obtiene y retorna el usuario por su nombre de usuario.
            return _unitOfWork.UserRepository.GetUserByUsername(tokenClaims.Username);
        }

    }

}