namespace MVCSession01.Models
{
    // Complex type bound from the QUERY STRING:
    //   /Products/Search?Term=lap&MinPrice=100&Page=2
    public class ProductFilter
    {
        public string? Term { get; set; }
        public decimal? MinPrice { get; set; }
        public int Page { get; set; }
    }
}
