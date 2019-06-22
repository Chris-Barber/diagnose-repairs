using System;
using System.IO;
using System.Linq;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using Repairs.Models;

namespace Repairs.Service
{
    public class GeolocationService : IGeolocationService
    {
        public Location GetLocation(Stream memoryStream)
        {
            memoryStream.Position = 0;

            var directories = ImageMetadataReader.ReadMetadata(memoryStream);

            var gps = directories.OfType<GpsDirectory>().FirstOrDefault();

            if (gps != null)
            {
                var location = gps.GetGeoLocation();

                if (location != null)
                {
                    return new Location()
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude
                    };
                }
            }

            return new Location();
        }
    }
}
