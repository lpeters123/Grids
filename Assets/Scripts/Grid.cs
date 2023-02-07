using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public static Vector3[,] CalculateGridPositions(Vector2Int dimensions) 
    {
        Vector3[,] grid = new Vector3[dimensions.x, dimensions.y];
        Vector3 gridStart = GetXYGridStart(dimensions);

        for (int x = 0; x < dimensions.x; x++)
        {
            for (int y = 0; y < dimensions.y; y++)
            {
                grid[x, y] = gridStart + new Vector3(x, 0, y);
            }
        }

        return grid;
    }

    public static Vector3 GetXYGridStart(Vector2Int dimensions) 
    {
        float xStart = 0;
        float yStart = 0;

        if (IsEven(dimensions.x)) 
        {
            xStart = -dimensions.x / 2 + 0.5f;
        }
        else 
        {
            xStart = -dimensions.x / 2;
        }

        if (IsEven(dimensions.y))
        {
            yStart = -dimensions.y / 2 + 0.5f;
        }
        else
        {
            yStart = -dimensions.y / 2;
        }

        return new Vector3(xStart, 0, yStart);        
    }

    private static bool IsEven(int number) 
    {
        return number % 2 == 0;
    }
}
