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