using System.Collections.Generic;
using UnityEngine;

public class JY_Tester : MonoBehaviour
{
    [Header("Grid Settings")]
    [Range(10, 20)][SerializeField] private int width;
    [Range(5, 10)][SerializeField] private int height;
    [SerializeField] private bool showDebugToggle;

    [Header("PathFinding Settings")]
    [SerializeField] private GameObject blockerPrefab;
    [SerializeField] private Vector2Int startPosition;
    [SerializeField] private float lineDuration;

    private JY_PathFinder pathFinder;

    private void Start()
    {
        pathFinder = new JY_PathFinder(width, height, showDebugToggle);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2Int endPosition = GetWorldPositionToGridXY();
            List<JY_PathNode> path = pathFinder.FindPath(startPosition, endPosition);
            for (int i = 0; i < path.Count - 1; i++)
            {
                Vector2 startLine = new Vector2(path[i].x, path[i].y) + Vector2.one * 0.5f;
                Vector2 endLine = new Vector2(path[i + 1].x, path[i + 1].y) + Vector2.one * 0.5f;

                Debug.DrawLine(startLine, endLine, Color.green, lineDuration);
            }
            startPosition = endPosition;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector2Int gridXY = GetWorldPositionToGridXY();
            pathFinder.GetNode(gridXY.x, gridXY.y).isWalkable = false;

            Vector2 blockerPosition = gridXY + Vector2.one * 0.5f;
            GameObject block = Instantiate(blockerPrefab);
            block.transform.position = blockerPosition;
        }
    }

    private Vector2Int GetWorldPositionToGridXY()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;

        int x = Mathf.FloorToInt(worldPosition.x);
        int y = Mathf.FloorToInt(worldPosition.y);

        return new Vector2Int(x, y);
    }
}
