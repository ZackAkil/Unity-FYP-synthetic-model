using UnityEngine;
using System;
using System.Xml.Linq;


	public class ApiCom
	{

		public string configFileName ="api_config.xml";
		private string apiKey;
		private string apiRoot;
		private string stationSubmitPath;
		private string scoutSubmitPath;

		public ApiCom (){

			XElement doc = XDocument.Load(configFileName).Element(XName.Get("apiConfig"));
			apiKey = doc.Element(XName.Get("apiKey")).Value;
			apiRoot = doc.Element(XName.Get("apiRoot")).Value;
			stationSubmitPath = doc.Element(XName.Get("stationSubmitPath")).Value;
			scoutSubmitPath = doc.Element(XName.Get("scoutSubmitPath")).Value;

			Debug.Log(apiRoot+stationSubmitPath);
		}

		public bool submitScoutData(){


			return true;
		}

	}

