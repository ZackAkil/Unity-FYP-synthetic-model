using UnityEngine;
using System.Collections;

public class windVisScript : MonoBehaviour {

	private float targetArrowScale = 1f;
	private float targetArrowDir = 0f;

	private float currenArrowScale = 1f;
	private float currentArrowDir = 0f;

	private float initialArrowScale = 1f;
	private float initialArrowDir = 0f;

	private float startTime = 0f;
	private float durationTime = 0f;

	private float easeArrowDir = 0;


	void Start () {
	
		startTime = Time.time;

	}

	void Update () {

		easeArrowDir = ElasticEaseOut(Time.time - startTime,0,targetArrowDir-initialArrowDir,1f);

		currentArrowDir = easeArrowDir + initialArrowDir;

		transform.localEulerAngles = new Vector3(0,0,currentArrowDir);
	
	}

	public void setNewArrowPosition(float newPosition){

		targetArrowDir = newPosition;

		initialArrowDir = currentArrowDir;

		if ((targetArrowDir - initialArrowDir) > 180)
			targetArrowDir -= 360;

		startTime = Time.time;

	}

	/// <summary>
	/// Easing equation function for an elastic (exponentially decaying sine wave) easing out: 
	/// decelerating from zero velocity.
	/// </summary>
	/// <param name="t">Current time in seconds.</param>
	/// <param name="b">Starting value.</param>
	/// <param name="c">Final value.</param>
	/// <param name="d">Duration of animation.</param>
	/// <returns>The correct value.</returns>
	private static float ElasticEaseOut( float t, float b, float c, float d )
	{
		if ( ( t /= d ) == 1 )
			return b + c;

		float p = d * .3f;
		float s = p / 4f;

		return ( c * Mathf.Pow( 2f, -10f * t ) * Mathf.Sin( ( t * d - s ) * ( 2 * Mathf.PI ) / p ) + c + b );
	}

}
