using TinyCsvParser.Mapping;

namespace ConnectedCar.Core.Tools.Data
{
    public class DealerMapping : CsvMapping<DealerData>
    {
        public DealerMapping() : base()
        {
            MapProperty(0, d => d.Name);
            MapProperty(1, d => d.StreetAddress);
            MapProperty(2, d => d.City);
            MapProperty(3, d => d.State);
            MapProperty(4, d => d.ZipCode);
        }
    }
}