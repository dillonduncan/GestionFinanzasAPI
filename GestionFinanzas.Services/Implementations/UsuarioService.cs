using AutoMapper;
using GestionFinanzas.Data;
using GestionFinanzas.Services.DTOs.Requests;
using GestionFinanzas.Services.DTOs.Responses;
using GestionFinanzas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionFinanzas.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly GestionFinanzasContext _context;
        private readonly IMapper _mapper;
        public UsuarioService(GestionFinanzasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioResponseDTO>> GetAll()
        {
            var usuarios = await _context.Usuarios
                .AsNoTracking()
                .Include(u => u.Transacciones)
                .Include(u => u.Meta)
                .ToListAsync();
            return _mapper.Map<IEnumerable<UsuarioResponseDTO>>(usuarios);
        }

        public async Task<UsuarioResponseDTO> GetById(int id)
        {
            var usuario = await _context.Usuarios
                .AsNoTracking()
                .Include(u => u.Transacciones)
                .Include(u => u.Meta)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
            if (usuario == null) return null;

            return _mapper.Map<UsuarioResponseDTO>(usuario);
        }

        public async Task<UsuarioResponseDTO> GetByNI(string numIdentificacion)
        {
            var usuario = await _context.Usuarios
                .AsNoTracking()
                .Include(u => u.Transacciones)
                .Include(u => u.Meta)
                .FirstOrDefaultAsync(u => u.NumeroIdentificacion == numIdentificacion);
            if (usuario == null) return null;

            return _mapper.Map<UsuarioResponseDTO>(usuario);
        }

        public async Task<bool> ExisteCorreo(string correoUsuario)
        {
            if (string.IsNullOrWhiteSpace(correoUsuario)) return false;

            var correoLimpio = correoUsuario.Trim().ToLower();

            return await _context.Usuarios
                .AsNoTracking()
                .AnyAsync(u => u.CorreoUsuario == correoLimpio);
        }

        public async Task<UsuarioResponseDTO> Insert(UsuarioCreateDTO usuario)
        {
            usuario.ContraseñaUsuario = BCrypt.Net.BCrypt.HashPassword(usuario.ContraseñaUsuario);

            var usuarioMap = _mapper.Map<Usuario>(usuario);

            usuarioMap.UsuarioRoles.Add(new Models.UsuarioRol
            {
                IdRol = 2
            });
            await _context.Usuarios.AddAsync(usuarioMap);
            await _context.SaveChangesAsync();
            return _mapper.Map<UsuarioResponseDTO>(usuarioMap);
        }

        public async Task<UsuarioResponseDTO> Update(int idUsuario, UsuarioUpdateDTO usuario)
        {
            var usuarioExiste = await _context.Usuarios.FindAsync(idUsuario);

            if (usuarioExiste == null) return null;

            if (!string.IsNullOrEmpty(usuario.ContraseñaUsuario))
            {
                usuarioExiste.ContraseñaUsuario = BCrypt.Net.BCrypt.HashPassword(usuario.ContraseñaUsuario);
            }

            _mapper.Map(usuario, usuarioExiste);
            await _context.SaveChangesAsync();
            return _mapper.Map<UsuarioResponseDTO>(usuarioExiste);
        }

        public async Task<bool> Delete(int idUsuario)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario == null) return false;
            usuario.EstadoActivoUsuario = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
