using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using ConnectedCar.Core.Shared.Data.Attributes;
using Newtonsoft.Json;
using System;

namespace ConnectedCar.Core.Services.Data.Converters
{
    public class AddressConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            if (!(value is Address address)) throw new ArgumentException();

            DynamoDBEntry entry = new Primitive
            {
                Value = JsonConvert.SerializeObject(address, Formatting.Indented)
            };

            return entry;
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            try
            {
                if (entry is Primitive primitive && primitive.Value is string input && !string.IsNullOrEmpty(input))
                {
                    return JsonConvert.DeserializeObject<Address>(input);
                }
            }
            catch (Exception)
            {
            }

            return null;
        }
    }
}
