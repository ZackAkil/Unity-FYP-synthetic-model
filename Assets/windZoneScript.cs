using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Assertions;

public class WindZoneScript : MonoBehaviour {

	public string logFileName = "windZone1.csv";
	public bool logDataToFile = true;

	public const int streetDirection = 0; // 0 - 180
	public const int streetWidth = 5; // 0 - 50

	private float windSpeed;
	private float windDir;

	public GameObject globalWindObject;
	private GlobalWindScript globalWindScript;

	public float dataUpdateRate = 1.0f; // seconds

	void Start () {

		// set wind script object 
		globalWindScript = globalWindObject.GetComponent<GlobalWindScript>();

		// set update rate of zones data
		InvokeRepeating("UpdateWindData", 0f, dataUpdateRate);
	}

	// Update is called once per frame
	void Update () {


	
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

		Debug.Log("wind speed : " + windSpeed + ", wind direction : "+ windDir);

		if(logDataToFile){
			string[] vals = {tempGWindDir.ToString(),tempGWindSpeed.ToString(),windDir.ToString(),windSpeed.ToString()};
			logValuesToCSV(vals,logFileName);
		}

		transform.GetChild(0).GetComponent<windVisScript>().setNewWindDir(windDir);
		transform.GetChild(0).GetComponent<windVisScript>().setNewWindSpeed(windSpeed);

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

		float scaledStreetWidth = ((0.1f/50f) * streetWdth) + 0.04f;

		Assert.IsTrue((scaledStreetWidth >= 0.04)&&(scaledStreetWidth <= 0.14));

		float sig1 = (1f/(1f+ Mathf.Pow(Mathf.Exp(1),(-(gWindDir-streetDir+90f)*scaledStreetWidth))))*180f; 
		float sig2 = (1f/(1f+ Mathf.Pow(Mathf.Exp(1),(-(gWindDir-streetDir-90f)*scaledStreetWidth))))*180f; 
		float sig3 = (1f/(1f+ Mathf.Pow(Mathf.Exp(1),(-(gWindDir-streetDir-270f)*scaledStreetWidth))))*180f; 

		return (sig1 + sig2 + sig3 - 180f + streetDir)%360f;
	}

	/*
	 save array of vals to CSV file in project directory 
	*/
	static void logValuesToCSV(string[] vals, string fileName){

		System.IO.File.AppendAllText(fileName, "\n" +
			string.Join(",",vals)
		);

	}

}
