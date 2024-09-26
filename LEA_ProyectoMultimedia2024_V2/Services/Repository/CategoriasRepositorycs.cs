
using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace LEA_ProyectoMultimedia2024_V2_.Services.Repository
{
    public class CategoriasRepository : ICategorias
    {
        private readonly GimnasioContext _context;

        public CategoriasRepository(GimnasioContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> GetCategoriasAsync()
        {
            var a = await _context.Categoria.ToListAsync();
            return a;
        }

        public async Task<Categoria> GetCategoriaByIdAsync(int id)
        {
            return await _context.Categoria.FindAsync(id);
        }

        public async Task AddCategoriaAsync(Categoria categoria)
        {
            _context.Categoria.Add(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoriaAsync(Categoria categoria)
        {
            _context.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoriaAsync(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria != null)
            {
                _context.Categoria.Remove(categoria);
                await _context.SaveChangesAsync();
            }
        }
        public async Task <Categoria> GetDetails(int id)
        {
                 return await _context.Categoria
                .FirstOrDefaultAsync(m => m.CategoriaId == id);
        }

        public async Task <bool> CategoriaExists(int id)
        {
            return await _context.Categoria.AnyAsync(e => e.CategoriaId == id);
        }
    }

}
