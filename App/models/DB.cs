using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Writes;

namespace MyModels{
    public enum DBType
    {
        InfluxDB
    }

    public class DBConfig
    {
        public string? Api {get; set;}               
    }

    public class InfluxDBConfig : DBConfig
    {
        public string? Token {get; set;}               
        public string? Bucket {get; set;}               
        public string? Org {get; set;}               
        public string? Measurement {get; set;}              
    }
    
    public class DB
    {
        public virtual int save(List<Record> records) {return 0;}
        public int save(List<Markers> markers_list)
        {
            List<Record> records = new List<Record>();
            foreach (var markers in markers_list)
            {
                var tags = markers.markers.Keys.ToList();
                foreach (string tag in tags)
                {
                    try
                    {
                        Marker marker = markers.markers[tag];
                        Record record = new Record();
                        record.Timestamp = markers.timestamp;
                        record.Latitude = marker.coordinate[0];
                        record.Longitude = marker.coordinate[1];
                        record.Data = marker.data;
                        foreach (var _ in marker.tags.Keys)
                            record.Tags.Add(_, marker.tags[_]);
                        if (! record.Tags.ContainsKey("id"))
                            record.Tags.Add("id", tag);
                        record.RFC3339Nano = Converter.UnixTimestampToRFC3339Nano(record.Timestamp);
                        records.Add(record);
                    }
                    catch
                    {
                        System.Console.WriteLine($"Error: The parsing of the coordinates failed.");
                        Environment.Exit(1);
                    }
                }
            }
            this.save(records);
            return 0;
        }
    }


    public class InfluxDB : DB
    {
        private InfluxDBConfig Config;

        public InfluxDB load(InfluxDBConfig config)
        {
            this.Config = config;
            return this;
        }

        public override int save(List<Record> records)
        {
            using(var client = new InfluxDBClient(this.Config.Api, this.Config.Token))
            {
                foreach (var record in records)
                {
                    var point = PointData.Measurement(this.Config.Measurement)
                    .Timestamp(record.RFC3339Nano, WritePrecision.Ns);

                    foreach (var f in record.Data.Keys)
                    {
                        try{
                            point = point.Field(f, Converter.strToDouble(record.Data[f]));
                            continue;
                        }catch{}

                        try{
                            point = point.Field(f, Converter.strToLong(record.Data[f]));
                            continue;
                        }catch{}

                        point = point.Field(f, "Unsupported string");
                    }

                    foreach (var t in record.Tags.Keys)
                        point = point.Tag(t, record.Tags[t]);

                    using (var writeApi = client.GetWriteApi())
                    {
                        writeApi.WritePoint(point, this.Config.Bucket, this.Config.Org);
                    }
                }
            }
            
            return 0;
        }
    }
}

