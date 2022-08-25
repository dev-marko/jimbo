using Forum.Domain.Models;
using Forum.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services.Interfaces
{
    public interface ISubforumService
    {
        List<Subforum> FetchAllSubforums();
        List<Subforum> FetchAllSubforumsByCategory(string category);
        Subforum FetchSubforumById(Guid? id);
        SubforumViewModel FetchSubforumViewModelWithTopicsById(Guid id);
        SubforumViewModel FetchSubforumViewModelWithTopics(Subforum entity);
        SubforumViewModel CreateSubforum(Subforum entity);
        SubforumViewModel UpdateSubforum(Subforum entity);
        SubforumViewModel DeleteSubforum(Subforum entity);
        bool SubforumExists(Guid id);
    }
}
