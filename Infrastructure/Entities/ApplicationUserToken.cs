using System;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities
{
    public class ApplicationUserToken
        : IdentityUserToken<Guid>
    {

    }
}
