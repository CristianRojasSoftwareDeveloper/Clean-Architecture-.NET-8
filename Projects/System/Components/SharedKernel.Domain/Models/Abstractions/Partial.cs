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