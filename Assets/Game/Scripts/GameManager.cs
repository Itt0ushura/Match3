using UnityEngine;



//перемещать челов имея два значения от слота и тайла
public class GameManager : MonoBehaviour
{

    public TileGeneration TileGenerator;
    private bool _checkable;
    [SerializeField] private Slot slot;

    private void Start()
    {
        TileGenerator.GenerateBoard();
        TileGenerator.GenerateOneRow();
    }

    private void Update()
    {
        GridFill();
    }

    [ContextMenu("test")]
    private void GridFill()
    {
        _checkable = false;
        for (int i = 0; i < TileGenerator.BoardSize.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < TileGenerator.BoardSize.GetLength(1); j++)
            {
                Slot currentCell = TileGenerator.BoardSize[i, j];
                Slot cellBelow = TileGenerator.BoardSize[i + 1, j];
                //если у слота ниже поле тайл == нулл и под тайлом есть место - тайлы двигаются ниже
                if (cellBelow.tile == null)
                {
                    _checkable = true;
                    Transform child = currentCell.transform.GetChild(0);
                    child.SetParent(cellBelow.transform);
                    child.transform.position = cellBelow.transform.position;
                }
            }
        }
        if (_checkable)
        {
            TileGenerator.GenerateOneRow();
        }
    }
}
