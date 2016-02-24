using UnityEngine;
using System.Collections;
using System.Linq;

public class windZoneScript : MonoBehaviour {


	public const int streetDirection = 0;
	public const int streetWidth = 5;

	private float windSpeed;
	private float windDirection;

	public GameObject gloablWind;



	void Start () {
	
		InvokeRepeating("UpdateWindData", 0f, 1.0f);
	}

	// Update is called once per frame
	void Update () {


	
	}

	void UpdateWindData(){

		Debug.Log("hello every second");

	}

	//using global wind condition object to calculate wind speed within street

	static float CalculateStreetWindSpeed (float gWindDir, float gWindSpeed, float streetDir, float streetWdth){

		float gausian1 = Mathf.Pow(2f,Mathf.Pow(-((gWindDir-streetDir)/streetWdth),2f));
		float gausian2 = Mathf.Pow(2f,Mathf.Pow(-((gWindDir-streetDir+180f)/streetWdth),2f)); 
		float gausian3 = Mathf.Pow(2f,Mathf.Pow(-((gWindDir-streetDir-180f)/streetWdth),2f)); 
		float gausian4 = Mathf.Pow(2f,Mathf.Pow(-((gWindDir-streetDir-360f)/streetWdth),2f)); 

		float[] gausian = {gausian1,gausian2,gausian3,gausian4};

		return gausian.Max()*gWindSpeed;
			
	}

	//using global wind condition object to calculate wind direction within street

	static float CalculateStreetWindDirection (float gWindDir, float gWindSpeed, float streetDir, float streetWdth){

		float sig1 = (1/(1+ Mathf.Pow(Mathf.Exp(1),(-(gWindDir-streetDir+90)*streetWdth))))*180; 
		float sig2 = (1/(1+ Mathf.Pow(Mathf.Exp(1),(-(gWindDir-streetDir-90)*streetWdth))))*180; 
		float sig3 = (1/(1+ Mathf.Pow(Mathf.Exp(1),(-(gWindDir-streetDir-270)*streetWdth))))*180; 

		return (sig1 + sig2 + sig3 - 180 + streetDir)%360;
	}
}
