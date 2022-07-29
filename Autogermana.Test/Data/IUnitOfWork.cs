using Autogermana.Test.Data.Repository;
using System;

namespace Autogermana.Test.Data
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoriaRepository Categoria { get; }
        IProductoRepository Producto { get; }
        void Save();
    }
}
