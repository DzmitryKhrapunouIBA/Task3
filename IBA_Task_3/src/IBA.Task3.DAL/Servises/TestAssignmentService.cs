using IBA.Task3.DAL.Context;
using IBA.Task3.DAL.Models;
using IBA.Task3.DAL.Servises.Contracts;

namespace IBA.Task3.DAL.Servises
{
    public class TestAssignmentService : AbstractService<AppContext, TestAssignment>, ITestAssignmentService
    {
        public TestAssignmentService(AppContext context) : base(context)
        {
        }

        //private Context.AppContext Context { get; }

        //public TestAssignmentService(Context.AppContext context)
        //{
        //    Context = context;
        //}

        //public virtual async Task<IEnumerable<TestAssignment>> AllAsync(Func<TestAssignment, bool> func, CancellationToken token = default, params Expression<Func<TestAssignment, object>>[] includes)
        //{
        //    var query = Context.Set<TestAssignment>().AsNoTracking();

        //    if (includes != null && includes.Any())
        //        query = includes.Aggregate(query, (x, y) => x.Include(y));

        //    if (func != null)
        //        query = (IQueryable<TestAssignment>)query.Where(func);

        //    return await query.ToListAsync(token);
        //}
    }
}