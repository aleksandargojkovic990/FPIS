using FPIS_projekat_aplikacija.Models;

namespace FPIS_projekat_aplikacija.SystemOperations
{
    public class UpdateSO : SystemOperationBase
    {
        public UpdateSO(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ExecuteOperation(IEntity entity)
        {
            broker.Update(entity);
        }
    }
}
