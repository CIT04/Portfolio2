using DataLayer.Models;
using DataLayer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IActorService
    {
        (IList<Person> products, int count) GetActors(int page, int pageSize);

        IList<ActorsForMediaDTO> GetActorsForMedia(string m_id);
    }
}
