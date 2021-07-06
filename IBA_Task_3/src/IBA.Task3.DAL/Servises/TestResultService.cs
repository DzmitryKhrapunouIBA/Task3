using IBA.Task3.DAL.Context;
using IBA.Task3.DAL.Models;
using IBA.Task3.DAL.Servises.Contracts;

namespace IBA.Task3.DAL.Servises
{
    public class TestResultService : AbstractService<AppContext, TestResult>, ITestResultService
    {
        public TestResultService(AppContext context) : base(context)
        { 
        }
    }
}