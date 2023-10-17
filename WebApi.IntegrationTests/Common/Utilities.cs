using ClaimProcessing.Domain.ValueObjects;
using ClaimProcessing.Persistance;
using Newtonsoft.Json;

namespace WebApi.IntegrationTests.Common
{
    public class Utilities
    {
        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }
        public static void InitializeDbForTests(ClaimProcessingDbContext context) 
        {
            var supplier = new ClaimProcessing.Domain.Entities.Supplier()
            {
                Id = 1,
                Name = "Supplier",
                Address = new Address("Wiejska", "Warszawa", "Polska", "00-950"),
                ContactPerson = new FullName("Bruce", "Lee"),
            };
            context.Suppliers.Add(supplier);

            var claim = new ClaimProcessing.Domain.Entities.Claim()
            {
                Id = 1,
                ClaimNumber = "C10/23",
                OwnerType = "Type o1",
                ClaimType = "Type c1",
                ItemCode = "12A34B",
                Qty = 1,
                CustomerName = "Customer",
                CustomerId = "00000000-aaaa-1111-0000-000000000000",
                ItemName = "item",
                ClaimDescription = "description",
                Remarks = "remarks",
                ClaimStatus = 2,
                RmaAvailable = false,
                ShipmentId = 2,
                SupplierId = supplier.Id
            };

            context.Claims.Add(claim);

            claim = new ClaimProcessing.Domain.Entities.Claim()
            {
                Id = 2,
                ClaimNumber = "C11/23",
                OwnerType = "Type o1",
                ClaimType = "Type c1",
                ItemCode = "12A34C",
                Qty = 1,
                CustomerName = "CustomerB",
                CustomerId = "00000000-aaaa-1111-0000-000000000001",
                ItemName = "itemB",
                ClaimDescription = "description",
                Remarks = "remarks",
                ClaimStatus = 2,
                RmaAvailable = false,
                ShipmentId = 3,
                SupplierId = supplier.Id
            };

            context.Claims.Add(claim);

            var fotoUrl = new ClaimProcessing.Domain.Entities.FotoUrl()
            {
                Id = 1,
                Path = "C:\\Windows\\System",
                ClaimId = claim.Id,
            };

            context.FotoUrls.Add(fotoUrl);

            var attachmentUrl = new ClaimProcessing.Domain.Entities.AttachmentUrl()
            {
                Id = 1,
                Path = "C:\\Windows\\System32",
                ClaimId = claim.Id,
            };

            context.AttachmentUrls.Add(attachmentUrl);

            var serialNumber = new ClaimProcessing.Domain.Entities.SerialNumber()
            {
                Id = 1,
                Value = "123456789",
                ClaimId = claim.Id,
            };

            context.SerialNumbers.Add(serialNumber);

            var shipment = new ClaimProcessing.Domain.Entities.Shipment()
            {
                Id = 1,
                ShipmentDate = new DateTime(2023, 10, 10),
                Speditor = "DHL",
                ShippingDocumentNo = "A1234XYZ",
                TotalWeight = 20,
                SupplierId = supplier.Id
            };

            context.Shipments.Add(shipment);

            var packaging = new ClaimProcessing.Domain.Entities.Packaging()
            {
                Id = 1,
                Type = "box",
                Dimensions = new Dimensions(10, 10, 10),
                Weight = 20,
                Notes = "notes",
                ShipmentId = shipment.Id,
            };
            context.Packagings.Add(packaging);

            var purchaseDetail = new ClaimProcessing.Domain.Entities.PurchaseDetail()
            {
                Id = 1,
                PurchaseInvoiceNo = "PI999/23",
                PurchaseDate = new DateTime(2023, 5, 15),
                InternalDocNo = "ID999",
                ClaimId = claim.Id
            };
            context.PurchaseDetails.Add(purchaseDetail);

            var saleDetail = new ClaimProcessing.Domain.Entities.SaleDetail()
            {
                Id = 1,
                SaleInvoiceNo = "SI888/23",
                SaleDate = new DateTime(2023, 5, 26),
                ClaimId = claim.Id
            };

            context.SaleDetails.Add(saleDetail);

            context.SaveChangesAsync();
        }

    }
}
