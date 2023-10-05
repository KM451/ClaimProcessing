using ClaimProcessing.Application.Common.Interfaces;
using ClaimProcessing.Domain.Entities;
using ClaimProcessing.Domain.ValueObjects;
using ClaimProcessing.Persistance;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.UnitTest.Common
{
    public static class ClaimProcessingDbContextFactory
    {
        public static Mock<ClaimProcessingDbContext> Create()
        {
            var dateTime = new DateTime(2000, 1, 1);
            var dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.Now).Returns(dateTime);

            var currentUserMock = new Mock<ICurrentUserService>();
            currentUserMock.Setup(m => m.Email).Returns("email@email.com");
            currentUserMock.Setup(m => m.IsAuthenticated).Returns(true);
            currentUserMock.Setup(m => m.Name).Returns("Jan Nowak");
            currentUserMock.Setup(m => m.UserId).Returns("00000000-aaaa-1111-0000-000000000000");

            var options = new DbContextOptionsBuilder<ClaimProcessingDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var mock = new Mock<ClaimProcessingDbContext>(options, dateTimeMock.Object, currentUserMock.Object) { CallBase = true };

            var context = mock.Object;

            context.Database.EnsureCreated();

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
                ShipmentDate = DateTime.Now,
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

            var purchaseDetail = new ClaimProcessing.Domain.Entities.PurchaseDetail()
            {
                Id = 1,
                PurchaseInvoiceNo = "PI999/23",
                PurchaseDate = DateTime.Now,
                InternalDocNo = "ID999",
                ClaimId = claim.Id
            };
            context.PurchaseDetails.Add(purchaseDetail);

            var saleDetail = new ClaimProcessing.Domain.Entities.SaleDetail()
            {
                Id = 1,
                SaleInvoiceNo = "SI888/23",
                SaleDate = DateTime.Now,
                ClaimId = claim.Id
            };

            context.SaleDetails.Add(saleDetail);

            context.Packagings.Add(packaging);


            context.SaveChangesAsync();

            return mock;

        }
        public static void Destroy(ClaimProcessingDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
