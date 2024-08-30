using CommandLine;

namespace MyModels
{
    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }
    }

    [Verb("json_verify", HelpText = "Verify the correct reading of the JSON file.")]
    public class JsonVerifyOptions : Options
    {
        [Option('i', "input", Required = true, HelpText = "Input file to read.")]
        public string? Input { get; set; }
    }

    [Verb("influxdb", HelpText = "Parse the JSON data and execute an import operation to transfer the information into the database.")]
    public class InfluxdbOptions : Options
    {
        [Option('i', "input", Required = true, HelpText = "Input file to read.")]
        public string? Input { get; set; }
        [Option('t', "token", Required = true, HelpText = "The api token for access.")]
        public string? Token { get; set; } 
        [Option('b', "bucket", Required = true, HelpText = "Named location where time series data is stored.")]
        public string? Bucket { get; set; }  
        [Option('O', "org", Required = true, HelpText = "The organization.")]
        public string? Org { get; set; } 
        [Option('a', "api", Required = true, HelpText = "The api interface for access.")]
        public string? Api { get; set; } 
        [Option('T', "timezone", Required = true, HelpText = "Timezone")]
        public string? Timezone { get; set; } 
        [Option('m', "measurement", Required = true, HelpText = "Logical grouping for time series data.")]
        public string? Measurement { get; set; } 

    }

}


