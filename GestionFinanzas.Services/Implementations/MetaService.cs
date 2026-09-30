using AutoMapper;
using GestionFinanzas.Data;
using GestionFinanzas.Services.DTOs.Requests;
using GestionFinanzas.Services.DTOs.Responses;
using GestionFinanzas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionFinanzas.Services.Implementations
{
    public class MetaService : IMetaService
    {
        private readonly GestionFinanzasContext _context;
        private readonly IMapper _mapper;
        public MetaService(GestionFinanzasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MetaResponseDTO>> GetAll(int idUsuario)
        {
            var metas = await _context.Metas
                .AsNoTracking()
                .Where(m => m.IdUsuario == idUsuario)
                .ToListAsync();
            return _mapper.Map<IEnumerable<MetaResponseDTO>>(metas);
        }

        public async Task<MetaResponseDTO> GetById(int idMeta, int idUsuario)
        {
            var meta = await _context.Metas
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdMeta == idMeta && m.IdUsuario == idUsuario);
            if (meta == null) return null;
            return _mapper.Map<MetaResponseDTO>(meta);
        }

        public async Task<MetaResponseDTO> Insert(MetaCreateDTO meta)
        {
            var metaMap = _mapper.Map<Meta>(meta);

            await _context.Metas.AddAsync(metaMap);
            await _context.SaveChangesAsync();

            return _mapper.Map<MetaResponseDTO>(metaMap);
        }

        public async Task<MetaResponseDTO> Update(int idMeta, MetaUpdateDTO meta)
        {
            var metaExiste = await _context.Metas
                .FirstOrDefaultAsync(m => m.IdMeta == idMeta && m.IdUsuario == meta.IdUsuario);

            if (metaExiste == null) return null;
            _mapper.Map(meta, metaExiste);

            await _context.SaveChangesAsync();
            return _mapper.Map<MetaResponseDTO>(metaExiste);
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
