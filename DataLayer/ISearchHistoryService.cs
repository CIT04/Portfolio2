using DataLayer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;

public interface ISearchHistoryService
{
    IEnumerable<SearchHistory> GetSearchHistory(int u_id);

    (IList<SearchHistory> products, int count) GetSearchHistories(int page, int pageSize);

    void AddSearchHistory(SearchHistory searchHistory);

    void DeleteSearchHistory(int u_id);

}


