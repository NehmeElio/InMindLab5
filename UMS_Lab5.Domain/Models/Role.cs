﻿using System;
using System.Collections.Generic;

namespace UMS_Lab5.Persistence.Models;

public partial class Role
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
