using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trees
{
    public partial class Form1 : Form 
    {
        static readonly Random r = new Random();
        AvlTree<int> AVLtree;
        RBTree<int> RBTree;
        Treap<int> treap;
        List<Label> labels;
        public Form1()
        {
            labels = new List<Label>();
            AVLtree = new AvlTree<int>();  
            RBTree = new RBTree<int>();
            treap = new Treap<int>();
            RBTree.root = null;
            AVLtree.root = null;
            InitializeComponent();
        }

        public void Draw(Node<int> node, Graphics g, int x, int y, int w, int h, int parent_x, int parent_y, int id)
        {
            if (node == null)
            {
                return;
            }
            Pen p = new Pen(Brushes.Black, 2f);
            if (node.color == 0)
                p.Color = Color.Red;
           
            g.DrawArc(p, x, y, w, h, 0, 360);
            Label label = new Label();
            label.Text = node.Value.ToString();
            label.Location = new Point(x + w / 2 - w / 5, y + h / 2 - h / 6);
            label.AutoSize = true;
            label.BackColor = Color.Transparent;
            labels.Add(label);
            switch (id)
            {
                case 1:
                    tabPage1.Controls.Add(label);
                    break;
                case 2:
                    tabPage2.Controls.Add(label);
                    break; 
                case 3:
                    tabPage3.Controls.Add(label);
                    break;
            }

            int add = w / 2;
            p.Color = Color.Black;
            if (node.left != null)
            {
                g.DrawLine(p, x + add, y + 2 * add, x - Math.Abs(x - parent_x) / 2 + add, y + h + 50);
            }
            if (node.right != null)
            {
                g.DrawLine(p, x + add, y + 2 * add, x + Math.Abs(parent_x - x) / 2 + add, y + h + 50);
            }
            Draw(node.left, g, x - Math.Abs(x - parent_x) / 2, y + h + 50, w, h, x, y, id);
            Draw(node.right, g, x + Math.Abs(parent_x - x) / 2, y + h + 50, w, h, x, y, id);
        }

        private int[] convertTextToArray(string text)
        {
            try
            {
                string[] str = text.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                int[] result = new int[str.Length];
                for (int i = 0; i < str.Length; ++i)
                {
                    result[i] = Convert.ToInt32(str[i]);
                }
                return result;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return new int[] { };
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SetList_Click(object sender, EventArgs e)
        {
            AVLtree.root = null;
            foreach (var i in labels)
            {
                tabPage1.Controls.Remove(i);
            }
            labels.Clear();
            int[] array = convertTextToArray(richTextBox1.Text);
            if (array.Length == 0)
            {
                return;
            }
            for (int i = 0; i < array.Length; ++i)
            {
                AVLtree.root = AVLtree.insert( AVLtree.root, array[i]);
            }
            tabPage1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var i in labels)
            {
                tabPage1.
                Controls.Remove(i);
            }
            labels.Clear();
            int[] array = convertTextToArray(richTextBox2.Text);
            for (int i = 0; i < array.Length; ++i)
            {
                AVLtree.root  = AVLtree.remove(AVLtree.root, array[i]);
            }
            tabPage1.Invalidate();
        }

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (AVLtree != null)
            {
                Draw(AVLtree.root, g, AVLtree.getHeight(AVLtree.root) * (120), 100, 25, 25, 0, 100, 1);
            }
        }

        private void tabPage2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (RBTree != null)
            {
                Draw(RBTree.root, g, RBTree.getHeight(RBTree.root) * (120), 100, 25, 25, 0, 100, 2);
            }
        }

        private void tabPage3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (treap != null)
            {
                Draw(treap.root, g, treap.getHeight(treap.root) * (120), 100, 25, 25, 0, 100, 3);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RBTree.root = null;
            foreach (var i in labels)
            {
                tabPage2.Controls.Remove(i);
            }
            labels.Clear();
            int[] array = convertTextToArray(richTextBox3.Text);
            if (array.Length == 0)
            {
                return;
            }
            for (int i = 0; i < array.Length; ++i)
            {
                RBTree.insert(array[i]);
            }
            tabPage2.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var i in labels)
            {
                tabPage2.Controls.Remove(i);
            }
            labels.Clear();
            int[] array = convertTextToArray(richTextBox4.Text);
            for (int i = 0; i < array.Length; ++i)
            {
                RBTree.remove(array[i]);
            }
            tabPage2.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            treap.root = null;
            foreach (var i in labels)
            {
                tabPage3.Controls.Remove(i);
            }
            labels.Clear();
            int[] array = convertTextToArray(richTextBox5.Text);
            if (array.Length == 0)
            {
                return;
            }
            for (int i = 0; i < array.Length; ++i)
            {
                treap.insert(array[i], r.Next());
            }
            tabPage3.Invalidate();
        }

    }
}
