using FPIS_projekat_aplikacija.DataTransferObjects;
using Microsoft.Data.SqlClient;
using System.ComponentModel;

namespace FPIS_projekat_aplikacija.Models
{
    public class CartItems : IEntity
    {
        private Dictionary<string, object> _set;
        public Cart Cart { get; set; }
        public Wine Wine { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is CartItems ci)
            {
                return ci.Cart.ID == this.Cart.ID && ci.Wine.ID == this.Wine.ID;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{Cart.ID} {Wine.ID}";
        }

        [Browsable(false)]
        public string TableName => "[CartItems]";

        [Browsable(false)]
        public string InsertValues => "@IDCart" +
                                        ", @IDWine" +
                                        ", @Quantity" +
                                        ", @Total";

        [Browsable(false)]
        public string IdName => "IDCart, IDWine";

        [Browsable(false)]
        public string JoinCondition => "";

        [Browsable(false)]
        public string JoinTable => "JOIN Cart c ON ci.IDCart  = c.ID " +
                                    "JOIN Wine w ON ci.IDWine = w.ID " +
                                    "JOIN WineSort ws ON w.IDSort = ws.ID " +
                                    "JOIN WineStyle wst ON w.IDStyle = wst.ID";

        [Browsable(false)]
        public string TableAlias => "ci";

        [Browsable(false)]
        public string Where => $"IDCart = @IDCart AND IDWine = @IDWine";

        [Browsable(false)]
        public Dictionary<string, object> Set
        {
            get
            {
                if (_set == null)
                {
                    _set = new Dictionary<string, object>
                    {
                        { "@IDCart", Cart.ID },
                        { "@IDWine", Wine.ID },
                        { "@Quantity", Quantity },
                        { "@Total", Total }
                    };
                }

                return _set;
            }
        }

        [Browsable(false)]
        public object SelectValues => "ci.IDCart AS IDCart" +
                                        ", ci.IDWine AS IDWine" +
                                        ", ci.Quantity AS Quantity" +
                                        ", ci.Total AS Total" +
                                        ", c.Total AS TotalCart" +
                                        ", w.Name AS Name" +
                                        ", w.Price AS Price" +
                                        ", ws.ID AS IDSort" +
                                        ", ws.Name AS SortName" +
                                        ", wst.ID AS IDStyle" +
                                        ", wst.Name AS StyleName";

        public List<object> GetEntities(SqlDataReader reader)
        {
            List<object> result = new List<object>();
            while (reader.Read())
            {
                result.Add(new CartItemsDTO
                {
                    Cart = new CartDTO()
                    {
                        ID = (int)reader["IDCart"],
                        Total = (decimal)reader["TotalCart"]
                    },
                    Wine = new WineDTO()
                    {
                        ID = (int)reader["IDWine"],
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
                    },
                    Quantity = (int)reader["Quantity"],
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
