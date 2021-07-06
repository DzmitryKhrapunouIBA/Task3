using IBA.Task3.DAL.Context;
using IBA.Task3.DAL.Models;
using IBA.Task3.DAL.Servises.Contracts;

namespace IBA.Task3.DAL.Servises
{
    public class UserAnswerService : AbstractService<AppContext, UserAnswer>, IUserAnswerService
    {
        public UserAnswerService(AppContext context) : base(context)
        {
        }
    }
}