using IBA.Task3.DAL.Models;

namespace IBA.Task3.DAL.Servises
{
    public class QuestionService : AbstractService<Context.AppContext, Question>, IQuestionService
    {
        public QuestionService(Context.AppContext context) : base(context)
        {
        }
    }

    public interface IQuestionService : ICrudService<Question>
    { }
}