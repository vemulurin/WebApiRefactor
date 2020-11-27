using XeroProducts.Data.Repository;
using XeroProducts.Data.Context;
using XeroProducts.Data.Models;
using XeroProducts.Data.Utils;
using System;

namespace XeroProducts.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ProductsContext productsContext;

        public UnitOfWork(ProductsContext context) => productsContext = context;

        private IRepository<Products> productsRepository;
        private IRepository<ProductOptions> productOptionsRepository;
        private IRepository<ErrorDetails> errorDetailsRepository;
        private IRepository<Product> productRepository;
        public IRepository<Products> ProductsRepository => productsRepository ??= new Repository<Products>(productsContext);
        public IRepository<Product> ProductRepository => productRepository ??= new Repository<Product>(productsContext);
        public IRepository<ProductOptions> ProductOptionsRepository => productOptionsRepository ??= new Repository<ProductOptions>(productsContext);
        public IRepository<ErrorDetails> ErrorDetailsRepository => errorDetailsRepository ??= new Repository<ErrorDetails>(productsContext);

        /// <summary>
        /// Method to support the commit of all the transaction hold by the repositories.
        /// </summary>
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
                    ErrorDetailsRepository.Add(Helper.GetErrorLogObject(ex));
                    productsContext.SaveChanges();
                }
                catch (Exception e)
                {
                    Helper.LogErrorInFile(e);
                }

                throw new Exception($"{ex.Message} {Environment.NewLine} Exception trace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Method to refresh all the repositories.
        /// </summary>
        public void Refresh()
        {
            productsContext = new ProductsContext(null);
            productsRepository = null;
            productOptionsRepository = null;
            errorDetailsRepository = null;
        }

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
