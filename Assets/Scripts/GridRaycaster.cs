using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRaycaster : MonoBehaviour
{
    public Camera cam;
    public GridManager gridManager;
    public Transform testRaycasting;
    public LayerMask gridRaycastLayerMask;
    private GameObject lastTileBeingHoveredOver;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        UpdateCurrentHighlighedTile();
        UpdateTilePressed();
    }

    private void UpdateTilePressed()
    {
        if (lastTileBeingHoveredOver == null)
            return;

        if (Input.GetMouseButton(0))
        {
            lastTileBeingHoveredOver.GetComponent<Tile>().SetPressedState(true);
        }
        else
        {
            lastTileBeingHoveredOver.GetComponent<Tile>().SetPressedState(false);
        }
    }

    void UpdateCurrentHighlighedTile()
    {

        Vector3? raycastHitPosition = RaycastMousePosition();
        if (!raycastHitPosition.HasValue)
        {
            return;
        }


        GameObject currentTileBeingHoveredOver = GetTileForWorldPosition(raycastHitPosition.Value);
        if (lastTileBeingHoveredOver != currentTileBeingHoveredOver)
        {
            UpdateTileStates(currentTileBeingHoveredOver, lastTileBeingHoveredOver);
            lastTileBeingHoveredOver = currentTileBeingHoveredOver;
        }
    }


    Vector3? RaycastMousePosition()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, gridRaycastLayerMask))
        {
            return hit.point;
        }
        else
        {
            return null;
        }
    }

    GameObject GetTileForWorldPosition(Vector3 worldPosition)
    {
        Vector3 gridStart = Grid.GetXYGridStart(gridManager.GridDimensions);
        Vector3 dif = worldPosition - gridStart;
        Vector2Int coordinates = new Vector2Int(Mathf.RoundToInt(dif.x), Mathf.RoundToInt(dif.z));

        if (coordinates.x >= 0 && coordinates.x < gridManager.tiles.GetLength(0) && coordinates.y >= 0 && coordinates.y < gridManager.tiles.GetLength(1))
        {
            return gridManager.tiles[coordinates.x, coordinates.y];
        }
        else
        {
            return null;
        }
    }

    void UpdateTileStates(GameObject currentTileBeingHoveredOver, GameObject lastTileBeingHoveredOver)
    {

        if (lastTileBeingHoveredOver != null) 
        {
            lastTileBeingHoveredOver.GetComponent<Tile>().SetHighlightedState(false);
            lastTileBeingHoveredOver.GetComponent<Tile>().SetPressedState(false);
        }

        if (currentTileBeingHoveredOver != null) 
        {
            currentTileBeingHoveredOver.GetComponent<Tile>().SetHighlightedState(true);
        }
    }
}
