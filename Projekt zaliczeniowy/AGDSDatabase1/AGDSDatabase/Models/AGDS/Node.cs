using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGDSDatabase.Models.AGDS
{
    public class Node<T>
    {
        private List<T> branches;

        public Node()
        {
            branches = new List<T>();
        }

        public List<T> Branches
        {
            get
            {
                return branches;
            }
            set
            {
                branches = value;
            }
        }

        public List<T> GetBranches()
        {
            return branches;
        }

        public virtual void AddNewValue(T val)
        {
            if(isValid(val))
            {
                branches.Add(val);
            }
        }

        private bool isValid(T val)
        {
            return val != null && !branches.Contains(val);
        }
    }
}
