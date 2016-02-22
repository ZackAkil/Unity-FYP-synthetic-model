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
	void calculateStreetWindSpeed (){

		float gausian1 = Mathf.Pow(2f,Mathf.Pow(-((globalWindDirection-windDirection)/streetWidth),2f));
		float gausian2 = Mathf.Pow(2f,Mathf.Pow(-((globalWindDirection-windDirection+180f)/streetWidth),2f)); 
		float gausian3 = Mathf.Pow(2f,Mathf.Pow(-((globalWindDirection-windDirection-180f)/streetWidth),2f)); 
		float gausian4 = Mathf.Pow(2f,Mathf.Pow(-((globalWindDirection-windDirection-360f)/streetWidth),2f)); 

		float[] gausian = {gausian1,gausian2,gausian3,gausian4};

		this.windSpeed = gausian.Max()*globalWindSpeed;
			
	}

	//using global wind condition object to calculate wind direction within street
	void calculateStreetWindDirection (){


	}
}
