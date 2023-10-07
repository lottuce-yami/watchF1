using System.Linq.Expressions;
using F1Project.Models;

namespace F1Project.Data;

public static class QueryExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> whereClause)
    {
        return condition ? query.Where(whereClause) : query;
    }

    public static void Authorize(this WatchF1Context dbContext, User user)
    {
        var registeredUser = dbContext.Users.Find(user.Id);

        if (registeredUser == null)
        {
            dbContext.Add(user);
        }
        else
        {
            registeredUser.FirstName = user.FirstName;
            registeredUser.LastName = user.LastName;
            registeredUser.Username = user.Username;
            registeredUser.Photo = user.Photo;
        }

        dbContext.SaveChanges();
    }
}