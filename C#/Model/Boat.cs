using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1dv407_workshop2.Model
{
    class Boat
    {
        public BoatType Type { get; set; }
        public int Length { get; set; }
        public User Owner { get; set; }
        public int UniqueId { get; set; }

        public Boat(BoatType type, int length, User owner, int uniqueId)
        {
            UniqueId = uniqueId;
            Type = type;
            Length = length;
            Owner = owner;
        }

    }

    //All the possible boat types.
    public enum BoatType
    {
        Segelbåt = 1,
        Motorseglare = 2,
        Motorbåt = 3,
        KajakKanot = 4,
        Övrig = 5
    }

}

