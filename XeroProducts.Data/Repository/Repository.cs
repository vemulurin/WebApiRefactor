using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XeroProducts.Data.Context;
using XeroProducts.Data.Utils;

namespace XeroProducts.Data.Repository
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> entity;
        private readonly ProductsContext productsContext;

        public Repository(ProductsContext context)
        {
            entity = context.Set<TEntity>();
            productsContext = context;
        }

        #region [ Sync Support ]

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return entity.AsQueryable();
            }
            catch (Exception ex)
            {
                try
                {
                    productsContext.Add(Helper.GetErrorLogObject(ex));
                    productsContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Helper.LogErrorInFile(e);
                }

                return (IEnumerable<TEntity>)Task.CompletedTask;
            }
        }

        public TEntity GetByKey(Guid id)
        {

            try
            {
                return entity.Find(id);
            }
            catch (Exception ex)
            {
                try
                {
                    productsContext.Add(Helper.GetErrorLogObject(ex));
                    productsContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Helper.LogErrorInFile(e);
                }

                return null;
            }
        }

        public void Add(TEntity tEntity)
        {
            try
            {
                entity.Add(tEntity);
            }
            catch (Exception ex)
            {
                try
                {
                    productsContext.Add(Helper.GetErrorLogObject(ex));
                    productsContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Helper.LogErrorInFile(e);
                }
            }
        }

        public void Delete(TEntity tEntity)
        {
            try
            {
                entity.Remove(tEntity);
            }
            catch (Exception ex)
            {

                try
                {
                    productsContext.Add(Helper.GetErrorLogObject(ex));
                    productsContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Helper.LogErrorInFile(e);
                }
            }
        }

        public void Update(TEntity tEntity)
        {
            try
            {
                entity.Attach(tEntity);
                productsContext.Entry(tEntity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                try
                {
                    productsContext.Add(Helper.GetErrorLogObject(ex));
                    productsContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Helper.LogErrorInFile(e);
                }
            }
        }

        public void Save()
        {
            try
            {
                productsContext.SaveChanges();
            }
            catch (Exception ex)
            {

                try
                {
                    productsContext.Add(Helper.GetErrorLogObject(ex));
                    productsContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Helper.LogErrorInFile(e);
                }
            }
        }

        #endregion

        #region [ Async Support ]

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await entity.AsQueryable().ToListAsync();

            }
            catch (Exception ex)
            {
                try
                {
                    productsContext.Add(Helper.GetErrorLogObject(ex));
                    productsContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Helper.LogErrorInFile(e);
                }

                return (IEnumerable<TEntity>)Task.CompletedTask;
            }

        }

        public async Task<TEntity> GetByKeyAsync(Guid id)
        {
            try
            {
                return await entity.FindAsync(id);

            }
            catch (Exception ex)
            {

                try
                {
                    productsContext.Add(Helper.GetErrorLogObject(ex));
                    productsContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Helper.LogErrorInFile(e);
                }

                return null;
            }
        }

        public async Task AddAsync(TEntity tEntity)
        {
            try
            {
                await entity.AddAsync(tEntity);

            }
            catch (Exception ex)
            {

                try
                {
                    productsContext.Add(Helper.GetErrorLogObject(ex));
                    productsContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Helper.LogErrorInFile(e);
                }

            }
        }

        public async Task DeleteAsync(TEntity tEntity)
        {
            try
            {
                entity.Remove(tEntity);
                await SaveAsync();

            }
            catch (Exception ex)
            {

                try
                {
                    productsContext.Add(Helper.GetErrorLogObject(ex));
                    productsContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Helper.LogErrorInFile(e);
                }
            }
        }

        public async Task UpdateAsync(TEntity tEntity)
        {
            try
            {
                if (tEntity != null)
                {
                    entity.Attach(tEntity);
                    productsContext.Entry(tEntity).State = EntityState.Modified;
                }

            }
            catch (Exception ex)
            {

                try
                {
                    productsContext.Add(Helper.GetErrorLogObject(ex));
                    productsContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Helper.LogErrorInFile(e);
                }
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await productsContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                try
                {
                    productsContext.Add(Helper.GetErrorLogObject(ex));
                    productsContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Helper.LogErrorInFile(e);
                }
            }
        }

        #endregion

        #region [ Disposable Pattern ]

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    productsContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
