namespace BLL.Results
{
    public partial class Result
    {
        public Result(ResultStatus status, IEnumerable<Error> errors)
            : this(status)
        {
            Errors = errors;
        }

        public Result(ResultStatus status, Error error)
            : this(status)
        {
            Errors = new List<Error> { error };
        }

        protected Result(ResultStatus status)
        {
            Status = status;
        }

        public ResultStatus Status { get; }

        public IEnumerable<Error> Errors { get; private set; }

        public IEnumerable<Error> Warnings { get; set; } = Enumerable.Empty<Error>();

        public bool IsSuccess => Status == ResultStatus.Success;

        public bool IsFailed => Status != ResultStatus.Success;

        public Result WithWarnings(params Error[] warnings)
        {
            Warnings = Warnings.Concat(warnings);
            return this;
        }

        public Result WithErrors(params Error[] errors)
        {
            Errors = Errors.Concat(errors);
            return this;
        }
    }

    public class Result<T> : Result
    {
        private readonly T _value;

        public Result(ResultStatus status, T value)
            : base(status)
        {
            _value = value;
        }

        public Result(ResultStatus status, Error error)
            : base(status, error)
        {
        }

        public Result(ResultStatus status, IEnumerable<Error> errors)
            : base(status, errors)
        {
        }

        public T Value
            => IsSuccess
                ? _value
                : throw new InvalidOperationException("A failed result has no value.");

        public new Result<T> WithWarnings(params Error[] warnings)
        {
            Warnings = Warnings.Concat(warnings);
            return this;
        }

        public T GetValueOrDefault()
        {
            return IsSuccess
                ? _value
                : default;
        }
    }
}