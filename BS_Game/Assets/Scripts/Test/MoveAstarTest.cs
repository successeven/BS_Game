using System.Collections;
using System.Collections.Generic;
using BSGame.Manager;
using BSGame.Model;
using BSGame.Model.AStar;
using UnityEngine;

public class MoveAstarTest : MonoBehaviour
{
    [SerializeField] private GridDrawer _gridDrawer;
    public LayerMask hitLayers;
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))//If the player has left clicked
        {
            Vector3 mouse = Input.mousePosition;//Get the mouse Position
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);//Cast a ray to get where the mouse is pointing at
            RaycastHit hit;//Stores the position where the ray hit.
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, hitLayers))//If the raycast doesnt hit a wall
            {
               
                var gameModel = MainApp.Instance.GameModel;
                var gridAStar = gameModel.GridAStar;
                var mousePoint = new Vector2(hit.point.x, hit.point.y);
                _gridDrawer.CurrentPath = Pathfinding.FindPath(gridAStar, 
                    this.transform.localPosition, 
                    mousePoint);
            }
            
        }
    }
}

