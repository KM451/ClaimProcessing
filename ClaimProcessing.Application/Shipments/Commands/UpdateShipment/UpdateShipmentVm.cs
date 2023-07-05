namespace ClaimProcessing.Application.Shipments.Commands.UpdateShipment
{
    public class UpdateShipmentVm
    {
        public DateTime ShipmentDate { get; set; }
        public int SupplierId { get; set; }
        public string Speditor { get; set; }
        public string ShippingDocumentNo { get; set; }
        public decimal TotalWeight { get; set; }

        public static explicit operator UpdateShipmentCommand(UpdateShipmentVm updateShipmentVm)
        {
            var command = new UpdateShipmentCommand
            {
                ShipmentDate = updateShipmentVm.ShipmentDate,
                SupplierId = updateShipmentVm.SupplierId,
                Speditor = updateShipmentVm.Speditor,
                ShippingDocumentNo = updateShipmentVm.ShippingDocumentNo,
                TotalWeight = updateShipmentVm.TotalWeight
            };
            return command;
        }
    }
}
