using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Assertions;

public class WindStreetScript : MonoBehaviour {

	public string logFileName = "windZone1.csv";
	public bool logDataToFile = true;
	public bool isPredicted;
	public int stationId;
	public bool uploadDataToApi;

	public int streetDirection = 0; // 0 - 180
	public int streetWidth = 5; // 0 - 50

	private float windSpeed;
	private float windDir;

	private ApiCom api;

	private GlobalWindScript globalWindScript;

	private int dataUpdateRate; // seconds

	void Start () {

		api = new ApiCom();

		// set wind script object 
		globalWindScript = GameObject.FindGameObjectWithTag("GlobalWind").GetComponent<GlobalWindScript>();

		dataUpdateRate = globalWindScript.dataUpdateRate;

		if(uploadDataToApi)
		InvokeRepeating("UploadData", 0.3f, dataUpdateRate);


		// set update rate of zones data
		InvokeRepeating("UpdateWindData", 0.1f, dataUpdateRate);

		transform.GetChild(1).GetComponent<StreetRenderScript>().setStreetRender(streetWidth,streetDirection);
	}


	//Update wind attributes of object relative to teh global wind conditions

	void UpdateWindData(){

		float tempGWindDir = globalWindScript.GetGlobalWindDir();
		float tempGWindSpeed = globalWindScript.GetGlobalWindSpeed();

		windSpeed = CalculateStreetWindSpeed(
			tempGWindDir,
			tempGWindSpeed,
			streetDirection,
			streetWidth);

		windDir = CalculateStreetWindDirection(
			tempGWindDir,
			tempGWindSpeed,
			streetDirection,
			streetWidth);

		//Debug.Log("wind speed : " + windSpeed + ", wind direction : "+ windDir);

		Assert.IsTrue(windDir >= 0);
		Assert.IsTrue(windDir <= 360);

		if(logDataToFile){
			string[] vals = {tempGWindDir.ToString(),tempGWindSpeed.ToString(),windDir.ToString(),windSpeed.ToString()};
			logValuesToCSV(vals,logFileName);
		}

		//set wind data vis to new values
		transform.GetChild(0).GetComponent<windVisScript>().setArrow(windSpeed,windDir);

	}

	/*	using global wind condition object to calculate wind speed within street
		street width is in range of 50 - 150
	*/
	static float CalculateStreetWindSpeed (float gWindDir, float gWindSpeed, float streetDir, float streetWdth){

		float scaledStreetWidth = (2f * streetWdth) + 50f;

		Assert.IsTrue((scaledStreetWidth >= 50)&&(scaledStreetWidth <= 150));

		float gausian1 = Mathf.Pow(2f,-Mathf.Pow(((gWindDir-streetDir)/scaledStreetWidth),2f));
		float gausian2 = Mathf.Pow(2f,-Mathf.Pow(((gWindDir-streetDir+180f)/scaledStreetWidth),2f)); 
		float gausian3 = Mathf.Pow(2f,-Mathf.Pow(((gWindDir-streetDir-180f)/scaledStreetWidth),2f)); 
		float gausian4 = Mathf.Pow(2f,-Mathf.Pow(((gWindDir-streetDir-360f)/scaledStreetWidth),2f)); 

		float[] gausian = {gausian1,gausian2,gausian3,gausian4};

		return gausian.Max()*gWindSpeed;

	}

	/*	using global wind condition object to calculate wind direction within street
		street width is in range of 0.04 - 0.14
	*/
	static float CalculateStreetWindDirection (float gWindDir, float gWindSpeed, float streetDir, float streetWdth){

		float scaledStreetWidth = 0.14f - ((0.1f/50f) * streetWdth);

		Assert.IsTrue((scaledStreetWidth >= 0.03)&&(scaledStreetWidth <= 0.15));

		float sig1 = (1f/(1f+ Mathf.Pow(Mathf.Exp(1),(-(gWindDir-streetDir+90f)*scaledStreetWidth))))*180f; 
		float sig2 = (1f/(1f+ Mathf.Pow(Mathf.Exp(1),(-(gWindDir-streetDir-90f)*scaledStreetWidth))))*180f; 
		float sig3 = (1f/(1f+ Mathf.Pow(Mathf.Exp(1),(-(gWindDir-streetDir-270f)*scaledStreetWidth))))*180f; 

		return MyMaths.mod((sig1 + sig2 + sig3 - 180f + streetDir),360f);


	}

	private void UploadData(){

		if(isPredicted){
			api.SubmitScoutData(new ScoutDataCollector(transform.position.x,transform.position.y,windSpeed,windDir));
		}else{
			api.SubmitStationData(new StationDataCollector(stationId,windSpeed,windDir));
		}

	}

	/*
	 save array of vals to CSV file in project directory 
	*/
	static void logValuesToCSV(string[] vals, string fileName){

		System.IO.File.AppendAllText(fileName, "\n" +
			string.Join(",",vals)
		);

	}

	public double getWindSpeed(){

		return windSpeed;
	}

	public double getWindDirection(){

		return windDir;
	}




}
