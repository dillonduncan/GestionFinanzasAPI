using GestionFinanzas.Data;
using GestionFinanzas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionFinanzas.Services.Implementations
{
    public class CategoriaService : ICategoriaService
    {
        private readonly GestionFinanzasContext _context;
        public CategoriaService(GestionFinanzasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> GetAll(int idUsuario)
        {
            return await _context.Categorias.Where(c => c.IdUsuario == idUsuario || c.IdUsuario == null).ToListAsync();
        }

        public async Task<Categoria> Insert(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> Update(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<bool> Delete(int idUsuario, int idCategoria)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == idCategoria && c.IdUsuario == idUsuario);
            if (categoria == null)
                return false;

            categoria.EstadoActivoCategoria = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
