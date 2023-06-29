﻿using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class SessionResult
{
    public Guid Id { get; set; }

    public Guid SessionId { get; set; }

    public string Result { get; set; } = null!;

    public virtual Session Session { get; set; } = null!;
}