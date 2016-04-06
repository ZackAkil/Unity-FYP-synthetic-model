using UnityEngine;
using System;
using System.Xml.Linq;
using System.Net;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;

public class PredictResponse
{
	public float value;
	public string latestDataUsed;
}

public class ApiCom
{

	public string configFileName = "api_config.xml";
	private string apiKey;
	private string apiRoot;
	private string stationSubmitPath;
	private string scoutSubmitPath;
	private string predictPath;

	private UTF8Encoding encoding;


	public ApiCom ()
	{

		XElement doc = XDocument.Load (configFileName).Element (XName.Get ("apiConfig"));
		apiKey = doc.Element (XName.Get ("apiKey")).Value;
		apiRoot = doc.Element (XName.Get ("apiRoot")).Value;
		stationSubmitPath = doc.Element (XName.Get ("stationSubmitPath")).Value;
		scoutSubmitPath = doc.Element (XName.Get ("scoutSubmitPath")).Value;
		predictPath = doc.Element (XName.Get ("predictionPath")).Value;


		encoding = new System.Text.UTF8Encoding ();

		Debug.Log (apiRoot + stationSubmitPath);
		Debug.Log (apiRoot + scoutSubmitPath);
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


		WWW postRequest = new WWW (url, encoding.GetBytes (jsonData), postHeader);

		if (!string.IsNullOrEmpty (postRequest.error)) {
			Debug.Log (postRequest.error);
		} else {
			Debug.Log ("Finished submiting station data");
		}
			
		return true;
	}

	public PredictResponse GetPrediction(int predictedZoneId, string dataSubject){

		WebRequest request = WebRequest.Create (apiRoot+predictPath
			+"?id="+predictedZoneId.ToString()
			+"&dataSubject="+ WWW.EscapeURL(dataSubject)
			+"&apiKey="+apiKey);
		
		// If required by the server, set the credentials.
		request.Credentials = CredentialCache.DefaultCredentials;
		// Get the response.
		WebResponse response = request.GetResponse ();
		// Display the status.
		Debug.Log (((HttpWebResponse)response).StatusDescription);
		// Get the stream containing content returned by the server.
		Stream dataStream = response.GetResponseStream ();
		// Open the stream using a StreamReader for easy access.
		StreamReader reader = new StreamReader (dataStream);
		// Read the content.
		string responseFromServer = reader.ReadToEnd ();

		PredictResponse output = JsonUtility.FromJson<PredictResponse>(responseFromServer);

		Debug.Log("prediction form server age = " + output.value + " - " + output.latestDataUsed);

		reader.Close ();
		response.Close ();

		return output;

	}
		


}





