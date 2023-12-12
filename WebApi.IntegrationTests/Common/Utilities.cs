using ClaimProcessing.Domain.Entities;
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
            var supplier = new Supplier()
            {
                Id = 1,
                Name = "Supplier",
                Address = new Address("Wiejska", "Warszawa", "Polska", "00-950"),
                ContactPerson = new FullName("Bruce", "Lee"),
            };
            context.Suppliers.Add(supplier);

            var claims = new List<Claim>
            {
                new()
                {
                    Id = 1,
                    ClaimNumber = "C10/23",
                    OwnerType = "o2",
                    ClaimType = "c1",
                    ItemCode = "12A34B",
                    Qty = 1,
                    CustomerName = "Customer",
                    CustomerId = "4B434A88-212D-4A4D-A17C-F35102D73CBB",
                    ItemName = "item",
                    ClaimDescription = "description",
                    Remarks = "remarks",
                    ClaimStatus = 2,
                    RmaAvailable = false,
                    ShipmentId = 1,
                    SupplierId = 1
                },
                new()
                {
                    Id = 2,
                    ClaimNumber = "C11/23",
                    OwnerType = "o1",
                    ClaimType = "c2",
                    ItemCode = "56C78D",
                    Qty = 2,
                    CustomerName = "Customer2",
                    CustomerId = "00000000-aaaa-1111-0000-000000000002",
                    ItemName = "item",
                    ClaimDescription = "description",
                    Remarks = "remarks",
                    ClaimStatus = 2,
                    RmaAvailable = false,
                    ShipmentId = 1,
                    SupplierId = 1
                }
            };

            context.Claims.AddRange(claims);

            var fotoUrls = new List<FotoUrl>
            {
                new()
                {
                    Id = 1,
                    Path = "C:\\Windows\\Foto",
                    ClaimId = 1
                },
                new()
                {
                    Id = 2,
                    Path = "C:\\Gallery",
                    ClaimId = 1,
                },
                new()
                {
                    Id = 3,
                    Path = "C:\\YYY",
                    ClaimId = 2,
                }
            };

            context.FotoUrls.AddRange(fotoUrls);

            var attachmentUrls = new List<AttachmentUrl>
            {
                new()
                {
                    Id = 1,
                    Path = "C:\\Windows\\System32",
                    ClaimId = 1
                },
                new()
                {
                    Id = 2,
                    Path = "C:\\Windows",
                    ClaimId = 1,
                },
                new()
                {
                    Id = 3,
                    Path = "C:\\XXX",
                    ClaimId = 2,
                }
            };

            context.AttachmentUrls.AddRange(attachmentUrls);


            var serialNumbers = new List<SerialNumber>
            {
                new()
                {
                    Id = 1,
                    Value = "123456789",
                    ClaimId = 1
                },
                new()
                {
                    Id = 2,
                    Value = "000000000",
                    ClaimId = 1
                },
                new()
                {
                    Id = 3,
                    Value = "999999999",
                    ClaimId = 2
                }
            };

            context.SerialNumbers.AddRange(serialNumbers);

            var shipments = new List<Shipment>
            {
                new()
                {
                    Id = 1,
                    ShipmentDate = new DateTime(2023, 10, 10),
                    Speditor = "DHL",
                    ShippingDocumentNo = "A1234XYZ",
                    TotalWeight = 20,
                    SupplierId = 1
                },
                 new()
                {
                    Id = 2,
                    ShipmentDate = new DateTime(2023, 11, 11),
                    Speditor = "DPD",
                    ShippingDocumentNo = "B5678UVW",
                    TotalWeight = 33,
                    SupplierId = 1
                },
            };

            context.Shipments.AddRange(shipments);

            var packagings = new List<Packaging>
            {
                new()
                {
                    Id = 1,
                    Type = "box",
                    Dimensions = new Dimensions(10, 10, 10),
                    Weight = 20,
                    Notes = "notes",
                    ShipmentId = 1,
                },
                new()
                {
                    Id = 2,
                    Type = "pallet",
                    Dimensions = new Dimensions(11, 11, 11),
                    Weight = 21,
                    Notes = "notes",
                    ShipmentId = 1,
                }
            };
            context.Packagings.AddRange(packagings);

            var purchaseDetail = new PurchaseDetail()
            {
                Id = 1,
                PurchaseInvoiceNo = "PI999/23",
                PurchaseDate = new DateTime(2023, 5, 15),
                InternalDocNo = "ID999",
                ClaimId = 1
            };
            context.PurchaseDetails.Add(purchaseDetail);

            var saleDetail = new SaleDetail()
            {
                Id = 1,
                SaleInvoiceNo = "SI888/23",
                SaleDate = new DateTime(2023, 5, 26),
                ClaimId = 1
            };

            context.SaleDetails.Add(saleDetail);

            context.SaveChangesAsync();
        }

    }
}
