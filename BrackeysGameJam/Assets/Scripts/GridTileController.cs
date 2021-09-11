using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileController : MonoBehaviour
{
    public bool removeOuter;
    public bool removeHalfLine_Vertical;
    public bool removeHalfLine_Horizontal;
    public bool removeXPattern;

    bool outerRemoved = false;
    bool halfRemoved_Vertical = false;
    bool halfRemoved_Horizontal = false;

    List<GameObject> hexTiles = new List<GameObject>();
    GridGenerator _gridGenerator;

    private void Start()
    {
        _gridGenerator = this.gameObject.GetComponent<GridGenerator>();
        ResetGrid();

        foreach (Transform child in this.transform)
        {
            hexTiles.Add(child.gameObject);
        }
    }

    private void Update()
    {
        if (removeOuter)
        {
            if (!outerRemoved)
            {
                OuterBorder(false);
            }
        }
        else
        {
            if (outerRemoved)
            {
                OuterBorder(true);
            }
        }

        if (removeHalfLine_Vertical)
        {
            if (!halfRemoved_Vertical)
            {
                HalfwayLine_Vertical(false);
            }
        }
        else
        {
            if (halfRemoved_Vertical)
            {
                HalfwayLine_Vertical(true);
            }
        }

        if (removeHalfLine_Horizontal)
        {
            if (!halfRemoved_Horizontal)
            {
                HalfwayLine_Horizontal(false);
            }
        }
        else
        {
            if (halfRemoved_Horizontal)
            {
                HalfwayLine_Horizontal(true);
            }
        }
    }

    private void ResetGrid()
    {
        foreach (GameObject tile in hexTiles)
        {
            tile.SetActive(true);
        }

        outerRemoved = false;
        halfRemoved_Vertical = false;
    }

    void OuterBorder(bool state)
    {
        foreach (GameObject tile in hexTiles)
        {
            string[] code = tile.name.Split('|');
            if(code[0] == "0")
            {
                tile.SetActive(state);
            }

            if(code[1] == _gridGenerator.mapHeight.ToString() || code[1] == (_gridGenerator.mapHeight-1).ToString())
            {
                tile.SetActive(state);
            }

            if (code[0] == (_gridGenerator.mapWidth-1).ToString())
            {
                tile.SetActive(state);
            }

            if (code[1] == "0" || code[1] == "1")
            {
                tile.SetActive(state);
            }

        }

        outerRemoved = !state;
    }

    void HalfwayLine_Vertical(bool state)
    {
        foreach (GameObject tile in hexTiles)
        {
            string[] code = tile.name.Split('|');
            if (code[0] == (_gridGenerator.mapWidth / 2).ToString())
            {
                tile.SetActive(state);
            }
        }

        halfRemoved_Vertical = !state;
    }

    void HalfwayLine_Horizontal(bool state)
    {
        foreach (GameObject tile in hexTiles)
        {
            string[] code = tile.name.Split('|');
            if (code[1] == (_gridGenerator.mapHeight / 2).ToString() 
                || code[1] == ((_gridGenerator.mapHeight / 2) +1).ToString()
                || code[1] == ((_gridGenerator.mapHeight / 2) -1).ToString())
            {
                tile.SetActive(state);
            }
        }

        halfRemoved_Horizontal = !state;
    }
}
