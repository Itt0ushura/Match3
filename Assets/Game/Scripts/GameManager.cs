using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("Speed of animated tile moving")] private float animationTimer;

    private TileGeneration _tileGenerator;
    private bool isDone; // for gridfill

    public List<Tile> _deletionGroup = new List<Tile>();
    public List<Tile> _checkedTiles = new List<Tile>();

    private void Start()
    {
        _tileGenerator = GetComponent<TileGeneration>();
        _tileGenerator.GenerateBoard();
    }

    private void LateUpdate()
    {
        StopCoroutine(WaitandSearch());
        GridFill();
        StartCoroutine(WaitandSearch());
    }

    private void GridFill()
    {
        for (int i = 0; i < _tileGenerator.Board.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < _tileGenerator.Board.GetLength(1); j++)
            {
                var slot = _tileGenerator.Board[i, j];
                var slotbelow = _tileGenerator.Board[i + 1, j];

                if (slot.IsHasTile)
                {
                    var tile = slot.Tile;

                    if (tile.IsCanMoveTo(slotbelow))
                    {
                        StartCoroutine(tile.MoveTileDown(slotbelow, animationTimer));
                    }
                }
            }
        }
        _tileGenerator.GenerateTile();
    }

    private void SearchMethod(List<Tile> checkedTiles, List<Tile> deleteGroup)
    {
        for (int i = 0; i < _tileGenerator.Board.GetLength(0); i++)
        {
            for (int j = 0; j < _tileGenerator.Board.GetLength(1); j++)
            {
                Tile tile = _tileGenerator.Board[i, j].Tile;
                Tile tilebelow;
                if (checkedTiles.Contains(tile)) { continue; }

                checkedTiles.Add(tile);

                if (i+1 < _tileGenerator.Board.GetLength(0))
                {
                    tilebelow = _tileGenerator.Board[i + 1, j].Tile;
                }
                else
                {
                    tilebelow = null;
                }

                if (!checkedTiles.Contains(tilebelow))
                {
                    checkedTiles.Add(tilebelow);
                }
                if (tilebelow != null && tile._color == tilebelow._color)
                {
                    deleteGroup.Add(tile);
                    deleteGroup.Add(tilebelow);
                    Delete(checkedTiles, deleteGroup);
                    StartCoroutine(WaitandSearch());
                }
            }
        }
    }

    private void Delete(List<Tile> checkedTiles, List<Tile> deleteGroup)
    {
        foreach (var x in deleteGroup)
        {
            Destroy(x.gameObject);
        }
        deleteGroup.Clear();
        checkedTiles.RemoveAll(item => item == null);
    }
    private IEnumerator WaitandSearch()
    {
        yield return new WaitForSeconds(5);
        SearchMethod(_checkedTiles, _deletionGroup);
    }
}