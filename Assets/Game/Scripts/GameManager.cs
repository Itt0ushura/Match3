using UnityEngine;

public class GameManager : MonoBehaviour
{

    public TileGeneration TileGenerator;

    private Tile _tile;
    private Slot _slot;
    private Slot _slotbelow;

    private void Start()
    {

        TileGenerator.GenerateBoard();
        TileGenerator.GenerateOneRow();

    }

    private void Update()
    {
        GridFill();
    }

    private void GridFill()
    {
        bool checkable = false;
        for (int i = 0; i < TileGenerator.Board.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < TileGenerator.Board.GetLength(1); j++)
            {
                _slot = TileGenerator.Board[i, j];
                _slotbelow = TileGenerator.Board[i + 1, j];
                _tile = _slot.GetComponentInChildren<Tile>();

                _slot.Init(_tile);
                _tile.CheckBelow(_slotbelow);

                if(_slotbelow != null && _tile.IsMovable == true)
                {
                    _slot.ClearTile();

                    _slotbelow.SetTile(_tile);

                    checkable = true;
                }
            }
        }
        if (checkable)
        {
            TileGenerator.GenerateOneRow();
        }
    }
}