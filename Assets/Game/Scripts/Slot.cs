using UnityEngine;
public class Slot : MonoBehaviour
{
    public bool IsHasTile => Tile != null;
    public Tile Tile { get; private set; }

    public void SetTile(Tile tile)
    {
        tile.Init(this);
        tile.transform.SetParent(transform);
        Tile = tile;
    }

    public void ClearTile()
    {
        Tile = null;
    }
}