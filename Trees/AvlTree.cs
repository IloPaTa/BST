using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Trees
{
    public class AvlTree<T> where T : IComparable
    {   
        public int getHeight(Node<T> p)
        {
            if (p == null)
                return 0;
            return p.height;
        }

        public int getBalance(Node<T> p)
        {
            if (p == null) return 0;
            return (p.right == null ? 0 : getHeight(p.right)) - (p.left == null ? 0 : getHeight(p.left));
        }

        public void fixHeight(Node<T> p)
        {
            if (p == null) return;
            int h1 = getHeight(p.left);
            int h2 = getHeight(p.right);
            p.height = (h1 > h2 ? h1 : h2) + 1;
        }

        public Node<T> root { get; set; }

        private Node<T> rotateRight(Node<T> p)
        {
            Node<T> q = p.left;
            p.left = q.right;
            q.right = p;
            fixHeight(p);
            fixHeight(q);
            return q;
        }

        private Node<T> rotateLeft(Node<T> q)
        {
            Node<T> p = q.right;
            q.right = p.left;
            p.left = q;
            fixHeight(q);
            fixHeight(p);
            return p;
        }

        private Node<T> balance(Node<T> p)
        {
            if (p == null) return null;
            fixHeight(p);
            if (getBalance(p) == -2)
            {
                if (getBalance(p.left) > 0)
                {
                    p.left = rotateLeft(p.left);
                }
                return rotateRight(p);
            }
            if (getBalance(p) == 2)
            {
                if (getBalance(p.right) < 0)
                {
                    p.right = rotateRight(p.right);
                }
                return rotateLeft(p);
            }
            return p;
        }

        public Node<T> insert(Node<T> p, T k)
        {
            if (p == null) { return new Node<T>(k); };
            if (k.CompareTo(p.Value) < 0)
            {
                p.left = insert(p.left, k);
            }
            else
            {
                p.right = insert(p.right, k);
            }
            return balance(p);
        }

        Node<T> findmin(Node<T> p)
        {
            return p.left != null ? findmin(p.left) : p;
        }

        Node<T> removemin(Node<T> p) 
        {
            if (p.left == null)
                return p.right;
            p.left = removemin(p.left);
            return balance(p);
        }

        public Node<T> remove(Node<T> p, T k) 
        {
            if (p == null) return null;
            if (k.CompareTo(p.Value) < 0)
                p.left = remove(p.left, k);
            else if (k.CompareTo(p.Value) > 0)
                p.right = remove(p.right, k);
            else 
            {
                Node<T> q = p.left;
                Node<T> r = p.right;
                if (r == null) return q;
                Node<T> min = findmin(r);
                min.right = removemin(r);
                min.left = q;
                return balance(min);
            }
            return balance(p); 
        }
    }
}
