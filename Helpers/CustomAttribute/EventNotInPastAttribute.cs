using System.ComponentModel.DataAnnotations;

namespace EventProjectApi.Helpers.CustomAttribute
{
    public class EventNotInPastAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if ( value is DateTime dateTime )
            {
                return dateTime > DateTime.Now;
            }

            return false;
        }
    }
}
