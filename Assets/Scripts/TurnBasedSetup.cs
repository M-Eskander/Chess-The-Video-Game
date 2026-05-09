using System;
using UnityEngine;

public class TurnBasedSetup : MonoBehaviour
{
    public SelectPieceToMove pieceSelector;
    public GameObject chessBoard;
    private Quaternion target;
    public float speed = 100;
    public bool whiteTurn = true;
    public bool blackTurn = false;

    public void finishedTurn(ChessPieces.PieceColor whatMoved)
    {
        pieceSelector.QuitSelection = true;
        pieceSelector.lastPieceSelected.transform.GetChild(0).gameObject.SetActive(false);
        if (whatMoved == ChessPieces.PieceColor.White)
        {
            whiteTurn = false;
            blackTurn = true;
            pieceSelector.Player = SelectPieceToMove.whoPlays.Black;
        }
        else
        {
            whiteTurn = true;
            blackTurn = false;
            pieceSelector.Player = SelectPieceToMove.whoPlays.White;
        }
        pieceSelector.resetHighlightPos();
    }

    private void Start()
    {
        pieceSelector.Player = SelectPieceToMove.whoPlays.White;
    }
    
    private void Update()
    {
        target = whiteTurn ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        chessBoard.transform.rotation = Quaternion.RotateTowards(chessBoard.transform.rotation, target, speed * Time.deltaTime);
    }
}
