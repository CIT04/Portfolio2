using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class SearchHistoryService : ISearchHistoryService
{
    public SearchHistory? GetSearchHistory(int u_id)
    {
        var db = new Context();
        return db.SearchHistory.FirstOrDefault(x => x.U_id == u_id);

    }

    public (IList<SearchHistory> products, int count) GetSearchHistories(int page, int pageSize)
    {
        var db = new Context();
        var searchhistory =
            db.SearchHistory
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (searchhistory, db.SearchHistory.Count());
    }
    public void AddSearchHistory(SearchHistory searchHistory)
    {
        using var db = new Context();

        var xSearchHistory = new SearchHistory
        {
            U_id = searchHistory.U_id,
            Search_string = searchHistory.Search_string,
            Time = searchHistory.Time,
        };

        db.Database.ExecuteSqlInterpolated($"select insert_search_history({xSearchHistory.U_id},{xSearchHistory.Search_string}, {xSearchHistory.Time} )");
        db.SaveChanges();
    }

    public void DeleteSearchHistory(int u_id)
    {
        using var db = new Context();
        db.Database.ExecuteSqlInterpolated($"select delete_search_history_by_uid({u_id})");
        db.SaveChanges();
    }
}