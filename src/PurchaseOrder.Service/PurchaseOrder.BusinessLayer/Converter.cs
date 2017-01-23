using PurchaseOrder.Common.Enum;
using PurchaseOrder.DataLayer.Entities.Datalake;
using PurchaseOrder.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PurchaseOrder.BusinessLayer
{
  public  class Converter
    {
        public static PurchaseOrderModel ConvertToPurchaseOrderModel(dynamic purchaseOrderPc01, string companyCode)
        {
            int outPurOrdTypePc01002;
            Int32.TryParse(purchaseOrderPc01.Pc01002, out outPurOrdTypePc01002);
            int outPurOrdPrintStatusPc01007;
            Int32.TryParse(purchaseOrderPc01.Pc01007, out outPurOrdPrintStatusPc01007);
            int outPurOrdPrintStatusPc01008;
            Int32.TryParse(purchaseOrderPc01.Pc01008, out outPurOrdPrintStatusPc01008);
            int outPurOrdPrintStatusPc01009;
            Int32.TryParse(purchaseOrderPc01.Pc01009, out outPurOrdPrintStatusPc01009);
            int outLinkToProjectPc01068;
            Int32.TryParse(purchaseOrderPc01.Pc01068, out outLinkToProjectPc01068);

            return new PurchaseOrderModel()
            {
                PurchaseOrderNo = purchaseOrderPc01.Pc01001,
                OrderType = ((OrderType)(outPurOrdTypePc01002)).ToString().Replace("_", " "),
                SupplierCode=purchaseOrderPc01.Pc01003,
                CustomerCodeDelivery=purchaseOrderPc01.Pc01004,               
                Remarks=purchaseOrderPc01.Pc01005+" "+ purchaseOrderPc01.Pc01006,
                PrintStatusPurchaseOrder= ((PrintStatus)(outPurOrdPrintStatusPc01007)).ToString().Replace("_"," "),
                PrintStatusRemindConfirm= ((PrintStatus)(outPurOrdPrintStatusPc01008)).ToString().Replace("_", " "),
                PrintStatusRemindShipment= ((PrintStatus)(outPurOrdPrintStatusPc01009)).ToString().Replace("_", " "),
                InvoiceEntered = purchaseOrderPc01.Pc01011,
                PaymentTerms = purchaseOrderPc01.Pc01012,
                DeliveryTerms = purchaseOrderPc01.Pc01013,
                DeliveryMethod = purchaseOrderPc01.Pc01014,
                OrderDate = purchaseOrderPc01.Pc01015,
                DeliveryDate = purchaseOrderPc01.Pc01016,
                OrderDiscount = string.Format("{0:0.00}",Convert.ToDecimal(purchaseOrderPc01.Pc01019)),
                OrderValue = string.Format("{0:0.00}", Convert.ToDecimal(purchaseOrderPc01.Pc01020)),
                CurrncyCode = purchaseOrderPc01.Pc01022,
                PurchaseCode= purchaseOrderPc01.Pc01046,
                ProjectNumber = purchaseOrderPc01.Pc01056,
                CustomerPurchaseOrderNo = purchaseOrderPc01.Pc01058,
                CustomerRequestNo = purchaseOrderPc01.Pc01062,
                LinkedToProject = ((ProjectLinkedOptions)(outLinkToProjectPc01068)).ToString().Replace("_", " ")
            };
        }

        public static List<PurchaseOrderModel> ConvertToPurchaseOrderList(IEnumerable<Pc01> purchaseOrderPc01, string companyCode)
        {
            var purchaseOrderList = new List<PurchaseOrderModel>();
            foreach (var purchaseOrderModel in purchaseOrderPc01)
            {
                purchaseOrderList.Add(ConvertToPurchaseOrderModel(purchaseOrderModel, companyCode));
            }
            return purchaseOrderList;
        }

        public static List<PurchaseOrderCustomerModel> ConvertToPurchaseOrderCustomerModel(IEnumerable<Pc04> purchaseOrderCustomers, string companyCode)
        {
            var purchaseOrderCustomerModels = new List<PurchaseOrderCustomerModel>();
            foreach (var purchaseOrderCustomer in purchaseOrderCustomers)
            {
                var purchaseOrderCustomerModel = purchaseOrderCustomerModels.Any()? purchaseOrderCustomerModels.First(po => po.CustomerName.Equals(purchaseOrderCustomer.Pc04002)) : null;
                if (purchaseOrderCustomerModel == null)
                {
                    purchaseOrderCustomerModel = new PurchaseOrderCustomerModel
                    {
                        CustomerName = purchaseOrderCustomer.Pc04002,
                        TelephoneNo = purchaseOrderCustomer.Pc04008,
                        PurchaseOrders = new List<PurchaseOrderModel>()
                    };
                    purchaseOrderCustomerModels.Add(purchaseOrderCustomerModel);
                }
                purchaseOrderCustomerModel.PurchaseOrders.Add(ConvertToPurchaseOrderModel(purchaseOrderCustomer,companyCode));
            }
            return purchaseOrderCustomerModels;
        }
    }
}
