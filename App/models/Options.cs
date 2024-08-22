using CommandLine;

namespace MyModels
{
    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }
    }

    [Verb("json_parse", HelpText = "Verify the correct reading of the JSON file.")]
    public class JsonOptions : Options
    {
        [Option('i', "input", Required = true, HelpText = "Input file to read.")]
        public string? Input { get; set; }
    }
}


