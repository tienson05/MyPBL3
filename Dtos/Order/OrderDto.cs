using HeThongMoiGioiDoCu.Dtos.OrderDetail;

namespace AgainPBL3.Dtos.Order
{
    public class OrderDto
    {
        public int BuyerID { get; set; }
        public int VendorID { get; set; }
        public decimal TotalPrice { get; set; }
        public string PayMethod { get; set; } = string.Empty;
        public string CancelReason { get; set; } = string.Empty;
        public List<CreateOrderDetailDto> OrderDetails { get; set; }
    }
}
