/// <summary>
/// Classes describing the USGS earthquake GeoJSON feed so it can be
/// deserialized with System.Text.Json (case-insensitive property names).
/// Only the parts of the JSON that we need are modelled.
/// </summary>
public class FeatureCollection
{
    public List<Feature> Features { get; set; }
}

public class Feature
{
    public Properties Properties { get; set; }
}

public class Properties
{
    public string Place { get; set; }
    public double? Mag { get; set; } // nullable: the feed can contain null magnitudes
}
