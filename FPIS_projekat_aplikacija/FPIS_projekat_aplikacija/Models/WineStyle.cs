using FPIS_projekat_aplikacija.DataTransferObjects;
using Microsoft.Data.SqlClient;
using System.ComponentModel;

namespace FPIS_projekat_aplikacija.Models
{
    public class WineStyle : IEntity
    {
        private Dictionary<string, object> _set;
        public int ID { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is WineStyle wst)
            {
                return wst.ID == this.ID;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        [Browsable(false)]
        public string TableName => "[WineStyle]";

        [Browsable(false)]
        public string InsertValues => "";

        [Browsable(false)]
        public string IdName => "ID";

        [Browsable(false)]
        public string JoinCondition => "";

        [Browsable(false)]
        public string JoinTable => "";

        [Browsable(false)]
        public string TableAlias => "wst";

        [Browsable(false)]
        public string Where => $"ID = @ID";

        [Browsable(false)]
        public Dictionary<string, object> Set
        {
            get
            {
                if (_set == null)
                {
                    _set = new Dictionary<string, object>
                    {
                        { "@ID", ID },
                        { "@Name", Name }
                    };
                }

                return _set;
            }
        }

        [Browsable(false)]
        public object SelectValues => "wst.ID"
                                       + ", wst.Name";

        public List<object> GetEntities(SqlDataReader reader)
        {
            List<object> result = new List<object>();
            while (reader.Read())
            {
                result.Add(new WineStyleDTO
                {
                    ID = (int)reader["ID"],
                    Name = (string)reader["Name"]
                });
            }
            return result;
        }

        public IEntity GetEntity(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public string GetWhere(IEntity entity)
        {
            string s = ((WineStyle)entity).ID == 0 ? "" : $" AND wst.ID = {((WineStyle)entity).ID}";
            return $"WHERE 1=1 " + s;
        }

        public string GetSet(IEntity entity)
        {
            return "";
        }
    }
}
