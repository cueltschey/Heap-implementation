using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap_implementation
{
    public class Node
    {
        public int data;
        public List<Node> children;
        public Node(int data)
        {
            children = new List<Node>(2);
            this.data = data;
        }
        public int degree()
        {
            return children.Count;
        }
    }
}
