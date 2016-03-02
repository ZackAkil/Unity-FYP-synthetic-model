using UnityEngine;
using System.Collections;

public class StreetRenderScript : MonoBehaviour {


	public void setStreetRender(float width, float direction){

		float result = MyMaths.Remap(direction,0,360,360,0);

		transform.localEulerAngles = new Vector3(0,0,result);

		float scale = (width/50.0f) + 0.2f; 

		transform.localScale = new Vector3 (scale,0.5f,1f);
	}
}
