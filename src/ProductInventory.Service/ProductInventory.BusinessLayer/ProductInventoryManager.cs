using System;
using System.Collections.Generic;
using System.Linq;
using ProductInventory.BusinessLayer.Interfaces;
using ProductInventory.Common;
using ProductInventory.Common.Error;
using ProductInventory.Common.Logger;
using ProductInventory.DataLayer.Entities;
using ProductInventory.DataLayer.Entities.Datalake;
using ProductInventory.DataLayer.Interfaces;
using ProductInventory.Model;
using ProductInventory.Model.Response;


namespace ProductInventory.BusinessLayer
{
    public class ProductInventoryManager : IProductInventoryManager
    {
        private readonly IDatabaseContext _databaseContext;

        public ProductInventoryManager(IDatabaseContext databaseContext)
        {
            // create ICustomer instance -Data Layer
            _databaseContext = databaseContext;
        }

        public ProductSearchByIdResponse GetProductById(string companyCode, string productCode)
        {
            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetProductById :: Custome Input: companyCode: [{companyCode}] And productCode: [{productCode}]");
            var response = new ProductSearchByIdResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateProductCode(productCode, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and productCode Status: Success");

                // Get Item from Master

                    var stockItem = _databaseContext.GetStockItemByProductCode(companyCode, productCode);
                    if (stockItem != null)
                    {
                        response.ProductInventory = new ProductInventoryModel
                        {
                            ProductMasterDetails = Converter.Convert(stockItem, companyCode)
                        };

                        // Get all the items from warehouse of that product code
                        var wareHouse = _databaseContext.GetItemWareHouseByProductCode(companyCode, productCode);
                        if (wareHouse.Any())
                        {
                            response.ProductInventory.StockItemWarehouse.AddRange(Converter.Convert(wareHouse, companyCode));
                            ApplicationLogger.InfoLogger($"Data to Business Model conversion successful.");
                        }
                    }
                    else
                    {
                        ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                        response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    }

                    return response;
               
            }

            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and ValidateProductCode Status: Failed");
            return response;
        }

        public ProductSearchByDescriptionResponse GetProductByDescription(string companyCode, string description)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetProductByDescription :: Custome Input: CompanyCode: [{companyCode}] And Description: [{description}]");
            var response = new ProductSearchByDescriptionResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateDescription(description, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and Description Status: Success");
               
                    var products = _databaseContext.GetProductInvetoryByProductName(companyCode, description);

                    if (products.Any())
                    {
                        response.ProductList.AddRange(Converter.ConvertProductEntity(products, companyCode));
                        ApplicationLogger.InfoLogger("Data to Business Model conversion successful.");
                    }
                    else
                    {
                        ApplicationLogger.InfoLogger("Error: No product found");
                        response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    }

                    return response;
               
            }
            else
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and Description Status: Failed");
            }

