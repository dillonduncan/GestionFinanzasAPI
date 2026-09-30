using GestionFinanzas.Data;
using GestionFinanzas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionFinanzas.Services.Implementations
{
    public class TransaccionesService : ITransaccionesService
    {
        private readonly GestionFinanzasContext _context;
        public TransaccionesService(GestionFinanzasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaccion>> GetAll(int idUsuario)
        {
            return await _context.Transacciones
                .AsNoTracking()
                .Where(t => t.IdUsuario == idUsuario && t.EstadoActivoTransaccion)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaccion>> GetForCategoria(int idUsuario, int idCategoria)
        {
            return await _context.Transacciones
                .AsNoTracking()
                .Where(t => t.IdUsuario == idUsuario && t.IdCategoria == idCategoria)
                .ToListAsync();
        }

        public async Task<Transaccion> GetById(int id, int idUsuario)
        {
            return await _context.Transacciones
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.IdTransaccion == id && t.IdUsuario == idUsuario);
        }

        public async Task<Transaccion> Insert(Transaccion transaccion)
        {
            await _context.Transacciones.AddAsync(transaccion);
            await _context.SaveChangesAsync();
            return transaccion;
        }

        public async Task<Transaccion> Update(Transaccion transaccione)
        {
            _context.Transacciones.Update(transaccione);
            await _context.SaveChangesAsync();
            return transaccione;
        }

        public async Task<bool> Delete(int idTransaccion, int idUsuario)
        {
            var transaccion = await _context.Transacciones.FirstOrDefaultAsync(t => t.IdTransaccion == idTransaccion && t.IdUsuario == idUsuario);
            if (transaccion == null) return false;
            transaccion.EstadoActivoTransaccion = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
