using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LedSiparisModulu.Infrastructures;

namespace LedSiparisModulu.Components
{
	public class ToastNotifyViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			if (TempData.TryGetValue("Toastr", out object toastr))
			{
				return View(JsonConvert.DeserializeObject<Toastr>(toastr.ToString()));
			}
			return View(new Toastr());
		}
	}
}
