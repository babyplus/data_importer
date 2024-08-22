namespace MyModels
{
  public class Marker{
    public float[] coordinate {get; set;}
    public string description {get; set;}
    public Dictionary<String, String> data {get; set;}
  }

  public class Markers{
    public int timestamp {get; set;}
    public Dictionary<String, Marker> markers {get; set;}
  }
}
