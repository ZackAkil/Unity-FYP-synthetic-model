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

	public string configFileName = "api_config.xml";
	private string apiKey;
	private string apiRoot;
	private string stationSubmitPath;
	private string scoutSubmitPath;

	private UTF8Encoding encoding;


	public ApiCom ()
	{

		XElement doc = XDocument.Load (configFileName).Element (XName.Get ("apiConfig"));
		apiKey = doc.Element (XName.Get ("apiKey")).Value;
		apiRoot = doc.Element (XName.Get ("apiRoot")).Value;
		stationSubmitPath = doc.Element (XName.Get ("stationSubmitPath")).Value;
		scoutSubmitPath = doc.Element (XName.Get ("scoutSubmitPath")).Value;

		encoding = new System.Text.UTF8Encoding ();


		Debug.Log (apiRoot + stationSubmitPath);
	}

	public bool SubmitScoutData (ScoutDataCollector data)
	{
		SubmitDataCollectorToUrl (data, apiRoot + scoutSubmitPath);
		return true;
	}

	public bool SubmitStationData (StationDataCollector data)
	{

		SubmitDataCollectorToUrl (data, apiRoot + stationSubmitPath);

		return true;
	}


	private bool SubmitDataCollectorToUrl (DataCollector data, string url)
	{

		data.apiKey = this.apiKey;
		string jsonData = JsonUtility.ToJson (data);
		Debug.Log (jsonData);

		var postHeader = new System.Collections.Generic.Dictionary<string, string> ();

		postHeader.Add ("Content-Type", "text/json");
		postHeader.Add ("Content-Length", jsonData.Length.ToString ());


		WWW postRequest = new WWW (apiRoot + stationSubmitPath, encoding.GetBytes (jsonData), postHeader);

		if (!string.IsNullOrEmpty (postRequest.error)) {
			Debug.Log (postRequest.error);
		} else {
			Debug.Log ("Finished submiting station data");
		}
			
		return true;
	}


}

