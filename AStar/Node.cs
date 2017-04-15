using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.AStar
{
   public class Node
    {
        public int X;
        public int Y;
        public int F;
        public int G;
        public int H;
        public int Value;
        public Node Parent;
    }
}
