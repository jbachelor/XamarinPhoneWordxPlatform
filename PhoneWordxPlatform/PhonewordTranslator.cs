using System;
using System.Text;


namespace PhoneWordxPlatform
{
	public static class PhonewordTranslator
	{
		static readonly string[] digitLetterGroups = {
			"ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
		};

		public static string ToNumber(string rawNumber)
		{
			if (string.IsNullOrWhiteSpace(rawNumber)) return null;

			rawNumber = rawNumber.ToUpperInvariant();
			var translatedNumber = new StringBuilder();

			foreach (var aCharacter in rawNumber)
			{
				if ("-0123456789".Contains(aCharacter))
				{
					translatedNumber.Append(aCharacter);
				}
				else
				{
					var result = TranslateToNumber(aCharacter);
					if (result != null)
						translatedNumber.Append(result);
				}
			}

			return translatedNumber.ToString();
		}

		static bool Contains(this string keyString, char c)
		{
			return keyString.IndexOf(c) >= 0;
		}

		static int? TranslateToNumber(char c)
		{
			int? number = null;

			for (int i = 0; i < digitLetterGroups.Length; i++)
			{
				if (digitLetterGroups[i].Contains(c))
				{
					number = i + 2;
					break;
				}
			}

			return number;
		}
	}
}

