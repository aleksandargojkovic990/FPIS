using FPIS_projekat_aplikacija.Models;
using Microsoft.Data.SqlClient;
using System.Security.Principal;

namespace FPIS_projekat_aplikacija.DatabaseBroker
{
    public class Broker
    {
        private SqlConnection connection;
        private SqlTransaction transaction;
        private string connectionString;

        public Broker(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("FPIS_projekat_DBCon");
            connection = new SqlConnection(connectionString);
        }

        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public void Commit()
        {
            transaction?.Commit();
        }

        public List<object> Get(IEntity entity)
        {
            List<object> result;
            SqlCommand command = new SqlCommand("", connection, transaction);

            command.CommandText = $"SELECT {entity.SelectValues} "
                                    + $"FROM {entity.TableName} {entity.TableAlias} "
                                    + $"{entity.JoinTable} {entity.JoinCondition} "
                                    + entity.GetWhere(entity);

            SqlDataReader reader = command.ExecuteReader();
            result = entity.GetEntities(reader);
            reader.Close();
            return result;
        }

        public IEntity Search(IEntity entity)
        {
            IEntity result;
            SqlCommand command = new SqlCommand("", connection, transaction);

            command.CommandText = $"SELECT {entity.SelectValues} "
                                 + $"FROM {entity.TableName} {entity.TableAlias} "
                                 + $"{entity.JoinTable} {entity.JoinCondition} {entity.Where}";

            foreach (var item in entity.Set)
                command.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);

            SqlDataReader reader = command.ExecuteReader();
            result = entity.GetEntity(reader);
            reader.Close();
            return result;
        }

        public int GetNewId(IEntity e)
        {
            SqlCommand command = new SqlCommand("", connection, transaction);

            command.CommandText = $"SELECT CAST(IDENT_CURRENT('{e.TableName}') AS INT) ";

            object result = command.ExecuteScalar();
            if (result is DBNull)
            {
                return 1;
            }
            else
            {
                return (int)result + 1;
            }
        }

        public void Delete(IEntity e)
        {
            SqlCommand command = new SqlCommand("", connection, transaction);

            command.CommandText = $"DELETE FROM {e.TableName} "
                                + $"WHERE {e.Where}";

            foreach (var item in e.Set)
                command.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);

            if (command.ExecuteNonQuery() == 0)
            {
                throw new Exception("Greška sa bazom!");
            }
        }

        public void OpenConnection()
        {
            connection.Open();
        }

        public void Rollback()
        {
            transaction?.Rollback();
        }

        public int Save(IEntity entity)
        {
            SqlCommand command = new SqlCommand("", connection, transaction);

            command.CommandText = $"INSERT INTO {entity.TableName} " +
                                $"VALUES ({entity.InsertValues}); SELECT SCOPE_IDENTITY();";

            foreach (var item in entity.Set)
                command.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);

            var insertedId = command.ExecuteScalar();

            return insertedId == null || insertedId == DBNull.Value ? 0 : Convert.ToInt32(insertedId);
        }

        public void Update(IEntity e)
        {
            SqlCommand command = new SqlCommand("", connection, transaction);

            command.CommandText = $"UPDATE {e.TableName} "
                                   + e.GetSet(e)
                                   + $" WHERE {e.Where}";

            foreach (var item in e.Set)
                command.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);

            if (command.ExecuteNonQuery() == 0)
            {
                throw new Exception("Greška!");
            }
        }
    }
}
