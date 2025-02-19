﻿using System;
using System.Collections.Generic;


namespace ClientSamokat.Model;

public partial class Person
{
    public int PersonId { get; set; }

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Age { get; set; }

    public virtual Courier? Courier { get; set; }
}
