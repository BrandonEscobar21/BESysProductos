using BE.SysProductos.BL;
using BE.SysProductos.DAL;
using BE.SysProductos.EN;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static BE.SysProductos.EN.Compra;
using static BE.SysProductos.EN.Venta;

namespace BE.SysProductos.WebApp.Controllers
{
    public class VentaController : Controller
    {
        readonly ClienteBL clienteBL;
        readonly VentaBL ventaBL;
        readonly ProductoBL productoBL;

        public VentaController(ClienteBL pClienteBL, VentaBL pVentaBL, ProductoBL pProductoBL)
        {
            clienteBL = pClienteBL;
            ventaBL = pVentaBL;
            productoBL = pProductoBL;
        }
        // GET: VentaController
        public async Task<IActionResult> Index(byte? estado)
        {
            var ventas = await ventaBL.ObtenerPorEstadoAsync(estado ?? 0);

            var estados = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "Todos"},
                new SelectListItem { Value = "1", Text = "Activa"},
                new SelectListItem { Value = "2", Text = "Anulada"}
            };

            ViewBag.Estados = new SelectList(estados, "Value", "Text", estado?.ToString());

            return View(ventas);
        }

        // GET: VentaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VentaController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Clientes = new SelectList(await clienteBL.ObtenerTodosAsync(), "Id", "Nombre");
            ViewBag.Productos = await productoBL.ObtenerTodosAsync();

            return View();
        }

        // POST: VentaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Venta venta)
        {
            try
            {
                venta.Estado = (byte)EnumEstadoVenta.Activa;
                venta.FechaVenta = DateTime.Now;
                await ventaBL.CrearAsync(venta);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VentaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VentaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VentaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VentaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Anular(int id)
        {
            var venta = await ventaBL.ObtenerPorIdAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            await ventaBL.AnularAsync(id);

            return RedirectToAction("Index");
        }
    }
}
