using UnityEngine;
using System.Collections;

public class windVisScript : MonoBehaviour {

	private float targetArrowScale = 1f;
	private float targetArrowDir = 0f;

	private float currenArrowScale = 1f;
	private float currentArrowDir = 0f;

	private float startTime = 0f;
	private float durationTime = 0f;


	// Use this for initialization
	void Start () {
	
		startTime = Time.time;

	}
	
	// Update is called once per frame
	void Update () {

		float newRotVal = ElasticEaseOut(Time.time - startTime,0f,180f,5f);

		transform.rotation = Quaternion.Euler(0,0,newRotVal);
	
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
	public static float ElasticEaseOut( float t, float b, float c, float d )
	{
		if ( ( t /= d ) == 1 )
			return b + c;

		float p = d * .3f;
		float s = p / 4f;

		return ( c * Mathf.Pow( 2f, -10f * t ) * Mathf.Sin( ( t * d - s ) * ( 2 * Mathf.PI ) / p ) + c + b );
	}

}
