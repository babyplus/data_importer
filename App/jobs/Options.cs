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
            CommandLine.Parser.Default.ParseArguments<JsonOptions>(args)
            .MapResult(
                (JsonOptions opts) => RunAndReturnExitCode(opts),
                errs => 1
            );
        }

        static public int RunAndReturnExitCode(JsonOptions opts)
        {
            var json_string = "[]";
            var input_file = "";

            if(opts.Input is not null)
                input_file = opts.Input;
 
            try
            {
                json_string = File.ReadAllText(input_file);
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error: {e}");
                Environment.Exit(1);
            }

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();
            
            var markers_list = deserializer.Deserialize<List<Markers>>(json_string);
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
            return 0;
        }
    }
}

