using Microsoft.Extensions.Configuration;

namespace ConfigurationApp
{
    public class TextConfigurationSource : IConfigurationSource
    {
        public string FilePath { get; private set; }
        public TextConfigurationSource(string path) {
            FilePath = path;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            string filePath = builder.GetFileProvider().GetFileInfo(FilePath).PhysicalPath;
            return new TextConfigurationProvider(filePath);
        }
    }
}