using System.Collections.Generic;

/// <summary>
/// Represents the main collection returned by the USGS GeoJSON feed.
/// </summary>
public class FeatureCollection
{
    public List<Feature> Features { get; set; } = new();
}

/// <summary>
/// Represents one earthquake feature from the USGS data.
/// </summary>
public class Feature
{
    public EarthquakeProperties Properties { get; set; } = new();
}

/// <summary>
/// Contains the earthquake information needed for the assignment.
/// </summary>
public class EarthquakeProperties
{
    public string Place { get; set; } = "";

    public double? Mag { get; set; }
}