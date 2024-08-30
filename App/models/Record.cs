namespace MyModels
{
  public class Record{
    public long Timestamp {get; set;}
    public double Latitude {get; set;}
    public double Longitude {get; set;}
    public string Description {get; set;}
    public DateTime RFC3339Nano {get; set;}
    public Dictionary<String, String> Data {get; set;}
    public Dictionary<String, String> Tags {get; set;}

    public Record()
    {
        Timestamp = 0;
        Latitude = 0.0;
        Longitude = 0.0;
        Description = "";
        RFC3339Nano = DateTime.UtcNow;
        Data = new Dictionary<String, String>(); 
        Tags = new Dictionary<String, String>(); 
    }
  }
}
