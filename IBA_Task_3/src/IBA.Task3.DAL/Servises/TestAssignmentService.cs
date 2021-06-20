using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using IBA.Task3.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace IBA.Task3.DAL.Servises
{
    public class TestAssignmentService :  ITestAssignmentService
    {
        private Context.AppContext Context { get; }

        public TestAssignmentService(Context.AppContext context)
        {
            Context = context;
        }

        public virtual async Task<IEnumerable<TestAssignment>> AllAsync(System.Func<TestAssignment, bool> func, CancellationToken token = default, params System.Linq.Expressions.Expression<System.Func<TestAssignment, object>>[] includes)
        {
            var query = Context.Set<TestAssignment>().AsNoTracking();

            if (includes != null && includes.Any())
                query = includes.Aggregate(query, (x, y) => x.Include(y));

            if (func != null)
                query = (IQueryable<TestAssignment>)query.Where(func);

            return await query.ToListAsync(token);
        }

    }

    public interface ITestAssignmentService 
    {
        Task<IEnumerable<TestAssignment>> AllAsync(System.Func<TestAssignment, bool> func, CancellationToken token = default, params System.Linq.Expressions.Expression<System.Func<TestAssignment, object>>[] includes);
    }
}