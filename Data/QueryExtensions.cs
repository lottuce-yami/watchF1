using System.Linq.Expressions;

namespace F1Project.Data;

public static class QueryExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> whereClause)
    {
        return condition ? query.Where(whereClause) : query;
    }
}