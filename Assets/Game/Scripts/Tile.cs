using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsMovable;
    private Slot _slot;

    public void Init(Slot slot)
    {

        this._slot = slot;

    }
    public void CheckBelow(Slot slotbelow)
    {

        IsMovable = !slotbelow.IsHasTile;

    }

    public IEnumerator MoveTileDown(Transform tileTransform, Vector3 targetPosition, float duration)
    {

        float elapsedTime = 0f;
        Vector3 initialPosition = tileTransform.position;

        while (elapsedTime < duration)
        {
            tileTransform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        tileTransform.position = targetPosition;

    }
}