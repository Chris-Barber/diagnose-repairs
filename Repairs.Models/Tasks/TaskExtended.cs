namespace Repairs.Models.Tasks
{
    using Newtonsoft.Json;

    public class TaskExtended
    {
        public bool BookOnline { get; set; }

        public string StanhopePFI { get; set; }

        public string Category { get; set; }

        public string Code { get; set; }

        public bool Communal { get; set; }

        public string CategoryTip { get; set; }

        public string RepairAreaTip { get; set; }

        public string ItemTip { get; set; }

        public string Item { get; set; }

        public string Priority { get; set; }

        public string Problem { get; set; }

        public string RepairArea { get; set; }

        public bool SpecialistContractor { get; set; }

        public bool TenureRented { get; set; }

        public bool TenureSharedOwner { get; set; }

        public bool TenureSupported { get; set; }

        public bool Unit { get; set; }

        public string Desc => $"{this.Category}, {this.RepairArea}, {this.Item}.";

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}