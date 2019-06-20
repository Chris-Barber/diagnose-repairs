namespace Repairs.Models.Tasks
{
    public class ConfigurationOptions
    {
        public bool GeneralNeeds { get; set; }

        public bool IsCommunal { get; set; }

        public bool IsUnit { get; set; }

        public string Priority { get; set; }
        
        public bool Shared { get; set; }

        public bool Sheltered { get; set; }
    }
}