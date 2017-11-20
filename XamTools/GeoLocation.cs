using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamTools
{
 public class GeoLocation : ObservableBase
    {
        string _description;
        double _latitude;
        double _longitude;
        DateTimeOffset? _timeStamp;

        string _title;

        public string Description
        {
            get => _description;
            set => SetValue(ref _description, value);
        }

        public double Latitude
        {
            get => _latitude;
            set => SetValue(ref _latitude, value);
        }

        public double Longitude
        {
            get => _longitude;
            set => SetValue(ref _longitude, value);
        }

        public DateTimeOffset? TimeStamp
        {
            get => _timeStamp;
            set => SetValue(ref _timeStamp, value);
        }

        public string Title
        {
            get => _title;
            set => SetValue(ref _title, value);
        }

        public static bool AreEqual(GeoLocation location1, GeoLocation location2)
        {
            if (location1 is null ^ location2 is null)
                return false;

            return ReferenceEquals(location1, location2) ||
                   ((location1.Latitude == location2.Latitude) && (location1.Longitude == location2.Longitude));
        }

        public static GeoLocation FromWellKnownText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;
            try
            {
                var location = new GeoLocation();

                var locationString = text.Substring(text.IndexOf("(") + 1, text.IndexOf(")") - (text.IndexOf("(") + 1)).Trim();
                var locations = locationString.Split(' ');

                location.Latitude = double.Parse(locations[1]);
                location.Longitude = double.Parse(locations[0]);

                return location;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("unable to create geolocation from " + text, ex);
                return null;
            }
        }

        public static bool IsInCloseProximity(GeoLocation location1, GeoLocation location2, int rounding = 4)
        {
            if (location1 is null ^ location2 is null)
                return false;

            return ReferenceEquals(location1, location2) || ((Math.Round(location1.Latitude, rounding) == Math.Round(location2.Latitude, rounding)) &&
                                                             (Math.Round(location1.Longitude, rounding) ==
                                                              Math.Round(location2.Longitude, rounding)));
        }

        public double DistanceFrom(GeoLocation location)
        {
            double distance = 0;

            if (location != null)
                distance = MapExtensions.CalculateDistance(Latitude, Longitude, location.Latitude, location.Longitude, 'M');

            return distance;
        }

        public bool IsInPolygon(IList<GeoLocation> polygonLocations)
        {
            //based in PIP: https://en.wikipedia.org/wiki/Point_in_polygon
            int i, j;
            var c = false;
            for (i = 0, j = polygonLocations.Count - 1; i < polygonLocations.Count; j = i++)
            {
                if ((((polygonLocations[i].Latitude <= Latitude) && (Latitude < polygonLocations[j].Latitude)) ||
                     ((polygonLocations[j].Latitude <= Latitude) && (Latitude < polygonLocations[i].Latitude))) &&
                    (Longitude < ((((polygonLocations[j].Longitude - polygonLocations[i].Longitude) * (Latitude - polygonLocations[i].Latitude)) /
                                   (polygonLocations[j].Latitude - polygonLocations[i].Latitude)) + polygonLocations[i].Longitude)))
                    c = !c;
            }

            return c;
        }

        public override string ToString() => SerializeToString();

        public string ToWellKnownText() => $"POINT ({SerializeToString()})";

        string SerializeToString() => $"{Longitude} {Latitude} {string.Empty} {string.Empty}";
    }
}
