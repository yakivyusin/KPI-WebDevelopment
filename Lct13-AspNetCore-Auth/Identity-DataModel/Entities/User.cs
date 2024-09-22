using Microsoft.AspNetCore.Identity;

namespace Identity_DataModel.Entities;

public class User : IdentityUser<int>
{
    public int Age { get; set; }
}
