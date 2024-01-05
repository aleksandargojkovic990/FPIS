using Microsoft.Data.SqlClient;

namespace FPIS_projekat_aplikacija.Models
{
    public interface IEntity
    {
        string TableName { get; }
        string InsertValues { get; }
        string IdName { get; }
        string JoinCondition { get; }
        string JoinTable { get; }
        string TableAlias { get; }
        string Where { get; }
        Dictionary<string, object> Set { get; }
        object SelectValues { get; }
        List<object> GetEntities(SqlDataReader reader);
        IEntity GetEntity(SqlDataReader reader);
        string GetWhere(IEntity entity);
        string GetSet(IEntity e);
    }
}
