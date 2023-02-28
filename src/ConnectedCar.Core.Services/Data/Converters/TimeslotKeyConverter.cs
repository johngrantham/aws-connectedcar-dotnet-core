using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using ConnectedCar.Core.Shared.Data.Attributes;
using System;

namespace ConnectedCar.Core.Services.Data.Converters
{
    public class TimeslotKeyConverter : IPropertyConverter
    {
        private const string Delimiter = "#";

        public DynamoDBEntry ToEntry(object value)
        {
            if (!(value is TimeslotKey timeslotKey)) throw new ArgumentException();

            DynamoDBEntry entry = new Primitive
            {
                Value = timeslotKey.DealerId + Delimiter + timeslotKey.ServiceDateHour
            };

            return entry;

        }

        public object FromEntry(DynamoDBEntry entry)
        {
            if (entry is Primitive primitive && primitive.Value is string input && !string.IsNullOrEmpty(input))
            {
                string[] parts = input.Split(Delimiter);

                if (parts.Length == 2)
                {
                    return new TimeslotKey
                    {
                        DealerId = parts[0],
                        ServiceDateHour = parts[1]
                    };
                }
            }

            return null;
        }
    }
}
