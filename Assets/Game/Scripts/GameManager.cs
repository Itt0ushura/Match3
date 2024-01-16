using UnityEngine;

public class GameManager : MonoBehaviour
{

    public TileGeneration _tileGenerator;
    public Slot slot;

    private void Start()
    {
        _tileGenerator.GenerateBoard();
        _tileGenerator.GenerateOneRow();
    }

    private void Update()
    {
        Check();
    }

    [ContextMenu("test")]
    private void Check()
    {
        for (int i = 0; i < _tileGenerator.boardSize.GetLength(0); i++)
        {
            for (int j = 0; j < _tileGenerator.boardSize.GetLength(1)-1; j++)
            {
                Debug.Log("i am working");
                if (_tileGenerator.boardSize[i, j+1].gameObject == slot)
                {
                    if (slot.tile == null)
                    {
                        Debug.Log("empty boy " + _tileGenerator.boardSize[i,j]);
                    }
                }
            }
        }
    }
}
