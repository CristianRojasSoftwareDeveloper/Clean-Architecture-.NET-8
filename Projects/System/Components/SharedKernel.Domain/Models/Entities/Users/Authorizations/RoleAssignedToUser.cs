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

using SharedKernel.Domain.Models.Abstractions;

namespace SharedKernel.Domain.Models.Entities.Users.Authorizations {

    /// <summary>
    /// Representa la relación entre un usuario y un rol en el sistema.
    /// Esta clase actúa como una tabla intermedia en una relación de muchos a muchos entre las entidades «User» y «Role».
    /// </summary>
    public class RoleAssignedToUser : GenericEntity {

        /// <summary>
        /// Identificador único del usuario asociado a esta relación.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Instancia del usuario relacionado.
        /// Esta propiedad es utilizada internamente por Entity Framework para las operaciones de navegación y mapeo.
        /// Para establecer esta relación, utilice la propiedad «UserID» en su lugar.
        /// </summary>
        public User? User { get; private set; }

        /// <summary>
        /// Identificador único del rol asociado a esta relación.
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// Instancia del rol relacionado.
        /// Esta propiedad es utilizada internamente por Entity Framework para las operaciones de navegación y mapeo.
        /// Para establecer esta relación, utilice la propiedad «RoleID» en su lugar.
        /// </summary>
        public Role? Role { get; private set; }

        /// <summary>
        /// Devuelve un objeto parcial de tipo «RoleAssignedToUser» que incluye un conjunto controlado
        /// de propiedades seleccionadas para modificaciones seguras.
        /// </summary>
        /// <remarks>
        /// En este caso, la colección de expresiones está vacía porque, por diseño, no está permitido modificar las claves foráneas
        /// («UserID» y «RoleID») directamente en esta tabla intermedia. Esto asegura que las relaciones solo se modifiquen 
        /// mediante la creación o eliminación de registros en la tabla intermedia.
        /// 
        /// Nota: Si esta tabla intermedia incluyera propiedades adicionales (por ejemplo, una «Fecha de asignación» o un «Estado»), 
        /// dichas propiedades podrían ser incluidas en la colección de expresiones del método «AsPartial».
        /// </remarks>
        /// <returns>Un objeto parcial de tipo «Partial<RoleAssignedToUser>».</returns>
        public Partial<RoleAssignedToUser> AsPartial () => new(this, [ /* Vacío por diseño */ ]);

    }

}