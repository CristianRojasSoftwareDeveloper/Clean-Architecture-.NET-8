using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.SystemLogs;

namespace SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories {

    /// <summary>
    /// Define las operaciones específicas del repositorio de logs del sistema.
    /// </summary>
    public interface ISystemLogRepository : IQueryableRepository<SystemLog> {

        /// <summary>
        /// Agrega un nuevo log del sistema al repositorio de forma asíncrona.
        /// </summary>
        /// <param name="newSystemLog">El log del sistema a agregar.</param>
        /// <returns>Una tarea que representa la operación asincrónica, con el log del sistema agregado como resultado.</returns>
        Task<SystemLog> AddSystemLog (SystemLog newSystemLog);

        /// <summary>
        /// Obtiene todos los logs del sistema del repositorio de forma asíncrona.
        /// </summary>
        /// <returns>Una tarea que representa la operación asincrónica, con una colección de logs del sistema como resultado.</returns>
        Task<List<SystemLog>> GetSystemLogs (bool enableTracking = false);

        /// <summary>
        /// Obtiene un log del sistema por su ID de forma asíncrona.
        /// </summary>
        /// <param name="systemLogID">El ID del log del sistema.</param>
        /// <returns>Una tarea que representa la operación asincrónica, con el log del sistema encontrado o null si no existe como resultado.</returns>
        Task<SystemLog?> GetSystemLogByID (int systemLogID, bool enableTracking = false);

        /// <summary>
        /// Obtiene todos los logs del sistema asociados a un determinado usuario según su ID.
        /// </summary>
        /// <param name="userID">El ID del usuario cuyos logs asociados se buscarán.</param>
        /// <returns>Una tarea que representa la operación asincrónica, con una colección de logs asociados como resultado.</returns>
        Task<List<SystemLog>> GetSystemLogsByUserID (int userID, bool enableTracking = false);

        /// <summary>
        /// Actualiza un registro del sistema existente de forma asíncrona.
        /// </summary>
        /// <param name="systemLogUpdate">Actualización del registro del sistema.</param>
        /// <returns>Una tarea que representa la operación asincrónica, con el log actualizado como resultado.</returns>
        Task<SystemLog> UpdateSystemLog (Partial<SystemLog> systemLogUpdate);

        /// <summary>
        /// Elimina un log del sistema por su ID de forma asíncrona.
        /// </summary>
        /// <param name="systemLogID">El ID del log del sistema a eliminar.</param>
        /// <returns>Una tarea que representa la operación asincrónica, con un valor booleano que indica si la eliminación fue exitosa.</returns>
        Task<SystemLog> DeleteSystemLogByID (int systemLogID);

    }

}