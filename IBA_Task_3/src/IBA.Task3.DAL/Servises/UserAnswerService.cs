using IBA.Task3.DAL.Models;

namespace IBA.Task3.DAL.Servises
{
    public class UserAnswerService : AbstractService<Context.AppContext, UserAnswer>, IUserAnswerService
    {
        public UserAnswerService(Context.AppContext context) : base(context)
        {
        }
    }

    public interface IUserAnswerService : ICrudService<UserAnswer>
    { }
}