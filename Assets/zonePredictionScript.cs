using UnityEngine;
using System.Collections;
using System;

public class zonePredictionScript : MonoBehaviour {

	public int dataUpdateRate = 3;
	private ApiCom api;
	public int zoneId;
	private DateTime predictionAge;

	private SpriteRenderer arrow;
	// Use this for initialization
	void Start () {
	
		predictionAge = DateTime.Now;
		api = new ApiCom();
		// start recurring meathode call for fetching a apply prediction value to wind data vis child object
		InvokeRepeating("updateWindVis", 3f, dataUpdateRate);

		InvokeRepeating("updateArrowColor", 0f, 0.2f);

		arrow = transform.GetChild(0).GetComponent<SpriteRenderer>();
	}

	void updateArrowColor(){

		var ageDiff = (DateTime.Now.Second - (predictionAge.Second + 3))*2;



		arrow.color = new Color(1f, 1f, 0f,  ((10 - ageDiff))/10f ); 


	}


	void updateWindVis(){

		float predictedWindDir = getPredictionOfWindDirection();

		transform.GetChild(0).GetComponent<windVisScript>().setArrow(20f, predictedWindDir);

	}


	float getPredictionOfWindDirection(){

		PredictResponse predictedDir = api.GetPrediction(zoneId, "wind direction");
		Debug.Log("prediction = " + predictedDir.ToString());
		predictionAge = Convert.ToDateTime(predictedDir.latestDataUsed);

		Debug.Log("prediction age  = "+predictionAge.ToString() +" - "+  (DateTime.Now.Second - predictionAge.Second).ToString());
		return predictedDir.value;
	}

}
