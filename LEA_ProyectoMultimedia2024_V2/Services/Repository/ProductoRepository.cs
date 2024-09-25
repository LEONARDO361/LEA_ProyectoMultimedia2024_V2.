using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Repository
{
    public class ProductoRepository : IProducto

    {


        private readonly GimnasioContext _gamnasioContext;

        public ProductoRepository(GimnasioContext gamnasioContext)
        {
            _gamnasioContext = gamnasioContext;

        }

        public async Task<List<Producto>>GetProductosAsync()
        {
            var GimnasioContext = _gamnasioContext.Producto.Include(p => p.Categoria).Include(p => p.Descuento).ToListAsync();
            return(await GimnasioContext);
        }
    }


}
