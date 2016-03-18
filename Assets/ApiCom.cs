using UnityEngine;
using System;
using System.Xml.Linq;


	public class ApiCom
	{

		public string configFileName ="api_config.xml";
		private string apiKey = "";

		public ApiCom ()
		{

			XDocument doc = XDocument.Load(configFileName);
			apiKey = doc.Element(XName.Get("apiKey")).Value;

		Debug.Log(apiKey);

		}
		


	}

