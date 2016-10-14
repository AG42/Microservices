using ServiceOrder.BusinessLayer.Interfaces;
using ServiceOrder.Common;
using ServiceOrder.Common.Error;
using ServiceOrder.Common.Logger;
using ServiceOrder.DataLayer.Entities.Datalake;
using ServiceOrder.DataLayer.Interfaces;
using ServiceOrder.Model;
using ServiceOrder.Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceOrder.BusinessLayer
{
    public class ServiceOrderManager : IServiceOrderManager
    {
        private readonly IDatabaseContext _databaseContext;
        public ServiceOrderManager(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ServiceOrderByCompanyCodeResponse GetServiceOrderByCompanyCode(string companyCode)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetServiceOrderByCompanyCode :: Custome Input: companyCode: {companyCode}");
            var response = new ServiceOrderByCompanyCodeResponse();
            try
            {
                // Get Serive Order Master table details
                var serviceOrderDetails = _databaseContext.GetServiceOrderAsync(companyCode).Result;
                if (serviceOrderDetails == null || !serviceOrderDetails.Any())
                {
                    ApplicationLogger.InfoLogger("Error: No Data Found.");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    return response;
                }

                foreach (var serviceModel in serviceOrderDetails)
                {
                    ServiceOrderModel serviceOrderModel = Converter.Convert(serviceModel, companyCode);
                    serviceOrderModel.ServiceOrderLineModel = new ServiceOrderLineModel();

                    
                    var activityLineDetails = _databaseContext.GetServiceOrderActivityLinesAsync(companyCode, serviceModel.SM01001).Result;
                    var costDetails = _databaseContext.GetServiceOrderCostLinesAsync(companyCode, serviceModel.SM01001).Result;
                    var materialItemDetails = _databaseContext.GetServiceOrderMaterialLinesAsync(companyCode, serviceModel.SM01001).Result;

                    // Labour/Activity
                    if (activityLineDetails != null)
                    {
                        ApplicationLogger.InfoLogger("Service Order has Activity line details");
                        serviceOrderModel.ServiceOrderLineModel.ServiceOrderLineLaborList.AddRange(
                            Converter.ConvertLabour(activityLineDetails, serviceModel, companyCode));
                        ApplicationLogger.InfoLogger("DM to BM conversion of Activity Line Model is done");
                    }

                    // Cost/Tool                   
                    if (costDetails != null)
                    {
                        ApplicationLogger.InfoLogger("Service Order has cost details");

                        serviceOrderModel.ServiceOrderLineModel.ServiceOrderLineToolList.AddRange(
                            Converter.ConvertTool(costDetails, serviceModel, companyCode));
                        ApplicationLogger.InfoLogger("DM to BM conversion of ConstModel is done");
                    }

                    // Material Item
                    if (materialItemDetails != null)
                    {
                        ApplicationLogger.InfoLogger("Service Order has material item details");
                        serviceOrderModel.ServiceOrderLineModel.ServiceOrderLineMOItemList.AddRange(
                            Converter.ConvertMOItem(materialItemDetails, serviceModel, companyCode));
                        ApplicationLogger.InfoLogger("DM to BM conversion of Material Item Model is done");
                    }

                    response.ServiceOrderModels.Add(serviceOrderModel);
                }


                ApplicationLogger.InfoLogger("All Model conversion done");

            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception occures: {ex.Message}");
                ApplicationLogger.Errorlog(ex.Message, Common.Enum.Category.Business, ex.StackTrace, ex.InnerException);
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }

            return response;
        }

        public ServiceOrderByServiceOrderNoResponse GetServiceOrderByServiceOrderNo(string companyCode, string serviceOrderNo)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetServiceOrderByServiceOrderNo :: Custome Input: companyCode: {companyCode}, serviceOrderNo: {serviceOrderNo}");
            var response = new ServiceOrderByServiceOrderNoResponse();

            try
            {
                // Get Serive Order Master table details
                var serviceOrderDetails = _databaseContext.GetServiceOrderAsync(companyCode, serviceOrderNo).Result.FirstOrDefault();
                if (serviceOrderDetails == null)
                {
                    ApplicationLogger.InfoLogger("Error: No Data Found.");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    return response;
                }

                response.ServiceOrderModel = new Model.ServiceOrderModel();

                response.ServiceOrderModel = Converter.Convert(serviceOrderDetails, companyCode);
                response.ServiceOrderModel.ServiceOrderLineModel = new Model.ServiceOrderLineModel();
                ApplicationLogger.InfoLogger("Data to Business Model of Service Order conversion successful");

                // Get Service Labour/Activity line details
                var activityLineDetails = _databaseContext.GetServiceOrderActivityLinesAsync(companyCode, serviceOrderNo).Result;
                var costDetails = _databaseContext.GetServiceOrderCostLinesAsync(companyCode, serviceOrderNo).Result;
                var materialItemDetails = _databaseContext.GetServiceOrderMaterialLinesAsync(companyCode, serviceOrderNo).Result;
                
                if (activityLineDetails != null)
                {
                    ApplicationLogger.InfoLogger("Service Order has Activity line details");
                    response.ServiceOrderModel.ServiceOrderLineModel.ServiceOrderLineLaborList.AddRange(
                        Converter.ConvertLabour(activityLineDetails, serviceOrderDetails, companyCode));
                    ApplicationLogger.InfoLogger("DM to BM conversion of Activity Line Model is done");
                }

                // Get Service Cost/Tool details
                if (costDetails != null)
                {
                    ApplicationLogger.InfoLogger("Service Order has cost details");

                    response.ServiceOrderModel.ServiceOrderLineModel.ServiceOrderLineToolList.AddRange(
                        Converter.ConvertTool(costDetails, serviceOrderDetails, companyCode));
                    ApplicationLogger.InfoLogger("DM to BM conversion of ConstModel is done");
                }

                // Get Service MaterialItem details
                if (materialItemDetails != null)
                {
                    ApplicationLogger.InfoLogger("Service Order has material item details");
                    response.ServiceOrderModel.ServiceOrderLineModel.ServiceOrderLineMOItemList.AddRange(
                        Converter.ConvertMOItem(materialItemDetails, serviceOrderDetails, companyCode));
                    ApplicationLogger.InfoLogger("DM to BM conversion of Material Item Model is done");
                }

                ApplicationLogger.InfoLogger("All Model conversion done");

            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception occures: {ex.Message}");
                ApplicationLogger.Errorlog(ex.Message, Common.Enum.Category.Business, ex.StackTrace, ex.InnerException);
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }

            return response;
        }

        public ServiceOrderStatusByServiceOrderNoResponse GetServiceOrderStatusByServiceOrderNo(string companyCode, string serviceOrderNo)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetServiceOrderStatusByServiceOrderNo :: Custome Input: companyCode: {companyCode}, serviceOrderNo: {serviceOrderNo}");
            var response = new ServiceOrderStatusByServiceOrderNoResponse();

            try
            {
                // Get Serive Order Master table details
                var serviceOrderDetails = _databaseContext.GetServiceOrderAsync(companyCode, serviceOrderNo).Result.FirstOrDefault();
                if (serviceOrderDetails == null)
                {
                    ApplicationLogger.InfoLogger("Error: No Data Found.");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    return response;
                }

                ApplicationLogger.InfoLogger("Service order details fetch successfully");
                // if(serviceOrderDetails.SM01064)
                response.OrderStatus = serviceOrderDetails.SM01064;
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception occures: {ex.Message}");
                ApplicationLogger.Errorlog(ex.Message, Common.Enum.Category.Business, ex.StackTrace, ex.InnerException);
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }

            return response;
        }

        public ServiceOrderTypeByServiceOrderNoResponse GetServiceOrderTypeByServiceOrderNo(string companyCode, string serviceOrderNo)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetServiceOrderTypeByServiceOrderNo :: Custome Input: companyCode: {companyCode}, serviceOrderNo: {serviceOrderNo}");
            var response = new ServiceOrderTypeByServiceOrderNoResponse();

            try
            {
                // Get Serive Order Master table details
                var serviceOrderDetails = _databaseContext.GetServiceOrderAsync(companyCode, serviceOrderNo).Result.FirstOrDefault();
                if (serviceOrderDetails == null)
                {
                    ApplicationLogger.InfoLogger("Error: No Data Found.");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    return response;
                }

                ApplicationLogger.InfoLogger("Service order details fetch successfully");
                response.OrderType = serviceOrderDetails.SM01097;
            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception occures: {ex.Message}");
                ApplicationLogger.Errorlog(ex.Message, Common.Enum.Category.Business, ex.StackTrace, ex.InnerException);
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }

            return response;
        }

        public ServiceOrderByInvoiceCustomerCodeResponse GetServiceOrderByInvoiceCustomerCode(string companyCode, string invoiceCustomerCode)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetServiceOrderByCompanyCode :: Custome Input: companyCode: {companyCode}");
            var response = new ServiceOrderByInvoiceCustomerCodeResponse();
            try
            {
                // Get Serive Order Master table details
                var serviceOrderDetails = _databaseContext.GetServiceOrdersByInvoiceCustomerCodeAsync(companyCode, invoiceCustomerCode).Result;
                if (serviceOrderDetails == null || !serviceOrderDetails.Any())
                {
                    ApplicationLogger.InfoLogger("Error: No Data Found.");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    return response;
                }

                foreach (var serviceModel in serviceOrderDetails.Take(5))
                {
                    ServiceOrderModel serviceOrderModel = Converter.Convert(serviceModel, companyCode);
                    serviceOrderModel.ServiceOrderLineModel = new ServiceOrderLineModel();

                    // Labour/Activity
                    var activityLineDetails = _databaseContext.GetServiceOrderActivityLinesByInvoiceCustomerCodeAsync(companyCode, serviceModel.SM01001, serviceModel.SM01002).Result;
                    var costDetails = _databaseContext.GetServiceOrderCostLinesByInvoiceCustomerCodeAsync(companyCode, serviceModel.SM01001, serviceModel.SM01002).Result;
                    var materialItemDetails = _databaseContext.GetServiceOrderMaterialLinesByInvoiceCustomerCodeAsync(companyCode, serviceModel.SM01001, serviceModel.SM01002).Result;

                    if (activityLineDetails != null)
                    {
                        ApplicationLogger.InfoLogger("Service Order has Activity line details");
                        serviceOrderModel.ServiceOrderLineModel.ServiceOrderLineLaborList.AddRange(
                            Converter.ConvertLabour(activityLineDetails, serviceModel, companyCode));
                        ApplicationLogger.InfoLogger("DM to BM conversion of Activity Line Model is done");
                    }

                    // Cost/Tool                    
                    if (costDetails != null)
                    {
                        ApplicationLogger.InfoLogger("Service Order has cost details");

                        serviceOrderModel.ServiceOrderLineModel.ServiceOrderLineToolList.AddRange(
                            Converter.ConvertTool(costDetails, serviceModel, companyCode));
                        ApplicationLogger.InfoLogger("DM to BM conversion of ConstModel is done");
                    }

                    // Material Item                    
                    if (materialItemDetails != null)
                    {
                        ApplicationLogger.InfoLogger("Service Order has material item details");
                        serviceOrderModel.ServiceOrderLineModel.ServiceOrderLineMOItemList.AddRange(
                            Converter.ConvertMOItem(materialItemDetails, serviceModel, companyCode));
                        ApplicationLogger.InfoLogger("DM to BM conversion of Material Item Model is done");
                    }

                    response.ServiceOrderModels.Add(serviceOrderModel);
                }

                ApplicationLogger.InfoLogger("All Model conversion done");

            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception occures: {ex.Message}");
                ApplicationLogger.Errorlog(ex.Message, Common.Enum.Category.Business, ex.StackTrace, ex.InnerException);
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }

            return response;
        }

        public ServiceOrderByInvoiceNumberResponse GetServiceOrderByInvoiceNumber(string companyCode, string invoiceNumber)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetServiceOrderByCompanyCode :: Custome Input: companyCode: {companyCode}, invoiceNumber: {invoiceNumber}");
            var response = new ServiceOrderByInvoiceNumberResponse();
            try
            {
                // Get Serive Order Master table details
                var serviceOrderDetails = _databaseContext.GetServiceOrderByInvoiceNumberAsync(companyCode, invoiceNumber).Result;
                if (serviceOrderDetails == null || !serviceOrderDetails.Any())
                {
                    ApplicationLogger.InfoLogger("Error: No Data Found.");
                    response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    return response;
                }

                foreach (var serviceModel in serviceOrderDetails)
                {
                    ServiceOrderModel serviceOrderModel = Converter.Convert(serviceModel, companyCode);
                    serviceOrderModel.ServiceOrderLineModel = new ServiceOrderLineModel();

                    // Labour/Activity
                    var activityLineDetails = _databaseContext.GetServiceOrderActivityLinesByInvoiceNumberAsync(companyCode, serviceModel.SM01001, serviceModel.SM01038).Result;
                    var costDetails = _databaseContext.GetServiceOrderCostLinesByInvoiceNumberAsync(companyCode, serviceModel.SM01001, serviceModel.SM01038).Result;
                    var materialItemDetails = _databaseContext.GetServiceOrderMaterialLinesByInvoiceNumberAsync(companyCode, serviceModel.SM01001, serviceModel.SM01038).Result;

                    if (activityLineDetails != null && activityLineDetails.Any())
                    {
                        ApplicationLogger.InfoLogger("Service Order has Activity line details");
                        serviceOrderModel.ServiceOrderLineModel.ServiceOrderLineLaborList.AddRange(
                            Converter.ConvertLabour(activityLineDetails, serviceModel, companyCode));
                        ApplicationLogger.InfoLogger("DM to BM conversion of Activity Line Model is done");
                    }

                    // Cost/Tool                    
                    if (costDetails != null && costDetails.Any())
                    {
                        ApplicationLogger.InfoLogger("Service Order has cost details");

                        serviceOrderModel.ServiceOrderLineModel.ServiceOrderLineToolList.AddRange(
                            Converter.ConvertTool(costDetails, serviceModel, companyCode));
                        ApplicationLogger.InfoLogger("DM to BM conversion of ConstModel is done");
                    }

                    // Material Item                    
                    if (materialItemDetails != null && materialItemDetails.Any())
                    {
                        ApplicationLogger.InfoLogger("Service Order has material item details");
                        serviceOrderModel.ServiceOrderLineModel.ServiceOrderLineMOItemList.AddRange(
                            Converter.ConvertMOItem(materialItemDetails, serviceModel, companyCode));
                        ApplicationLogger.InfoLogger("DM to BM conversion of Material Item Model is done");
                    }

                    response.ServiceOrderModels.Add(serviceOrderModel);
                }


                ApplicationLogger.InfoLogger("All Model conversion done");

            }
            catch (Exception ex)
            {
                ApplicationLogger.InfoLogger($"Exception occures: {ex.Message}");
                ApplicationLogger.Errorlog(ex.Message, Common.Enum.Category.Business, ex.StackTrace, ex.InnerException);
                response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
            }

            return response;
        }
    }
}
