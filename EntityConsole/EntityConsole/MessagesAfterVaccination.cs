using System;
using System.Collections.Generic;

namespace EntityConsole;

public partial class MessagesAfterVaccination
{
    public int MessageId { get; set; }

    public string? Description { get; set; }

    public DateTime Date { get; set; }

    public string? Recommendations { get; set; }

    public string Doctor { get; set; } = null!;
}
