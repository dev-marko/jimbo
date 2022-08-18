using Forum.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Repository.Interfaces
{
    public interface ISubforumRepository
    {
        IEnumerable<Subforum> FetchAllSubforums();
        IEnumerable<Subforum> FetchAllSubforumsByCategory(string category);
        Subforum FetchSubforumById(Guid? id);
        Subforum Insert(Subforum entity);
        Subforum Update(Subforum entity);
        Subforum Delete(Subforum entity);
    }
}
