using GestionFinanzas.Data;
using GestionFinanzas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionFinanzas.Services.Implementations
{
    public class MetaService : IMetaService
    {
        private readonly GestionFinanzasContext _context;
        public MetaService(GestionFinanzasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Meta>> GetAll(int idUsuario)
        {
            return await _context.Metas.Where(m => m.IdUsuario == idUsuario).ToListAsync();
        }

        public async Task<Meta> GetById(int idMeta, int idUsuario)
        {
            return await _context.Metas.FirstOrDefaultAsync(m => m.IdMeta == idMeta && m.IdUsuario == idUsuario);
        }

        public async Task<Meta> Insert(Meta meta)
        {
            await _context.Metas.AddAsync(meta);
            await _context.SaveChangesAsync();
            return meta;
        }

        public async Task<Meta> Update(Meta meta)
        {
            _context.Metas.Update(meta);
            await _context.SaveChangesAsync();
            return meta;
        }

        public async Task<bool> Delete(int idMeta, int idUsuario)
        {
            var meta = await _context.Metas.FirstOrDefaultAsync(m => m.IdMeta == idMeta && m.IdUsuario == idUsuario);
            if (meta == null) return false;
            meta.EstadoActivoMeta = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
