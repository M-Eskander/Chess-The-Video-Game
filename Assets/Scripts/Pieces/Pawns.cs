using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;

public class Pawns : ChessPieces
{
    private Vector3 _pawnMoveDistance = new Vector3(0,0,1.25f);
    public override void Update()
    {
        base.Update();
        if(!isControllable) return;
        canKill = false;
        RaycastHit hitRight, hitLeft;
        if (Physics.Raycast(transform.position, new Vector3(-1 ,0,1), out hitLeft,  1.25f))
        {
            ChessPieces killablePieceLeft = hitLeft.collider.gameObject.GetComponent<ChessPieces>();
            if (killablePieceLeft != null && killablePieceLeft.pieceColor != pieceColor)
            {
                canKill = true;
                if (moveForwardAction.IsPressed())
                {
                    if (moveLeftAction.WasPressedThisFrame())
                    {
                        transform.localPosition = killablePieceLeft.transform.localPosition;
                        turnBasedSetup.finishedTurn(pieceColor);
                        isControllable = false;
                        Destroy(killablePieceLeft.gameObject);
                    }
                }
            }
        }
        if (Physics.Raycast(transform.position, new Vector3(1 , 0, 1 ), out hitRight, 1.25f))
        {
            ChessPieces killablePieceRight = hitRight.collider.gameObject.GetComponent<ChessPieces>();
            if (killablePieceRight != null && killablePieceRight.pieceColor != pieceColor)
            {
                canKill = true;
                if (moveForwardAction.IsPressed())
                {
                    if (moveRightAction.WasPressedThisFrame())
                    {
                        transform.localPosition = killablePieceRight.transform.localPosition;
                        turnBasedSetup.finishedTurn(pieceColor);
                        isControllable = false;
                        Destroy(killablePieceRight.gameObject);
                    }
                }
            }
        }
        if (moveForwardAction.WasReleasedThisFrame() && transform.localPosition.z < 8.75f)
        {
            RaycastHit hit;
            bool blocked = Physics.Raycast(transform.position, _pawnMoveDistance.normalized, out hit, 1.25f)
                           && hit.collider.GetComponent<ChessPieces>() != null;
            if (!blocked)
            {
                float multiplier = 1;
                if (pawnDoubleMoveAction.IsPressed())
                {
                    if (pieceColor == PieceColor.White)
                    {
                        if (transform.localPosition.z == 7.5f)
                            multiplier = 2;
                    }
                    else
                    {
                        if (transform.localPosition.z == 1.25f)
                            multiplier = 2;
                    }
                }
                else
                {
                    multiplier = 1;
                }
                
                if(pieceColor == PieceColor.White)
                    transform.localPosition += -1 * _pawnMoveDistance * multiplier;
                else
                    transform.localPosition += _pawnMoveDistance * multiplier;
                turnBasedSetup.finishedTurn(pieceColor);
                isControllable = false;
            }
        }
    }
}
