using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("Speed of animated tile moving")] private float animationTimer;

    private TileGeneration _tileGenerator;

    private Tile _tile;
    private Slot _slot;
    private Slot _slotbelow;

    private void Start()
    {

        _tileGenerator = GetComponent<TileGeneration>();
        _tileGenerator.GenerateBoard();

    }

    private void LateUpdate()
    {
        GridFill();
    }

    private void GridFill()
    {
        for (int i = 0; i < _tileGenerator.Board.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < _tileGenerator.Board.GetLength(1); j++)
            {

                _slot = _tileGenerator.Board[i, j];
                _slotbelow = _tileGenerator.Board[i + 1, j];

                if (_slot.IsHasTile)
                {
                    _tile = _slot.Tile;

                    if (_tile.IsCanMoveTo(_slotbelow))
                    {

                        StartCoroutine(_tile.MoveTileDown(_slotbelow, animationTimer));

                    }
                }
            }
        }
        _tileGenerator.GenerateTile();
    }
}