using UnityEngine;


//генерировать сетку элементов и спавнить ряды тайлов
public class TileGeneration : MonoBehaviour
{
    [SerializeField] private Tile[] _tilePrefab;
    [SerializeField] private Slot _slotPrefab;
    [SerializeField] private GameObject _board;

    public Slot[,] BoardSize = new Slot[5, 5]; //передать в тайл.цс

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

        for (int j = 0; j < BoardSize.GetLength(1); j++)
        {
            randomIndex = Random.Range(0, _tilePrefab.Length);

            Instantiate(_tilePrefab[randomIndex], spawnPosition, Quaternion.identity, BoardSize[0, j].transform);

            spawnPosition.x += 1.6f;
        }
        spawnPositionY -= 1.6f;

        spawnPosition = new Vector2(0f, spawnPositionY);

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
}