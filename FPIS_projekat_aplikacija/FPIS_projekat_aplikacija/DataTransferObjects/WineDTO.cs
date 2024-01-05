namespace FPIS_projekat_aplikacija.DataTransferObjects
{
    public class WineDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public WineSortDTO WineSort { get; set; }
        public WineStyleDTO WineStyle { get; set; }
    }
}
