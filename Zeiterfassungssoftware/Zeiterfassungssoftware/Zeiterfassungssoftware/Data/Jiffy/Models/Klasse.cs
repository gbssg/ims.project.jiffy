using System;
using System.Collections.Generic;

namespace Zeiterfassungssoftware.Data.Jiffy.Models;

public partial class Klasse
{
    public int Id { get; set; }

    public int StartJahr { get; set; }

    public string Name { get; set; } = null!;
}
