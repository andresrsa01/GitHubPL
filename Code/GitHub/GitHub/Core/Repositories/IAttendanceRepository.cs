using System.Collections.Generic;
using GitHub.Core.Models;

namespace GitHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string userId);

        Attendance GetAttendance(int gigId, string userId);

        void Add(Attendance attendance);

        void Remove(Attendance attendance);
    }
}