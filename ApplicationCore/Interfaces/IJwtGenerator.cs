using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user, IList<string> roles);
    }
}
