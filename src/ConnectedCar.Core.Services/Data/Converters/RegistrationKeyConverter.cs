using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using ConnectedCar.Core.Shared.Data.Attributes;
using System;

namespace ConnectedCar.Core.Services.Data.Converters
{
    public class RegistrationKeyConverter : IPropertyConverter
    {
        private const string Delimiter = "#";

        public DynamoDBEntry ToEntry(object value)
        {
            if (!(value is RegistrationKey registrationKey)) throw new ArgumentException();

            DynamoDBEntry entry = new Primitive
            {
                Value = registrationKey.Username + Delimiter + registrationKey.Vin
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
                    return new RegistrationKey
                    {
                        Username = parts[0],
                        Vin = parts[1]
                    };
                }
            }

            return null;
        }
    }
}
