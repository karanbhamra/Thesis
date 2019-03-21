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

        public string PreviousRecordHash { get; set; } // previous hash

        public string CurrentNodeHash { get; set; }    // current hash will be the hash of current object + hash of salt 

        public string Salt { get; set; }    // salt will be hashed and then added to current nodehash

    }
}
