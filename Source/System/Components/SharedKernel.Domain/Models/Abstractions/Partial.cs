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

using SharedKernel.Domain.Models.Abstractions.Interfaces;
using SharedKernel.Domain.Utils.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace SharedKernel.Domain.Models.Abstractions {

    /// <summary>
    /// Representa un conjunto parcial de propiedades de una entidad específica, lo que permite
    /// trabajar con actualizaciones o modificaciones controladas.
    /// </summary>
    /// <typeparam name="EntityType">El tipo de la entidad que implementa <see cref="IGenericEntity"/>.</typeparam>
    public class Partial<EntityType> : GenericEntity where EntityType : IGenericEntity {

        public Dictionary<string, object?> Properties { get; }

        private Dictionary<string, PropertyInfo> _properties { get; }

        /// <summary>
        /// Inicializa un diccionario con las propiedades especificadas mediante expresiones lambda.
        /// </summary>
        /// <param name="source">La instancia de la entidad que contiene los valores actuales.</param>
        /// <param name="propertyExpressions">
        /// Una colección de expresiones que representan las propiedades que se incluirán en el diccionario.
        /// Las expresiones deben ser del tipo «entity => entity.PropertyName».
        /// </param>
        /// <exception cref="ArgumentNullException">Si «source» es nulo.</exception>
        /// <exception cref="ArgumentException">Si no se proporcionan expresiones de propiedades.</exception>
        public Partial (EntityType source, params Expression<Func<EntityType, object?>>[] propertyExpressions) : base(source.ID) {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (propertyExpressions == null || propertyExpressions.Length == 0)
                throw new ArgumentException("Debe proporcionar al menos una propiedad.", nameof(propertyExpressions));
            _properties = new Dictionary<string, PropertyInfo>(propertyExpressions.Length);
            Properties = new Dictionary<string, object?>(propertyExpressions.Length);
            // Precompila las expresiones lambda una sola vez para mejorar el rendimiento.
            var compiledExpressions = propertyExpressions.Select(expression => new {
                PropertyInfo = expression.GetPropertyInfo(),
                Compiled = expression.Compile()
            });
            // Itera sobre las expresiones precompiladas y asigna los valores correspondientes.
            foreach (var compiledExpression in compiledExpressions) {
                var propertyName = compiledExpression.PropertyInfo.Name;
                _properties[propertyName] = compiledExpression.PropertyInfo;
                var propertyValue = compiledExpression.Compiled.Invoke(source);
                Properties[propertyName] = propertyValue;
            }
        }

        public PropertyInfo GetPropertyInfoByName (string propertyName) => _properties[propertyName];

    }

}