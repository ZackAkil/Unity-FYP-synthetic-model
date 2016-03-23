using UnityEngine;
using System;
using System.Xml.Linq;
using System.Net;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


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

	public bool submitScoutData(double longitude, double latitude, double windSpeed, double windDirection){

		ScoutDataCollector data = new ScoutDataCollector();
		data.apiKey = this.apiKey;
		data.dateTimeCollected = DateTime.Now;
		data.windSpeed = windSpeed;
		data.windDirection = windDirection;
		data.longitude = longitude;
		data.latitude = latitude;

		WWWForm form = data.getPostData();

		WWW postRequest = new WWW( apiRoot + scoutSubmitPath, form );

		if (!string.IsNullOrEmpty(postRequest.error)) {
			Debug.Log(postRequest.error);
		}
		else {
			Debug.Log("Finished submiting scout data");
		}
			
		return true;


	}
}

