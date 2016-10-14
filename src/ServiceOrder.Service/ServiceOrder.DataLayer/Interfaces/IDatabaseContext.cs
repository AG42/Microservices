using System.Collections.Generic;
using ServiceOrder.DataLayer.Entities.Datalake;
using System.Threading.Tasks;

namespace ServiceOrder.DataLayer.Interfaces
{
    public interface IDatabaseContext
    {
        /// <summary>
        /// Get all master service order details.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <returns></returns>
        Task<IEnumerable<SM01>> GetServiceOrderAsync(string companyCode);
        /// <summary>
        /// Gets the customer details.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="customerCode">The customer code.</param>
        /// <returns></returns>
        Task<SL01> GetCustomerDetailsAsync(string companyCode, string customerCode);

        /// <summary>
        /// Gets the service order with customer details based on service order no.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="serviceOrderNo">The service order no.</param>
        /// <returns></returns>
        Task<IEnumerable<SM01>> GetServiceOrderAsync(string companyCode, string serviceOrderNo);
        /// <summary>
        /// Gets the service order activity lines/ labour details based on service order no.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="serviceOrderNo">The service order no.</param>
        /// <returns></returns>
        Task<IEnumerable<SM03>> GetServiceOrderActivityLinesAsync(string companyCode, string serviceOrderNo);

        /// <summary>
        /// Gets the service order cost lines based on service order no.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="serviceOrderNo">The service order no.</param>
        /// <returns></returns>
        Task<IEnumerable<SM05>> GetServiceOrderCostLinesAsync(string companyCode, string serviceOrderNo);

        /// <summary>
        /// Gets the service order material lines based on service order no.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="serviceOrderNo">The service order no.</param>
        /// <returns></returns>
        Task<IEnumerable<SM07>> GetServiceOrderMaterialLinesAsync(string companyCode, string serviceOrderNo);

        /// <summary>
        /// Gets the service orders by bill to customer code.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="invoiceCustomerCode">The bill to customer code.</param>
        /// <returns></returns>
        Task<IEnumerable<SM01>> GetServiceOrdersByInvoiceCustomerCodeAsync(string companyCode, string invoiceCustomerCode);
        /// <summary>
        /// Gets the service order activity lines by service order no. and bill to customer code.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="serviceOrderNo">The service order no.</param>
        /// <param name="invoiceCustomerCode">The bill to customer code.</param>
        /// <returns></returns>
        Task<IEnumerable<SM03>> GetServiceOrderActivityLinesByInvoiceCustomerCodeAsync(string companyCode, string serviceOrderNo, string invoiceCustomerCode);
        /// <summary>
        /// Gets the service order cost lines by service order no. and bill to customer code.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="serviceOrderNo">The service order no.</param>
        /// <param name="invoiceCustomerCode">The bill to customer code.</param>
        /// <returns></returns>
        Task<IEnumerable<SM05>> GetServiceOrderCostLinesByInvoiceCustomerCodeAsync(string companyCode, string serviceOrderNo, string invoiceCustomerCode);
        /// <summary>
        /// Gets the service order material lines by service order no. and bill to customer code.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="serviceOrderNo">The service order no.</param>
        /// <param name="invoiceCustomerCode">The bill to customer code.</param>
        /// <returns></returns>
        Task<IEnumerable<SM07>> GetServiceOrderMaterialLinesByInvoiceCustomerCodeAsync(string companyCode, string serviceOrderNo, string invoiceCustomerCode);

        /// <summary>
        /// Gets the service order by invoice number.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="invoiceNumber">The invoice number.</param>
        /// <returns></returns>
        Task<IEnumerable<SM01>> GetServiceOrderByInvoiceNumberAsync(string companyCode, string invoiceNumber);
        /// <summary>
        /// Gets the service order activity lines by service order no. and invoice number.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="serviceOrderNo">The service order no.</param>
        /// <param name="invoiceNumber">The invoice number.</param>
        /// <returns></returns>
        Task<IEnumerable<SM03>> GetServiceOrderActivityLinesByInvoiceNumberAsync(string companyCode, string serviceOrderNo, string invoiceNumber);
        /// <summary>
        /// Gets the service order cost lines by service order no. and invoice number.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="serviceOrderNo">The service order no.</param>
        /// <param name="invoiceNumber">The invoice number.</param>
        /// <returns></returns>
        Task<IEnumerable<SM05>> GetServiceOrderCostLinesByInvoiceNumberAsync(string companyCode, string serviceOrderNo, string invoiceNumber);
        /// <summary>
        /// Gets the service order material lines by service order no. and invoice number.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="serviceOrderNo">The service order no.</param>
        /// <param name="invoiceNumber">The invoice number.</param>
        /// <returns></returns>
        Task<IEnumerable<SM07>> GetServiceOrderMaterialLinesByInvoiceNumberAsync(string companyCode, string serviceOrderNo, string invoiceNumber);

    }   
}
