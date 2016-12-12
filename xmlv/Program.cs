//
// xmlv
// (c) 2016 Andrzej Budzanowski <kontakt@andrzej.budzanowski.pl>
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
//

using System;
using System.Xml;
using System.Xml.Schema;

namespace xmlv
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("xmlv");
			Console.WriteLine("(c) 2016 Andrzej Budzanowski <kontakt@andrzej.budzanowski.pl>");
			Console.WriteLine("--");

			XmlSchemaSet xss = new XmlSchemaSet();

			foreach (var it in args)
			{
				if (it.EndsWith(".xsd"))
					xss.Add(null, it);
			}

			foreach (var it in args)
			{
				if (it.EndsWith(".xml"))
				{
					XmlReaderSettings set = new XmlReaderSettings();
					set.ValidationType = ValidationType.Schema;
					set.Schemas = xss;
					set.ValidationEventHandler += OnXmlError;

					Console.WriteLine("Validate: {0}", it);
					CurrentFileName = it;

					using (XmlReader xr = XmlReader.Create(it, set))
						while (xr.Read()) ;
				}
			}
		}

		private static string CurrentFileName = "";

		private static void OnXmlError(object sender, ValidationEventArgs e)
		{
			Console.WriteLine("{0}:{1}:{2} - {3}: {4}", CurrentFileName,
				e.Exception.LineNumber, e.Exception.LinePosition, e.Severity,
				e.Message);
		}
	}
}
