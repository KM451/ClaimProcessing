namespace ClaimProcessing.Application.Suppliers.Commands.UpdateSupplier
{
    public class UpdateSupplierVm
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string ContactPerson { get; set; }

        public static explicit operator UpdateSupplierCommand(UpdateSupplierVm updateSupplierVm)
        {
            var command = new UpdateSupplierCommand()
            {
                Name = updateSupplierVm.Name,
                Street = updateSupplierVm.Street,
                City = updateSupplierVm.City,
                Country = updateSupplierVm.Country,
                ZipCode = updateSupplierVm.ZipCode,
                ContactPerson = updateSupplierVm.ContactPerson
            };
            return command;
        }
    }
}
