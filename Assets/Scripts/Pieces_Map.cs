using UnityEngine.Tilemaps;
using UnityEngine;

public class Pieces_Map : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public Piece activePiece { get; private set; }
    public TetrominoData[] tetrominos;
    public Vector3Int spawnPosition;

    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < tetrominos.Length; i++)
        {
            tetrominos[i].Initialize();
        }
    }

    private void Start()
    {
        SpawnPieces();
    }

    public void SpawnPieces()
    {
            if (this.tetrominos.Length == 0)
        {
            Debug.Log("Tetrominos array is empty. Ensure that it is properly initialized.");
            return;
        }

        int rand = Random.Range(0, 6);
        TetrominoData data = this.tetrominos[rand];

        if (rand >= this.tetrominos.Length)
        {
            Debug.Log("Random index is out of range for tetrominos array.");
            return;
        }

        this.activePiece.Initialize(this, data, this.spawnPosition);
        Set(this.activePiece);
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }
}
