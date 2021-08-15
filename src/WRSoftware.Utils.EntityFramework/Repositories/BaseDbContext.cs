using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using WRSoftware.Utils.EntityFrameworkCore.Constants;
using WRSoftware.Utils.EntityFrameworkCore.Exceptions;
using WRSoftware.Utils.EntityFrameworkCore.Interfaces;

namespace WRSoftware.Utils.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// The Base Context, who will hold the Context and Database connection
    /// </summary>
    public abstract class BaseDbContext : DbContext, IUnitOfWork
    {
        /// <summary>
        /// The current transaction
        /// </summary>
        private IDbContextTransaction _currentTransaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDbContext"/> class.
        /// </summary>
        protected BaseDbContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        protected BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Gets a value indicating whether this instance has active transaction.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has active transaction; otherwise, <c>false</c>.
        /// </value>
        public bool HasActiveTransaction
        {
            get { return this._currentTransaction != null; }
        }

        /// <summary>
        /// Saves the entities.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DuplicateKeyViolationException">There was a problem saving the entry caused by a duplicate key violation (PK or UNIQUE constraint).</exception>
        public bool SaveEntities()
        {
            try
            {
                this.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (this.IsDuplicateKeyViolationException(ex))
                    throw new DuplicateKeyViolationException(
                        "There was a problem saving the entry caused by a duplicate key violation (PK or UNIQUE constraint).",
                        ex.InnerException.InnerException);
                throw;
            }

            return true;
        }

        /// <summary>
        /// Saves the entities asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="DuplicateKeyViolationException">There was a problem saving the entry caused by a duplicate key violation (PK or UNIQUE constraint).</exception>
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var baseDbContext = this;
            try
            {
                await baseDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                if (baseDbContext.IsDuplicateKeyViolationException(ex))
                    throw new DuplicateKeyViolationException(
                        "There was a problem saving the entry caused by a duplicate key violation (PK or UNIQUE constraint).",
                        ex.InnerException.InnerException);
                throw;
            }

            return true;
        }

        /// <summary>
        /// Gets the current transaction.
        /// </summary>
        /// <returns></returns>
        public IDbContextTransaction GetCurrentTransaction()
        {
            return this._currentTransaction;
        }

        /// <summary>
        /// Determines whether [is duplicate key violation exception] [the specified e].
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns>
        ///   <c>true</c> if [is duplicate key violation exception] [the specified e]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsDuplicateKeyViolationException(DbUpdateException e)
        {
            try
            {
                var innerException = e.InnerException.InnerException as SqlException;

                if (innerException.Number != Configs.PRIMARY_KEY_VIOLATION_CODE && innerException.Number != Configs.UNIQUE_CONSTRAINT_VIOLATION_CODE)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// Begins the transaction asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            var baseDbContext = this;
            if (baseDbContext._currentTransaction != null)
                return (IDbContextTransaction)null;
            var contextTransaction =
                await baseDbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted,
                    new CancellationToken());
            baseDbContext._currentTransaction = contextTransaction;
            return baseDbContext._currentTransaction;
        }

        /// <summary>
        /// Commits the transaction asynchronous.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <exception cref="ArgumentNullException">transaction</exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            var baseDbContext = this;

            ValidateTransaction(transaction, baseDbContext);

            try
            {
                await baseDbContext.SaveChangesAsync(new CancellationToken());
                transaction.Commit();
            }
            catch
            {
                baseDbContext.RollbackTransaction();
                throw;
            }
            finally
            {
                if (baseDbContext._currentTransaction != null)
                {
                    baseDbContext._currentTransaction.Dispose();
                    baseDbContext._currentTransaction = (IDbContextTransaction)null;
                }
            }
        }

        private static void ValidateTransaction(IDbContextTransaction transaction, BaseDbContext baseDbContext)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));
            if (transaction != baseDbContext._currentTransaction)
                throw new InvalidOperationException(string.Format("Transaction {0} is not current",
                    (object)transaction.TransactionId));
        }

        /// <summary>
        /// Rollbacks the transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                this._currentTransaction?.Rollback();
            }
            finally
            {
                if (this._currentTransaction != null)
                {
                    this._currentTransaction.Dispose();
                    this._currentTransaction = (IDbContextTransaction)null;
                }
            }
        }
    }
}