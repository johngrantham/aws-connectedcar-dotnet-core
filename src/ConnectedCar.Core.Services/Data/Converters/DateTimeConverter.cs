using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Globalization;

namespace ConnectedCar.Core.Services.Data.Converters
{
    public class DateTimeConverter : IPropertyConverter
    {
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public DynamoDBEntry ToEntry(object value)
        {
            if (!(value is DateTime dateTime)) throw new ArgumentException();

            DynamoDBEntry entry = new Primitive
            {
                Value = dateTime.ToString(DateTimeFormat)
            };

            return entry;
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            if (entry is Primitive primitive && primitive.Value is string input && !string.IsNullOrEmpty(input))
            {
                if (DateTime.TryParseExact(input, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
                {
                    return dateTime;
                }
            }

            return null;
        }
    }
}
