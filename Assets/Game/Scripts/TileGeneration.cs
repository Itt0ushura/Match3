using UnityEngine;

public class TileGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] _tilePrefab;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _board;

    public GameObject[,] BoardSize = new GameObject[5, 5];

    private int randomIndex;

    private Vector2 spawnPosition;

    [ContextMenu("generate board")]
    public void GenerateBoard()
    {

        float spawnPositionY = 6.4f;

        spawnPosition = new Vector2(0, spawnPositionY);

        for (int i = 0; i < BoardSize.GetLength(0); i++)
        {
            for (int j = 0; j < BoardSize.GetLength(1); j++)
            {

                randomIndex = Random.Range(0, _tilePrefab.Length);

                BoardSize[i, j] = Instantiate(_slotPrefab, spawnPosition, Quaternion.identity, _board.transform);

                spawnPosition.x += 1.6f;
            }
            spawnPositionY -= 1.6f;

            spawnPosition = new Vector2(0f, spawnPositionY);
        }
    }

    [ContextMenu("generate one row")]
    public void GenerateOneRow()
    {

        float spawnPositionY = 6.4f;

        spawnPosition = new Vector2(0, spawnPositionY);

        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < BoardSize.GetLength(1); j++)
            {
                randomIndex = Random.Range(0, _tilePrefab.Length);

                Instantiate(_tilePrefab[randomIndex], spawnPosition, Quaternion.identity, BoardSize[i, j].transform);

                spawnPosition.x += 1.6f;
            }
            spawnPositionY -= 1.6f;

            spawnPosition = new Vector2(0f, spawnPositionY);
        }
    }

    [ContextMenu("desintegrate board")]
    private void DestroyBoard()
    {
        for (int i = 0; i < BoardSize.GetLength(0); i++)
        {
            for (int j = 0; j < BoardSize.GetLength(1); j++)
            {
                DestroyImmediate(BoardSize[i, j]);
            }
        }
    }

    /*
    private void GenerationBombs()
    {
        for (int i = 0; i < boardSize.GetLength(0); i++)
        {
            for (int j = 0; j < boardSize.GetLength(1); j++)
            {
                randomIndex = Random.Range(0, _tilePrefab.Length);
                Instantiate(_tilePrefab[randomIndex], boardSize[i, j].transform.position, Quaternion.identity, boardSize[i, j].transform);
            }
        }
    } 
    */
}