using System;
using System.Collections.Generic;
using UnityEngine;

public class JY_PathFinder
{
    private const int STRAIGHT_MOVE_COST = 10;
    private const int DIAGONAL_MOVE_COST = 14;

    private JY_Grid<JY_PathNode> grid;

    public JY_PathFinder(int width, int height, bool showDebug)
    {
        // 모든 Node를 관리할 Grid 생성
        grid = new JY_Grid<JY_PathNode>(width, height, (x, y) => new JY_PathNode(x, y), showDebug);
    }

    public List<JY_PathNode> FindPath(Vector2Int startPosition, Vector2Int endPosition)
    {
        
        List<JY_PathNode> openList = new();
        List<JY_PathNode> closedList = new();

        JY_PathNode startNode = grid.GetGridObject(startPosition.x, startPosition.y);
        JY_PathNode endNode = grid.GetGridObject(endPosition.x, endPosition.y);

        openList.Add(startNode);

        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                JY_PathNode node = grid.GetGridObject(x, y);
                node.gCost = int.MaxValue;
                node.hCost = CalculateDistance(node, endNode);
                node.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, endNode);
        startNode.CalculateFCost();
        startNode.cameFromNode = null;

        int index = 0;
        while (openList.Count > 0)
        {
            JY_PathNode currentNode = GetLowestFCostNode(openList);
            JY_Util.CreateNodeCostText(currentNode.fCost.ToString(), new Vector3(currentNode.x, currentNode.y) + Vector3.one * 0.5f, 4);
            JY_Util.CreateNodeCostText(currentNode.gCost.ToString(), new Vector3(currentNode.x, currentNode.y) + Vector3.one * 0.5f, 2, TMPro.TextAlignmentOptions.TopLeft);
            JY_Util.CreateNodeCostText(currentNode.hCost.ToString(), new Vector3(currentNode.x, currentNode.y) + Vector3.one * 0.5f, 2, TMPro.TextAlignmentOptions.BottomRight);
            if (currentNode == endNode)
                return CalculatePath(endNode);

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (JY_PathNode neighborNode in GetNeighborNodes(currentNode))
            {
                if (closedList.Contains(neighborNode))
                    continue;
                if (!neighborNode.isWalkable)
                {
                    closedList.Add(neighborNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistance(currentNode, neighborNode);
                if(tentativeGCost < neighborNode.gCost)
                {
                    neighborNode.cameFromNode = currentNode;
                    neighborNode.gCost = tentativeGCost;
                    neighborNode.hCost = CalculateDistance(neighborNode, endNode);
                    neighborNode.CalculateFCost();

                    if (!openList.Contains(neighborNode))
                        openList.Add(neighborNode);
                }                
            }

            #region 방지턱
            index++;
            if(index > 1000)
            {
                Debug.LogError("FindPath 인덱스 초과 - 무한루프");
                break;
            }
            #endregion
        }

        return null;
    }

    private List<JY_PathNode> CalculatePath(JY_PathNode endNode)
    {
        int index = 0;
        List<JY_PathNode> path = new();
        JY_PathNode currentNode = endNode;
        path.Add(currentNode);
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;

            #region 방지턱
            index++;
            if (index > 1000)
            {
                Debug.LogError("CalculatePath 인덱스 초과 - 무한루프");
                break;
            }
            #endregion
        }

        path.Reverse();
        return path;
    }

    private int CalculateDistance(JY_PathNode aNode, JY_PathNode bNode)
    {
        int xDistance = Mathf.Abs(aNode.x - bNode.x);
        int yDistance = Mathf.Abs(aNode.y - bNode.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return remaining * STRAIGHT_MOVE_COST + Mathf.Min(xDistance, yDistance) * DIAGONAL_MOVE_COST;
    }

    private JY_PathNode GetLowestFCostNode(List<JY_PathNode> nodes)
    {
        JY_PathNode lowestFCostNode = nodes[0];
        for (int i = 1; i < nodes.Count; i++)
            if (nodes[i].fCost < lowestFCostNode.fCost)
                lowestFCostNode = nodes[i];

        return lowestFCostNode;
    }

    private List<JY_PathNode> GetNeighborNodes(JY_PathNode currentNode)
    {
        List<JY_PathNode> neighborNodes = new();

        // Left
        if (currentNode.x - 1 >= 0)
        {
            neighborNodes.Add(GetNode(currentNode.x - 1, currentNode.y));

            // LeftDown
            if (currentNode.y - 1 >= 0)
                neighborNodes.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            // LeftUp
            if (currentNode.y + 1 < grid.Height)
                neighborNodes.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        // Right
        if (currentNode.x + 1 < grid.Width)
        {
            neighborNodes.Add(GetNode(currentNode.x + 1, currentNode.y));

            // RightDown
            if (currentNode.y - 1 >= 0)
                neighborNodes.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            // RightUp
            if (currentNode.y + 1 < grid.Height)
                neighborNodes.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        // Up
        if (currentNode.y + 1 < grid.Height)
            neighborNodes.Add(GetNode(currentNode.x, currentNode.y + 1));
        // Down
        if (currentNode.y - 1 >= 0)
            neighborNodes.Add(GetNode(currentNode.x, currentNode.y - 1));

        return neighborNodes;
    }

    public JY_PathNode GetNode(int x, int y)
        => grid.GetGridObject(x, y);
}