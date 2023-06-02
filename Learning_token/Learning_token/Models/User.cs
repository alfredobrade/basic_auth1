using System;
using System.Collections.Generic;

namespace Learning_token.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }
}
