using System.Linq.Expressions;
using System.Reflection;

namespace SharedKernel.Domain.Utils.Extensions {

    public static class ExpressionExtensions {

        /// <summary>
        /// Obtiene el <see cref="PropertyInfo"/> de una expresión lambda que define una propiedad.
        /// </summary>
        /// <typeparam name="GenericType">El tipo (genérico) de la clase que contiene la propiedad.</typeparam>
        /// <param name="propertyExpression">La expresión lambda que define la propiedad a analizar.</param>
        /// <returns>El objeto <see cref="PropertyInfo"/> correspondiente a la propiedad especificada.</returns>
        /// <exception cref="ArgumentException">Se lanza si la expresión no corresponde a una propiedad válida.</exception>
        public static PropertyInfo GetPropertyInfo<GenericType> (this Expression<Func<GenericType, object?>> propertyExpression) {
            // Verifica si el cuerpo de la expresión es una referencia directa a un miembro (ejemplo: x => x.Property).
            if (propertyExpression.Body is MemberExpression member)
                // Retorna el miembro como un PropertyInfo si es válido.
                return member.Member as PropertyInfo
                    ?? throw new ArgumentException("La expresión no hace referencia a una propiedad válida.", nameof(propertyExpression));
            // Verifica si el cuerpo de la expresión es una conversión explícita (ejemplo: x => (object)x.Property).
            if (propertyExpression.Body is UnaryExpression unary && unary.Operand is MemberExpression memberExpr)
                // Retorna el miembro convertido como un PropertyInfo si es válido.
                return memberExpr.Member as PropertyInfo
                    ?? throw new ArgumentException("La expresión no hace referencia a una propiedad válida.", nameof(propertyExpression));
            // Si no cumple ninguna de las condiciones anteriores, lanza una excepción indicando que la expresión es inválida.
            throw new ArgumentException("La expresión no es válida. Asegúrese de que apunta a una propiedad.", nameof(propertyExpression));
        }

    }

}