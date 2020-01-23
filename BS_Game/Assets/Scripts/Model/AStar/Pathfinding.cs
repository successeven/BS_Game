using System.Collections.Generic;
using UnityEngine;

namespace BSGame.Model.AStar
{
    public static class Pathfinding
    {
        public static List<Node> FindPath(Grid grid, Vector2 startPos, Vector2 targetPos)
        {
            var startNode = grid.NodeFromWorldPoint(startPos); //Gets the node closest to the starting position
            var targetNode = grid.NodeFromWorldPoint(targetPos); //Gets the node closest to the target position

            if (startNode.IsWall || targetNode.IsWall)
                return null;
            
            var openList = new List<Node>(); //List of nodes for the open list
            var closedList = new HashSet<Node>(); //Hashset of nodes for the closed list

            openList.Add(startNode);

            while (openList.Count > 0) 
            {
                var currentNode = openList[0]; 
                
                for(var i = 1; i < openList.Count; i++)//Loop through the open list starting from the second object
                {
                    if (openList[i].FCost < currentNode.FCost 
                        || openList[i].FCost == currentNode.FCost 
                        && openList[i].hCost < currentNode.hCost)
                    {
                        currentNode = openList[i];//Set the current node to that object
                    }
                }
                openList.Remove(currentNode); 
                closedList.Add(currentNode);
                
                if (currentNode == targetNode) //If the current node is the same as the target node
                    return GetFinalPath(startNode, targetNode);

                foreach (var neighborNode in grid.GetNeighboringNodes(currentNode, 1))// Mathf.RoundToInt(radius))) //Loop through each neighbor of the current node
                {
                    if (neighborNode.IsWall || closedList.Contains(neighborNode))
                        continue; 

                    var moveCost = currentNode.gCost + GetManhattenDistance(currentNode, neighborNode); 
                    if (moveCost >= neighborNode.gCost && openList.Contains(neighborNode)) continue;
                    
                    neighborNode.gCost = moveCost; //Set the g cost to the f cost
                    neighborNode.hCost = GetManhattenDistance(neighborNode, targetNode); //Set the h cost
                    neighborNode.ParentNode = currentNode; 
                    
                    if (!openList.Contains(neighborNode)) 
                        openList.Add(neighborNode);
                }
            }
            return null;
        }

        private static List<Node> GetFinalPath(Node a_StartingNode, Node a_EndNode)
        {
            var finalPath = new List<Node>(); 
            var currentNode = a_EndNode; //Node to store the current node being checked
            while (currentNode != a_StartingNode) //While loop to work through each node going through the parents to the beginning of the path
            {
                finalPath.Add(currentNode); //Add that node to the final path
                currentNode = currentNode.ParentNode; //Move onto its parent node
            }

            finalPath.Reverse(); //Reverse the path to get the correct order
            return finalPath; //Set the final path
        }

        private static int GetManhattenDistance(Node a_nodeA, Node a_nodeB)
        {
            var ix = Mathf.Abs(a_nodeA.X - a_nodeB.X); //x1-x2
            var iy = Mathf.Abs(a_nodeA.Y - a_nodeB.Y); //y1-y2
            return ix + iy;
        }
    }
}