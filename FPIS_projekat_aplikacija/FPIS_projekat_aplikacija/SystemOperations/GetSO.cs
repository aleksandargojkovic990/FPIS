using FPIS_projekat_aplikacija.Models;

namespace FPIS_projekat_aplikacija.SystemOperations
{
    public class GetSO<T> : SystemOperationBase
    {
        public GetSO(IConfiguration configuration) : base(configuration)
        {
        }

        public List<T> Result { get; private set; }

        protected override void ExecuteOperation(IEntity entity)
        {
            Result = broker.Get(entity).Cast<T>().ToList();
        }
    }
}
