using UnityEngine;

public class JY_PathNode
{
    public int x;
    public int y;

    public int fCost;
    public int gCost;
    public int hCost;
    public bool isWalkable;

    public JY_PathNode cameFromNode;

    public JY_PathNode(int x, int y)
    {
        this.x = x;
        this.y = y;
        isWalkable = true;
        cameFromNode = null;
    }

    public void CalculateFCost()
        => fCost = gCost + hCost;
    
}
