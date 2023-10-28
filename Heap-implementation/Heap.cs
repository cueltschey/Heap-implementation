using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Heap_implementation
{
    internal class Heap
    {
        private Node root;
        private int depth;
        private string minOrMax;
        public Heap(int start)
        {
            root = new Node(start);
            depth = 0;
        }
        // private facing display that prints vertically in preorder
        private void display(Node node, int curdepth = 0)
        {
            if (curdepth < depth)
            {
                Console.WriteLine(node.data);
                display(node.children[0], curdepth + 1);
                display(node.children[1], curdepth + 1);
            }
            else
            {
                if (node.degree() == 0)
                {
                    Console.WriteLine(node.data);
                }
                else
                {
                    Console.WriteLine(node.data);
                    foreach (Node n in node.children)
                    {
                        Console.WriteLine(n.data);
                    }
                }
            }
        }
        //public facing display that prints in preorder
        public void display()
        {
            Console.WriteLine("Depth: " + depth);
            display(root);
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
         This is the public facing insert:
        If the private insert returns false (the bottom row is full) then the depth is incremented
        and the private insert is called again.
         */
        public void insert(int data)
        {
            if (!insert(root, data))
            {
                depth += 1;
                insert(root, data);
            }

            makeMin(root);
        }

        private void makeMin(Node node, int curdepth = 0)
        {
            if (curdepth < depth)
            {
                makeMin(node.children[0], curdepth + 1);
                makeMin(node.children[1], curdepth + 1);
                foreach (Node n in node.children)
                {
                    if (n.data < node.data)
                    {
                        int inter = n.data;
                        n.data = node.data;
                        node.data = inter;
                    }
                }
            }
            else
            {
                if (node.degree() != 0)
                {
                    foreach (Node n in node.children)
                    {
                        if (n.data < node.data)
                        {
                            int inter = n.data;
                            n.data = node.data;
                            node.data = inter;
                        }
                    }
                }
            }
        }
        private void balance(Node node)
        {
            foreach (Node n in node.children)
            {
                if (n.data < node.data)
                {
                    int inter = node.data;
                    node.data = n.data;
                    n.data = inter;
                    balance(n);
                }
            }

        }
        private bool swap_values(Node node, int curdepth = 0)
        {
            bool right;
            bool left;
            if (curdepth < depth)
            {
                right = swap_values(node.children[1], curdepth + 1);
                if (!right)
                {
                    left = swap_values(node.children[0], curdepth + 1);
                    return left;
                }
                else
                {
                    return right;
                }
            }
            else
            {
                if (node.degree() != 0)
                {
                    int last = node.children.Count - 1;
                    int res = node.children[last].data;
                    node.children.RemoveAt(last);
                    root.data = res;
                    return true;
                }
                return false;
            }
        }
        public int? remove()
        {
            int? res = root.data;
            swap_values(root);
            balance(root);
            return res;
        }
    }
}
