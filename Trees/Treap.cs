using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    public class Treap<T> where T : IComparable
    {
        public Node<T> root;

        public Treap(Node<T> node = null)
        {
            root = node;
        }
        private Node<T> merge(Node<T> l, Node<T> r)
        {
            if (l == null) return r;
            if (r == null) return l;
            if(l.key > r.key)
            {
                l.right = merge(l.right, r);
                return l;
            }
            else
            {
                r.left = merge(l, r.left);
                return r;
            }
        }

        public int getHeight(Node<T> p)
        {
            return p == null ? 0 : Math.Max(getHeight(p.right), getHeight(p.left)) + 1;
        }

        private (Node<T>, Node<T>) split(Node<T> p, T x)
        {
            if (p == null) return (null, null);
            if(x.CompareTo(p.Value) > 0)
            {
                Node<T> l;
                Node<T> r;
                (l, r) = split(p.right, x);
                p.right = l;
                return (p, r);
            }
            else
            {
                Node<T> l;
                Node<T> r;
                (l, r) = split(p.left, x);
                p.left = r;
                return (l, p);
            }
        }

        public void insert(T x, int key)
        {
            Node<T> l;
            Node<T> r;
            (l, r) = split(root, x);
            Node<T> t = new Node<T> (x, key);
            root = merge(l, merge(t, r));
        }
            
    }
}
