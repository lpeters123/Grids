using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for enabling the user to raycast the grid and changing
/// the colors of the tiles depending on whether they are hovered over, selected, etc.
/// </summary>
public class GridRaycaster : MonoBehaviour
{
    public Camera cam;
    public GridManager gridManager;
    public LayerMask gridRaycastLayerMask;    
    
    private GameObject currentlyHoveredOverTile;
    public delegate void CurrentlyHoveredOverTileChanged(GameObject previouslyHoveredOverTile, GameObject currentlyHoveredOverTile);
    public static event CurrentlyHoveredOverTileChanged OnCurrentlyHoveredOverTileChanged;

    private GameObject tileCurrentlyUnderMouse0;
    public delegate void CurrentTileUnderMouse0Changed(GameObject previousTileUnderMouse0, GameObject currentlTileUnderMouse0);
    public static event CurrentTileUnderMouse0Changed OnCurrentTileUnderMouse0Changed;

    private GameObject tileCurrentlyUnderMouse0Down;
    public delegate void CurrentTileUnderMouse0DownChanged(GameObject previousTileUnderMouse0Down, GameObject currentlTileUnderMouse0Down);
    public static event CurrentTileUnderMouse0DownChanged OnCurrentTileUnderMouse0DownChanged;

    private GameObject tileCurrentlyUnderMouse0Up;
    public delegate void CurrentTileUnderMouse0UpChanged(GameObject previousTileUnderMouse0Up, GameObject currentlTileUnderMouse0Up);
    public static event CurrentTileUnderMouse0UpChanged OnCurrentTileUnderMouse0UpChanged;

    private GameObject tileCurrentlyUnderMouse1;
    public delegate void CurrentTileUnderMouse1Changed(GameObject previousTileUnderMouse1, GameObject currentlTileUnderMouse1);
    public static event CurrentTileUnderMouse1Changed OnCurrentTileUnderMouse1Changed;

    private GameObject tileCurrentlyUnderMouse1Down;
    public delegate void CurrentTileUnderMouse1DownChanged(GameObject previousTileUnderMouse1Down, GameObject currentlTileUnderMouse1Down);
    public static event CurrentTileUnderMouse1DownChanged OnCurrentTileUnderMouse1DownChanged;

