using System;
using UnityEngine;


public abstract class DataCollector
{
	public DateTime dateTimeCollected { get; set; }
	public string apiKey { get; set; }
	public double windSpeed { get; set; }
	public double windDirection { get; set; }
	
	protected WWWForm getBasePostData(){
	
		WWWForm formData = new WWWForm();

		formData.AddField( "apiKey", this.apiKey );
		formData.AddField( "dateTimeCollected", this.dateTimeCollected.ToString() );
		formData.AddField( "windSpeed", this.windSpeed.ToString() );
		formData.AddField( "windDirection", this.windDirection.ToString());

		return formData;
	}

	public abstract WWWForm getPostData();
}


public class ScoutDataCollector : DataCollector
{
	public double longitude { get; set; }
	public double latitude { get; set; }

	public override  WWWForm getPostData(){

		WWWForm formData = getBasePostData();

		formData.AddField( "longitude", this.longitude.ToString() );
		formData.AddField( "latitude", this.latitude.ToString() );

		return formData;
	}
}


public class StationDataCollector : DataCollector
{
	public int zoneId { get; set; }

	public override  WWWForm getPostData(){

		WWWForm formData = getBasePostData();

		formData.AddField( "zoneId", this.zoneId.ToString() );

		return formData;
	}
}


