using IBA.Task3.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IBA.Task3.DAL.Servises.Contracts
{
    public interface IUserService : ICrudService<User>
    {
        Task<IEnumerable<NamedEntity>> AllNamedAsync(Func<User, bool> func, CancellationToken token = default);
    }
}
