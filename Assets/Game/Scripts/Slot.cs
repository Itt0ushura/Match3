using UnityEngine;
public class Slot : MonoBehaviour
{
    public bool IsHasTile => Tile != null;
    public Tile Tile { get; private set; }

    private void Awake()
    {
        Actions.OnDelete.AddListener(DeleteTile);
    }

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
    public void DeleteTile()
    {
        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector3.forward);
        if (hit.collider != null && IsHasTile && hit.collider.gameObject == Tile.gameObject)
        {
            Destroy(Tile.gameObject);
            ClearTile();
        }
    }
}