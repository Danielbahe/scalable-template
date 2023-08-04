using CSharpFunctionalExtensions;

namespace Scalable.Core.ResultHelpers
{
    public class ConstraintResult
    {
        private readonly List<Result> _resultsCollection;

        internal ConstraintResult()
        {
            _resultsCollection = new List<Result>();
        }

        public ConstraintResult AddResult(Result item)
        {
            _resultsCollection.Add(item);
            return this;
        }

        public ConstraintResult AddResultIf(bool condition, Result item)
        {
            if (!condition)
            {
                return this;
            }

            return AddResult(item);
        }

        public ConstraintResult AddResultIf(bool condition, Result itemIfTrue, Result itemIfFalse)
        {
            if (condition)
            {
                return AddResult(itemIfTrue);
            }

            return AddResult(itemIfFalse);
        }

        public ConstraintResult AddResults(IEnumerable<Result> items)
        {
            if (items == null)
            {
                return this;
            }

            _resultsCollection.AddRange(items);
            return this;
        }

        public Result Combine()
        {
            return Result.Combine(_resultsCollection, errorMessagesSeparator: Environment.NewLine);
        }

        public Result<T> CombineIn<T>(T value)
        {
            var combinedResult = Combine();
            if (combinedResult.IsFailure)
            {
                return Result.Failure<T>(combinedResult.Error);
            }

            return Result.Success(value);
        }
    }
}