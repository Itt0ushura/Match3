using UnityEngine;

public class TileGeneration : MonoBehaviour
{
    [SerializeField] private Tile[] tilePrefab;
    [SerializeField] public Slot SlotPrefab;
    [SerializeField] private GameObject board;

    public Slot[,] Board = new Slot[5, 5];

    private Vector2 _spawnOffset;
    [SerializeField,Tooltip("set start x coordinates for grid")] private float spawnOffsetX;
    [SerializeField, Tooltip("set start y coordinates for grid")] private float _spawnOffsetY;

    public void Init(Slot slot)
    {
        SlotPrefab = slot;
    }

    [ContextMenu("generate board")]
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

    [ContextMenu("gentest")]
    public void GenerateTile()
    {
        int randomIndex;
        for (int j = 0; j < Board.GetLength(1); j++)
        {
            randomIndex = Random.Range(0, tilePrefab.Length);
            Slot slot = Board[0, j];
            if (slot.Tile == null)
            {

                var tile = Instantiate(tilePrefab[randomIndex], slot.transform.position, Quaternion.identity);

                slot.Init(tile);

                slot.SetTile(tile);
            
            }
        }
    }
}