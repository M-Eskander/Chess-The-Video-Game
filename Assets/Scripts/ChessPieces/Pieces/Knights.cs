using UnityEngine;
using MainClasses;
namespace chessPieces.Pieces
{
    public class Knights : ChessPieces
    {
        public override void Update()
        {
            base.Update();
            HandleKnightMovement();
        }
        private void HandleKnightMovement()
        {
            if (!isControllable) return;
            if (chessActions.MoveRight.IsPressed())
            {
                if (chessActions.MoveForward.WasPressedThisFrame())
                {
                    Vector3 predictedPosition = transform.localPosition + new Vector3(2.5f * dir ,0 ,1.25f * dir);
                    if (CanKillPiecesInPredictedPosition(predictedPosition))
                    {
                        if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                            predictedPosition.z >= 0f)
                        {
                            transform.localPosition += new Vector3( 2.5f * dir, 0, 1.25f * dir);
                            baseGameplay.HasPlayed(pieceColor);
                            isControllable = false;
                        }
                    }
                    
                }
                else if (chessActions.MoveBackward.WasPressedThisFrame())
                {
                    Vector3 predictedPosition = transform.localPosition + new Vector3(2.5f * dir ,0 ,-1.25f * dir);
                    if(CanKillPiecesInPredictedPosition(predictedPosition))
                    {
                        if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                            predictedPosition.z >= 0f)
                        {
                            transform.localPosition += new Vector3( + 2.5f * dir, 0,- 1.25f * dir);
                            baseGameplay.HasPlayed(pieceColor);
                            isControllable = false;
                        }
                    }
                }
            }
            else if (chessActions.MoveLeft.IsPressed())
            {
                if (chessActions.MoveForward.WasPressedThisFrame())
                {
                    Vector3 predictedPosition = transform.localPosition + new Vector3(-2.5f * dir ,0 ,1.25f * dir);
                    if (CanKillPiecesInPredictedPosition(predictedPosition))
                    {
                        if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                            predictedPosition.z >= 0f)
                        {
                            baseGameplay.HasPlayed(pieceColor);
                            isControllable = false;
                            transform.localPosition += new Vector3( - 2.5f * dir, 0, + 1.25f * dir);
                        }
                    }
                    
                }
                else if (chessActions.MoveBackward.WasPressedThisFrame())
                {
                    Vector3 predictedPosition = transform.localPosition + new Vector3(-2.5f * dir ,0 ,-1.25f * dir);
                    if (CanKillPiecesInPredictedPosition(predictedPosition))
                    {
                        if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                            predictedPosition.z >= 0f)
                        {
                            baseGameplay.HasPlayed(pieceColor);
                            isControllable = false;
                            transform.localPosition += new Vector3( -2.5f * dir, 0,- 1.25f * dir);
                        }
                    }
                   
                }
            }
            if (chessActions.MoveForward.IsPressed())
            {
                if (chessActions.MoveLeft.WasPressedThisFrame())
                {
                    Vector3 predictedPosition = transform.localPosition + new Vector3(-1.25f * dir ,0 ,2.5f * dir);
                    if (CanKillPiecesInPredictedPosition(predictedPosition))
                    {
                        if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                            predictedPosition.z >= 0f)
                        {
                            baseGameplay.HasPlayed(pieceColor);
                            isControllable = false;
                            transform.localPosition += new Vector3( - 1.25f * dir, 0, +2.5f * dir);
                        }
                    }
                }
                else if (chessActions.MoveRight.WasPressedThisFrame())
                {
                    Vector3 predictedPosition = transform.localPosition + new Vector3(+1.25f * dir ,0 ,2.5f * dir);
                    if (CanKillPiecesInPredictedPosition(predictedPosition))
                    {
                        if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                            predictedPosition.z >= 0f)
                        {
                            baseGameplay.HasPlayed(pieceColor);
                            isControllable = false;
                            transform.localPosition += new Vector3(+ 1.25f * dir, 0,+2.5f * dir);
                        }
                    }
                }
            }
            else if(chessActions.MoveBackward.IsPressed())
            {
                if (chessActions.MoveLeft.WasPressedThisFrame())
                {
                    Vector3 predictedPosition = transform.localPosition + new Vector3(-1.25f * dir ,0 ,-2.5f * dir);
                    if (CanKillPiecesInPredictedPosition(predictedPosition))
                    {
                        if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                            predictedPosition.z >= 0f)
                        {
                            baseGameplay.HasPlayed(pieceColor);
                            isControllable = false;
                            transform.localPosition += new Vector3(- 1.25f * dir, 0, -2.5f * dir);
                        }
                    }
                   
                }
                else if (chessActions.MoveRight.WasPressedThisFrame())
                {
                    Vector3 predictedPosition = transform.localPosition + new Vector3(+1.25f * dir ,0 ,-2.5f * dir);
                    if (CanKillPiecesInPredictedPosition(predictedPosition))
                    {
                        if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                            predictedPosition.z >= 0f)
                        {
                            baseGameplay.HasPlayed(pieceColor);
                            isControllable = false;
                            transform.localPosition += new Vector3( + 1.25f * dir, 0, -2.5f * dir);
                        }
                    }
                    
                }
            }
        }
        protected override bool CanKillPiecesInPredictedPosition(Vector3 predictedPosition)
        {
            Vector3 halfExtents = new Vector3(0.4f, 0.4f, 0.4f);
            if(pieceColor == PieceColor.White) predictedPosition = new Vector3(8.75f - predictedPosition.x, predictedPosition.y, 8.75f - predictedPosition.z);
            Collider[] hits = Physics.OverlapBox(predictedPosition, halfExtents);
            foreach (Collider col in hits)
            {
                ChessPieces killablePiece = col.gameObject.GetComponent<ChessPieces>();
                if (killablePiece == null) continue;
                if (killablePiece.pieceColor != pieceColor)
                {
                    Destroy(killablePiece.gameObject);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}

