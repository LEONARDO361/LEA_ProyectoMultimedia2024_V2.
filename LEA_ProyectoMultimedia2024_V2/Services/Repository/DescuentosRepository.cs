using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Repository;

public class DescuentosRepository: IDescuento
{
    private readonly GimnasioContext _context;

    public DescuentosRepository(GimnasioContext context)
    {
        _context = context;
    }

    public async Task GetDescuentoAsync()
    {
        var descuentos = await _context.Descuento.Include(d => d.Producto).ToListAsync();

        // Solo para depuración: Recorrer los descuentos y productos
        foreach (var descuento in descuentos)
        {
            Console.WriteLine($"Descuento ID: {descuento}, Producto: {descuento.Producto}");
        }
    }

    Task<List<Producto>> IDescuento.GetDescuentoAsync()
    {
        throw new NotImplementedException();
    }
}
