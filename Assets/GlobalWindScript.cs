using UnityEngine;
using System.Collections;

public class GlobalWindScript : MonoBehaviour {

	public float windSpeed = 0;
	public float windDir = 0;

	// Use this for initialization
	void Start () {

		InvokeRepeating("ChangeWind", 0f, 1.0f);
	
	}
	
	// Update is called once per frame
	void Update () {

	}


	void ChangeWind(){

		this.windSpeed +=5;
		//this.windDir +=15;

		windDir = Random.value*360;

		if(this.windDir>360)
			this.windDir = 0;
		
		if(this.windSpeed>50)
			this.windSpeed = 0;



	}

	public float GetGlobalWindSpeed(){

		return windSpeed;
	}

	public float GetGlobalWindDir(){

		return windDir;
	}
}
