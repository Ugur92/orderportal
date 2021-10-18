using System;
using System.Collections.Generic;

namespace LedSiparisModulu.Infrastructures
{
	[Serializable]
	public class Toastr
	{
		public bool ShowNewestOnTop { get; set; }
		public bool ShowCloseButton { get; set; }
		public List<ToastMessage> ToastMessages { get; set; }

		public void AddToastMessage(string title, string message, ToastType toastType)
		{
			ToastMessage toast = new ToastMessage()
			{
				Title = title,
				Message = message,
				ToastType = toastType
			};
			ToastMessages.Add(toast);
		}

		public void AddToastMessage(ToastMessage message)
		{
			ToastMessages.Add(message);
		}

		public Toastr()
		{
			ToastMessages = new List<ToastMessage>();
			ShowNewestOnTop = false;
			ShowCloseButton = false;
		}
	}
}