using UnityEngine;
using MainClasses;
namespace chessPieces.Pieces
{
    public class Pawns : ChessPieces
    {
        private Vector3 _pawnMoveDistance = new Vector3(0,0,1.25f);
        public override void Update()
        {
            base.Update();
            HandlePawnMovement();
        }
        private void HandlePawnMovement()
        {
            if (!isControllable) return;
            RaycastHit hitRight, hitLeft;
            if (Physics.Raycast(transform.position, new Vector3(-1 ,0,1), out hitLeft,  1.25f))
            {
                ChessPieces killablePieceLeft = hitLeft.collider.gameObject.GetComponent<ChessPieces>();
                if (killablePieceLeft != null && killablePieceLeft.pieceColor != pieceColor)
                {
                    if (chessActions.MoveForward.IsPressed())
                    {
                        if (chessActions.MoveLeft.WasPressedThisFrame())
                        {
                            transform.localPosition = killablePieceLeft.transform.localPosition;
                            baseGameplay.HasPlayed(pieceColor);
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
                    if (chessActions.MoveForward.IsPressed())
                    {
                        if (chessActions.MoveRight.WasPressedThisFrame())
                        {
                            transform.localPosition = killablePieceRight.transform.localPosition;
                            baseGameplay.HasPlayed(pieceColor);
                            isControllable = false;
                            Destroy(killablePieceRight.gameObject);
                        }
                    }
                }
            }

            var multiplier = CheckIfCanDoubleStep();
            if (chessActions.MoveForward.WasReleasedThisFrame() && transform.localPosition.z < 8.75f)
            {
                RaycastHit hit;
                bool blocked = Physics.Raycast(transform.position, _pawnMoveDistance.normalized, out hit, 1.25f)
                               && hit.collider.GetComponent<ChessPieces>() != null;
                if (!blocked)
                {
                    if(pieceColor == PieceColor.White)
                        transform.localPosition += -1 * _pawnMoveDistance * multiplier;
                    else
                        transform.localPosition += _pawnMoveDistance * multiplier;
                    baseGameplay.HasPlayed(pieceColor);
                    isControllable = false;
                }
            }
        }

        private float CheckIfCanDoubleStep()
        {
            RaycastHit hit2;
            bool blocked2 = Physics.Raycast(transform.position, _pawnMoveDistance.normalized, out hit2, 2.5f)
                            && hit2.collider.GetComponent<ChessPieces>() != null;
            float multiplier = 1;
            if (chessActions.Num2.IsPressed() && !blocked2)
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

            return multiplier;
        }
    }
}
