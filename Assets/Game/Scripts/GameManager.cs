using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("Speed of animated tile moving")] private float animationTimer;

    private TileGeneration _tileGenerator;
    private bool isDone; // for gridfill

    public List<Tile> _deletionGroup = new List<Tile>();
    public List<Tile> _checkedTiles = new List<Tile>();
    List<Tile> _combinedList = new List<Tile>();

    private void Start()
    {
        _tileGenerator = GetComponent<TileGeneration>();
        _tileGenerator.GenerateBoard();
    }

    private void LateUpdate()
    {
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

    //search realization

    private void SearchMethod()
    {
        for (int i = 0; i < _tileGenerator.Board.GetLength(0); i++)
        {
            for (int j = 0; j < _tileGenerator.Board.GetLength(1); j++)
            {

                Tile tile = _tileGenerator.Board[i, j].Tile;
                if (_checkedTiles.Contains(tile))
                {
                    continue;
                }
                _checkedTiles.Add(tile);
                Debug.Log("i'm center " + tile.name);
                RecursiveSearch(i, j, tile);
            }
        }
    }
    private void RecursiveSearch(int i, int j, Tile tile)
    {
        if (i + 1 < _tileGenerator.Board.GetLength(0))
        {
            Tile tilebelow = _tileGenerator.Board[i + 1, j].Tile;
            if (!_checkedTiles.Contains(tilebelow))
            {
                _checkedTiles.Add(tilebelow);
            }
            if (tile._color == tilebelow._color)
            {
                if (!_deletionGroup.Contains(tilebelow))
                {
                    _deletionGroup.Add(tilebelow);
                }
                if (!_deletionGroup.Contains(tile))
                {
                    _deletionGroup.Add(tile);
                }
                Debug.Log("i'm down below " + tilebelow.name);
            }
        }
        if (i - 1 >= 0)
        {
            Tile tileabove = _tileGenerator.Board[i - 1, j].Tile;
            if (!_checkedTiles.Contains(tileabove))
            {
                _checkedTiles.Add(tileabove);
            }
            if (tile._color == tileabove._color)
            {
                if (!_deletionGroup.Contains(tileabove))
                {
                    _deletionGroup.Add(tileabove);
                }
                if (!_deletionGroup.Contains(tile))
                {
                    _deletionGroup.Add(tile);
                }
                Debug.Log("i'm above " + tileabove.name);
            }
        }
        if (j + 1 < _tileGenerator.Board.GetLength(1))
        {
            Tile tileright = _tileGenerator.Board[i, j + 1].Tile;
            if (!_checkedTiles.Contains(tileright))
            {
                _checkedTiles.Add(tileright);
            }
            if (tile._color == tileright._color)
            {
                if (!_deletionGroup.Contains(tileright))
                {
                    _deletionGroup.Add(tileright);
                }
                if (!_deletionGroup.Contains(tile))
                {
                    _deletionGroup.Add(tile);
                }
                Debug.Log("i'm to the right " + tileright.name);
            }
        }
        if (j - 1 >= 0)
        {
            Tile tileleft = _tileGenerator.Board[i, j - 1].Tile;
            if (!_checkedTiles.Contains(tileleft))
            {
                _checkedTiles.Add(tileleft);
            }
            if (tile._color == tileleft._color)
            {
                if (!_deletionGroup.Contains(tileleft))
                {
                    _deletionGroup.Add(tileleft);
                }
                if (!_deletionGroup.Contains(tile))
                {
                    _deletionGroup.Add(tile);
                }
                Debug.Log("i'm to the left " + tileleft.name);
            }
        }
    }

    private void Delete(List<Tile> list)
    {
        foreach (var x in list)
        {
            if (x.gameObject != null)
            {
                Destroy(x.gameObject);
            }
            else
            {
                list.Remove(x);
            }
        }
        list.Clear();
    }

    private IEnumerator WaitandSearch()
    {
        yield return new WaitForSeconds(5);
        SearchMethod();
    }
}