    private GameObject tileCurrentlyUnderMouse1Up;
    public delegate void CurrentTileUnderMouse1UpChanged(GameObject previousTileUnderMouse1Up, GameObject currentlTileUnderMouse1Up);
    public static event CurrentTileUnderMouse1UpChanged OnCurrentTileUnderMouse1UpChanged;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentlyHoveredOverTile();
        UpdateCurrentlyClickedMouse0Tiles();
        UpdateCurrentlyClickedMouse1Tiles();
    }

    #region Update Currently Hovered Over Tile
    private void UpdateCurrentlyHoveredOverTile()
    {
        GameObject tileBeingHoveredOverThisFrame = null;

        Vector3? raycastHitPosition = RaycastMousePosition();
        if (raycastHitPosition.HasValue)
        {
            tileBeingHoveredOverThisFrame = GetTileForWorldPosition(raycastHitPosition.Value);
        }

        UpdateCurrentlyHoveredOverTileIfChanged(tileBeingHoveredOverThisFrame);
    }

    private void UpdateCurrentlyHoveredOverTileIfChanged(GameObject tileHoveredOverThisFrame)
    {
        if (currentlyHoveredOverTile != tileHoveredOverThisFrame)
        {
            GameObject previousTileBeingHoveredOver = currentlyHoveredOverTile;
            currentlyHoveredOverTile = tileHoveredOverThisFrame;
            OnCurrentlyHoveredOverTileChanged?.Invoke(previousTileBeingHoveredOver, tileHoveredOverThisFrame);
        }
    }
    #endregion

    #region Update Mouse0 Variables
    private void UpdateCurrentlyClickedMouse0Tiles() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpdateTileCurrentlyUnderMouse0DownIfChanged(currentlyHoveredOverTile);
        }
        else 
        {
            UpdateTileCurrentlyUnderMouse0DownIfChanged(null);
        }


        if (Input.GetMouseButton(0))
        {
            UpdateTileCurrentlyUnderMouse0IfChanged(currentlyHoveredOverTile);

        }
        else
        {
            UpdateTileCurrentlyUnderMouse0IfChanged(null);
        }

        if (Input.GetMouseButtonUp(0))
        {
            UpdateTileCurrentlyUnderMouse0UpIfChanged(currentlyHoveredOverTile);
        }
        else
        {
            UpdateTileCurrentlyUnderMouse0UpIfChanged(null);
        }

    }

    private void UpdateTileCurrentlyUnderMouse0DownIfChanged(GameObject currentTile)
    {
        if (tileCurrentlyUnderMouse0Down != currentTile)
        {
            GameObject previousTileUnderMouse0Down = tileCurrentlyUnderMouse0Down;
            tileCurrentlyUnderMouse0Down = currentTile;
            OnCurrentTileUnderMouse0DownChanged?.Invoke(previousTileUnderMouse0Down, currentTile);
        }
    }

    private void UpdateTileCurrentlyUnderMouse0IfChanged(GameObject currentTile)
    {
        if (tileCurrentlyUnderMouse0 != currentTile)
        {
            GameObject previousTileUnderMouse0 = tileCurrentlyUnderMouse0;
            tileCurrentlyUnderMouse0 = currentTile;
            OnCurrentTileUnderMouse0Changed?.Invoke(previousTileUnderMouse0, currentTile);
        }
    }

    private void UpdateTileCurrentlyUnderMouse0UpIfChanged(GameObject currentTile)
    {
        if (tileCurrentlyUnderMouse0Up != currentTile)
        {
            GameObject previousTileUnderMouse0Up = tileCurrentlyUnderMouse0Up;
            tileCurrentlyUnderMouse0Up = currentTile;
            OnCurrentTileUnderMouse0UpChanged?.Invoke(previousTileUnderMouse0Up, currentTile);
        }
    }

    #endregion

    #region Update Mouse1 Variables
    private void UpdateCurrentlyClickedMouse1Tiles()
    {
        if (Input.GetMouseButtonDown(1))
        {
            UpdateTileCurrentlyUnderMouse1DownIfChanged(currentlyHoveredOverTile);
        }
        else
        {
            UpdateTileCurrentlyUnderMouse1DownIfChanged(null);
        }


        if (Input.GetMouseButton(1))
        {
            UpdateTileCurrentlyUnderMouse1IfChanged(currentlyHoveredOverTile);

        }
        else
        {
            UpdateTileCurrentlyUnderMouse1IfChanged(null);
        }

        if (Input.GetMouseButtonUp(1))
        {
            UpdateTileCurrentlyUnderMouse1UpIfChanged(currentlyHoveredOverTile);
        }
        else
        {
            UpdateTileCurrentlyUnderMouse1UpIfChanged(null);
        }
    }

    private void UpdateTileCurrentlyUnderMouse1DownIfChanged(GameObject currentTile)
    {
        if (tileCurrentlyUnderMouse1Down != currentTile)
        {
            GameObject previousTileUnderMouse1Down = tileCurrentlyUnderMouse0Down;
            tileCurrentlyUnderMouse1Down = currentTile;
            OnCurrentTileUnderMouse1DownChanged?.Invoke(previousTileUnderMouse1Down, currentTile);
        }
    }

    private void UpdateTileCurrentlyUnderMouse1IfChanged(GameObject currentTile)
    {
        if (tileCurrentlyUnderMouse1 != currentTile)
        {
            GameObject previousTileUnderMouse1 = tileCurrentlyUnderMouse1;
            tileCurrentlyUnderMouse1 = currentTile;
            OnCurrentTileUnderMouse1Changed?.Invoke(previousTileUnderMouse1, currentTile);
        }
    }

    private void UpdateTileCurrentlyUnderMouse1UpIfChanged(GameObject currentTile)
    {
        if (tileCurrentlyUnderMouse1Up != currentTile)
        {
            GameObject previousTileUnderMouse1Up = tileCurrentlyUnderMouse1Up;
            tileCurrentlyUnderMouse1Up = currentTile;
            OnCurrentTileUnderMouse1UpChanged?.Invoke(previousTileUnderMouse1Up, currentTile);
        }
    }

    #endregion

    #region Helper functions

    private Vector3? RaycastMousePosition()
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

    private GameObject GetTileForWorldPosition(Vector3 worldPosition)
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
    #endregion


    ///////// Move methods below to other script, this script shouldnt changes tiles directly. Only through events published on delegates.

}
