using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("Speed of animated tile moving")] private float animationTimer;

    private TileGeneration _tileGenerator;

    public List<Tile> _deletionGroup = new List<Tile>();
    public List<Tile> _checkedTiles = new List<Tile>();
    public List<Tile> _combinedList = new List<Tile>();

    private void Start()
    {
        _tileGenerator = GetComponent<TileGeneration>();
        _tileGenerator.GenerateBoard();
    }

    private void LateUpdate()
    {
        GridFill();
        StartCoroutine(WaitandSearch());
        StartCoroutine(WaitandDelete());
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
                if (!_checkedTiles.Contains(tile))
                {
                    _checkedTiles.Add(tile);
                }
                if (_deletionGroup.Contains(tile))
                {
                    continue;
                }
                List<Tile> result = RecursiveSearch(i, j, tile);
                _deletionGroup.AddRange(result);
            }
        }
        _checkedTiles.RemoveAll(item => item == null);
    }


    private List<Tile> RecursiveSearch(int i, int j, Tile tile)
    {
        List<Tile> result = new List<Tile>();
        if (i + 1 < _tileGenerator.Board.GetLength(0))
        {
            Tile tilebelow = _tileGenerator.Board[i + 1, j].Tile;
            if (tile._color == tilebelow._color)
            {
                if (i - 1 >= 0)
                {
                    Tile tileabove = _tileGenerator.Board[i - 1, j].Tile;
                    if (tile._color == tileabove._color)
                    {
                        result.Add(tilebelow);
                        result.Add(tileabove);
                    }
                }
            }
        }
        if (j + 1 < _tileGenerator.Board.GetLength(1))
        {
            Tile tileright = _tileGenerator.Board[i, j + 1].Tile;
            if (tile._color == tileright._color)
            {
                if (j - 1 >= 0)
                {
                    Tile tileleft = _tileGenerator.Board[i, j - 1].Tile;
                    if (tile._color == tileleft._color)
                    {
                        result.Add(tileright);
                        result.Add(tileleft);
                    }
                }
            }
        }
        if (result.Count > 0)
        {
            result.Add(tile);
        }
        return result;
    }

    private void Delete(List<Tile> list)
    {
        foreach (var x in list)
        {
            if (x != null)
            {
                Destroy(x.gameObject);
            }
            else
            {
                list.Remove(x);
            }
        }
    }

    private IEnumerator WaitandSearch()
    {
        yield return new WaitForSeconds(5);
        SearchMethod();
        yield return new WaitForSeconds(2);
        StopCoroutine(WaitandSearch());
    }

    private IEnumerator WaitandDelete()
    {
        yield return new WaitForSeconds(5);
        Delete(_deletionGroup);
        yield return new WaitForSeconds(2);
        StopCoroutine(WaitandDelete());
    }
}