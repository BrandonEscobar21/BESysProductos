using BE.SysProductos.EN;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.SysProductos.DAL
{
    public class VentaDAL
    {
        readonly BESysProductosDbContext dbContext;

        public VentaDAL(BESysProductosDbContext sysProductosDB)
        {
            dbContext = sysProductosDB;
        }

        public async Task<int> CrearAsync(Venta pVenta)
        {
            // Agregar la venta con sus detalles
            dbContext.Ventas.Add(pVenta);
            int result = await dbContext.SaveChangesAsync();
            if (result > 0)
            {
                // Actualizar stock de peoductos
                foreach (var detalle in pVenta.DetalleVentas)
                {
                    var producto = await dbContext.Productos.FirstOrDefaultAsync(p => p.Id == detalle.IdProducto);
                    if (producto != null)
                    {
                        producto.CantidadDisponible -= detalle.Cantidad;
                    }
                }
            }
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> AnularAsync(int idVenta)
        {
            var venta = await dbContext.Ventas
                .Include(c => c.DetalleVentas)
                .FirstOrDefaultAsync(c => c.Id == idVenta);

            if (venta != null && venta.Estado != (byte)Venta.EnumEstadoVenta.Anulada)
            {
                // Marcar la venta como anulada
                venta.Estado = (byte)Venta.EnumEstadoVenta.Anulada;

                //Sumar la cantidad de los productos vendidos
                foreach (var detalle in venta.DetalleVentas)
                {
                    var producto = await dbContext.Productos.FirstOrDefaultAsync(p => p.Id == detalle.IdProducto);
                    if (producto != null)
                    {
                        producto.CantidadDisponible += detalle.Cantidad;
                    }
                }

                return await dbContext.SaveChangesAsync();
            }

            return 0; // si ya estaba anulada, no hacer nada
        }

        public async Task<Venta> ObtenerPorIdAsync(int idVenta)
        {
            var venta = await dbContext.Ventas
                .Include(c => c.DetalleVentas).Include(c => c.Cliente)
                .FirstOrDefaultAsync(c => c.Id == idVenta);

            return venta ?? new Venta();
        }

        public async Task<List<Venta>> ObtenerTodosAsync()
        {
            var ventas = await dbContext.Ventas
                .Include(c => c.DetalleVentas)
                .Include(c => c.Cliente).ToListAsync();
            return ventas ?? new List<Venta>();
        }

        public async Task<List<Venta>> ObtenerPorEstadoAsync(byte estado)
        {
            var ventasQuery = dbContext.Ventas.AsQueryable();

            if (estado != 0)
            {
                ventasQuery = ventasQuery.Where(c => c.Estado == estado);
            }

            ventasQuery = ventasQuery
                .Include(c => c.DetalleVentas)
                .Include(c => c.Cliente);

            var ventas = await ventasQuery.ToListAsync();

            return ventas ?? new List<Venta>();
        }
    }
}
