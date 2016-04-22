using System;
using UnityEngine;


public abstract class DataCollector
{
	public string dateTimeCollected;
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
		this.dataLables = new string[]{"wind speed","wind direction","sin wind direction","cos wind direction"}; 
		this.dataValues = new double[]{windSpeed,windDirection,Math.Sin(MyMaths.DegreeToRadian(windDirection)),Math.Cos(MyMaths.DegreeToRadian(windDirection))}; 
		this.dateTimeCollected = DateTime.Now.ToString();
	}
}


public class StationDataCollector : DataCollector
{
	public int zoneId;

	public StationDataCollector(int zoneId, double windSpeed, double windDirection){

		this.zoneId = zoneId;
		this.dataLables = new string[]{"wind speed","wind direction","sin wind direction","cos wind direction"}; 
		this.dataValues = new double[]{windSpeed,windDirection, Math.Sin(MyMaths.DegreeToRadian(windDirection)),Math.Cos(MyMaths.DegreeToRadian(windDirection))}; 
		this.dateTimeCollected = DateTime.Now.ToString();
	}		
}


