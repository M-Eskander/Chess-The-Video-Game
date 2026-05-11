using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChessPieces : MonoBehaviour
{
    public InputAction moveForwardAction;
    public InputAction moveBackwardAction;
    public InputAction moveLeftAction;
    public InputAction moveRightAction;
    public InputAction pawnDoubleMoveAction;
    public TurnBasedSetup turnBasedSetup;

    public virtual void OnEnable()
    {
        moveForwardAction.Enable();
        moveBackwardAction.Enable();
        moveLeftAction.Enable();
        moveRightAction.Enable();
        pawnDoubleMoveAction.Enable();
    }
    
    public bool isControllable = false;
    public bool canKill;
    public enum PieceColor { White, Black }
    public PieceColor pieceColor;

    public virtual void Update()
    {
        CheckForNearbyEnemies();
        HighlightPiece();
    }

    private void HighlightPiece() //not being used yet
    {
        if (isControllable)
            transform.GetChild(0).gameObject.SetActive(true);
        //add highlight possible positions
    }

    protected virtual void CheckForNearbyEnemies() //not being used
    {
        Vector3 center = transform.position;
        Vector3 halfExtents = new Vector3(1.2f, 1.2f, 1.2f);
        Collider[] hitColliders = Physics.OverlapBox(center, halfExtents, transform.rotation);

        foreach (Collider col in hitColliders)
        {
            if (col.gameObject == gameObject) continue;
            ChessPieces otherPiece = col.gameObject.GetComponent<ChessPieces>();
            if (otherPiece == null) continue;
            if (otherPiece.pieceColor == pieceColor) continue;
            Debug.Log("Danger is close: " + col.gameObject.name);
        }
    }
}