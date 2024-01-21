using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Slot _currentSlot;

    public void Init(Slot slot)
    {

        this._currentSlot = slot;

    }
    public bool IsCanMoveTo(Slot slot) => !slot.IsHasTile;

    public IEnumerator MoveTileDown(Slot targetSlot, float duration)
    {

        float elapsedTime = 0f;

        Vector3 initialPosition = _currentSlot.transform.position;

        while (elapsedTime < duration)
        {
            this.transform.position = Vector3.Lerp(initialPosition, targetSlot.transform.position, elapsedTime / duration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        this.transform.position = targetSlot.transform.position;

        _currentSlot.ClearTile();
        
        targetSlot.SetTile(this);
        
        yield break;
    }
}