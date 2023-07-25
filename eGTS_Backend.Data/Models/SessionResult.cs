using System;
using System.Collections.Generic;

namespace eGTS_Backend.Data.Models;

public partial class SessionResult
{
    public Guid Id { get; set; }

    public Guid SessionId { get; set; }

    public string Result { get; set; } = null!;

    public bool IsDelete { get; set; }

    public SessionResult(Guid id, Guid sessionId, string result, bool isDelete)
    {
        Id = id;
        SessionId = sessionId;
        Result = result;
        IsDelete = isDelete;
    }

    public virtual Session Session { get; set; } = null!;
}
