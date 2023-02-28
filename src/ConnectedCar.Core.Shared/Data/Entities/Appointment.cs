using ConnectedCar.Core.Shared.Data.Attributes;
using Newtonsoft.Json;

namespace ConnectedCar.Core.Shared.Data.Entities
{
    public class Appointment : BaseEntity
    {
        [JsonProperty("appointmentId")]
        public string AppointmentId { get; set; }

        [JsonProperty("timeslotKey")]
        public TimeslotKey TimeslotKey { get; set; }

        [JsonProperty("registrationKey")]
        public RegistrationKey RegistrationKey { get; set; }

        public override bool Validate()
        {
            return TimeslotKey != null && TimeslotKey.Validate() &&
                   RegistrationKey != null && RegistrationKey.Validate();
        }
    }
}
