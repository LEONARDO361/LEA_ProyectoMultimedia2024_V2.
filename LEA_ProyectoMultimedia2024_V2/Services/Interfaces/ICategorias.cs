using LEA_ProyectoMultimedia2024_V2_.Models.Tables;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Interfaces
{
    public interface ICategorias
    {
        Task<List<Categoria>> GetCategoriasAsync();

        Task<Categoria> GetCategoriaByIdAsync(int id);
        Task AddCategoriaAsync(Categoria categoria);
        Task UpdateCategoriaAsync(Categoria categoria);
        Task DeleteCategoriaAsync(int id);
        Task <bool> CategoriaExists(int id);

        Task<Categoria> GetDetails(int id);

    }
}
