using ConnectedCar.Core.Shared.Data.Attributes;
using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Data.Enums;
using System;

namespace ConnectedCar.Core.Tools.Data
{
    public class DealerData
    {
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Dealer GetDealer()
        {
            Enum.TryParse(State, out StateCodeEnum stateCode);

            return new Dealer{
                Name = Name,
                Address = new Address
                {
                    StreetAddress = StreetAddress,
                    City = City,
                    State = State,
                    ZipCode = ZipCode
                },
                StateCode = stateCode
            };
        }
    }
}