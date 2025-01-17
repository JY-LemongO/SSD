using UnityEngine;

public class SAM_Node
{
    public SAM_Node(Vector3 position)
    {
        this.position.x= position.x;
        this.position.y= 0;
        this.position.z= position.z;
    }

    public Vector3 position; // y값은 항상 0일듯

    public int GCost;
    public int HCost;
    public int FCost => GCost + HCost;

    public SAM_Node parent;

    public bool IsClosed; // 닫힘 여부 (이미 방문)
    public bool IsWalkable; // 장애물 관련 구현 나중에
}
