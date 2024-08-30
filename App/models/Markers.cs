namespace MyModels
{
  public class Marker{
    public double[] coordinate {get; set;}
    public string description {get; set;}
    public Dictionary<String, String> data {get; set;}
    public Dictionary<String, String> tags {get; set;}
  }

  public class Markers{
    public long timestamp {get; set;}
    public Dictionary<String, Marker> markers {get; set;}
  }
}
