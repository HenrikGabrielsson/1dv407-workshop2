using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1dv407_workshop2.Model
{
    class User
    {
        private string name;
        private int uniqueKey;
        private string personalNum;


        public User(int uniqueKey, string name, string personalNum)
        {
            UniqueKey = uniqueKey;
            Name = name;
            PersonalNum = personalNum;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ApplicationException("Name is missing!");
                }
                this.name = value;
            }
        }

        public int UniqueKey
        {
            get
            {
                return this.uniqueKey;
            }
            set
            {

                this.uniqueKey = value;
            }
        }

        public string PersonalNum
        {
            get
            {
                return this.personalNum;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ApplicationException("PersonalNum is missing!");
                }
                this.personalNum = value;
            }
        }


    }
}
