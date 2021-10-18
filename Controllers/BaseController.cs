using Microsoft.AspNetCore.Mvc;
using LedSiparisModulu.Infrastructures;

namespace LedSiparisModulu.Controllers
{
    public abstract class BaseController : Controller
    {
        protected void AddToastMessage(string title, string message, ToastType toastType = ToastType.info)
        {
            AddToastMessage(new ToastMessage() { Title = title, Message = message, ToastType = toastType });
        }

        protected void AddToastMessage(ToastMessage toastMessage)
        {
            Toastr toastr = TempData["Toastr"] as Toastr;
            toastr ??= new Toastr();

            toastr.AddToastMessage(toastMessage);
            TempData.Put("Toastr", toastr);
        }
    }
}