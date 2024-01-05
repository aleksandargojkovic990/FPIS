using FPIS_projekat_aplikacija.Models;

namespace FPIS_projekat_aplikacija.SystemOperations
{
    public class AddSO : SystemOperationBase
    {
        public AddSO(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ExecuteOperation(IEntity entity)
        {
            int id = broker.Save(entity);

            if(entity is Cart)
            {
                foreach(CartItems ci in ((Cart)entity).CartItems)
                {
                    ci.Cart = new Cart() { ID = id };
                    broker.Save(ci);
                }
            }
        }
    }
}
