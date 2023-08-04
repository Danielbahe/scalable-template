using CSharpFunctionalExtensions;

namespace Scalable.Core.ResultHelpers
{
    public static class Constraints
    {
        public static ConstraintResult AddResult(Result item)
        {
            var myConstraint = new ConstraintResult();
            myConstraint.AddResult(item);
            return myConstraint;
        }

        public static ConstraintResult AddResultIf(bool condition, Result item)
        {
            var myConstraint = new ConstraintResult();
            myConstraint.AddResultIf(condition, item);
            return myConstraint;
        }

        public static ConstraintResult AddResultIf(bool condition, Result itemIfTrue, Result itemIfFalse)
        {
            var myConstraint = new ConstraintResult();
            myConstraint.AddResultIf(condition, itemIfTrue, itemIfFalse);
            return myConstraint;
        }

        public static object Bind(Func<object> p)
        {
            throw new NotImplementedException();
        }

        public static ConstraintResult AddResults(IEnumerable<Result> items)
        {
            var myConstraint = new ConstraintResult();
            myConstraint.AddResults(items);
            return myConstraint;
        }

        public static Result Evaluate()
        {
            return Result.Success();
        }

        public static Result Evaluate(Result result)
        {
            return result;
        }

        public static Result<T> Evaluate<T>(Result<T> result)
        {
            return result;
        }
    }
}