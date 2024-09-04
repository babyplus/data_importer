using System.IO;
using CommandLine;
using MyModels;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MyJobs
{
    public class Jobs
    {
        static public void parse(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<JsonVerifyOptions, InfluxdbOptions>(args)
            .MapResult(
                (JsonVerifyOptions opts) => RunAndReturnExitCode(opts),
                (InfluxdbOptions opts) => RunAndReturnExitCode(opts),
                errs => 1
            );
        }

        static private string read_from_file(string input_file)
        {
            try
            {
                return System.IO.File.ReadAllText(input_file);
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error: {e}");
                Environment.Exit(1);
            }
            return "[]";
        }

        static private void print(List<Markers> markers_list)
        {
            foreach (var markers in markers_list)
            {
                var timestamp = markers.timestamp;
                var tags = markers.markers.Keys.ToList();
                foreach (string tag in tags)
                {
                    Marker marker = markers.markers[tag];
                    try
                    {
                        System.Console.WriteLine($"latitude: {marker.coordinate[0]}");
                        System.Console.WriteLine($"longitude: {marker.coordinate[1]}");
                    }
                    catch
                    {
                        System.Console.WriteLine($"Error: The parsing of the coordinates failed.");
                        Environment.Exit(1);
                    }
                    try
                    {
                        foreach (string d in marker.data.Keys.ToList())
                            System.Console.WriteLine($"{d}: {marker.data[d]}");
                    }
                    catch {}
                    System.Console.WriteLine($"description: {marker.description}");
                }
            }
        }

        static public int RunAndReturnExitCode(JsonVerifyOptions opts)
        {
            string json_string = read_from_file(opts.Input);

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();
            
            List<Markers> markers_list = deserializer.Deserialize<List<Markers>>(json_string);
            print(markers_list);
            return 0;
        }

        static private void save(List<Markers> markers_list)
        {
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
                    }
                    catch
                    {
                        System.Console.WriteLine($"Error: The parsing of the coordinates failed.");
                        Environment.Exit(1);
                    }
                }
            }
        }

        static public int RunAndReturnExitCode(InfluxdbOptions opts)
        {

            string json_string = read_from_file(opts.Input);

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();
            
            List<Markers> markers_list = deserializer.Deserialize<List<Markers>>(json_string);

            {
                MyModels.InfluxDBConfig config = new MyModels.InfluxDBConfig();
                config.Token = opts.Token;
                config.Bucket = opts.Bucket;
                config.Org = opts.Org;
                config.Api = opts.Api;
                config.Measurement = opts.Measurement;
                MyModels.InfluxDB db = new MyModels.InfluxDB();
                db.load(config).save(markers_list); 
            }
            return 0;
        }
    }
}

