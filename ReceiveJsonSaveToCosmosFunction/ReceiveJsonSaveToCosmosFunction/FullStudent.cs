using System;
using System.Collections.Generic;
using System.Text;

namespace ReceiveJsonSaveToCosmosFunction
{
    class FullStudent
    {
        public int RecordId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Organization { get; set; }

        public string SchoolDivision { get; set; }

        public string Degree { get; set; }

        public DateTime Awarded { get; set; }

        public string Major { get; set; }

        public int PreviousRecordHash { get; set; } // previous hash

        public int CurrentNodeHash { get; set; }    // current hash

    }
}
