namespace Repairs.Service
{
    using System;

    using Newtonsoft.Json;

    public class PropertyRepairConfiguration
    {
        public Guid? BlockId { get; set; }

        public bool CanAddCommunalRepair => this.BlockId.HasValue
                                            && (this.IsShelteredOrSupported || this.IsLeaseholdOrSharedOwner
                                                || this.IsGeneralNeeds);

        public bool CanAddUnitRepair => this.IsShelteredOrSupported || this.IsGeneralNeeds 
                                                || this.IsInDefects;

        public bool HasManagingAgent { get; set; } = false;

        public bool IsGeneralNeeds { get; set; }

        public bool IsInDefects { get; set; }

        public bool IsLeaseholdOrSharedOwner { get; set; }

        public bool IsShelteredOrSupported { get; set; }

        public bool IsStanhope { get; set; }

        public bool IsStanhopePfi { get; set; }

        public Guid PropertyId { get; set; }

        public bool IsGarage { get; set; }

        public Guid RepairArea { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
