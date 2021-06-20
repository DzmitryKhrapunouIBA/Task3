using IBA.Task3.DAL.Models;

namespace IBA.Task3.DAL.Servises
{
    public class TestResultService : AbstractService<Context.AppContext, TestResult>, ITestResultService
    {
        public TestResultService(Context.AppContext context) : base(context)
        {
        }
    }

    public interface ITestResultService : ICrudService<TestResult>
    { }
}