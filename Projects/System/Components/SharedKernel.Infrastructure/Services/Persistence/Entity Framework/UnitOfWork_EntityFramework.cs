using Microsoft.EntityFrameworkCore.Storage;
using SharedKernel.Application.Models.Abstractions.Interfaces.ApplicationManager.Services.Persistence;
using SharedKernel.Infrastructure.Services.Persistence.Entity_Framework.Contexts;

namespace SharedKernel.Infrastructure.Services.Persistence.Entity_Framework {

    /// <summary>
    /// Implementación concreta del Unit of Work que coordina las operaciones de todos los repositorios
    /// y gestiona las transacciones de manera eficiente.
    /// </summary>
    public class UnitOfWork_EntityFramework : PersistenceService, IUnitOfWork {

        // Indica si ya se han liberado los recursos.
        private bool _disposed = false;

        // Contexto de base de datos de la aplicación.
        private readonly ApplicationDbContext _applicationDbContext;

        // Transacción actual, en caso de existir.
        private IDbContextTransaction? _currentTransaction = null;

        /// <summary>
        /// Constructor que inyecta el contexto de base de datos y la colección de repositorios.
        /// </summary>
        /// <param name="applicationDbContext">Contexto de base de datos de la aplicación.</param>
        /// <param name="repositories">Colección de repositorios.</param>
        public UnitOfWork_EntityFramework (ApplicationDbContext applicationDbContext, RepositoryCollection repositories) : base(repositories) =>
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));

        /// <summary>
        /// Inicia una transacción explícita en el contexto.
        /// Si ya existe una transacción activa, lanza una excepción.
        /// </summary>
        /// <returns>
        /// La transacción iniciada, que implementa «IAsyncDisposable».
        /// </returns>
        public async Task BeginTransactionAsync () {
            // Verifica si ya hay una transacción activa.
            if (_currentTransaction != null)
                // Si ya existe una transacción activa, se lanza una excepción para evitar conflictos.
                throw new InvalidOperationException("Ya existe una transacción activa");
            // Inicia una nueva transacción de base de datos de forma asíncrona, luego la asigna a «_currentTransaction».
            _currentTransaction = await _applicationDbContext.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// Persiste los cambios en el contexto y confirma la transacción activa.
        /// Si ocurre un error, se revierte la transacción para mantener la atomicidad.
        /// </summary>
        public async Task CommitTransactionAsync () {
            try {
                // Guarda los cambios pendientes en la base de datos de manera asíncrona.
                await SaveChangesAsync();
                // Si hay una transacción activa, la confirma.
                if (_currentTransaction != null)
                    await _currentTransaction.CommitAsync();
            } catch {
                // En caso de que ocurra un error durante la confirmación, se revierte la transacción.
                await RollbackTransactionAsync();
                // Relanza la excepción para que el llamador pueda manejarla.
                throw;
            } finally {
                // Si existe una transacción activa, se asegura de liberarla para evitar fugas de recursos.
                if (_currentTransaction != null) {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null; // Se restablece la referencia a «null».
                }
            }
        }

        /// <summary>
        /// Revierte la transacción activa, deshaciendo los cambios realizados en el contexto.
        /// </summary>
        public async Task RollbackTransactionAsync () {
            try {
                // Si hay una transacción activa, se revierte para deshacer los cambios no confirmados.
                if (_currentTransaction != null)
                    await _currentTransaction.RollbackAsync();
            } finally {
                // Libera los recursos de la transacción, asegurando que no haya fugas de memoria.
                if (_currentTransaction != null) {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null; // Se restablece la referencia a «null».
                }
            }
        }

        /// <summary>
        /// Persiste los cambios realizados en el contexto sin gestionar explícitamente una transacción.
        /// </summary>
        /// <returns>
        /// El número de registros afectados.
        /// </returns>
        public async Task<int> SaveChangesAsync () => await _applicationDbContext.SaveChangesAsync();

        #region Patrones de Disposición de Recursos

        /// <summary>
        /// Libera los recursos utilizados por la instancia de «UnitOfWork_EntityFramework» de forma síncrona.
        /// </summary>
        public void Dispose () {
            // Llama al método protegido «Dispose» con «disposing» en «true»,
            // indicando que se deben liberar tanto los recursos administrados como los no administrados.
            Dispose(disposing: true);
            // Suprime la finalización de la instancia, lo que evita que el recolector de basura
            // intente llamar al destructor (finalizador) de esta clase, optimizando la gestión de memoria.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Método protegido que libera los recursos de forma síncrona.
        /// Se invoca desde el método público «Dispose».
        /// </summary>
        /// <param name="disposing">
        /// Si es «true», se liberan recursos administrados; de lo contrario, solo se liberan recursos no administrados.
        /// </param>
        protected virtual void Dispose (bool disposing) {
            // Verifica si la instancia ya ha sido dispuesta previamente.
            // Si es así, sale del método para evitar liberaciones múltiples.
            if (_disposed)
                return;
            // Si «disposing» es «true», significa que estamos liberando recursos administrados.
            if (disposing) {
                // Libera de forma síncrona la transacción activa, si existe.
                _currentTransaction?.Dispose();
                _currentTransaction = null;
                // Libera de forma síncrona el contexto de base de datos, que es un recurso administrado.
                _applicationDbContext.Dispose();
            }
            // * Aquí se liberarían recursos no administrados si existieran (por ejemplo: manejadores de archivos o sockets).
            // Marca la instancia como dispuesta para evitar liberar recursos nuevamente.
            _disposed = true;
        }

        /// <summary>
        /// Libera los recursos utilizados por la instancia de «UnitOfWork_EntityFramework» de forma asíncrona.
        /// </summary>
        /// <returns>
        /// Un «ValueTask» que representa la operación asíncrona de liberación de recursos.
        /// </returns>
        public async ValueTask DisposeAsync () {
            // Si la instancia ya ha sido dispuesta, no hace nada y retorna inmediatamente.
            if (_disposed)
                return;
            // Llama a la versión asíncrona de «Dispose» para liberar los recursos de forma eficiente.
            await DisposeAsyncCore();
            // Se marca como ya dispuesta para evitar liberaciones múltiples.
            _disposed = true;
            // Suprime la finalización para evitar que el recolector de basura intente liberar la instancia nuevamente.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Método protegido que realiza la liberación asíncrona de recursos administrados.
        /// Se separa la lógica asíncrona para aprovechar «IAsyncDisposable» en los recursos, si está disponible.
        /// </summary>
        /// <returns>
        /// Un «ValueTask» que representa la operación asíncrona de liberación.
        /// </returns>
        protected virtual async ValueTask DisposeAsyncCore () {
            // Verifica si hay una transacción activa antes de intentar disponerla.
            if (_currentTransaction != null) {
                // Libera la transacción de forma asíncrona.
                await _currentTransaction.DisposeAsync();
                // Se asigna «null» para indicar que la transacción ha sido eliminada.
                _currentTransaction = null;
            }
            // Libera el contexto de base de datos de forma asíncrona.
            await _applicationDbContext.DisposeAsync();
        }

        #endregion

    }

}