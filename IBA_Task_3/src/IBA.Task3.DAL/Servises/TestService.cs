using IBA.Task3.DAL.Models;

namespace IBA.Task3.DAL.Servises
{
    public class TestService : AbstractService<Context.AppContext, Test>, ITestService
    {
        public TestService(Context.AppContext context) : base(context)
        {
        }
    }

    public interface ITestService : ICrudService<Test> { }
}