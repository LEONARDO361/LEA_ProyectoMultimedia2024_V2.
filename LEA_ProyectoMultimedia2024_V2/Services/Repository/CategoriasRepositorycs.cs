
using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LEA_ProyectoMultimedia2024_V2_.Services.Repository
{
    public class CategoriasRepositorycs:ICategorias

        
    {
        private readonly GimnasioContext _gamnasioContext;

        public CategoriasRepositorycs(GimnasioContext gamnasioContext)
        {
            _gamnasioContext = gamnasioContext;
        }

        public async Task<List<Producto>> GetCategoriasAsync()
        {
            throw new NotImplementedException();
        }

    }
}
