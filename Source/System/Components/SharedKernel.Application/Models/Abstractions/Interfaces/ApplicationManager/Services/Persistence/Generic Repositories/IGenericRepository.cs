
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

using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories {

    /// <summary>
    /// Define las operaciones CRUD genéricas para un repositorio.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de entidad que maneja el repositorio.</typeparam>
    public interface IGenericRepository<EntityType> : IQueryableRepository<EntityType> where EntityType : IGenericEntity {

        /// <summary>
        /// Agrega de manera asíncrona una nueva entidad al repositorio.
        /// </summary>
        /// <param name="newEntity">La entidad a agregar.</param>
        /// <returns>La entidad agregada.</returns>
        Task<EntityType> AddEntity (EntityType newEntity);

        /// <summary>
        /// Obtiene de manera asíncrona todas las entidades del repositorio.
        /// </summary>
        /// <param name="enableTracking">Si es true, habilita el tracking de Entity Framework para las entidades retornadas.
        /// Si es false (por defecto), deshabilita el tracking para mejor rendimiento en consultas de solo lectura.</param>
        /// <returns>Lista de todas las entidades en el repositorio.</returns>
        Task<List<EntityType>> GetEntities (bool enableTracking = false);

        /// <summary>
        /// Obtiene una entidad por su ID de forma asíncrona.
        /// </summary>
        /// <param name="entityID">El ID de la entidad.</param>
        /// <returns>La tarea que representa la operación asincrónica, con la entidad encontrada o null si no existe como resultado.</returns>
        Task<EntityType?> GetEntityByID (int entityID, bool enableTracking = false);

        /// <summary>
        /// Actualiza de manera asíncrona una entidad en el repositorio.
        /// </summary>
        /// <param name="entityUpdate">La entidad con los valores actualizados.</param>
        /// <returns>La entidad actualizada.</returns>
        Task<EntityType> UpdateEntity (Partial<EntityType> entityUpdate);

        /// <summary>
        /// Elimina una entidad por su ID de forma asíncrona.
        /// </summary>
        /// <param name="entityID">El ID de la entidad a eliminar.</param>
        /// <returns>La tarea que representa la operación asincrónica, con un valor booleano que indica si la eliminación fue exitosa.</returns>
        Task<EntityType> DeleteEntityByID (int entityID);

    }

}