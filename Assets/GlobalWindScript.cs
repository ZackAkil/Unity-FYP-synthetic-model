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

		//set wind data vis to new values
		transform.GetChild(0).GetComponent<windVisScript>().setArrow(windSpeed,windDir);

		windSpeed +=5;
		//this.windDir +=15;

		windDir = Random.value*360;

		if(windDir>360)
			windDir = 0;
		
		if(windSpeed>50)
			windSpeed = 0;

		//set wind data vis to new values
		transform.GetChild(0).GetComponent<windVisScript>().setArrow(windSpeed,windDir);

	}

	public float GetGlobalWindSpeed(){

		return windSpeed;
	}

	public float GetGlobalWindDir(){

		return windDir;
	}
}
