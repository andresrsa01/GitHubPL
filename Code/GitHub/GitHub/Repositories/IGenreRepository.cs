using System.Collections.Generic;
using GitHub.Models;

namespace GitHub.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}