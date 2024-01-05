using FPIS_projekat_aplikacija.Models;

namespace FPIS_projekat_aplikacija.SystemOperations
{
    public class DeleteSO : SystemOperationBase
    {
        public DeleteSO(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ExecuteOperation(IEntity entity)
        {
            broker.Delete(entity);
        }
    }
}
