using System;


[Serializable]
	public abstract class DataCollector
	{
		public DateTime dateTimeCollected { get; set; }
		public string apiKey { get; set; }
		public double windSpeed { get; set; }
		public double windDirection { get; set; }
	}

[Serializable]
	public class ScoutDataCollector : DataCollector
	{
		public double longitude { get; set; }
		public double latitude { get; set; }
	}

[Serializable]
	public class StationDataCollector : DataCollector
	{
		public int zoneId { get; set; }
	}


