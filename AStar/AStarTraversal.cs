using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTraversal.AStar
{
    public sealed class AStarTraversal
    {
        private const int clearPathValue = 0;

        private Node[,] map;
        Node current = null;
        Node start = new Node { X = 2, Y = 2 };
        Node target = new Node { X = 19, Y = 15 };

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        public AStarTraversal()
        { }

        public AStarTraversal(Node[,] map)
        {
            this.map = map;
        }

        public void SetMap(Node[,] map)
        {
            this.map = map;
        }

        public List<Node> GetPath()
        {
            // initialize
            var pathList = new List<Node>();            
            openList.Clear();
            closedList.Clear();

            // start by adding the original position to the open list
            openList.Add(start);

            ProcessNodes();

            // get final path node list
            while (current != null)
            {
                pathList.Add(new Node() { X = current.X, Y = current.Y });
                current = current.Parent;
            }

            return pathList;
        }

        private void ProcessNodes()
        {
            int g = 0;

            while (openList.Count > 0)
            {
                // get the node with the lowest F score
                var lowest = openList.Min(l => l.F);
                current = openList.First(l => l.F == lowest);
                closedList.Add(current);
                openList.Remove(current);

                // if we added the destination to the closed list, we've found a path
                if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                    break;                

                ProcessAdjacentNodes(++g);
            }
        }

        private void ProcessAdjacentNodes(int g)
        {
            var adjacentNodes = GetWalkableAdjacentNodes(current.X, current.Y);

            foreach (var adjacentNode in adjacentNodes)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.X == adjacentNode.X
                        && l.Y == adjacentNode.Y) != null)
                    continue;

                // if it's not in the open list...
                if (openList.FirstOrDefault(l => l.X == adjacentNode.X
                        && l.Y == adjacentNode.Y) == null)
                {
                    adjacentNode.G = g;
                    adjacentNode.H = ComputeHScore(adjacentNode.X, adjacentNode.Y, target.X, target.Y);
                    adjacentNode.F = adjacentNode.G + adjacentNode.H;
                    adjacentNode.Parent = current;
                    openList.Insert(0, adjacentNode);
                }
                else
                {
                    // test if using the current G score makes the adjacent square's F score
                    // lower, if yes update the parent because it means it's a better path
                    if (g + adjacentNode.H < adjacentNode.F)
                    {
                        adjacentNode.G = g;
                        adjacentNode.F = adjacentNode.G + adjacentNode.H;
                        adjacentNode.Parent = current;
                    }
                }
            }
        }

        private List<Node> GetWalkableAdjacentNodes(int x, int y)
        {
            var proposedNodes = new List<Node>()
            {
                new Node { X = x, Y = y - 1 },
                new Node { X = x, Y = y + 1 },
                new Node { X = x - 1, Y = y },
                new Node { X = x + 1, Y = y },
            };

            var proposedNodesList = proposedNodes.Where(IsAcceptableNode()).ToList();

            return proposedNodesList;
        }

        private Func<Node, bool> IsAcceptableNode()
        {
            return  l => l.X < this.map.GetLength(0) && 
                    l.Y < this.map.GetLength(1) && 
                    l.X >= 0 && l.Y >= 0 && 
                    this.map[l.X, l.Y].Value == clearPathValue;
        }

        private int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }
    }
}
