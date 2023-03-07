namespace BLL.Results
{
    public partial class Result
    {
        public static Result Success()
        {
            return new Result(ResultStatus.Success);
        }

        public static Task<Result> SuccessTask()
        {
            return Task.FromResult(new Result(ResultStatus.Success));
        }

        public static Result<T> Success<T>(T value)
        {
            return new Result<T>(ResultStatus.Success, value);
        }

        public static Task<Result<T>> SuccessTask<T>(T value)
        {
            return Task.FromResult(Success(value));
        }

        public static Result ValidationError(Error error)
        {
            return new Result(ResultStatus.ValidationError, error);
        }

        public static Task<Result> ValidationErrorTask(Error error)
        {
            return Task.FromResult(ValidationError(error));
        }

        public static Result ValidationError(string code, string message)
        {
            return new Result(ResultStatus.ValidationError, new Error(code, message));
        }

        public static Task<Result> ValidationErrorTask(string code, string message)
        {
            return Task.FromResult(ValidationError(code, message));
        }

        public static Result ValidationError(IEnumerable<Error> errors)
        {
            return new Result(ResultStatus.ValidationError, errors);
        }

        public static Task<Result> ValidationErrorTask(IEnumerable<Error> errors)
        {
            return Task.FromResult(ValidationError(errors));
        }

        public static Result<T> ValidationError<T>(Error error)
        {
            return new Result<T>(ResultStatus.ValidationError, error);
        }

        public static Result<T> ValidationError<T>(string code, string message)
        {
            return new Result<T>(ResultStatus.ValidationError, new Error(code, message));
        }

        public static Result<T> ValidationError<T>(IEnumerable<Error> errors)
        {
            return new Result<T>(ResultStatus.ValidationError, errors);
        }

        public static Result NotFound(Error error)
        {
            return new Result(ResultStatus.NotFound, error);
        }

        public static Result<T> NotFound<T>(Error error)
        {
            return new Result<T>(ResultStatus.NotFound, error);
        }

        public static Result Failure(Error error)
        {
            return new Result(ResultStatus.Failed, error);
        }

        public static Result<T> Failure<T>(Error error)
        {
            return new Result<T>(ResultStatus.Failed, error);
        }

        public static Result Unauthorized(Error error)
        {
            return new Result(ResultStatus.Unauthorized, error);
        }

        public static Result<T> Unauthorized<T>(Error error)
        {
            return new Result<T>(ResultStatus.Unauthorized, error);
        }

        public static Result Unauthorized(IEnumerable<Error> errors)
        {
            return new Result(ResultStatus.Unauthorized, errors);
        }

        public static Result<T> Unauthorized<T>(IEnumerable<Error> errors)
        {
            return new Result<T>(ResultStatus.Unauthorized, errors);
        }

        public static Result Forbidden(IEnumerable<Error> errors)
        {
            return new Result(ResultStatus.Forbidden, errors);
        }

        public static Result FromResult<TR>(Result<TR> result)
        {
            return new Result(result.Status, result.Errors).WithWarnings(result.Warnings.ToArray());
        }

        public static Result<T> FromResult<T>(Result result, T value)
        {
            return new Result<T>(result.Status, value) { Errors = result.Errors, Warnings = result.Warnings };
        }

        public static Result<T> FailureFromResult<T>(Result result)
        {
            return new Result<T>(ResultStatus.Failed, result.Errors);
        }

        public static Result<T> FromErrorResult<T>(Result result)
        {
            return new Result<T>(result.Status, result.Errors).WithWarnings(result.Warnings.ToArray());
        }

        public static Result FromErrorResult(Result result)
        {
            return new Result(result.Status, result.Errors).WithWarnings(result.Warnings.ToArray());
        }
    }
}