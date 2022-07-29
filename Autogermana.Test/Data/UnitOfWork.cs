using Autogermana.Test.Data.Repository;
using System;

namespace Autogermana.Test.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly ApplicationDbContext _db;
        private ICategoriaRepository _categoria;
        private IProductoRepository _producto;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }

        public ICategoriaRepository Categoria
        {
            get
            {
                if (this._categoria == null)
                {
                    this._categoria = new CategoriaRepository(_db);
                }
                return this._categoria;
            }
        }

        public IProductoRepository Producto
        {
            get
            {
                if (this._producto == null)
                {
                    this._producto = new ProductoRepository(_db);
                }
                return _producto;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
