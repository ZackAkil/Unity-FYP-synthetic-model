using UnityEngine;
using System.Collections;

public class ScoutDataCollectorScript : MonoBehaviour {

	ApiCom api;
	// Use this for initialization
	void Start () {
	
		api = new ApiCom();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {

		Vector3 pos = Input.mousePosition;
		pos.z = 10;
		pos = Camera.main.ScreenToWorldPoint(pos);

		api.submitScoutData(pos.x,pos.y,15,150);

		Debug.Log("scout data fire at :" + pos.ToString() );
	}



}
