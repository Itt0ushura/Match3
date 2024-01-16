using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject tile;

    private void Update()
    {
        TileDetection();
    }

    private void TileDetection()
    {
        if (transform.childCount > 0)
        {
            tile = transform.GetChild(0).gameObject;
            Debug.Log(tile.name);
        }
        else
        {
            Debug.Log(tile.name);
            tile = null;
        }
    }
}
