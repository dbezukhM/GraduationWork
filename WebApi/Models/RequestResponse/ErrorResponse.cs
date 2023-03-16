using Newtonsoft.Json;

namespace WebApi.Models.RequestResponse
{
    public class ErrorResponse
    {
        public IEnumerable<ErrorModel> Errors { get; set; } = Enumerable.Empty<ErrorModel>();

        public IEnumerable<ErrorModel> Warnings { get; set; } = Enumerable.Empty<ErrorModel>();

        public ErrorResponse WithErrors(params ErrorModel[] errors)
        {
            Errors = Errors.Concat(errors);
            return this;
        }

        public ErrorResponse WithWarnings(params ErrorModel[] warnings)
        {
            Warnings = Warnings.Concat(warnings);
            return this;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public class ErrorModel
        {
            public string Code { get; set; }

            public string Message { get; set; }

            public string FieldName { get; set; }

            public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

            public IEnumerable<ErrorModel> ToEnumerable()
            {
                yield return this;
            }
        }
    }
}