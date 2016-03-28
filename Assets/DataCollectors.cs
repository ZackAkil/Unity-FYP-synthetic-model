using System;
using UnityEngine;


public abstract class DataCollector
{
	public DateTime dateTimeCollected;
	public string apiKey;
	public string[] dataLables;
	public double[] dataValues;
}


public class ScoutDataCollector : DataCollector
{
	public double longitude;
	public double latitude;

	public ScoutDataCollector(double longitude, double latitude, double windSpeed, double windDirection){

		this.longitude = longitude;
		this.latitude = latitude;
		this.dataLables = new string[]{"windSpeed","windDirection"}; 
		this.dataValues = new double[]{windSpeed,windDirection}; 
		this.dateTimeCollected = DateTime.Now;
	}
}


public class StationDataCollector : DataCollector
{
	public int zoneId;

	public StationDataCollector(int zoneId, double windSpeed, double windDirection){

		this.zoneId = zoneId;
		this.dataLables = new string[]{"windSpeed","windDirection"}; 
		this.dataValues = new double[]{windSpeed,windDirection}; 
		this.dateTimeCollected = DateTime.Now;
	}		
}


