using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IBA.Task3.DAL.Context;
using IBA.Task3.DAL.Models;
using IBA.Task3.DAL.Servises.Contracts;
using Microsoft.EntityFrameworkCore;

namespace IBA.Task3.DAL.Servises
{
    public class UserService : AbstractService<AppContext, User>, IUserService
    {
        public UserService(AppContext context) : base(context)
        {
        }

        public virtual async Task<IEnumerable<NamedEntity>> AllNamedAsync(System.Func<User, bool> func, CancellationToken token = default)
        {
            InitTokenThrow(token);

            var query = Entry;

            if (func != null)
                query = (IQueryable<User>)query.Where(func);

            return await query
                .Select(x => new NamedEntity() { Id = x.Id, Name = x.FirstName + " " + x.LastName + " " + x.SurName })
                .ToListAsync(token);
        }
    }
}