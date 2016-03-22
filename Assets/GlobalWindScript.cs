using UnityEngine;
using System.Collections;

public class GlobalWindScript : MonoBehaviour {

	public float windSpeed = 50;
	public float windDir = 0;

	void Start () {

		InvokeRepeating("ChangeWind", 0f, 3.0f);

		ApiCom connection = new ApiCom();
	
	}

	void ChangeWind(){

		windDir = Random.value*360;

		//Debug.Log("gwind: " + windDir);

		transform.GetChild(0).GetComponent<windVisScript>().setArrow(windSpeed,windDir);

	}

	public float GetGlobalWindSpeed(){

		return windSpeed;
	}

	public float GetGlobalWindDir(){

		return windDir;
	}

	void Update(){


		if (Input.GetKeyDown("space")){
			Vector3 pos = Input.mousePosition;
			pos.z = 10;
			pos = Camera.main.ScreenToWorldPoint(pos);
			Debug.Log(pos);
		}



	}
}
