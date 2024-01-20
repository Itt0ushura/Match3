using UnityEngine;
public class Slot : MonoBehaviour
{
    public bool IsHasTile => Tile != null;

    public Tile Tile { get; private set; }

    public void Init(Tile tile)
    {
        tile.Init(this);
    }

    public void SetTile(Tile tile)
    {
        tile.transform.SetParent(transform);
        //tile.transform.localPosition = Vector3.zero;
        StartCoroutine(tile.MoveTileDown(tile.transform, transform.position, 0.6f));
        Tile = tile;
    }

    public void ClearTile()
    {
        Tile = null;
    }
}