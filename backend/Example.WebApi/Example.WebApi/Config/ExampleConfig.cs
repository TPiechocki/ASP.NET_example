using System.ComponentModel.DataAnnotations;

namespace Example.WebApi.Config
{
    internal interface IExampleConfig
    {
        public string JwtSigningKey { get; }
    }

    internal class ExampleConfig : IExampleConfig
    {
        public static string ConfigurationPrefix = "Example";

        [Required] 
        public string JwtSigningKey { get; set; } = null!;
    }
}