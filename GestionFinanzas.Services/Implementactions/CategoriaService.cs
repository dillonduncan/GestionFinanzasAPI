using GestionFinanzas.Data;
using GestionFinanzas.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GestionFinanzas.Services.Implementactions
{
    public class CategoriaService : ICategoriaService
    {
        private readonly GestionFinanzasContext _context;
        public CategoriaService(GestionFinanzasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria> Insert(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }
    }
}
