using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.BST
{
    public class TreeNode
    {
        private int value;
        public TreeNode RightLeaf;
        public TreeNode LeftLeaf;
        public int Value { get { return value; } } 
        public TreeNode(int value)
        {
            this.value = value;
            this.RightLeaf = null;
            this.LeftLeaf = null;
        }

        public bool IsLeaf(ref TreeNode node)
        {
            return (node.RightLeaf == null && node.LeftLeaf == null);
        }
    }
}
