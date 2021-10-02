﻿using System;
using System.Collections.Generic;
using System.Text;
using NodaTime;

namespace AV.AvA.Model;

public class PersonVersion
{
    public int PersonVersionId { get; set; }

    public int AvId { get; set; }

    public Person Person { get; set; } = default!;

    public int? ComitterAvId { get; set; }

    public Instant CommittedAt { get; set; }

    public string CommitMessage { get; set; } = default!;

    public string Software { get; set; } = default!;
}
