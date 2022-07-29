using Autogermana.Test.Models;

namespace Autogermana.Test.Data.Repository
{
    public interface IProductoRepository : IRepository<Producto>
    {
        void Update(Producto producto);
    }
}
