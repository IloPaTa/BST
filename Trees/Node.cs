using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    public class Node<T> where T : IComparable
    {
        public T Value { get; set; }
        public int key;
        public Node<T> left;
        public Node<T> right;
        public Node<T> parent;
        public int height;
        public int color;
        public Node(T k = default(T), int _key = 0)
        {
            Value = k;
            key = _key;
            color = 0; 
            parent = null;
            left = null;
            right = null;
            height = 1;
        }
    }
}
