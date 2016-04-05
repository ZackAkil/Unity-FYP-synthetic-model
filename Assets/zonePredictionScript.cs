using UnityEngine;
using System.Collections;

public class zonePredictionScript : MonoBehaviour {

	public int dataUpdateRate = 3;
	private ApiCom api;
	// Use this for initialization
	void Start () {
	
		api = new ApiCom();
		// start recurring meathode call for fetching a apply prediction value to wind data vis child object
		InvokeRepeating("getPredictionOfWindDirection", 0.3f, dataUpdateRate);
	}


	double getPredictionOfWindDirection(){

		double predictedDir = api.GetPrediction(3, "wind direction");
		Debug.Log("prediction = " + predictedDir.ToString());
		return predictedDir;
	}

}
