using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestruction : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile dirt,stone,diamond;
    public Tile boundary;

    public void DestroyOn(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);
        DestroyCell(originCell + new Vector3Int(0, 0, 0));
    }
    public void DestroyUnder(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);
        DestroyCell(originCell + new Vector3Int(0,-1,0));
    }


    public void DestroyLeft(Vector2 worldPos)
    {
       Vector3Int originCell = tilemap.WorldToCell(worldPos);
        DestroyCell(originCell + new Vector3Int(-1, 0, 0));
    }

    public void DestroyRight(Vector2 worldPos)
    {
       Vector3Int originCell = tilemap.WorldToCell(worldPos);
        DestroyCell(originCell + new Vector3Int(1, 0, 0));
    }

    public void DestroyAbove(Vector2 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);
        DestroyCell(originCell + new Vector3Int(0, 1, 0));
    }


    void DestroyCell(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell); 

        if (tile == boundary) //don't remove due to it being a boundary
        {
            return;
        }

        if (tile == dirt || stone || diamond) //remove the tile
        {
            tilemap.SetTile(cell, null);
        }
    }
   
   
 
}
