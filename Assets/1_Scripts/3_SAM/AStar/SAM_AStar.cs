using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class SAM_AStar : MonoBehaviour
{
    // 길 찾기 로직
    // 
    // 루트 간단화..는 시간 무리
    int width = 10;
    int height = 10;
    int nodeSize = 1;

    private SAM_Node[,] nodeGrid;
    // 열린목록 일단 List로
    private List<SAM_Node> openedList = new List<SAM_Node>();
    // 닫힌목록
    private List<SAM_Node> closedList = new List<SAM_Node> ();

    void Start()
    {
        nodeGrid = new SAM_Node[height, width];
        CreateGrid();
        FindPath(new Vector2(-4.3f, -4.3f), new Vector2(0,0));
    }

    private void CreateGrid()
    {
        for(int y = 0; y < height; ++y)
        {
            for(int x = 0; x < width; ++x)
            {
                float nodeX = x - (width / 2);
                float nodeY = y - (height / 2);
                nodeGrid[y, x] = new SAM_Node(new Vector3(nodeX, 0, nodeY));
            }
        }
        int i = 0;
    }

    void Update()
    {
        
    }

    private void FindPath(Vector2 startPosition, Vector2 targetPosition)
    {
        SAM_Node start = GetColseNode(startPosition);
        SAM_Node target = GetColseNode(targetPosition);

        if (start == null)
            Assert.Fail();

        openedList.Add(start);
        start.HCost = CalHCost(start, target);

        while(false)
        {
            SAM_Node currentNode = GetLowestFCostNode();
        }
    }

    private SAM_Node GetColseNode(Vector2 position)
    {
        if (nodeGrid == null)
            return null;

        int x = Mathf.FloorToInt(position.x + (width / 2));
        int y = Mathf.FloorToInt(position.y + (height / 2));
        return nodeGrid[y, x];
    }

    private int CalHCost(SAM_Node node1, SAM_Node node2)
    {
        int x = Mathf.Abs((int)(node1.position.x - node2.position.x));
        int y = Mathf.Abs((int)(node1.position.z - node2.position.z));
        return (x+y)*10;
    }

    private SAM_Node GetLowestFCostNode()
    {
        if (openedList == null)
            return null;
        SAM_Node lowestNode = openedList.OrderBy(x => x.FCost).First();
        return lowestNode;
    }

    private List<SAM_Node> GetNeighbor()
    {
        // 으악...
        int x;
        int y;
        return null;
    }

    private void OnDrawGizmos()
    {
        if(nodeGrid != null)
        {
            Gizmos.color = Color.gray;
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    Gizmos.DrawCube(nodeGrid[y, x].position, Vector3.one * nodeSize/2);
                }
            }
        }
        
    }
}
