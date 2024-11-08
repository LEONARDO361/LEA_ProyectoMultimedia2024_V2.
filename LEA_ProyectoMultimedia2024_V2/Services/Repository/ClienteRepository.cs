﻿using LEA_ProyectoMultimedia2024_V2_.Models.Contexts;
using LEA_ProyectoMultimedia2024_V2_.Models.Tables;
using LEA_ProyectoMultimedia2024_V2_.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LEA_ProyectoMultimedia2024_V2_.Services.Repository
{
    public class ClienteRepository : ICliente
    {
        private readonly GimnasioContext _context;

        public ClienteRepository(GimnasioContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAllClientesAsync()
        {
            return await _context.Cliente.ToListAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            return await _context.Cliente.FirstOrDefaultAsync(c => c.ClienteId == id);
        }

        public async Task<bool> CreateClienteAsync(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            _context.Cliente.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClienteAsync(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Cliente> GetDetails(int id)
        {
            return await _context.Cliente
           .FirstOrDefaultAsync(m => m.ClienteId == id);
        }

        public async Task<bool> ClienteExists(int id)
        {
            return await _context.Cliente.AnyAsync(c => c.ClienteId == id);
        }
    }

}
