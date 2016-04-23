using UnityEngine;
using System.Collections;
using System;

public class zonePredictionScript : MonoBehaviour {

	public int dataUpdateRate = 3;

	public int zoneId;
	public bool getPrediction;

	private ApiCom api;
	private DateTime predictionAge;

	private SpriteRenderer arrow;
	// Use this for initialization
	void Start () {
	
		predictionAge = DateTime.Now;
		api = new ApiCom();
		arrow = transform.GetChild(0).GetComponent<SpriteRenderer>();
		// start recurring meathode call for fetching a apply prediction value to wind data vis child object

		if(getPrediction)
			InvokeRepeating("updateWindVis", 3f, dataUpdateRate);
		else
			arrow.enabled = false;		

		InvokeRepeating("updateArrowColor", 0f, 0.2f);


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

		PredictResponse predictedDirSin = api.GetPrediction(zoneId, "sin wind direction");
		Debug.Log("prediction = " + predictedDirSin.ToString());
		DateTime sinAge = Convert.ToDateTime(predictedDirSin.latestDataUsed);


		Debug.Log("prediction age  = "+predictionAge.ToString() +" - "+  (DateTime.Now.Second - predictionAge.Second).ToString());

		PredictResponse predictedDirCos = api.GetPrediction(zoneId, "cos wind direction");
		Debug.Log("prediction = " + predictedDirCos.ToString());
		DateTime cosAge = Convert.ToDateTime(predictedDirCos.latestDataUsed);
		Debug.Log("prediction age  = "+predictionAge.ToString() +" - "+  (DateTime.Now.Second - predictionAge.Second).ToString());

		if (sinAge == cosAge){
			predictionAge = sinAge;
			arrow.color = new Color(1f, 1f, 0f); 
		}else{
			arrow.color = new Color(1f, 1f, 1f); 
		}



		return MyMaths.SinCosToDegrees(predictedDirSin.value, predictedDirCos.value);
	}

}
