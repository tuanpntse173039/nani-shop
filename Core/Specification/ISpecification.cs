using System.Linq.Expressions;

namespace Core.Specification
{
    /// <summary>
    /// Represents a specification interface that defines criteria and includes for querying entities.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Gets the criteria expression for the specification.
        /// </summary>
        Expression<Func<T, bool>> Criteria { get; }

        /// <summary>
        /// Gets the list of include expressions for the specification.
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }
    }
}