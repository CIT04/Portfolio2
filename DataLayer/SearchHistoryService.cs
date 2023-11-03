using DataLayer.Objects;

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

   
}