            return response;
        }

        public ProductSearchByLocationIdResponse GetProductByLocationId(string companyCode, string locationId)
        {
            ApplicationLogger.InfoLogger($"Business Method Name: GetProductByLocationId :: Custome Input: CompanyCode: [{companyCode}] and locationId: [{locationId}]");
            var response = new ProductSearchByLocationIdResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateLocationId(locationId, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and LocationId Status: Success");
               
                    var products = _databaseContext.GetProductInvetoryByLocationId(companyCode, locationId);

                    if (products.Any())
                    {
                        response.ProductInventoryList.AddRange(Converter.ConvertProductEntity(products, companyCode));
                        ApplicationLogger.InfoLogger($"ProductInventoryList data length: {products.Count()}");
                    }
                    else
                    {
                        ApplicationLogger.InfoLogger("Error: No product found");
                        response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    }

            }
            else
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and LocationId Status: Failed");
            }

            return response;
        }

        public ProductResponse GetProduct(string companyCode, string productCode, string locationId)
        {
            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetProductByLocationId :: Custome Input: CompanyCode: [{companyCode}] And Description: [{productCode}] And Description: [{locationId}]");
            var response = new ProductResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateProductCode(productCode, response) &&
                !InputValidation.ValidateLocationId(locationId, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode, productCode and LocationId Status: Success");

                    var stockItem = _databaseContext.GetStockItemByProductCode(companyCode, productCode);
                    if (stockItem != null)
                    {
                        response.ProductInventory = new ProductInventoryModel
                        {
                            ProductMasterDetails = Converter.Convert(stockItem, companyCode)
                        };

                        var wareHouse = _databaseContext.GetItemWareHouse(companyCode, productCode, locationId);
                        if (wareHouse.Any())
                        {                           
                            response.ProductInventory.StockItemWarehouse.AddRange(Converter.Convert(wareHouse, companyCode));
                            ApplicationLogger.InfoLogger("Data to Business Model conversion successful.");
                        }
                    }
                    else
                    {
                        ApplicationLogger.InfoLogger("Error: No product found in stock master");
                        response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    }
               
            }
            else
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and LocationId Status: Failed");
            }

            return response;
        }

        public ProductStockBalanceQuantityResponse GetProductStockBalanceQuantity(string companyCode, string productCode, string locationId)
        {
            ApplicationLogger.InfoLogger(
                 $"Business Method Name: GetProductStockBalanceQuantity :: Custome Input: CompanyCode: [{companyCode}] And ProductCode: [{productCode}] And LocationId: [{ locationId}]");
            var response = new ProductStockBalanceQuantityResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateProductCode(productCode, response) &&
                !InputValidation.ValidateLocationId(locationId, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and ProductCode and locationId Status: Success");

                    var wareHouseList = _databaseContext.GetItemWareHouse(companyCode, productCode, locationId);
                    if (wareHouseList.Any())
                    {
                        // Get first StockBalance Quantity for locationid and productcode 
                        var wareHouse = wareHouseList.FirstOrDefault();
                        if (wareHouse != null)
                            response.StockBalanceQuantity = string.IsNullOrWhiteSpace(wareHouse.sc03003) ? 0 : Convert.ToDouble(wareHouse.sc03003);
                        else
                        {
                            ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                            response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                        }

                        ApplicationLogger.InfoLogger($"Item warehouse data length: {wareHouseList.Count()}");
                    }
                    else
                    {
                        ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                        response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    }
                    return response;                
              

            }

            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and ProductCode  and locationId Status: Failed");
            return response;
        }

        public ProductStockBalanceQuantityForAllLocationResponse GetProductStockBalanceQuantityForAllLocation(string companyCode, string productCode)
        {
            ApplicationLogger.InfoLogger(
                $"Business Method Name: GetProductStockBalanceQuantityForAllLocation :: Custome Input: CompanyCode: [{companyCode}] And ProductCode: [{productCode}]");
            var response = new ProductStockBalanceQuantityForAllLocationResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateProductCode(productCode, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and ProductCode and locationId Status: Success");
                try
                {
                    var wareHouse = _databaseContext.GetItemWareHouseByProductCode(companyCode, productCode);
                    var itemWarehouses = wareHouse as List<ItemWarehouse> ?? wareHouse.ToList();
                    if (itemWarehouses.Any())
                    {
                        itemWarehouses.ForEach(
                            x =>
                                response.LocationList.Add(new ProductLocationModel
                                {
                                    LocationId = x.sc03002,
                                    ProductQuantity = string.IsNullOrWhiteSpace(x.sc03003) ? 0 : Convert.ToDouble(x.sc03003)
                                }));
                        ApplicationLogger.InfoLogger($"Item warehouse data length: {itemWarehouses.Count}");
                    }
                    else
                    {
                        ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                        response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    }
                }
                catch (Exception ex)
                {
                    ApplicationLogger.InfoLogger("Error: Exception occured at conversion.");
                    response.ErrorInfo.Add(new ErrorInfo(ex.Message));
                    return response;
                }
            }

            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and ProductCode Status: Failed");
            return response;
        }

        public LocationwiseProductStockBalanceQuantityResponse GetLocationwiseProductStockBalanceQuantity(string companyCode, string locationId)
        {
            ApplicationLogger.InfoLogger(
               $"Business Method Name: GetLocationwiseProductStockBalanceQuantity :: Custome Input: CompanyCode: [{companyCode}] And ProductCode: [{locationId}]");
            var response = new LocationwiseProductStockBalanceQuantityResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateLocationId(locationId, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and ProductCode and locationId Status: Success");
                try
                {
                    var wareHouse = _databaseContext.GetItemWareHouseByLocationId(companyCode, locationId);
                    var itemWarehouses = wareHouse as List<ItemWarehouse> ?? wareHouse.ToList();
                    if (itemWarehouses.Any())
                    {
                        itemWarehouses.ForEach(x => response.ProductList.Add(new LocationWiseProductQuantityModel { ProductCode = x.sc03001, ProductQuantity = string.IsNullOrWhiteSpace(x.sc03003) ? 0 : Convert.ToDouble(x.sc03003) }));
                        ApplicationLogger.InfoLogger($"Item warehouse data length: {itemWarehouses.Count}");
                    }
                    else
                    {
                        ApplicationLogger.InfoLogger("Error: No item warehouse data found");
                        response.ErrorInfo.Add(new ErrorInfo(Constants.NoDataFoundMessage));
                    }

                    return response;
                }
                catch (Exception ex)
                {
                    ApplicationLogger.InfoLogger("Error: Exception occured at conversion.");
                    response.ErrorInfo.Add(new ErrorInfo(ex.Message));
                    return response;
                }

            }

            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and ProductCode Status: Failed");
            return response;
        }




        public ProductAvailableQuantityResponse GetProductAvailableQuantity(string companyCode, string productCode, string locationId)
        {
            ApplicationLogger.InfoLogger(
                  $"Business Method Name: GetAvailableQuantity :: Custome Input: CompanyCode: [{companyCode}] And ProductCode: [{productCode}],  locationId: [{locationId}]");
            var response = new ProductAvailableQuantityResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) && !InputValidation.ValidateProductCode(productCode, response)
                && !InputValidation.ValidateLocationId(locationId, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and productCode  Status: Success");
                try
                {
                    var wareHouse = _databaseContext.GetItemWareHouse(companyCode, productCode, locationId);
                    if (wareHouse.Any())
                    {
                        var firstOrDefault = wareHouse.FirstOrDefault();
                        if (firstOrDefault != null)
                            response.AvailableQuantity = Converter.GetAvailableQuantity(firstOrDefault.sc03003, firstOrDefault.sc03004, firstOrDefault.sc03005);

                        ApplicationLogger.InfoLogger($"Item warehouse data length: {wareHouse.Count()}");
                    }                    

                    return response;
                }
                catch (Exception ex)
                {
                    ApplicationLogger.InfoLogger("Error: Exception occured at conversion.");
                    response.ErrorInfo.Add(new ErrorInfo(ex.Message));
                    return response;
                }

            }

            ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and ProductCode Status: Failed");
            return response;
        }

        public ProductAvailableQuantityForAllLocationResponse GetProductAvailableQuantityForAllLocation(string companyCode, string productCode)
        {
            ApplicationLogger.InfoLogger(
                  $"Business Method Name: GetAvailableQuantity :: Custome Input: CompanyCode: [{companyCode}] And ProductCode: [{productCode}]");
            var response = new ProductAvailableQuantityForAllLocationResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateProductCode(productCode, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and productCode  Status: Success");
                try
                {
                    var wareHouse = _databaseContext.GetItemWareHouseByProductCode(companyCode, productCode);
                    var itemWarehouses = wareHouse as List<ItemWarehouse> ?? wareHouse.ToList();
                    if (itemWarehouses.Any())
                    {
                        itemWarehouses.ForEach(
                            x =>
                                response.ProductList.Add(new ProductAvailableQuantity
                                {
                                    LocationId = x.sc03002,
                                    AvailableQuantity = Converter.GetAvailableQuantity(x.sc03003,x.sc03004,x.sc03005)
                                }));
                        ApplicationLogger.InfoLogger($"Item warehouse data length: {itemWarehouses.Count}");
                    }

                    return response;

                }
                catch (Exception ex)
                {
                    ApplicationLogger.InfoLogger("Error: Exception occured at conversion.");
                    response.ErrorInfo.Add(new ErrorInfo(ex.Message));
                    return response;
                }

            }

            ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and ProductCode Status: Failed");
            return response;
        }
        public LocationwiseProductAvailableQuantityResponse GetLocationwiseProductAvailableQuantity(string companyCode, string locationId)
        {
            ApplicationLogger.InfoLogger(
               $"Business Method Name: GetLocationwiseProductAvailableQuantity :: Custome Input: CompanyCode: [{companyCode}] And ProductCode: [{locationId}]");
            var response = new LocationwiseProductAvailableQuantityResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateLocationId(locationId, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and ProductCode and locationId Status: Success");
                try
                {
                    var wareHouse = _databaseContext.GetItemWareHouseByLocationId(companyCode, locationId);
                    var itemWarehouses = wareHouse as List<ItemWarehouse> ?? wareHouse.ToList();
                    if (itemWarehouses.Any())
                    {
                        itemWarehouses.ForEach(x => response.ProductList.Add(new LocationWiseProductAvailableQuantityModel { ProductCode = x.sc03001, AvailableQuantity = Converter.GetAvailableQuantity(x.sc03003, x.sc03004, x.sc03005) }));
                        ApplicationLogger.InfoLogger($"Item warehouse data length: {itemWarehouses.Count}");
                    }

                    return response;
                }
                catch (Exception ex)
                {
                    ApplicationLogger.InfoLogger("Error: Exception occured at conversion.");
                    response.ErrorInfo.Add(new ErrorInfo(ex.Message));
                    return response;
                }

            }

            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and ProductCode Status: Failed");
            return response;
        }




        public ProductFamilyTypeResponse GetProductFamilyType(string companyCode, string productCode)
        {
            ApplicationLogger.InfoLogger(
                  $"Business Method Name: GetProductFamilyType :: Custome Input: CompanyCode: [{companyCode}] And ProductCode: [{productCode}]");
            var response = new ProductFamilyTypeResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateProductCode(productCode, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and ProductCode  Status: Success");
                try
                {
                    StockItemMaster stockitem = _databaseContext.GetStockItemByProductCode(companyCode, productCode);

                    response.FamilyType = Converter.GetProductFamily(stockitem.sc01037);

                    ApplicationLogger.InfoLogger($"ProductFamilyType  data : {stockitem.sc01037}");

                    return response;

                }
                catch (Exception ex)
                {
                    ApplicationLogger.InfoLogger("Error: Exception occured at conversion.");
                    response.ErrorInfo.Add(new ErrorInfo(ex.Message));
                    return response;
                }

            }

            ApplicationLogger.InfoLogger("InputValidation.ValidateCompanyCode and ProductCode Status: Failed");
            return response;
        }

        public ProductLineTypeResponse GetProductLineType(string companyCode, string productCode)
        {
            ApplicationLogger.InfoLogger(
                   $"Business Method Name: GetProductLineType :: Custome Input: CompanyCode: [{companyCode}] And ProductCode: [{productCode}]");
            var response = new ProductLineTypeResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateProductCode(productCode, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and productCode  Status: Success");
                try
                {
                    StockItemMaster stockitem = _databaseContext.GetStockItemByProductCode(companyCode, productCode);
                    response.LineType = Converter.GetProductLineMapping(stockitem.sc01037);
                    ApplicationLogger.InfoLogger($"ProductLineType  data : {stockitem.sc01037}");
                }
                catch (Exception ex)
                {
                    ApplicationLogger.InfoLogger("Error: Exception occured at conversion.");
                    response.ErrorInfo.Add(new ErrorInfo(ex.Message));
                }

                return response;
            }

            ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and ProductCode Status: Failed");
            return response;
        }

        public ProductStatusResponse IsProductActive(string companyCode, string productCode)
        {
            ApplicationLogger.InfoLogger(
                   $"Business Method Name: IsProductActive :: Custome Input: CompanyCode: [{companyCode}] And ProductCode: [{productCode}]");
            var response = new ProductStatusResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateProductCode(productCode, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and ProductCode  Status: Success");
                try
                {
                    StockItemMaster stockitem = _databaseContext.GetStockItemByProductCode(companyCode, productCode);

                    response.IsActive = Converter.CheckActiveProduct(string.IsNullOrWhiteSpace(stockitem.sc01120) ? 0 : Convert.ToDouble(stockitem.sc01120));

                    ApplicationLogger.InfoLogger($"ProductLineType  data : {stockitem.sc01120}");
                }
                catch (Exception ex)
                {
                    ApplicationLogger.InfoLogger("Error: Exception occured at conversion.");
                    response.ErrorInfo.Add(new ErrorInfo(ex.Message));
                }

                return response;
            }

            ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and ProductCode Status: Failed");
            return response;
        }
        public ProductStockStatusResponse IsProductStockable(string companyCode, string productCode)
        {
            ApplicationLogger.InfoLogger(
                   $"Business Method Name: IsProductStockable :: Custome Input: CompanyCode: [{companyCode}] And ProductCode: [{productCode}]");
            var response = new ProductStockStatusResponse();
            if (!InputValidation.ValidateCompanyCode(companyCode, response) &&
                !InputValidation.ValidateProductCode(productCode, response))
            {
                ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and ProductCode  Status: Success");
                try
                {
                    StockItemMaster stockitem = _databaseContext.GetStockItemByProductCode(companyCode, productCode);

                    response.Stockable = Converter.GetStockable(Convert.ToInt32(stockitem.sc01066), stockitem.sc01001);

                    ApplicationLogger.InfoLogger($"ArtiStatus  data : {stockitem.sc01066}");
                }
                catch (Exception ex)
                {
                    ApplicationLogger.InfoLogger("Error: Exception occured at conversion.");
                    response.ErrorInfo.Add(new ErrorInfo(ex.Message));
                }

                return response;
            }

            ApplicationLogger.InfoLogger("InputValidation.Validate CompanyCode and ProductCode Status: Failed");
            return response;
        }
    }
}
