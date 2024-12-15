using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Trees
{

    public class RBTree<T> where T : IComparable
    {
        public Node<T> root { get; set; }

        public int getHeight(Node<T> p)
        {
            return p == null ? 0 : Math.Max(getHeight(p.right), getHeight(p.left)) + 1;
        }

        public RBTree()
        {
            root = null;
        }

        public void rotateLeft(Node<T> node)
        {
            Node<T> child = node.right;
            node.right = child.left;
            if (node.right != null) {
                node.right.parent = node;   
            }
            child.parent = node.parent;
            if (node.parent == null) 
                root = child;
            else if(node == node.parent.left)
                node.parent.left = child;
            else
                node.parent.right = child;
            child.left = node;
            node.parent = child;
        }

        public void rotateRight(Node<T> node)
        {
            Node<T> child = node.left;
            node.left = child.right;
            if (node.left != null)
            {
                node.left.parent = node;
            }
            child.parent = node.parent;
            if (node.parent == null)
                root = child;
            else if (node == node.parent.left)
                node.parent.left = child;
            else
                node.parent.right = child;
            child.right = node;
            node.parent = child;
        }

        private void fixInsert(Node<T> node)
        {
            Node<T> parent = null;
            Node<T> gparent = null;
            while (node != root && node.color == 0 && node.parent.color == 0)
            {
                parent = node.parent;
                gparent = parent.parent;
                if (parent == gparent.left)
                {
                    Node<T> uncle = gparent.right;
                    if (uncle != null && uncle.color == 0)
                    {
                        gparent.color = 0;
                        parent.color = 1;
                        uncle.color = 1;
                        node = gparent;
                    }
                    else
                    {
                        if (node == parent.right)
                        {
                            rotateLeft(parent);
                            node = parent;
                            parent = node.parent;
                        }
                        rotateRight(gparent);
                        (parent.color, gparent.color) = (gparent.color, parent.color);
                        node = parent;
                    }
                }
                else
                {
                    Node<T> uncle = gparent.left;
                    if(uncle != null && uncle.color == 0)
                    {
                        gparent.color = 0;
                        parent.color = 1;
                        uncle.color = 1;
                        node = gparent;
                    }
                    else
                    {
                        if(node == parent.left)
                        {
                            rotateRight(parent);
                            node = parent;  
                            parent = node.parent;
                        }
                        rotateLeft(gparent);
                        (parent.color, gparent.color) = (gparent.color, parent.color);
                        node = parent;
                    }
                }
            }
            root.color = 1;
        }

        private void transplant(Node<T> root, Node<T> u, Node<T> v)
        {
            if (u.parent == null)
                root = v;
            else if (u == u.parent.left)
                u.parent.left = v;
            else
                u.parent.right = v;
            if (v != null)
                v.parent = u.parent;
        }

        private void fixDelete(Node<T> node)
        {
            while(node != root && node.color == 1)
            {
                if (node == node.parent.left)
                {
                    Node<T> sibling = node.parent.right;
                    if(sibling.color == 0)
                    {
                        sibling.color = 1;
                        node.parent.color = 0;
                        rotateLeft(node.parent);
                        sibling = node.parent.right;
                    }
                    if((sibling.left == null || sibling.left.color == 1) && 
                        (sibling.right == null || sibling.right.color == 1))
                    {
                        sibling.color = 0;
                        node = node.parent;
                    }
                    else
                    {
                        if(sibling.right == null|| sibling.right.color == 1)
                        {
                            if (sibling.left != null)
                            {
                                sibling.left.color = 1;
                            }
                            sibling.color = 0;
                            rotateRight(sibling);
                            sibling = node.parent.right;
                        }
                        sibling.color = node.parent.color;
                        node.parent.color = 1;
                        if(sibling.right != null)
                        {
                            sibling.right.color = 1;
                        }
                        rotateLeft(node.parent);
                        node = root;
                    }
                }
                else
                {
                    Node<T> sibling = node.parent.right;
                    if (sibling.color == 0)
                    {
                        sibling.color = 1;
                        node.parent.color = 0;
                        rotateRight(node.parent);
                        sibling = node.parent.left;
                    }
                    if ((sibling.left == null || sibling.left.color == 1) &&
                        (sibling.right == null || sibling.right.color == 1))
                    {
                        sibling.color = 0;
                        node = node.parent;
                    }
                    else
                    {
                        if (sibling.left == null || sibling.left.color == 1)
                        {
                            if (sibling.right != null)
                            {
                                sibling.right.color = 1;
                            }
                            sibling.color = 0;
                            rotateLeft(sibling);
                            sibling = node.parent.left;
                        }
                        sibling.color = node.parent.color;
                        node.parent.color = 1;
                        if (sibling.left != null)
                        {
                            sibling.left.color = 1;
                        }
                        rotateLeft(node.parent);
                        node = root;
                    }
                }
            }
            node.color = 1;
        }

        Node<T> minValueNode(Node<T> node)
        {
            Node<T> current = node;
            while (current.left != null)
                current = current.left;
            return current;
        }

        public void remove(T k)
        {
            Node<T> node = root;
            Node<T> z = null;
            Node<T> x = null;
            Node<T> y = null;
            while(node != null)
            {
                if(k.CompareTo(node.Value) == 0)
                {
                    z = node;
                }
                if(k.CompareTo(node.Value) > 0)
                {
                    node = node.right;
                }
                else
                {
                    node = node.left;
                }
            }

            if(z == null)
            {
                return;
            }
            y = z;
            int originColor = y.color;
            if (z.left == null)
            {
                x = z.right;
                transplant(root, z, z.right);
            }
            else if (z.right == null)
            {
                x = z.left;
                transplant(root, z, z.left);
            }
            else
            {
                y = minValueNode(z.right);
                originColor = y.color;
                x = y.right;
                if (y.parent == z)
                {
                    if (x != null)
                        x.parent = y;
                }
                else
                {
                    transplant(root, y, y.right);
                    y.right = z.right;
                    y.right.parent = y;
                }
                transplant(root, z, y);
                y.left = z.left;
                y.left.parent = y;
                y.color = z.color;
            }
            if (originColor == 1)
            {
                fixDelete(x);
            }
        }
        public void insert(T k)
        {
            Node<T> node = new Node<T>(k);
            Node<T> parent = null;
            Node<T> current = root;
            while (current != null)
            {
                parent = current;
                if (k.CompareTo(current.Value) < 0)
                {
                    current = current.left;
                }
                else
                {
                    current = current.right;
                }
            }
            node.parent = parent;
            if (parent == null)
            {
                root = node;
            }
            else if (k.CompareTo(parent.Value) < 0)
            {
                parent.left = node;
            }
            else
            {
                parent.right = node;
            }
            fixInsert(node);
        }
    }
}
