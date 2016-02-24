using UnityEngine;
using System.Collections;

public class globalWindScript : MonoBehaviour {

	public float windSpeed;
	public float windDir;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float GetGlobalWindSpeed(){

		return 22f;
	}

	public float GetGlobalWindDir(){

		return 90f;
	}
}
