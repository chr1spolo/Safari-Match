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

    // Start is called before the first frame update
    void Start()
    {
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
                o.GetComponent<Tile>()?.Setup(x, y, this);
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
                o.GetComponent<Piece>()?.Setup(x, y, this);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
