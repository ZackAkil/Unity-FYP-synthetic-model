using UnityEngine;
using System.Collections;

public class zonePredictionScript : MonoBehaviour {

	public int dataUpdateRate = 3;
	private ApiCom api;
	public int zoneId;
	// Use this for initialization
	void Start () {
	
		api = new ApiCom();
		// start recurring meathode call for fetching a apply prediction value to wind data vis child object
		InvokeRepeating("updateWindVis", 3f, dataUpdateRate);
	}


	void updateWindVis(){

		float predictedWindDir = getPredictionOfWindDirection();

		transform.GetChild(0).GetComponent<windVisScript>().setArrow(20f, predictedWindDir);

	}


	float getPredictionOfWindDirection(){

		float predictedDir = api.GetPrediction(zoneId, "wind direction");
		Debug.Log("prediction = " + predictedDir.ToString());
		return predictedDir;
	}

}
