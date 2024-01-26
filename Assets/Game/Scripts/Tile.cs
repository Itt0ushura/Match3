using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Slot _currentSlot;
    private bool _isMoving;
    private float _spawnNextDelay;

    public void Init(Slot slot, float spawnNextAfterDelay)
    {
        this._currentSlot = slot;
        _spawnNextDelay = spawnNextAfterDelay;
    }

    public bool IsCanMoveTo(Slot slot) => !_isMoving && !slot.IsHasTile;

    public IEnumerator MoveTileDown(Slot targetSlot, float duration)
    {
        _isMoving = true;

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

        _isMoving = false;

        yield break;
    }
}