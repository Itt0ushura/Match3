using UnityEngine;

//проверка есть ли в слоте тайл, если есть, передать это в гейм менеджер
public class Slot : MonoBehaviour
{
    public Tile tile;

    private void Update()
    {
        TileDetection();
    }

    private void TileDetection()
    {

        if (transform.childCount > 0)
        {
            Transform childTransform = transform.GetChild(0);

            tile = childTransform.GetComponent<Tile>();
        }
        else
        {
            tile = null;
        }        
    }
}
