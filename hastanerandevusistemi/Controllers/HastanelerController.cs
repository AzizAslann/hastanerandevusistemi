using Microsoft.AspNetCore.Mvc;
using hastanerandevusistemi.Models;
using Microsoft.EntityFrameworkCore;


namespace hastanerandevusistemi.Controllers
{
    public class HastanelerController : Controller
    {
        private readonly ConnectionStringClass _cc;

        public HastanelerController(ConnectionStringClass cc)
        {
            _cc = cc;
        }
        public IActionResult Hastaneler()
        {
            var hastaneler = _cc.Hastaneler.ToList();
            return View(hastaneler);
        }
    }
}
