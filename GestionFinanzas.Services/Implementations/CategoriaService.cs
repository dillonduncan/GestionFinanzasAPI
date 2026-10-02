using AutoMapper;
using GestionFinanzas.Data;
using GestionFinanzas.Services.DTOs.Requests;
using GestionFinanzas.Services.DTOs.Responses;
using GestionFinanzas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionFinanzas.Services.Implementations
{
    public class CategoriaService : ICategoriaService
    {
        private readonly GestionFinanzasContext _context;
        private readonly IMapper _mapper;
        public CategoriaService(GestionFinanzasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaResponseDTO>> GetAll(int idUsuario)
        {
            var categorias = await _context.Categorias
                .AsNoTracking()
                .Where(c => c.IdUsuario == idUsuario || c.IdUsuario == null).ToListAsync();
            return _mapper.Map<IEnumerable<CategoriaResponseDTO>>(categorias);
        }

        public async Task<CategoriaResponseDTO> GetById(int idUsuario, int idCategoria)
        {
            var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(c =>
            c.IdCategoria == idCategoria &&
            (c.IdUsuario == idUsuario || c.IdUsuario == null));

            return _mapper.Map<CategoriaResponseDTO>(categoria);
        }

        public async Task<CategoriaResponseDTO> Insert(int idUsuario, CategoriaCreateDTO categoria)
        {
            var categoriaMap = _mapper.Map<Categoria>(categoria);
            categoriaMap.IdUsuario = idUsuario;

            await _context.Categorias.AddAsync(categoriaMap);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoriaResponseDTO>(categoriaMap);
        }

        public async Task<CategoriaResponseDTO> Update(int idCategoria, int idUsuario, CategoriaUpdateDTO categoria)
        {
            var categoriaExiste = await _context.Categorias.FirstOrDefaultAsync(c => c.IdCategoria == idCategoria && c.IdUsuario == idUsuario);

            if (categoriaExiste == null) return null;

            _mapper.Map(categoria, categoriaExiste);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoriaResponseDTO>(categoriaExiste);
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
