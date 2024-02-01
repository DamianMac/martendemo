using System.Linq.Expressions;
using Marten;
using MartenApi.Model;

namespace MartenApi.Extensions;

public static class DataExtensions
{
    public static Page<T> RunPageQueryOrderedDescending<T, TKey>(
        this IDocumentSession session,
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, TKey>> orderBy,
        int page,
        int pageSize = 20)
    {
        var skip = pageSize * (page - 1);
        var query = session.Query<T>().Stats(out var stats)
            .Where(predicate)
            .OrderByDescending(orderBy)
            .Skip(skip).Take(pageSize);
        var results = query.ToList();

        var totalResults = Convert.ToInt32(stats.TotalResults);
        var totalPages = (int)Math.Ceiling(stats.TotalResults / (double)pageSize);

        var pageResult = new Page<T>(results, totalResults, totalPages, page);
        return pageResult;
    }

    public static Page<T> RunPageQueryOrdered<T, TKey>(
        this IDocumentSession session,
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, TKey>> orderBy,
        int page,
        int pageSize = 20)
    {
        var skip = pageSize * (page - 1);
        var query = session.Query<T>().Stats(out var stats)
            .Where(predicate)
            .OrderBy(orderBy)
            .Skip(skip).Take(pageSize);
        var results = query.ToList();

        var totalResults = Convert.ToInt32(stats.TotalResults);
        var totalPages = (int)Math.Ceiling(stats.TotalResults / (double)pageSize);

        var pageResult = new Page<T>(results, totalResults, totalPages, page);
        return pageResult;
    }
}