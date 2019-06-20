namespace Repairs.ManualConfiguration
{
    using System.IO;

    using Newtonsoft.Json;

    using NUnit.Framework;

    using Repairs.ManualConfiguration.FileHandling;

    [TestFixture]
    public class RepairTaskTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        /// <summary>
        /// This routine can be run to create the base repair
        /// tasks from the spreadsheet supplied by property services.
        /// </summary>
        [Test]
        public void CreateJsonData()
        {
            var tasks = TaskManager.GetRepairTaskTemplateData(DirectoryUtils.PathToRepairTaskTemplateCsv());

            var jsonData = JsonConvert.SerializeObject(tasks);

            File.WriteAllText(DirectoryUtils.PathToRepairJsonData(), jsonData);
        }

        /// <summary>
        /// This routine can be run to generate the repair tasks in a hierarchical format.
        /// </summary>
        [Test]
        public void CreateJsonTasks()
        {
            var tasks = TaskManager.CreateRepairTasks(DirectoryUtils.PathToRepairJsonData());
            
            var jsonTasks = JsonConvert.SerializeObject(tasks);

            File.WriteAllText(DirectoryUtils.PathToRepairJsonTasks(), jsonTasks);
        }        
    }
}
