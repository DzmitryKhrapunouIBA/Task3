using IBA.Task3.DAL.Context;
using IBA.Task3.DAL.Models;
using IBA.Task3.DAL.Servises.Contracts;

namespace IBA.Task3.DAL.Servises
{
    public class QuestionService : AbstractService<AppContext, Question>, IQuestionService
    {
        public QuestionService(AppContext context) : base(context)
        { 
        }
    }
}