using System;
using System.Collections.Generic;
using System.Text;

namespace AV.AvA.Model
{
    public class PersonVersion
    {
        public int PersonVersionId { get; set; }

        public int AvId { get; set; }

        public Person Person { get; set; }
    }
}
