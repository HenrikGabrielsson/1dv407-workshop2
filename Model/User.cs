using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1dv407_workshop2.Model
{
    class User : IComparable, IComparable<User>
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

        public User(string name, string personalNum)
        {
            UniqueKey = GenerateUniqueKey();
            Name = name;
            PersonalNum = personalNum;
        }

        public int GenerateUniqueKey() {
            return new Random().Next(1, 10000000);
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


        public int CompareTo(object obj)
        {
            // Check if object is null.
            if (obj == null)
            {
                return 1;
            }

            // Type convert the reference obj from object to Member.
            // Throw error if it can't be converted
            User other = obj as User;
            if (other == null)
            {
                throw new ArgumentException();
            }

            // The UserID has been compared to with the CompareTo method 
            // that the String class implements.
            return UniqueKey.CompareTo(other.UniqueKey);
        }

        public int CompareTo(User other)
        {
            // Check if other is null.
            if (other == null)
            {
                return 1;
            }

            // The UserID has been compared to with the CompareTo method 
            // that the String class implements.
            return UniqueKey.CompareTo(other.UniqueKey);
        }
    }
}
