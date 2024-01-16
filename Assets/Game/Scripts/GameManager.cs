using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public TileGeneration TileGenerator;
    private bool _checkable;
    private Slot slot;

    private void Start()
    {
        TileGenerator.GenerateBoard();
        TileGenerator.GenerateOneRow();
    }

    private void FixedUpdate()
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
                GameObject currentCell = TileGenerator.BoardSize[i, j];
                GameObject cellBelow = TileGenerator.BoardSize[i + 1, j];

                if (cellBelow.transform.childCount == 0)
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
