namespace Moat.Api.Helpers
{
    using System.IO;
    using System.Web;

    public static class DirectoryHelper
    {
        public static string MapPath(string path)
        {
            return HttpContext.Current == null
                       ? Path.Combine(Directory.GetCurrentDirectory(), path)
                       : HttpContext.Current.Server.MapPath(path);
        }
    }
}