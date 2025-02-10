using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence.Generic_Repositories;
using SharedKernel.Domain.Models.Abstractions;
using SharedKernel.Domain.Models.Entities.SystemLogs;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Repositories;

namespace SystemLogs.Infrastructure.Services.Persistence.Entity_Framework.Repositories {

    /// <summary>
    /// Repositorio especializado para operaciones de persistencia de registros de sistema utilizando Entity Framework.
    /// Extiende las capacidades genéricas de un repositorio de Entity Framework con métodos específicos de gestión de logs del sistema.
    /// </summary>
    /// <remarks>
    /// Este repositorio proporciona métodos para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) 
    /// sobre entidades de registro de sistema, permitiendo un seguimiento detallado de los eventos y actividades 
    /// realizadas en la aplicación.
    /// </remarks>
    public class SystemLog_EntityFrameworkRepository (ApplicationDbContext dbContext) : Generic_EntityFrameworkRepository<SystemLog>(dbContext), ISystemLogRepository {

        /// <summary>
        /// Agrega un nuevo registro de sistema.
        /// </summary>
        /// <param name="newSystemLog">Objeto de registro de sistema a crear en la base de datos.</param>
        /// <returns>El registro de sistema recién creado con su identificador asignado.</returns>
        public Task<SystemLog> AddSystemLog (SystemLog newSystemLog) =>
            AddEntity(newSystemLog);

        /// <summary>
        /// Recupera la lista completa de registros de sistema.
        /// </summary>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>Lista de registros de sistema almacenados en el sistema.</returns>
        public Task<List<SystemLog>> GetSystemLogs (bool enableTracking = false) =>
            GetEntities(enableTracking);

        /// <summary>
        /// Busca un registro de sistema específico por su identificador único.
        /// </summary>
        /// <param name="systemLogID">Identificador numérico del registro de sistema.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>El registro de sistema encontrado o null si no existe.</returns>
        public Task<SystemLog?> GetSystemLogByID (int systemLogID, bool enableTracking = false) =>
            GetEntityByID(systemLogID, enableTracking);

        /// <summary>
        /// Recupera todos los registros de sistema asociados a un usuario específico.
        /// </summary>
        /// <param name="userID">Identificador del usuario.</param>
        /// <param name="enableTracking">
        /// Indica si se debe habilitar el seguimiento de cambios de Entity Framework.
        /// Por defecto está deshabilitado para mejorar el rendimiento.
        /// </param>
        /// <returns>Lista de registros de sistema para el usuario especificado.</returns>
        public Task<List<SystemLog>> GetSystemLogsByUserID (int userID, bool enableTracking = false) =>
            GetQueryable(enableTracking).
            Where(systemLog => systemLog.UserID == userID).
            ToListAsync();

        /// <summary>
        /// Actualiza la información de un registro de sistema existente.
        /// </summary>
        /// <param name="systemLogUpdate">Objeto con las actualizaciones parciales del registro de sistema.</param>
        /// <returns>El registro de sistema actualizado con los cambios aplicados.</returns>
        public Task<SystemLog> UpdateSystemLog (Partial<SystemLog> systemLogUpdate) =>
            UpdateEntity(systemLogUpdate);

        /// <summary>
        /// Elimina un registro de sistema del sistema por su identificador.
        /// </summary>
        /// <param name="systemLogID">Identificador numérico del registro de sistema a eliminar.</param>
        /// <returns>El registro de sistema que ha sido eliminado.</returns>
        public Task<SystemLog> DeleteSystemLogByID (int systemLogID) =>
            DeleteEntityByID(systemLogID);

    }

}