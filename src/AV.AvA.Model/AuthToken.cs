using System;
using System.Collections.Generic;
using System.Text;
using NodaTime;

namespace AV.AvA.Model
{
    public class AuthToken
    {
        public int AuthTokenId { get; set; }

        public string Token { get; set; }

        public int AvId { get; set; }

        public Instant CreatedAt { get; set; }

        public Instant? ValidUntil { get; set; }

        public Instant? UsedAt { get; set; }

    }
}
