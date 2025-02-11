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

using SharedKernel.Application.Models.Abstractions.Operations;
using SharedKernel.Domain.Models.Entities.Users;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Auth {

    /// <summary>
    /// Interfaz que define los métodos para la autenticación y generación de tokens JWT.
    /// </summary>
    public interface IAuthService {

        /// <summary>
        /// Método para hashear una contraseña utilizando el algoritmo BCrypt.
        /// </summary>
        /// <param name="password">Contraseña a hashear.</param>
        /// <returns>Hash de la contraseña.</returns>
        string HashPassword (string password);

        /// <summary>
        /// Método para verificar si una contraseña coincide con su hash utilizando el algoritmo BCrypt.
        /// </summary>
        /// <param name="password">Contraseña a verificar.</param>
        /// <param name="hashedPassword">Hash de la contraseña.</param>
        /// <returns>True si la contraseña es válida, False de lo contrario.</returns>
        bool VerifyPassword (string password, string hashedPassword);

        /// <summary>
        /// Método para generar un token JWT.
        /// </summary>
        /// <param name="user">Usuario asociado al token, incluyendo sus roles y permisos.</param>
        /// <returns>Token JWT generado.</returns>
        string GenerateToken (User user);

        /// <summary>
        /// Método para validar un token JWT y obtener los atributos en este.
        /// </summary>
        /// <param name="token">Token JWT a validar.</param>
        /// <returns>TokenClaims con los atributos extraídos del token.</returns>
        TokenClaims ValidateToken (string token);

    }

}