using IBA.Task3.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace IBA.Task3.DAL.Servises.Contracts
{
    public interface ITestAssignmentService
    {
        Task<IEnumerable<TestAssignment>> AllAsync(Func<TestAssignment, bool> func, CancellationToken token = default, params Expression<Func<TestAssignment, object>>[] includes);
    }
}
