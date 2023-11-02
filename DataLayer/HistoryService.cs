using DataLayer.Objects;

namespace DataLayer;

public class HistoryService : IHistoryService
{
    public History? GetHistory(string history)
    {
        var db = new Context();
        return db.History.FirstOrDefault(x => x.M_id == history);

    }

    public (IList<History> products, int count) GetHistories(int page, int pageSize)
    {
        var db = new Context();
        var history =
            db.History
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (history, db.History.Count());
    }
}