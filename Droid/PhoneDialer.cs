using System;
using System.Linq;
using Xamarin.Forms;
using Android.Content;
using Android.Telephony;
using PhoneWordxPlatform.Droid;
using Uri = Android.Net.Uri;


[assembly: Dependency(typeof(PhoneDialer))]
namespace PhoneWordxPlatform.Droid
{
	public class PhoneDialer : IDialer
	{
		/// <summary>
		/// Dial the specified number.
		/// </summary>
		/// <param name="numberToDial">Number to dial.</param>
		public bool Dial(string numberToDial)
		{
			bool result = false;
			var context = Forms.Context;
			if (context == null) return result;

			var intent = new Intent(Intent.ActionCall);
			intent.SetData(Uri.Parse($"tel:{numberToDial}"));
			var canCall = IsIntentAvailable(context, intent);

			if (canCall)
			{
				context.StartActivity(intent);
				result = true;
			}

			Console.WriteLine($"**** Droid dialing {numberToDial}:  {canCall}");
			return result;
		}

		/// <summary>
		/// Checks to see if the intent available.
		/// </summary>
		/// <returns><c>true</c>, if intent available, <c>false</c> otherwise.</returns>
		/// <param name="context">Context.</param>
		/// <param name="intent">Intent.</param>
		public static bool IsIntentAvailable(Context context, Intent intent)
		{
			var packageManager = context.PackageManager;
			var list = packageManager.QueryIntentServices(intent, 0)
									 .Union(packageManager.QueryIntentActivities(intent, 0));

			if (list.Any()) return true;

			TelephonyManager telephonyManager = TelephonyManager.FromContext(context);
			return telephonyManager.PhoneType != PhoneType.None;
		}
	}
}

