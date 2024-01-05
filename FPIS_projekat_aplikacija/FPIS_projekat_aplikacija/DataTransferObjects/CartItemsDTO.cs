using FPIS_projekat_aplikacija.Models;

namespace FPIS_projekat_aplikacija.DataTransferObjects
{
    public class CartItemsDTO
    {
        public CartDTO Cart { get; set; }
        public WineDTO Wine { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
