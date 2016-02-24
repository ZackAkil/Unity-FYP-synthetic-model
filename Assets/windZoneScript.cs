using UnityEngine;
using System.Collections;
using System.Linq;

public class WindZoneScript : MonoBehaviour {


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

		Debug.Log("hello every second - " + globalWindScript.GetGlobalWindDir());

		windSpeed = CalculateStreetWindSpeed(
			globalWindScript.GetGlobalWindDir(),
			globalWindScript.GetGlobalWindSpeed(),
			streetDirection,
			streetWidth);

		windDir = CalculateStreetWindDirection(
			globalWindScript.GetGlobalWindDir(),
			globalWindScript.GetGlobalWindSpeed(),
			streetDirection,
			streetWidth);

	}

	/*	using global wind condition object to calculate wind speed within street
		wind speed is in range of 50 - 150
	*/
	static float CalculateStreetWindSpeed (float gWindDir, float gWindSpeed, float streetDir, float streetWdth){

		float gausian1 = Mathf.Pow(2f,Mathf.Pow(-((gWindDir-streetDir)/streetWdth),2f));
		float gausian2 = Mathf.Pow(2f,Mathf.Pow(-((gWindDir-streetDir+180f)/streetWdth),2f)); 
		float gausian3 = Mathf.Pow(2f,Mathf.Pow(-((gWindDir-streetDir-180f)/streetWdth),2f)); 
		float gausian4 = Mathf.Pow(2f,Mathf.Pow(-((gWindDir-streetDir-360f)/streetWdth),2f)); 

		float[] gausian = {gausian1,gausian2,gausian3,gausian4};

		return gausian.Max()*gWindSpeed;
			
	}

	/*	using global wind condition object to calculate wind direction within street
		wind speed is in range of 0.04 - 0.14
	*/
	static float CalculateStreetWindDirection (float gWindDir, float gWindSpeed, float streetDir, float streetWdth){

		float sig1 = (1/(1+ Mathf.Pow(Mathf.Exp(1),(-(gWindDir-streetDir+90)*streetWdth))))*180; 
		float sig2 = (1/(1+ Mathf.Pow(Mathf.Exp(1),(-(gWindDir-streetDir-90)*streetWdth))))*180; 
		float sig3 = (1/(1+ Mathf.Pow(Mathf.Exp(1),(-(gWindDir-streetDir-270)*streetWdth))))*180; 

		return (sig1 + sig2 + sig3 - 180 + streetDir)%360;
	}
}
