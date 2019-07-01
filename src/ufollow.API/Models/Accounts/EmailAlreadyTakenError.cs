using ufollow.API.Models.Errors;

namespace ufollow.API.Models.Accounts
{
    public sealed class EmailAlreadyTakenError : UnprocessableEntityJson
    {
        public EmailAlreadyTakenError()
        {
            Error = "EMAIL_ALREADY_TAKEN";
        }
    }
}
