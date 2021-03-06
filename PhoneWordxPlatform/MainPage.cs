﻿using System;
using Xamarin.Forms;


namespace PhoneWordxPlatform
{
	public class MainPage : ContentPage
	{
		Entry phoneNumberText;
		Button translateButton;
		Button callButton;

		string translatedNumber = string.Empty;
		string originalNumber = string.Empty;

		public MainPage()
		{
			this.Padding = new Thickness(20, Device.OnPlatform<double>(40, 20, 20), 20, 20);
			StackLayout mainPanel = SetupMainView();
			ConfigureHandlers();

			this.Content = mainPanel;
		}

		StackLayout SetupMainView()
		{
			StackLayout panel = new StackLayout
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Vertical,
				Spacing = 15
			};

			panel.Children.Add(new Label
			{
				Text = "Enter a phoneword:"
			});

			panel.Children.Add(phoneNumberText = new Entry
			{
				Placeholder = "1-800-PUGLOVE"
			});

			panel.Children.Add(translateButton = new Button
			{
				Text = "Translate"
			});

			panel.Children.Add(callButton = new Button
			{
				Text = "Call",
				IsEnabled = false
			});
			return panel;
		}

		void ConfigureHandlers()
		{
			translateButton.Clicked += OnTranslate;
			callButton.Clicked += OnCall;
		}

		async void OnCall(object sender, EventArgs e)
		{
			bool alertResponse = await this.DisplayAlert("Dial a Number", $"Call {translatedNumber}?", "Yes", "No");
			if (alertResponse)
			{
				var dialer = DependencyService.Get<IDialer>();
				if (dialer == null) return;

				dialer.Dial(translatedNumber);
			}
		}

		void OnTranslate(object sender, EventArgs e)
		{
			originalNumber = phoneNumberText.Text;
			translatedNumber = PhonewordTranslator.ToNumber(originalNumber);

			if (string.IsNullOrWhiteSpace(translatedNumber))
			{
				callButton.Text = "Call";
				callButton.IsEnabled = false;
			}
			else {
				callButton.Text = $"Call {translatedNumber}";
				callButton.IsEnabled = true;
			}
		}
	}
}

