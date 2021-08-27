using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridGenerator : MonoBehaviour
{
    #region VARIABLE_REG
    [SerializeField] private List<Transform> hexTiles = new List<Transform>();
    [SerializeField] private List<float> rotations = new List<float>();

    public int mapWidth = 25;
    public int mapHeight = 12;

    [SerializeField] private float m_tileXOffset = 1.8f;
    [SerializeField] private float m_tileYOffset = -1f;
    [SerializeField] private float m_tileZOffset = 1.565f;

    int index = 0;

    #endregion

    #region UNITY_REG
    #endregion

    #region CLASS_REG
    [ContextMenu("Fetch Childs")]
    private void FetchChilds()
    {
        hexTiles.Clear();

        foreach (Transform child in this.transform)
        {
            hexTiles.Add(child);
            child.gameObject.name = "HexTile";
            child.position = Vector3.zero;
        }
        index = 0;
    }

    [ContextMenu("Create TileMap")]
    private void CreateTileMap()
    {
        for (int x = 0; x <= mapWidth; x++)
        {
            for (int z = 0; z <= mapHeight; z++)
            {
                if (index < hexTiles.Count)
                {
                    if (z % 2 == 0)
                    {
                        hexTiles[index].position = new Vector3(x * m_tileXOffset, m_tileYOffset, z * m_tileZOffset);
                    }
                    else
                    {
                        hexTiles[index].position = new Vector3(x * m_tileXOffset + m_tileXOffset / 2, m_tileYOffset, z * m_tileZOffset);
                    }
                    SetTileInfo(hexTiles[index], x, z);
                    index++;
                }
            }
        }
    }

    private void SetTileInfo(Transform tile, int x, int z)
    {
        tile.parent = this.transform;
        tile.rotation = Quaternion.Euler(tile.rotation.x, rotations[Random.Range(0, rotations.Count)], tile.rotation.z);
        tile.gameObject.name = x + "|" + z;
    }
    #endregion 
}