using System;
using System.Collections.Generic;
using System.Text;
using NodaTime;

namespace AV.AvA.StorageModel
{
    public class LoginToken
    {
        public int LoginTokenId { get; set; }

        public string Token { get; set; } = default!;

        public int AvId { get; set; }

        public Instant CreatedAt { get; set; }

        public Instant? ValidUntil { get; set; }

        public Instant? UsedAt { get; set; }
    }
}
