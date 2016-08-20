using System;
using UIKit;
using Foundation;
using Xamarin.Forms;
using PhoneWordxPlatform.iOS;


[assembly: Dependency(typeof(PhoneDialer))]
namespace PhoneWordxPlatform.iOS
{
	public class PhoneDialer : IDialer
	{
		/// <summary>
		/// Dial the specified number.
		/// </summary>
		/// <param name="numberToDial">Number to dial.</param>
		public bool Dial(string numberToDial)
		{
			var result = UIApplication.SharedApplication.OpenUrl
								(new NSUrl($"tel:{numberToDial}"));

			Console.WriteLine($"**** iOS dialing {numberToDial}:  {result}");
			return result;
		}
	}
}

