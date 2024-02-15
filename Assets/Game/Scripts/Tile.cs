using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsMoving { get; private set; }

    private Slot _currentSlot;
    private float _spawnNextDelay;
    [SerializeField] public TileColor _color;

    public enum TileColor 
    { 
        Blue,
        Green,
        Orange,
        Purple,
        Red
    };

    public void Init(Slot slot, float spawnNextAfterDelay)
    {
        this._currentSlot = slot;
        _spawnNextDelay = spawnNextAfterDelay;
    }
    public bool IsCanMoveTo(Slot slot) => !IsMoving && !slot.IsHasTile;

    public IEnumerator MoveTileDown(Slot targetSlot, float duration)
    {
        IsMoving = true;

        float elapsedTime = 0f;

        var lastTile = _currentSlot;
        var initialPosition = lastTile.transform.position;
        var lastTileCleared = false;

        targetSlot.SetTile(this, _spawnNextDelay);

        while (elapsedTime < duration)
        {
            if (this != null)
            {
                var lerpValue = elapsedTime / duration;
                this.transform.position = Vector3.Lerp(initialPosition, targetSlot.transform.position, lerpValue);

                if (!lastTileCleared && lerpValue >= _spawnNextDelay)
                {
                    lastTileCleared = true;
                    lastTile.ClearTile();
                }

                elapsedTime += Time.deltaTime;
            }
            else
            {
                yield break;
            }

            yield return null;

        }

        this.transform.position = targetSlot.transform.position;

        IsMoving = false;

        yield break;
    }
}