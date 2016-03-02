﻿using UnityEngine;
using System.Collections;

public class GlobalWindScript : MonoBehaviour {

	public float windSpeed = 50;
	public float windDir = 0;

	// Use this for initialization
	void Start () {

		InvokeRepeating("ChangeWind", 0f, 3.0f);
	
	}
	
	// Update is called once per frame
	void Update () {

	}


	void ChangeWind(){


		//windSpeed +=5;
		windDir +=90;

		//windDir = Random.value*360;

		if(windDir>=360)
		windDir = windDir%360;
		
//		if(windSpeed>50)
//			windSpeed = 0;
		Debug.Log("gwind: " + windDir);
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
