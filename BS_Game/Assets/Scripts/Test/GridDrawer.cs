using System.Collections;
using System.Collections.Generic;
using BSGame.Manager;
using BSGame.Model;
using BSGame.Model.AStar;
using UnityEngine;

public class GridDrawer : MonoBehaviour
{
    public List<Node> CurrentPath;
    private void OnDrawGizmos()
    {
        var gameModel = MainApp.Instance?.GameModel;

        var grid = gameModel?.GridAStar;
        if (grid?.Nodes == null) return;

        foreach (Node n in grid.Nodes) //Loop through every node in the grid
        {
            Gizmos.color = n.IsWall ? Color.yellow : Color.white;
            Gizmos.DrawSphere(n.Position, grid.NodeRadius ); //Draw the node at the position of the node.
        }
            
        if (CurrentPath == null) return;
        Debug.Log("Путь есть " + CurrentPath.Count);
        foreach (Node n in CurrentPath) //Loop through every node in the grid
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(n.Position, grid.NodeRadius );
            
        }
    }
}
