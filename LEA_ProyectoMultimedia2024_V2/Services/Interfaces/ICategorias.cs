using LEA_ProyectoMultimedia2024_V2_.Models.Tables;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Interfaces
{
    public interface ICategorias
    {
        Task<List<Producto>> GetCategoriasAsync();
    }
}
