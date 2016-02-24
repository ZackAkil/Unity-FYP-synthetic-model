using UnityEngine;
using System.Collections;
using System.Linq;

public class windZoneScript : MonoBehaviour {


	public const int streetDirection = 0;
	public const int streetWidth = 5;

	public float windSpeed;
	public float windDirection;

	public float globalWindSpeed;
	public float globalWindDirection;



	void Start () {
	
	}

	// Update is called once per frame
	void Update () {


	
	}

	//using global wind condition object to calculate wind speed within street

	static float calculateStreetWindSpeed (float gWindDir, float gWindSpeed, float steetDir, float streetWdth){

		float gausian1 = Mathf.Pow(2f,Mathf.Pow(-((gWindDir-steetDir)/streetWdth),2f));
		float gausian2 = Mathf.Pow(2f,Mathf.Pow(-((gWindDir-steetDir+180f)/streetWdth),2f)); 
		float gausian3 = Mathf.Pow(2f,Mathf.Pow(-((gWindDir-steetDir-180f)/streetWdth),2f)); 
		float gausian4 = Mathf.Pow(2f,Mathf.Pow(-((gWindDir-steetDir-360f)/streetWdth),2f)); 

		float[] gausian = {gausian1,gausian2,gausian3,gausian4};

		return gausian.Max()*gWindSpeed;
			
	}

	//using global wind condition object to calculate wind direction within street

	float calculateStreetWindDirection (float gWindDir, float gWindSpeed, float steetDir, float streetWdth){

	
		return 0f;
	}
}
