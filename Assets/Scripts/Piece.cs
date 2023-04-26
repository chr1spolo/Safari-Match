using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Piece : MonoBehaviour
{

    public int x;
    public int y;
    public Board board;

    public enum type
    {
        elephant,
        giraffe,
        hippo,
        monkey,
        panda,
        parrot,
        penguin,
        pig,
        rabbit,
        snake
    };

    public type pieceType;

    public int valuePerMatch = 0;

    public void Setup(int x_, int y_, Board board_)
    {
        x = x_;
        y = y_;
        board = board_;

        transform.localScale = Vector3.one * 0.35f;
        transform.DOScale(Vector3.one, 0.35f);

        if (
            pieceType == type.elephant ||
            pieceType == type.giraffe ||
            pieceType == type.hippo
        )
        {
            valuePerMatch = 1;
        }
        else if (
            pieceType == type.monkey ||
            pieceType == type.panda ||
            pieceType == type.parrot
        )
        {
            valuePerMatch = 2;
        }
        else
        {
            valuePerMatch = 3;
        }
    }

    public void Move(int destX, int destY)
    {
        transform.DOMove(new Vector3(destX, destY, -5), 0.4f).SetEase(Ease.InOutCubic).onComplete = () =>
        {
            x = destX;
            y = destY;
        };
    }

    public void DissapearPiece(bool animated)
    {
        if (animated)
        {
            transform.DORotate(new Vector3(0, 0, -120f), 0.12f);
            transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.4f).onComplete = () =>
            {
                transform.DOScale(Vector3.zero, 0.1f).onComplete = () =>
                {
                    Destroy(gameObject);
                };
            };
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [ContextMenu("Test Move")]
    public void MoveTest()
    {
        Move(0, 0);
    }

    [ContextMenu("Dissapear")]
    public void DissapearPieceTest()
    {
        DissapearPiece(true);
    }
}
