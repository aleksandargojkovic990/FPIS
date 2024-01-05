using FPIS_projekat_aplikacija.DataTransferObjects;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace FPIS_projekat_aplikacija.Models
{
    public class Wine : IEntity
    {
        private Dictionary<string, object> _set;
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public WineSort WineSort { get; set; }
        public WineStyle WineStyle { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Wine w)
            {
                return w.ID == this.ID;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        [Browsable(false)]
        public string TableName => "[Wine]";

        [Browsable(false)]
        public string InsertValues => "";

        [Browsable(false)]
        public string IdName => "ID";

        [Browsable(false)]
        public string JoinCondition => "";

        [Browsable(false)]
        public string JoinTable => "JOIN WineSort ws ON w.IDSort = ws.ID " +
                                    "JOIN WineStyle wst ON w.IDStyle = wst.ID";

        [Browsable(false)]
        public string TableAlias => "w";

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
                        { "@Name", Name },
                        { "@Price", Price},
                        { "@IDSort", WineSort.ID },
                        { "@IDStyle", WineStyle.ID }
                    };
                }

                return _set;
            }
        }

        [Browsable(false)]
        public object SelectValues => "w.ID" +
                                        ", w.Name" +
                                        ", w.Price" +
                                        ", ws.ID AS IDSort" +
                                        ", ws.Name AS SortName" +
                                        ", wst.ID AS IDStyle" +
                                        ", wst.Name AS StyleName";

        public List<object> GetEntities(SqlDataReader reader)
        {
            List<object> result = new List<object>();
            while (reader.Read())
            {
                result.Add(new WineDTO
                {
                    ID = (int)reader["ID"],
                    Name = (string)reader["Name"],
                    Price = (decimal)reader["Price"],
                    WineSort = new WineSortDTO()
                    {
                        ID = (int)reader["IDSort"],
                        Name = (string)reader["SortName"]
                    },
                    WineStyle = new WineStyleDTO()
                    {
                        ID = (int)reader["IDStyle"],
                        Name = (string)reader["StyleName"]
                    }
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
            string s = ((Wine)entity).ID == 0 ? "" : $" AND ID = {((Wine)entity).ID}"; 
            s += string.IsNullOrWhiteSpace(((Wine)entity).Name) ? "" : $" AND Name = {((Wine)entity).Name}";
            s += ((Wine)entity).Price == 0 ? "" : $" AND Price = {((Wine)entity).Price}";
            s += ((Wine)entity).WineSort == null || ((Wine)entity).WineSort.ID == 0 ? "" : $" AND IDSort = {((Wine)entity).WineSort.ID}";
            s += ((Wine)entity).WineStyle == null || ((Wine)entity).WineStyle.ID == 0 ? "" : $" AND IDStyle = {((Wine)entity).WineStyle.ID}";
            return "WHERE 1=1 " + s;
        }

        public string GetSet(IEntity entity)
        {
            return "";
        }
    }
}
