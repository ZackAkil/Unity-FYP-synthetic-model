using UnityEngine;
using System.Collections;

public class GlobalWindScript : MonoBehaviour {

	private float windSpeed = 0;
	private float windDir = 0;

	// Use this for initialization
	void Start () {

		InvokeRepeating("ChangeWind", 0f, 1.0f);
	
	}
	
	// Update is called once per frame
	void Update () {

	}


	void ChangeWind(){

		windSpeed ++;
		windDir ++;

		if(windDir>360)
			windDir = 0;


		if(windSpeed>50)
			windSpeed = 0;
	}

	public float GetGlobalWindSpeed(){

		return windSpeed;
	}

	public float GetGlobalWindDir(){

		return windDir;
	}
}
