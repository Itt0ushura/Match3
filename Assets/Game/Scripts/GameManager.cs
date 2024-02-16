using System;
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

    private bool _isBoardGenerationProccessActive;

    private void Start()
    {
        _tileGenerator = GetComponent<TileGeneration>();
        _tileGenerator.GenerateBoard();
        StartCoroutine(FillAllBoardRoutine(null));
    }

    private void Update()
    {
        if (_isBoardGenerationProccessActive)
            return;
        StartCoroutine(CheckLoop(FillAllBoardRoutine(SearchAndDelete)));
        if (Input.GetMouseButtonDown(0))
        {
            Actions.OnDelete.Invoke();

            StartCoroutine(FillAllBoardRoutine(SearchAndDelete));
        }
    }

    private IEnumerator CheckLoop(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
        if (IsAllSlotsHasTiles())
        {
            yield return new WaitForEndOfFrame();
            StopCoroutine(coroutine);
        }
    }

    private IEnumerator FillAllBoardRoutine(Action callback)
    {
        _isBoardGenerationProccessActive = true;

        while (!IsAllSlotsHasTiles())
        {
            yield return new WaitForEndOfFrame();
            GridFill();
        }

        _isBoardGenerationProccessActive = false;
        callback?.Invoke();
    }

    private bool IsAllSlotsHasTiles()
    {
        foreach (var slot in _tileGenerator.Board)
        {
            if (!slot.IsHasTile || slot.Tile.IsMoving)
            {
                return false;
            }
        }

        return true;
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
                List<Tile> result = AbsolutelyNotRecursiveSearch(i, j, tile);
                _deletionGroup.AddRange(result);
            }
        }
        _checkedTiles.RemoveAll(item => item == null);
    }

    private List<Tile> AbsolutelyNotRecursiveSearch(int i, int j, Tile tile)
    {
        List<Tile> result = new List<Tile>();

        if (tile == null)
            return result;

        var isHasSlotAboveAndBelow = IsHasOpositeSlots(i, 0);
        var isHasSlotLeftAndRight = IsHasOpositeSlots(j, 1);

        if (isHasSlotAboveAndBelow)
        {
            if (TryGetTileWithColorMatch(i + 1, j, tile._color, out var tileBelow))
            {
                if (TryGetTileWithColorMatch(i - 1, j, tile._color, out var tileAbove))
                {
                    result.Add(tileBelow);
                    result.Add(tileAbove);
                }
            }
        }
        if (isHasSlotLeftAndRight)
        {
            if (TryGetTileWithColorMatch(i, j + 1, tile._color, out var tileRight))
            {
                if (TryGetTileWithColorMatch(i, j - 1, tile._color, out var tileLeft))
                {
                    result.Add(tileRight);
                    result.Add(tileLeft);
                }
            }
        }

        if (result.Count > 0)
            result.Add(tile);

        return result;
    }

    private bool TryGetTileWithColorMatch(int i, int j, Tile.TileColor color, out Tile tile)
    {
        return TryGetTile(i, j, out tile) && tile._color == color;
    }
    private bool TryGetTile(int i, int j, out Tile tile)
    {
        tile = _tileGenerator.Board[i, j].Tile;
        return tile != null;
    }
    private bool IsHasOpositeSlots(int elementIndex, int dimensionIndex)
    {
        return elementIndex - 1 >= 0 && elementIndex + 1 < _tileGenerator.Board.GetLength(dimensionIndex);
    }

    private void Delete(List<Tile> list)
    {
        list.Distinct();

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] != null)
                Destroy(list[i].gameObject);
        }
        list.Clear();
    }

    private void SearchAndDelete()
    {
        SearchMethod();
        Delete(_deletionGroup);
    }
}