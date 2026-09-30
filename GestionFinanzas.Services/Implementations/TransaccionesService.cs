using AutoMapper;
using GestionFinanzas.Data;
using GestionFinanzas.Services.DTOs.Requests;
using GestionFinanzas.Services.DTOs.Responses;
using GestionFinanzas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionFinanzas.Services.Implementations
{
    public class TransaccionesService : ITransaccionesService
    {
        private readonly GestionFinanzasContext _context;
        private readonly IMapper _mapper;
        public TransaccionesService(GestionFinanzasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransaccionResponseDTO>> GetAll(int idUsuario)
        {
            var transacciones = await _context.Transacciones
                .AsNoTracking()
                .Include(t => t.IdCategoriaNavigation)
                .Where(t => t.IdUsuario == idUsuario)
                .ToListAsync();
            return _mapper.Map<IEnumerable<TransaccionResponseDTO>>(transacciones);
        }

        public async Task<IEnumerable<TransaccionResponseDTO>> GetForCategoria(int idUsuario, int idCategoria)
        {
            var transacciones = await _context.Transacciones
                .AsNoTracking()
                .Include(t => t.IdCategoriaNavigation)
                .Where(t => t.IdUsuario == idUsuario && t.IdCategoria == idCategoria)
                .ToListAsync();
            return _mapper.Map<IEnumerable<TransaccionResponseDTO>>(transacciones);
        }

        public async Task<TransaccionResponseDTO> GetById(int id, int idUsuario)
        {
            var transaccion = await _context.Transacciones
                .AsNoTracking()
                .Include(t => t.IdCategoriaNavigation)
                .FirstOrDefaultAsync(t => t.IdTransaccion == id && t.IdUsuario == idUsuario);
            return _mapper.Map<TransaccionResponseDTO>(transaccion);
        }

        public async Task<TransaccionResponseDTO> Insert(TransaccionCreateDTO transaccion)
        {
            var transaccionMap = _mapper.Map<Transaccion>(transaccion);

            await _context.Transacciones.AddAsync(transaccionMap);
            await _context.SaveChangesAsync();
            return _mapper.Map<TransaccionResponseDTO>(transaccionMap);
        }

        public async Task<TransaccionResponseDTO> Update(int idTransaccion, TransaccionUpdateDTO transaccion)
        {
            var transaccionExiste = await _context.Transacciones
                .FirstOrDefaultAsync(t => t.IdTransaccion == idTransaccion && t.IdUsuario == transaccion.IdUsuario);

            if (transaccionExiste == null) return null;
            _mapper.Map(transaccion, transaccionExiste);
            await _context.SaveChangesAsync();
            return _mapper.Map<TransaccionResponseDTO>(transaccionExiste);
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
