using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IActorService
    {
        IList<ActorsForMediaDTO> GetActorsForMedia(int page, int pageSize, string m_id);
    }
}
