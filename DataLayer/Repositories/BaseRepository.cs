using Core;
using Core.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataLayer
{
    /// <summary>
    /// <inheritdoc cref="TRepository"/>
    /// </summary>
    /// <typeparam name="TIdentity"><see cref="TIdentity"/></typeparam>
    /// <typeparam name="TEntity"><see cref="TEntity"/></typeparam>
    /// <typeparam name="TRepository"><see cref="TRepository"/></typeparam>
    public abstract class BaseRepository<TIdentity, TEntity, TRepository> : IBaseRepository<TIdentity, TEntity, TRepository>
        where TEntity : class
        where TRepository : BaseRepository<TIdentity, TEntity, TRepository>
    {
        protected readonly DbContext Context;
        protected IQueryable<TEntity> Query;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{TIdentity, TEntity, TRepository}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected BaseRepository(DbContext context)
        {
            Context = context;
            Query = context.Set<TEntity>();
        }


        /// <summary>
        /// Get the record that has a matching Key.
        /// </summary>
        /// <param name="id">The key to search for.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /></param>
        /// <returns>
        /// <see cref="!:TEntity" />
        /// </returns>
        /// <exception cref="NotFoundException{TIdentity}"></exception>
        public virtual async Task<TEntity> ByIdAsync(TIdentity id, CancellationToken cancellationToken = default)
        {
            var result = await Context.FindAsync<TEntity>(new object[] { id }, cancellationToken);
            if (result == null) throw new NotFoundException<TIdentity>(typeof(TEntity).Name, id);
            return result;
        }

        /// <summary>
        /// Converts to singleasync.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="!:TEntity" />
        /// </returns>
        public async Task<TEntity> ToSingleAsync(CancellationToken cancellationToken = default)
        {
            return await Query.SingleOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Converts to listasync.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="!:TEntity" />
        /// </returns>
        public async Task<IEnumerable<TEntity>> ToListAsync(CancellationToken cancellationToken = default)
        {
            return await
                Query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Make the query Non Tracking
        /// </summary>
        /// <returns>
        /// <see cref="T:Core.Abstractions.Repositories.IBaseRepository`3" />
        /// </returns>
        public TRepository NoTrack()
        {
            Query.AsNoTracking();
            return this as TRepository;
        }

        /// <summary>
        /// Create a new record of type <see cref="!:TEntity" />
        /// </summary>
        /// <param name="record"></param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /></param>
        /// <returns>
        /// <see cref="!:TEntity" />
        /// </returns>
        public virtual async Task<TEntity> CreateRecordAsync(TEntity record, CancellationToken cancellationToken = default)
        {
            var result = await Context.AddAsync(record, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
            return result.Entity;

        }

        /// <summary>
        /// Update the values for a record of type <see cref="!:TEntity" />
        /// </summary>
        /// <param name="record">Object with updated values.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /></param>
        /// <returns>
        /// Number of records changed.
        /// </returns>
        public virtual async Task<int> UpdateRecordAsync(TEntity record, CancellationToken cancellationToken = default)
        {
            var result = Context.Attach(record);
            result.State = EntityState.Modified;
            return await Context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Delete record of type <see cref="!:TEntity" />
        /// </summary>
        /// <param name="id">Id for record to delete.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /></param>
        /// <returns>
        /// Count of records to delete.
        /// </returns>
        /// <exception cref="NotFoundException{TIdentity}"></exception>
        public virtual async Task<int> DeleteRecordAsync(TIdentity id, CancellationToken cancellationToken = default)
        {
            var item = await Context.FindAsync<TEntity>(id);
            if (item == null) throw new NotFoundException<TIdentity>(typeof(TEntity).Name, id);
            Context.Remove(item);
            return await Context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Patch a record in the database of type <see cref="!:TEntity" />
        /// </summary>
        /// <param name="id">Id for the record to patch.</param>
        /// <param name="data">json data for patching</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /></param>
        /// <returns>
        /// Count of records patched
        /// </returns>
        /// <exception cref="NotFoundException{TIdentity}"></exception>
        public virtual async Task<int> PatchRecordAsync(TIdentity id, string data, CancellationToken cancellationToken = default)
        {
            var item = await Context.FindAsync<TEntity>(id);
            if (item == null) throw new NotFoundException<TIdentity>(typeof(TEntity).Name, id);
            JsonConvert.PopulateObject(data, item);
            Context.Entry(item).State = EntityState.Modified;
            return await Context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Bulk create records of type <see cref="!:TEntity" />
        /// </summary>
        /// <param name="records">Records to Add</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /></param>
        /// <returns>
        /// Records added with IDs assigned
        /// </returns>
        public virtual async Task<IEnumerable<TEntity>> CreateBulkAsync(IEnumerable<TEntity> records, CancellationToken cancellationToken = default)
        {
            var bulkAsync = records.ToList();
            foreach (var record in bulkAsync)
            {
                await Context.AddAsync(record, cancellationToken);
            }

            await Context.SaveChangesAsync(cancellationToken);
            return bulkAsync;
        }

        /// <summary>
        /// Bulk Delete records of type <see cref="!:TEntity" />
        /// </summary>
        /// <param name="ids">IDs of records to delete.</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /></param>
        /// <returns>
        /// Count of Deleted Records
        /// </returns>
        /// <exception cref="NotFoundException{TIdentity}"></exception>
        public virtual async Task<int> DeleteBulkAsync(IEnumerable<TIdentity> ids, CancellationToken cancellationToken = default)
        {
            foreach (var id in ids)
            {
                var item = await Context.FindAsync<TEntity>(id);
                if (item == null) throw new NotFoundException<TIdentity>(typeof(TEntity).Name, id);
                Context.Remove(item);
            }

            return await Context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Checks for the existence of a record based on it's ID.
        /// </summary>
        /// <param name="id">ID for checking record</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /></param>
        /// <returns>
        /// true or false
        /// </returns>
        public virtual async Task<bool> ExistsAsync(TIdentity id, CancellationToken cancellationToken = default)
        {
            return (await Context.FindAsync<TEntity>(id)) != null;
        }

        /// <summary>
        /// Used to reset the Query for QueryBuilder.
        /// </summary>
        /// <returns>
        /// <see cref="!:TRepository" />
        /// </returns>
        public TRepository Reset()
        {
            Query = Context.Set<TEntity>().AsQueryable();
            return this as TRepository;
        }


        /// <summary>
        /// Used to return First or Default for the current query;
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// <see cref="!:TEntity" />
        /// </returns>
        public virtual async Task<TEntity> SelectFirstOrDefaultAsync(CancellationToken cancellationToken = default)
        {
            return await Query.FirstOrDefaultAsync(cancellationToken);
        }
    }


}
