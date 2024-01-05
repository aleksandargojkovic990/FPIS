using System.Reflection;
using FPIS_projekat_aplikacija.DatabaseBroker;
using FPIS_projekat_aplikacija.Models;
using Microsoft.Extensions.Configuration;

namespace FPIS_projekat_aplikacija.SystemOperations
{
    public abstract class SystemOperationBase
    {
        protected Broker broker;

        public SystemOperationBase(IConfiguration configuration)
        {
            broker = new Broker(configuration);
        }

        public void ExecuteTemplate(IEntity entity)
        {
            try
            {
                broker.OpenConnection();
                broker.BeginTransaction();
                ExecuteOperation(entity);
                broker.Commit();
            }
            catch (Exception)
            {
                broker.Rollback();
                throw;
            }
            finally
            {
                broker.CloseConnection();
            }
        }

        protected abstract void ExecuteOperation(IEntity entity);
    }
}


