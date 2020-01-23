using UnityEngine;

namespace BSGame.Model.AStar
{
    public class Node {
        public int FCost => gCost + hCost;        
        public Node ParentNode { get; set; }

        public int X;//X Position in the Node Array
        public int Y;//Y Position in the Node Array
        public bool IsWall;//Tells the program if this node is being obstructed.
        public Vector2 Position;//The world position of the node.
        public int gCost;//The cost of moving to the next square.
        public int hCost;//The distance to the goal from this node.

        public Node(bool isWall, Vector2 position, int x, int y)//Constructor
        {
            IsWall = isWall;//Tells the program if this node is being obstructed.
            Position = position;//The world position of the node.
            X = x;
            Y = y;
        }

    }
}

