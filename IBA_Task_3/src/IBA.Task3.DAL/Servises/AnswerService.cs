using IBA.Task3.DAL.Context;
using IBA.Task3.DAL.Models;
using IBA.Task3.DAL.Servises.Contracts;

namespace IBA.Task3.DAL.Servises
{
    public class AnswerService : AbstractService<AppContext, Answer>, IAnswerService
    {
        public AnswerService(AppContext context) : base(context)
        { 
        }
    }
}