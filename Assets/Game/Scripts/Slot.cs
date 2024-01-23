using UnityEngine;
public class Slot : MonoBehaviour
{
    public bool IsHasTile => Tile != null;
    public Tile Tile { get; private set; }

    public void SetTile(Tile tile, float spawnNextAfterDelay)
    {
        tile.Init(this, spawnNextAfterDelay);
        tile.transform.SetParent(transform);
        Tile = tile;
    }

    public void ClearTile()
    {
        Tile = null;
    }
}