using IBA.Task3.DAL.Models;

namespace IBA.Task3.DAL.Servises
{
    public class AnswerService : AbstractService<Context.AppContext, Answer>, IAnswerService
    {
        public AnswerService(Context.AppContext context) : base(context)
        {
        }
    }

    public interface IAnswerService : ICrudService<Answer>
    { }
}