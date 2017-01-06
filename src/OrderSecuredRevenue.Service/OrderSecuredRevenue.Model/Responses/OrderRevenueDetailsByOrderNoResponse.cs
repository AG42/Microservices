namespace OrderSecuredRevenue.Model.Responses
{
    public class SalesOrderDetailsByOrderNoResponse: BaseResponse
    {
        public SalesOrderDetailsModel SalesOrderDetails { get; set; } = new SalesOrderDetailsModel();
    }
}
