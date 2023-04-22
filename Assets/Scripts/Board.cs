using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public int width;
    public int height;

    public GameObject tileObject;

    public int CameraSizeOffset;
    public int CameraVerticalOffset;

    public GameObject[] availablePieces;

    Tile[,] Tiles;
    Piece[,] Pieces;

    Tile startTile;
    Tile endTile;

    // Start is called before the first frame update
    void Start()
    {

        Tiles = new Tile[width, height];
        Pieces = new Piece[width, height];

        SetupBoard();
        PositionCamera();
        SetupPieces();
    }

    private void SetupBoard() 
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var o = Instantiate(tileObject, new Vector3(x, y, -5), Quaternion.identity);
                o.transform.parent = transform;
                Tiles[x, y] = o.GetComponent<Tile>();
                Tiles[x, y]?.Setup(x, y, this);
            }
        }
    }

    private void PositionCamera()
    {
        float newPosX = (float)width / 2f;
        float newPosY = (float)height / 2f;

        Camera.main.transform.position = new Vector3(newPosX - 0.5f, newPosY - 0.5f + CameraVerticalOffset, -10);

        float horizontal = width + 0.5f;
        float vertical = (height / 2) + 0.5f;

        Camera.main.orthographicSize = horizontal > vertical ? horizontal + CameraSizeOffset : vertical;
    }

    private void SetupPieces()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var selectedPiece = availablePieces[UnityEngine.Random.Range(0, availablePieces.Length)];
                var o = Instantiate(selectedPiece, new Vector3(x, y, -5), Quaternion.identity);
                o.transform.parent = transform;
                Pieces[x, y] = o.GetComponent<Piece>();
                Pieces[x, y]?.Setup(x, y, this);
            }
        }
    }

    public void TileDown(Tile tile_)
    {
        startTile = tile_;
    }

    public void TileOver(Tile tile_)
    {
        endTile = tile_;
    }

    public void TileUp(Tile tile_)
    {

        var checkMove = IsCloseTo(startTile, endTile);

        if (startTile != null && endTile != null && checkMove)
        {
            SwapTiles();
        }

        startTile = null;
        endTile = null;
    }

    public void SwapTiles()
    {
        var startPiece = Pieces[startTile.x, startTile.y];
        var endPiece = Pieces[endTile.x, endTile.y];

        startPiece.Move(endPiece.x, endPiece.y);
        endPiece.Move(startPiece.x, startPiece.y);


        Pieces[startTile.x, startTile.y] = endPiece;
        Pieces[endTile.x, endTile.y] = startPiece;

    }

    public bool IsCloseTo(Tile start, Tile end)
    {
        if(
            Math.Abs(start.x-end.x) == 1 && start.y == end.y ||
            Math.Abs(start.y - end.y) == 1 && start.x == end.x
         )
        {
            return true;
        }

        return false;
    }
}
