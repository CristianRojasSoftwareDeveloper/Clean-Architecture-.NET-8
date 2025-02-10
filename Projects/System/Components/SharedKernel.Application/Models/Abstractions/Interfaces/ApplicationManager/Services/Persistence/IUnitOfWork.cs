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