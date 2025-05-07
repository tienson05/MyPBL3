namespace AgainPBL3.Dtos.Product
{
    public class GetProductListDto
    {
        public int? category_id { get; set; }
        public int? user_id { get; set; }
        public int page_number { get; set; }
        public int page_size { get; set; }
        public string? status { get; set; }
        public string? keyword { get; set; }
    }
}
