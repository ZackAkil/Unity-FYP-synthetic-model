using UnityEngine;
using System.Collections;


	public class MyMaths : MonoBehaviour
	{
		/// <summary>
		/// Prefered modulo function which works for negative values
		/// </summary>
		/// <param name="x">devider.</param>
		/// <param name="m">devisor.</param>
		public static float mod(float x, float m) {
			float r = x%m;
			return r<0 ? r+m : r;
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


		/// <summary>
		/// Easing equation function for a cubic (t^3) easing out: 
		/// decelerating from zero velocity.
		/// </summary>
		/// <param name="t">Current time in seconds.</param>
		/// <param name="b">Starting value.</param>
		/// <param name="c">Final value.</param>
		/// <param name="d">Duration of animation.</param>
		/// <returns>The correct value.</returns>
		public static float CubicEaseOut( float t, float b, float c, float d )
		{
			return c * ( ( t = t / d - 1f ) * t * t + 1f ) + b;
		}
	}


