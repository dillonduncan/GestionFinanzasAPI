using GestionFinanzas.Data;
using GestionFinanzas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionFinanzas.Services.Implementactions
{
    public class UsuarioService : IUsuarioService
    {
        private readonly GestionFinanzasContext _context;
        public UsuarioService(GestionFinanzasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _context.Usuarios
                .Include(u => u.Transacciones)
                .Include(u => u.Meta)
                .ToListAsync();
        }

        public async Task<Usuario> GetById(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return null;

            await _context.Entry(usuario).Collection(u => u.Transacciones).LoadAsync();
            await _context.Entry(usuario).Collection(u => u.Meta).LoadAsync();

            return usuario;
        }

        public async Task<Usuario> GetByNI(string numIdentificacion)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.NumeroIdentificacion == numIdentificacion);
            if (usuario == null) return null;

            await _context.Entry(usuario).Collection(u => u.Transacciones).LoadAsync();
            await _context.Entry(usuario).Collection(u => u.Meta).LoadAsync();

            return usuario;
        }

        public async Task<Usuario> Insert(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
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
