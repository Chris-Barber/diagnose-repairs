using Repairs.Models;
using System.IO;

namespace Repairs.Service
{
    public interface IGeolocationService
    {
        Location GetLocation(Stream memoryStream);
    }
}
