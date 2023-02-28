using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using ConnectedCar.Core.Shared.Data.Attributes;
using Newtonsoft.Json;
using System;

namespace ConnectedCar.Core.Services.Data.Converters
{
    public class ColorsConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            if (!(value is Colors colors)) throw new ArgumentException();

            DynamoDBEntry entry = new Primitive
            {
                Value = JsonConvert.SerializeObject(colors, Formatting.Indented)
            };

            return entry;
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            try
            {
                if (entry is Primitive primitive && primitive.Value is string input && !string.IsNullOrEmpty(input))
                {
                    return JsonConvert.DeserializeObject<Colors>(input);
                }
            }
            catch (Exception)
            {
            }

            return null;
        }
    }
}
