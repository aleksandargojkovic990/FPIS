using FPIS_projekat_aplikacija.DataTransferObjects;
using Microsoft.Data.SqlClient;
using System.ComponentModel;

namespace FPIS_projekat_aplikacija.Models
{
    public class Cart : IEntity
    {
        private Dictionary<string, object> _set;
        public int ID { get; set; }
        public decimal Total { get; set; }
        public List<CartItems> CartItems { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Cart c)
            {
                return c.ID == this.ID;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{ID}";
        }

        [Browsable(false)]
        public string TableName => "[Cart]";

        [Browsable(false)]
        public string InsertValues => "@Total";

        [Browsable(false)]
        public string IdName => "ID";

        [Browsable(false)]
        public string JoinCondition => "";

        [Browsable(false)]
        public string JoinTable => "";

        [Browsable(false)]
        public string TableAlias => "c";

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
                        { "@Total", Total }
                    };
                }

                return _set;
            }
        }

        [Browsable(false)]
        public object SelectValues => "c.ID"
                                       + ", c.Total";

        public List<object> GetEntities(SqlDataReader reader)
        {
            List<object> result = new List<object>();
            while (reader.Read())
            {
                result.Add(new CartDTO
                {
                    ID = (int)reader["ID"],
                    Total = (decimal)reader["Total"]
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
            return "";
        }

        public string GetSet(IEntity entity)
        {
            return "";
        }
    }
}
