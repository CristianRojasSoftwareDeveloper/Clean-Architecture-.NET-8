using System.Linq.Expressions;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories {

    public interface IQueryableRepository<EntityType> {

        /// <summary>
        /// Obtiene la primera entidad que cumple con el predicado especificado.
        /// </summary>
        /// <param name="predicate">Expresión que define las condiciones que debe cumplir la entidad.</param>
        /// <param name="enableTracking">Si es true, habilita el tracking de Entity Framework para la entidad retornada.
        /// Si es false (por defecto), deshabilita el tracking para mejor rendimiento en consultas de solo lectura.</param>
        /// <returns>La primera entidad que cumple con el predicado, o null si no existe.</returns>
        Task<EntityType?> FirstOrDefault (Expression<Func<EntityType, bool>> predicate, bool enableTracking = false);

    }

}