using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.BST
{
    public class BinaryTree
    {
        private TreeNode root = null;
        private int count = 0;

        public TreeNode Root { get { return root; } }

        public int Count { get; private set; }
        public BinaryTree()
        { }
        public bool IsEmpty()
        {
            return root == null;
        }

        public void Insert(int data)
        {
            if (IsEmpty())
            {
                root = new TreeNode(data);
            }
            else
            {
                InsertNode(ref root, data);
            }

            this.count++;
        }

        private void InsertNode(ref TreeNode node, int value)
        {
            if (node == null)
            {
                node = new TreeNode(value);
            }
            else if (node.Value < value)
            {
                InsertNode(ref node.RightLeaf, value);
            }
            else if (node.Value > value)
            {
                InsertNode(ref node.LeftLeaf, value);
            }
        }
    }
}
