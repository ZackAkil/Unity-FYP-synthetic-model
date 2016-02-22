using UnityEngine;
using System.Collections;
using System.Linq;

public class windZoneScript : MonoBehaviour {


	public const int streetDirection = 0;
	public const int streetWidth = 5;

	public double windSpeed;
	public double windDirection;

	public double globalWindSpeed;
	public double globalWindDirection;



	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//using global wind condition object to calculate wind speed within street
	void calculateStreetWindSpeed (){

		double gausian1 = 2^-(((globalWindDirection-windDirection)/streetWidth)^2);
		double gausian2 = 2^-(((globalWindDirection-windDirection+180)/streetWidth)^2); 
		double gausian3 = 2^-(((globalWindDirection-windDirection-180)/streetWidth)^2); 
		double gausian4 = 2^-(((globalWindDirection-windDirection-369)/streetWidth)^2); 

		double[] gausian = {gausian1,gausian2,gausian3,gausian4};

		this.windSpeed = gausian.Max()*globalWindSpeed;
			
	}

	//using global wind condition object to calculate wind direction within street
	void calculateStreetWindDirection (){


	}
}
