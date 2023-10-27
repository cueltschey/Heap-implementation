using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Heap_implementation
{
    internal class Heap
    {
        private Node root;
        private int depth;
        public Heap(int start)
        {
            root = new Node(start);
            depth = 0;
        }
        private string display(Node node, int curdepth = 0)
        {
            Console.Write("\n");
            string right;
            string left;
            if (curdepth < depth)
            {
                left = insert(node.children[0], data, curdepth + 1);
                if (left)
                {
                    return true;
                }
                right = insert(node.children[1], data, curdepth + 1);
                if (right)
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (node.degree() < 2)
                {
                    node.children.Add(new Node(data));
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /*
         This insert uses recursion to perform the following steps:
        1. check if the current node is at the last full 'row' of nodes (using curdepth)
        2. if  not keep calling itself otherwise fill up the bottom row with values
        3. if the bottom row is full return false
         */
        private bool insert(Node node, int data, int curdepth = 0)
        {
            bool right;
            bool left;
            if(curdepth < depth)
            {
                left = insert(node.children[0], data, curdepth + 1);
                if (left)
                {
                    return true;
                }
                right = insert(node.children[1], data, curdepth + 1);
                if (right)
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (node.degree() < 2)
                {
                    node.children.Add(new Node(data));
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /*
         This is the public facing insert:
        If the private insert returns false (the bottom row is full) then the depth is incremented
        and the private insert is called again.
         */
        public void insert(int data)
        {
            if(!insert(root, data))
            {
                depth += 1;
                insert(root, data);
            }
        }

        private bool makeMin(Node node, int data, int curdepth = 0)
        {
            bool right;
            bool left;
            if (curdepth < depth)
            {
                left = insert(node.children[0], data, curdepth + 1);
                if (left)
                {
                    return true;
                }
                right = insert(node.children[1], data, curdepth + 1);
                if (right)
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (node.degree() < 2)
                {
                    node.children.Add(new Node(data));
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
