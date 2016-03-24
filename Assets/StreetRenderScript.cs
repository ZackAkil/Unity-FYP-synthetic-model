using UnityEngine;
using System.Collections;

public class StreetRenderScript : MonoBehaviour {


	private WindStreetScript parentZone;
	private ApiCom api;

	public void setStreetRender(float width, float direction){

		float result = MyMaths.Remap(direction,0,360,360,0);

		transform.localEulerAngles = new Vector3(0,0,result);

		float scale = (width/50.0f) + 0.2f; 

		transform.localScale = new Vector3 (scale,0.5f,1f);


	}

	void Start(){

		api = new ApiCom();
		parentZone = GetComponentInParent<WindStreetScript>();

		//getting the perimiter points of zones-- dosnt translate to global world space yet
		Invoke("outputPoints", 1);
	}

	private void outputPoints(){

		var points =  GetComponent<PolygonCollider2D>().points;

		Debug.Log( points[0].ToString() + "," +points[1].ToString() + "," + points[2].ToString() + "," + points[3].ToString());

	}

	void OnMouseDown() {

		Vector3 pos = Input.mousePosition;
		pos.z = 10;
		pos = Camera.main.ScreenToWorldPoint(pos);

		api.submitScoutData(pos.x,pos.y,parentZone.getWindSpeed(),parentZone.getWindDirection());

		Debug.Log("scout data fire in zone at:" + pos.ToString() );
	}

}
