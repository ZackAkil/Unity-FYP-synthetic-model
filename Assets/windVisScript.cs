using UnityEngine;
using System.Collections;

public class windVisScript : MonoBehaviour {

	private float targetArrowScale = 1f;
	private float targetArrowDir = 0f;

	private float currentArrowScale = 0.2f;
	private float currentArrowDir = 0f;

	private float initialArrowScale = 1f;
	private float initialArrowDir = 0f;

	private float startTime = 0f;
	public float durationTime = 3.0f;

	private float easeArrowDir = 0;


	void Start () {
	
		startTime = Time.time;

	}

	void Update () {

		float easeArrowScale = MyMaths.CubicEaseOut(Time.time - startTime,0,targetArrowScale- initialArrowScale,durationTime);

		currentArrowScale =  easeArrowScale + initialArrowScale;

		transform.localScale = new Vector3 (currentArrowScale,currentArrowScale,1f);

		easeArrowDir = MyMaths.ElasticEaseOut(Time.time - startTime,0,targetArrowDir-initialArrowDir,durationTime);

		currentArrowDir = easeArrowDir + initialArrowDir;

		transform.localEulerAngles = new Vector3(0,0,currentArrowDir);
	
	}

	private void setNewWindDir(float newDir){

		targetArrowDir = newDir;

		initialArrowDir = currentArrowDir;

		if ((targetArrowDir - initialArrowDir) > 180)
			targetArrowDir -= 360;

		startTime = Time.time;
	}

	private void setNewWindSpeed(float newSpeed){

		targetArrowScale = (newSpeed/150f)+0.1f;

		initialArrowScale = currentArrowScale;

	}

	/// <summary>
	/// Set arrow to new shape based on wind speed and direction
	/// </summary>
	/// <param name="newSpeed">New speed (0 - 50).</param>
	/// <param name="newDir">New direction (0 - 359).</param>
	/// <returns>Void.</returns>
	public void setArrow(float newSpeed, float newDir){

		setNewWindSpeed(newSpeed);

		float nDir = MyMaths.mod(newDir, 360);
		float oDir = MyMaths.mod(currentArrowDir, 360);

		float diff = 360 - oDir;
		float sum = diff + nDir;

		newDir = oDir + sum;


		setNewWindDir(newDir);

	}

}
