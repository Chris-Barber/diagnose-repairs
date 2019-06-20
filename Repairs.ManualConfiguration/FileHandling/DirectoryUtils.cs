namespace Repairs.ManualConfiguration.FileHandling
{
    using System;

    internal static class DirectoryUtils
    {
        public static string PathToRepairJsonData()
        {
            return $"{PathToAppData()}jsonData.json";
        }

        public static string PathToRepairJsonTasks()
        {
            return $"{PathToAppData()}jsonTasks.json";
        }

        public static string PathToRepairTaskTemplateCsv()
        {
            return $"{PathToDocs()}PORTAL FINAL.csv";
        }

        private static string CurrentProjectPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "\\");
        }

        private static string PathToDocs()
        {
            return $"{CurrentProjectPath()}docs\\";
        }

        private static string PathToRoot()
        {
            return AppDomain.CurrentDomain.BaseDirectory.Replace("\\Repairs.ManualConfiguration\\bin\\Debug", "\\");
        }

        private static string PathToAppData()
        {
            return $"{PathToRoot()}Repairs\\App_Data\\";
        }
    }
}
