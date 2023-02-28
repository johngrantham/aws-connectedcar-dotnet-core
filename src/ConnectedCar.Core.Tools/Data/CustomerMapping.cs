using TinyCsvParser.Mapping;

namespace ConnectedCar.Core.Tools.Data
{
    public class CustomerMapping : CsvMapping<CustomerData>
    {
        public CustomerMapping() : base()
        {
            MapProperty(0, c => c.Username);
            MapProperty(1, c => c.Firstname);
            MapProperty(2, c => c.Lastname);
            MapProperty(3, c => c.PhoneNumber);
            MapProperty(4, c => c.Vin);
            MapProperty(5, c => c.VehiclePin);
        }
    }
}