using UnityEngine;
using System;
using System.Xml.Linq;
using System.Net;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;



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

//		ScoutDataCollector data = new ScoutDataCollector();
//		data.apiKey = this.apiKey;
//		data.dateTimeCollected = DateTime.Now;
//		data.windSpeed = windSpeed;
//		data.windDirection = windDirection;
//		data.longitude = longitude;
//		data.latitude = latitude;
//
//		WWWForm form = data.getPostData();
//
//		WWW postRequest = new WWW( apiRoot + scoutSubmitPath, form );
//
//		if (!string.IsNullOrEmpty(postRequest.error)) {
//			Debug.Log(postRequest.error);
//		}
//		else {
//			Debug.Log("Finished submiting scout data");
//		}
			
		return true;
	}

	public bool submitStationData(int zoneId, double windSpeed, double windDirection){

//		StationDataCollector data = new StationDataCollector();
//		data.apiKey = this.apiKey;
//		data.dateTimeCollected = DateTime.Now;
//		data.windSpeed = windSpeed;
//		data.windDirection = windDirection;
//		data.zoneId = zoneId;
//
//		WWWForm form = data.getPostData();
//
//		WWW postRequest = new WWW( apiRoot + stationSubmitPath, form );
//
//		if (!string.IsNullOrEmpty(postRequest.error)) {
//			Debug.Log(postRequest.error);
//		}
//		else {
//			Debug.Log("Finished submiting station data");
//		}

		return true;
	}

	public bool submitStationDataJson(StationDataCollector data){

	
		data.apiKey = this.apiKey;

		string jsonData = JsonUtility.ToJson(data);

		Debug.Log(jsonData);
	
		var encoding = new System.Text.UTF8Encoding();
		System.Collections.Generic.Dictionary<string, string> postHeader = new System.Collections.Generic.Dictionary<string, string>();

		postHeader.Add("Content-Type", "text/json");
		postHeader.Add("Content-Length", jsonData.Length.ToString());

		WWW postRequest = new WWW( apiRoot + stationSubmitPath,  encoding.GetBytes(jsonData), postHeader );

		if (!string.IsNullOrEmpty(postRequest.error)) {
			Debug.Log(postRequest.error);
		}
		else {
			Debug.Log("Finished submiting station data");
		}

		return true;
	}


}

