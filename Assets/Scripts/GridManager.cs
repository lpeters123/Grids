using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public Vector2Int GridDimensions;

    public GameObject tilePrefab;
    public GameObject gridParentObject;
    public GameObject[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        SpawnOrUpdateGrid();
        GridRaycaster.OnCurrentlyHoveredOverTileChanged += UpdateHoveredOverStateOfTiles;
        GridRaycaster.OnCurrentTileUnderMouse0Changed += UpdatePressedStateOfTiles;
    }

    // Update is called once per frame
    void Update()
    {

    }    


    public void SpawnOrUpdateGrid() 
    {
        if(tiles != null) 
        {
            DeleteAllCurrentTiles();
        }

        tiles = new GameObject[GridDimensions.x, GridDimensions.y];
        Vector3[,] gridPositions = Grid.CalculateGridPositions(GridDimensions);

        for (int x = 0; x < gridPositions.GetLength(0); x++)
        {
            for (int y = 0; y < gridPositions.GetLength(1); y++)
            {
                tiles[x,y] =  Instantiate(tilePrefab, gridPositions[x, y], Quaternion.identity, gridParentObject.transform);
            }
        }
    }

    void DeleteAllCurrentTiles() 
    {
        bool inEditMode = Application.isEditor;
        for (int x = 0; x < tiles.GetLength(0); x++) 
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (inEditMode) 
                {
                    DestroyImmediate(tiles[x, y]);
                }
                else 
                {
                    Destroy(tiles[x, y]);
                }                
            }
        }
    }

    private void UpdateHoveredOverStateOfTiles(GameObject lastTileBeingHoveredOver, GameObject currentTileBeingHoveredOver)
    {

        if (lastTileBeingHoveredOver != null)
        {
            Tile lastTileScript = lastTileBeingHoveredOver.GetComponent<Tile>();
            lastTileScript.SetHighlightedState(false);
            lastTileScript.SetPressedState(false);
        }

        if (currentTileBeingHoveredOver != null)
        {
            currentTileBeingHoveredOver.GetComponent<Tile>().SetHighlightedState(true);
        }
    }

    private void UpdatePressedStateOfTiles(GameObject beforeLastClickedTile, GameObject lastClickedTile)
    {
        if (beforeLastClickedTile != null)
        {
            beforeLastClickedTile.GetComponent<Tile>().SetPressedState(false);
        }

        if (lastClickedTile != null)
        {
            lastClickedTile.GetComponent<Tile>().SetPressedState(true);
        }
    }

}
