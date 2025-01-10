using System;
using UnityEngine;

public class JY_Grid<TGridObject>
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    private TGridObject[,] gridObjects;

    public JY_Grid(int width, int height, Func<int, int, TGridObject> createGridObject, bool showDebug)
    {
        Width = width;
        Height = height;
        gridObjects = new TGridObject[Width, Height];

        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                gridObjects[x, y] = createGridObject(x, y);

        if (showDebug)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
        => new Vector3(x, y);

    public TGridObject GetGridObject(int x, int y)
        => gridObjects[x, y];
}
