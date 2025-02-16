
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

using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.AddEntity;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.DeleteEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Commands.UpdateEntity;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntities;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic.Operations.CRUD.Queries.GetEntityByID;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Domain.Models.Abstractions.Interfaces;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Operators.Generic {

    /// <summary>
    /// Interfaz que define las operaciones básicas CRUD compartidas por todos los operadores genéricos.
    /// Proporciona métodos factory para crear manejadores especializados en operaciones específicas.
    /// </summary>
    /// <typeparam name="EntityType">Tipo de entidad sobre la que opera el operador.</typeparam>
    public interface IGenericOperationHandlerFactory<EntityType> : IOperationHandlerFactory where EntityType : IGenericEntity {

        #region Queries (Consultas)

        /// <summary>
        /// Crea un manejador para obtener una entidad específica por su identificador.
        /// Este manejador implementa la lógica de consulta para recuperar una única entidad.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        /// <returns>Instancia de <see cref="IGetEntityByID_QueryHandler{EntityType}"/> configurada para el tipo de entidad especificado.</returns>
        IGetEntityByID_QueryHandler<EntityType> Create_GetEntityByID_Handler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para obtener una lista de todas las entidades del tipo especificado.
        /// Este manejador implementa la lógica de consulta para recuperar múltiples entidades.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        /// <returns>Instancia de <see cref="IGetEntities_QueryHandler{EntityType}"/> configurada para el tipo de entidad especificado.</returns>
        IGetEntities_QueryHandler<EntityType> Create_GetEntities_Handler (IUnitOfWork unitOfWork);

        #endregion

        #region Commands (Comandos)

        /// <summary>
        /// Crea un manejador para agregar una nueva entidad al repositorio.
        /// Este manejador implementa la lógica de creación y persistencia de nuevas entidades.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        /// <returns>Instancia de <see cref="IAddEntity_CommandHandler{EntityType}"/> configurada para el tipo de entidad especificado.</returns>
        IAddEntity_CommandHandler<EntityType> Create_AddEntity_Handler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para actualizar una entidad existente en el repositorio.
        /// Este manejador implementa la lógica de actualización de entidades existentes.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        /// <returns>Instancia de <see cref="IUpdateEntity_CommandHandler{EntityType}"/> configurada para el tipo de entidad especificado.</returns>
        IUpdateEntity_CommandHandler<EntityType> Create_UpdateEntity_Handler (IUnitOfWork unitOfWork);

        /// <summary>
        /// Crea un manejador para eliminar una entidad del repositorio mediante su identificador.
        /// Este manejador implementa la lógica de eliminación de entidades.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo del servicio de persistencia de datos (IUnitOfWork : IPersistenceService).</param>
        /// <returns>Instancia de <see cref="IDeleteEntityByID_CommandHandler{EntityType}"/> configurada para el tipo de entidad especificado.</returns>
        IDeleteEntityByID_CommandHandler<EntityType> Create_DeleteEntityByID_Handler (IUnitOfWork unitOfWork);

        #endregion

    }

}