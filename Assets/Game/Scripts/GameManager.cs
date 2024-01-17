using System.Collections;
using UnityEngine;



//перемещать челов имея два значения от слота и тайла
public class GameManager : MonoBehaviour
{

    public TileGeneration TileGenerator;
    private bool _checkable;
    [SerializeField] private Slot slot;

    private void Start()
    {
        TileGenerator.GenerateBoard();
        TileGenerator.GenerateOneRow();
    }

    private void Update()
    {
        GridFill();
    }
    //сделать анимированное падение фишек(хотя бы не такое дёрганное)
    private void GridFill()
    {
        _checkable = false;
        for (int i = 0; i < TileGenerator.BoardSize.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < TileGenerator.BoardSize.GetLength(1); j++)
            {
                Slot currentCell = TileGenerator.BoardSize[i, j];
                Slot cellBelow = TileGenerator.BoardSize[i + 1, j];
                if (cellBelow.tile == null)
                {
                    _checkable = true;
                    Transform child = currentCell.transform.GetChild(0);
                    child.SetParent(cellBelow.transform);
                    StartCoroutine(MoveTileDown(child.transform, cellBelow.transform.position, 0.5f));
                }
            }
        }
        if (_checkable)
        {
            TileGenerator.GenerateOneRow();
        }
    }
    private IEnumerator MoveTileDown(Transform tileTransform, Vector3 targetPosition, float duration)
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