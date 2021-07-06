using IBA.Task3.DAL.Context;
using IBA.Task3.DAL.Models;
using IBA.Task3.DAL.Servises.Contracts;

namespace IBA.Task3.DAL.Servises
{
    public class TestService : AbstractService<AppContext, Test>, ITestService
    {
        public TestService(AppContext context) : base(context)
        { 
        }
    }
}