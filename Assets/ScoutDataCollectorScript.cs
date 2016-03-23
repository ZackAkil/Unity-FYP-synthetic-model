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



		Debug.Log("scout data fire at :" + pos.ToString() );
	}



}
