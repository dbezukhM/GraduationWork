namespace BLL.Settings
{
    public class ProgramSettings
    {
        public const string SectionName = "ProgramSettings";

        public string ApiUrl { get; set; }

        public string ClientUrl { get; set; }

        public string AzureFunctionGetFileUrl { get; set; }

        public string AzureFunctionPostFileUrl { get; set; }

        public string TemplateFileName { get; set; }
    }
}