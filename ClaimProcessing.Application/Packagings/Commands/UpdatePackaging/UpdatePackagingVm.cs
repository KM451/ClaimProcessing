namespace ClaimProcessing.Application.Packagings.Commands.UpdatePackaging
{
    public class UpdatePackagingVm
    {
        public string Type { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public decimal Weight { get; set; }
        public string Notes { get; set; }
        public int ShipmentId { get; set; }

        public static explicit operator UpdatePackagingCommand(UpdatePackagingVm updatePackagingVm)
        {
            var command = new UpdatePackagingCommand
            {
                Type = updatePackagingVm.Type,
                Height = updatePackagingVm.Height,
                Width = updatePackagingVm.Width,
                Depth = updatePackagingVm.Depth,
                Weight = updatePackagingVm.Weight,
                Notes = updatePackagingVm.Notes,
                ShipmentId = updatePackagingVm.ShipmentId
            };
            return command;
        }
    }
}
