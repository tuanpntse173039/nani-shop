using System.Linq.Expressions;

namespace Core.Specification
{
    /// <summary>
    /// Represents a base specification for querying entities.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public class BaseSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// Gets the criteria expression used for filtering entities.
        /// </summary>
        public Expression<Func<T, bool>> Criteria { get; } = null!;

        /// <summary>
        /// Gets the list of include expressions used for eager loading related entities.
        /// </summary>
        public List<Expression<Func<T, object>>> Includes { get; } =
            new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; } = null!;

        public Expression<Func<T, object>> OrderByDescending { get; private set; } = null!;

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        public BaseSpecification() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSpecification{T}"/> class with the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria expression used for filtering entities.</param>
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        /// <summary>
        /// Adds an include expression to the list of include expressions.
        /// </summary>
        /// <param name="includeExpression">The include expression used for eager loading related entities.</param>
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}
