using System;

namespace XRMWebUI.Library.Models
{
    public interface ILoggedInUser
    {
        string AuthUserId { get; set; }
        DateTime CreatedDate { get; set; }
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Token { get; set; }
    }
}