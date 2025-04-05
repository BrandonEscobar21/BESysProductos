using BE.SysProductos.EN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.SysProductos.DAL
{
    public class ClienteDAL
    {
        readonly BESysProductosDbContext dbContext;
        public ClienteDAL(BESysProductosDbContext sysProductosDB)
        {
            dbContext = sysProductosDB;
        }

        public async Task<int> CrearAsync(Cliente pCliente)
        {
            Cliente cliente = new Cliente()
            {
                Nombre = pCliente.Nombre,
                Apellido = pCliente.Apellido,
                Telefono = pCliente.Telefono,
                Email = pCliente.Email,
                DUI = pCliente.DUI
            };
            dbContext.Clientes.Add(cliente);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> ModificarAsync(Cliente pCliente)
        {
            var cliente = await dbContext.Clientes.FirstOrDefaultAsync(s => s.Id == pCliente.Id);
            if (cliente != null && cliente.Id != 0)
            {
                cliente.Nombre = pCliente.Nombre;
                cliente.Apellido = pCliente.Apellido;
                cliente.Telefono = pCliente.Telefono;
                cliente.Email = pCliente.Email;
                cliente.DUI = pCliente.DUI;

                dbContext.Update(cliente);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<int> EliminarAsync(Cliente pCliente)
        {
            var cliente = await dbContext.Clientes.FirstOrDefaultAsync(s => s.Id == pCliente.Id);
            if (cliente != null && cliente.Id != 0)
            {
                dbContext.Clientes.Remove(cliente);
                return await dbContext.SaveChangesAsync();
            }
            else
                return 0;
        }

        public async Task<Cliente> ObtenerPorIdAsync(Cliente pCliente)
        {
            var cliente = await dbContext.Clientes.FirstOrDefaultAsync(s => s.Id == pCliente.Id);
            if (cliente != null && cliente.Id != 0)
            {
                return new Cliente
                {
                    Id = cliente.Id,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Telefono = cliente.Telefono,
                    Email = cliente.Email,
                    DUI = cliente.DUI,
                };
            }
            else
                return new Cliente();
        }

        public async Task<List<Cliente>> ObtenerTodosAsync()
        {
            var clientes = await dbContext.Clientes.ToListAsync();
            if (clientes != null && clientes.Count > 0)
            {
                var list = new List<Cliente>();
                clientes.ForEach(p => list.Add(new Cliente
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    Telefono = p.Telefono,
                    Email = p.Email,
                    DUI = p.DUI
                }));
                return list;
            }
            else
                return new List<Cliente>();
        }
    }
}
