using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject tile;
    void Update()
    {
        TileDetection();
    }
    private void TileDetection()
    {
        if (transform.childCount == 1)
        {
            tile = transform.GetChild(0).gameObject;
        }
        else
        {
            tile = null;
        }
    }
}
