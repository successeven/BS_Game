using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BSGame.Model.AStar
{
    public class Grid
    {
        public Node[,] Nodes => _nodes;
        public float NodeRadius => _nodeRadius;
        public Vector2 GridWorldSize => _gridWorldSize;

        private LayerMask _wallMask; 
        private float _nodeRadius; 
        public float fDistanceBetweenNodes; 

        private Vector2 _gridWorldSize;
        private  Node[,] _nodes;
        private int _width, _height; 

        public Grid SetMask(LayerMask wallMask)
        {
            _wallMask = wallMask;
            return this;
        }

        public Grid SetNodeRadius(float nodeRadius)
        {
            _nodeRadius = nodeRadius;
            return this;
        }

        public Grid SetGridSize(Vector2 size)
        {
            _gridWorldSize = size;
            return this;
        }

        public void CreateGrid()
        {
            var nodeDiameter = _nodeRadius * 2;
            _width = Mathf.RoundToInt(_gridWorldSize.x / nodeDiameter);
            _height = Mathf.RoundToInt(_gridWorldSize.y / nodeDiameter);
            _nodes = new  Node[_width, _height];

            var bottomLeft = Vector2.zero
                             - Vector2.right * _gridWorldSize.x / 2
                             + Vector2.down * _gridWorldSize.y / 2;

            for (var x = 0; x < _width; x++)
                for (var y = 0; y < _height; y++)
                {
                    // var posX = x * nodeDiameter + _nodeRadius + bottomLeft.x;
                    // var posZ = z * nodeDiameter + bottomLeft.z;
                    // var worldPoint = new Vector3(posX, 0 , posZ);
                        
                        var worldPoint = bottomLeft
                                         + Vector2.right * (x * nodeDiameter + _nodeRadius)
                                         + Vector2.up* (y * nodeDiameter);

                    var checkPoint = worldPoint;

                    var wall = Physics2D.OverlapCircle(
                        checkPoint,
                        _nodeRadius,
                        _wallMask);

                    //_nodes.Add(new Node(wall != null, worldPoint, x, y));
                    _nodes[x, y] = new Node(wall != null, worldPoint, x, y);
                }
        }

        private void CheckNeighboringNodePos(List<Node> neighborNodes, int x, int y)
        {
            if (x < 0 || x >= _width) return;
            if (y < 0 || y >= _height) return;
            neighborNodes.Add(_nodes[x,y]);
        }

        public List<Node> GetNeighboringNodes(Node neighborNode, int radius)
        {
            var neighborList = new List<Node>(); //Make a new list of all available neighbors.

            //Check the right side of the current node.
            var posX = neighborNode.X + radius;
            var posY = neighborNode.Y;
            CheckNeighboringNodePos(neighborList, posX, posY);

            //Check the Left side of the current node.
            posX = neighborNode.X - radius;
            posY = neighborNode.Y;
            CheckNeighboringNodePos(neighborList, posX, posY);

            //Check the Top side of the current node.
            posX = neighborNode.X;
            posY = neighborNode.Y + radius;
            CheckNeighboringNodePos(neighborList, posX, posY);

            //Check the Bottom side of the current node.
            posX = neighborNode.X + radius;
            posY = neighborNode.Y - radius;
            CheckNeighboringNodePos(neighborList, posX, posY);
                
            posX = neighborNode.X + radius;
            posY = neighborNode.Y + radius;
            CheckNeighboringNodePos(neighborList, posX, posY);
                
            posX = neighborNode.X - radius;
            posY = neighborNode.Y - radius;
            CheckNeighboringNodePos(neighborList, posX, posY);
                
            posX = neighborNode.X - radius;
            posY = neighborNode.Y - radius;
            CheckNeighboringNodePos(neighborList, posX, posY);
                
            posX = neighborNode.X;
            posY = neighborNode.Y - radius;
            CheckNeighboringNodePos(neighborList, posX, posY);

            return neighborList;
        }

        public Node NodeFromWorldPoint(Vector2 worldPos)
        {
            //
            // var posX = (float)Math.Round(worldPos.x * 2, MidpointRounding.AwayFromZero) / 2;
            // var posY = (float)Math.Round(worldPos.y * 2, MidpointRounding.AwayFromZero) / 2;
            //
            // var gridWorldPos = new Vector2(posX+ _nodeRadius, posY);
            //
            // var resultNode = _nodes.FirstOrDefault(node => gridWorldPos == node.Position);
            // return resultNode;
            //
            var ixPos = ((worldPos.x + _gridWorldSize.x / 2) / _gridWorldSize.x);
            var iyPos = ((worldPos.y + _gridWorldSize.y / 2) / _gridWorldSize.y);
            
            ixPos = Mathf.Clamp01(ixPos);
            iyPos = Mathf.Clamp01(iyPos);
            
            var ix = Mathf.RoundToInt((_width - 1) * ixPos);
            var iy = Mathf.RoundToInt((_height - 1) * iyPos);
            
            // var resultNode = _nodes[ix, iy];//.FirstOrDefault(node => node.X  == ix  && node.Y == iy);
            // return resultNode;
             return _nodes[ix, iy];
        }
    }
}