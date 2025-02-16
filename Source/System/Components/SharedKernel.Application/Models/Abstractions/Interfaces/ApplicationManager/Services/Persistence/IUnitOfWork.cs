
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

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence {

    /// <summary>
    /// Define la interfaz para el patrón Unit of Work, proporcionando capacidades
    /// de gestión de transacciones y acceso a servicios de persistencia
    /// </summary>
    public interface IUnitOfWork : IPersistenceService, IDisposable, IAsyncDisposable {

        /// <summary>
        /// Inicia una nueva transacción en la base de datos
        /// </summary>
        /// <returns>Una tarea que representa la operación asíncrona</returns>
        /// <exception cref="InvalidOperationException">Se lanza cuando ya existe una transacción en curso</exception>
        Task BeginTransactionAsync ();

        /// <summary>
        /// Confirma todos los cambios realizados dentro de la transacción actual
        /// </summary>
        /// <returns>Una tarea que representa la operación asíncrona</returns>
        /// <exception cref="InvalidOperationException">Se lanza cuando no hay ninguna transacción en curso</exception>
        Task CommitTransactionAsync ();

        /// <summary>
        /// Revierte todos los cambios realizados dentro de la transacción actual
        /// </summary>
        /// <returns>Una tarea que representa la operación asíncrona</returns>
        Task RollbackTransactionAsync ();

        /// <summary>
        /// Guarda todos los cambios pendientes en la base de datos sin gestión de transacciones
        /// </summary>
        /// <returns>El número de objetos escritos en la base de datos</returns>
        /// <exception cref="ConcurrencyError">Se lanza cuando ocurre un conflicto de concurrencia</exception>
        /// <exception cref="DatabaseError">Se lanza cuando ocurre un error en la base de datos</exception>
        Task<int> SaveChangesAsync ();

    }

}