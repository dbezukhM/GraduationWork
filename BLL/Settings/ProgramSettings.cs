namespace BLL.Settings
{
    public class ProgramSettings
    {
        public const string SectionName = "ProgramSettings";

        public string ApiUrl { get; set; }

        public string ClientUrl { get; set; }

        public string AzureFunctionUrl { get; set; }

        public string TemplateFileName { get; set; }
    }
}