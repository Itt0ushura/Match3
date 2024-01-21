using UnityEngine;

public class TileGeneration : MonoBehaviour
{
    [SerializeField, Tooltip("Array of Tiles")] private Tile[] tilePrefab;
    [SerializeField] public Slot SlotPrefab;
    [SerializeField, Tooltip("Game Object for board, where slots will be placed")] private GameObject board;

    public Slot[,] Board = new Slot[5, 5];

    private Vector2 _spawnOffset;
    [SerializeField,Tooltip("set start x coordinates for grid")] private float spawnOffsetX;
    [SerializeField, Tooltip("set start y coordinates for grid")] private float _spawnOffsetY;


    [ContextMenu("Generate Board")]
    public void GenerateBoard()
    {
        _spawnOffset = new Vector2(spawnOffsetX, _spawnOffsetY);

        for (int i = 0; i < Board.GetLength(0); i++)
        {
            for (int j = 0; j < Board.GetLength(1); j++)
            {

                Board[i, j] = Instantiate(SlotPrefab, _spawnOffset, Quaternion.identity, board.transform);

                _spawnOffset.x += 1.6f;

            }

           _spawnOffset.x = 0f;
           _spawnOffset.y -= 1.6f;
        }
    }

    public void GenerateTile()
    {
        int randomIndex;

        for (int j = 0; j < Board.GetLength(1); j++)
        {
            Slot slot = Board[0, j];

            if (!slot.IsHasTile)
            {

                randomIndex = Random.Range(0, tilePrefab.Length);

                var tile = Instantiate(tilePrefab[randomIndex], slot.transform.position, Quaternion.identity);

                slot.SetTile(tile);
            
            }
        }
    }
}