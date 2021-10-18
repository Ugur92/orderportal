using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LedSiparisModulu.Components
{
    public class KampanyaViewComponent : ViewComponent
    {
        private readonly LAF_BUPILICContext _dbContext;

        public KampanyaViewComponent(LAF_BUPILICContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            var kampanyalar = _dbContext.KampanyaListe.AsNoTracking().Where(x => x.BitisTarihi > DateTime.Now && x.Durum == true).ToList();
            return View(kampanyalar);
        }
    }
}
