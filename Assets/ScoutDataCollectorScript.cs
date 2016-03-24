using UnityEngine;
using System.Collections;

public class ScoutDataCollectorScript : MonoBehaviour {

	private ApiCom api;
	private GlobalWindScript globalWindScript;

	void Start () {
	
		globalWindScript = GameObject.FindGameObjectWithTag("GlobalWind").GetComponent<GlobalWindScript>();
		api = new ApiCom();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {

		Vector3 pos = Input.mousePosition;
		pos.z = 10;
		pos = Camera.main.ScreenToWorldPoint(pos);

		api.submitScoutData(pos.x,pos.y,globalWindScript.GetGlobalWindSpeed(),globalWindScript.GetGlobalWindDir());

		Debug.Log("scout data fire at :" + pos.ToString() );
	}



}
