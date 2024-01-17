using UnityEngine;


// Проверка есть ли под ним пустая ячейка, если да, сформировать истину и передать в гейм менеджер
public class Tile : MonoBehaviour
{
    private TileGeneration TileGenerator;


    public bool isMovable; //придумать куда деть
    private void Start()
    {
        GameObject coreFinder = GameObject.Find("GameCore");
        if (coreFinder != null)
        {
            TileGenerator = coreFinder.GetComponent<TileGeneration>();
        }
    }

    private void Update()
    {
        CheckUnder();
    }

    private void CheckUnder()
    {

        for (int i = 0; i < TileGenerator.BoardSize.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < TileGenerator.BoardSize.GetLength(1); j++)
            {
                Slot slotBelow = TileGenerator.BoardSize[i + 1, j];
                if (slotBelow.tile == null)
                {
                    isMovable = true;
                }
                else
                {
                    isMovable = false;
                }
            }
        }
    }
}
