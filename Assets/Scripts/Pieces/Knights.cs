using UnityEngine;

public class Knights : ChessPieces
{
    public override void Update()
    {
        base.Update();
        HandleKnightMovement();
    }
    private void HandleKnightMovement()
    {
        if(!isControllable) return;
        float d = pieceColor == PieceColor.White ? -1f : 1f;
        if (chessActions.MoveRight.IsPressed())
        {
            if (chessActions.MoveForward.WasPressedThisFrame())
            {
                Vector3 predictedPosition = transform.localPosition + new Vector3(2.5f * d ,0 ,1.25f * d);
                if (CanKillPiecesInPredictedPosition(predictedPosition))
                {
                    if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                        predictedPosition.z >= 0f)
                    {
                        transform.localPosition += new Vector3( 2.5f * d, 0, 1.25f * d);
                        gameplay.HasPlayed(pieceColor);
                        isControllable = false;
                    }
                }
                
            }
            else if (chessActions.MoveBackward.WasPressedThisFrame())
            {
                Vector3 predictedPosition = transform.localPosition + new Vector3(2.5f * d ,0 ,-1.25f * d);
                if(CanKillPiecesInPredictedPosition(predictedPosition))
                {
                    if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                        predictedPosition.z >= 0f)
                    {
                        transform.localPosition += new Vector3( + 2.5f * d, 0,- 1.25f * d);
                        gameplay.HasPlayed(pieceColor);
                        isControllable = false;
                    }
                }
            }
        }
        else if (chessActions.MoveLeft.IsPressed())
        {
            if (chessActions.MoveForward.WasPressedThisFrame())
            {
                Vector3 predictedPosition = transform.localPosition + new Vector3(-2.5f * d ,0 ,1.25f * d);
                if (CanKillPiecesInPredictedPosition(predictedPosition))
                {
                    if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                        predictedPosition.z >= 0f)
                    {
                        gameplay.HasPlayed(pieceColor);
                        isControllable = false;
                        transform.localPosition += new Vector3( - 2.5f * d, 0, + 1.25f * d);
                    }
                }
                
            }
            else if (chessActions.MoveBackward.WasPressedThisFrame())
            {
                Vector3 predictedPosition = transform.localPosition + new Vector3(-2.5f * d ,0 ,-1.25f * d);
                if (CanKillPiecesInPredictedPosition(predictedPosition))
                {
                    if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                        predictedPosition.z >= 0f)
                    {
                        gameplay.HasPlayed(pieceColor);
                        isControllable = false;
                        transform.localPosition += new Vector3( -2.5f * d, 0,- 1.25f * d);
                    }
                }
               
            }
        }
        if (chessActions.MoveForward.IsPressed())
        {
            if (chessActions.MoveLeft.WasPressedThisFrame())
            {
                Vector3 predictedPosition = transform.localPosition + new Vector3(-1.25f * d ,0 ,2.5f * d);
                if (CanKillPiecesInPredictedPosition(predictedPosition))
                {
                    if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                        predictedPosition.z >= 0f)
                    {
                        gameplay.HasPlayed(pieceColor);
                        isControllable = false;
                        transform.localPosition += new Vector3( - 1.25f * d, 0, +2.5f * d);
                    }
                }
            }
            else if (chessActions.MoveRight.WasPressedThisFrame())
            {
                Vector3 predictedPosition = transform.localPosition + new Vector3(+1.25f * d ,0 ,2.5f * d);
                if (CanKillPiecesInPredictedPosition(predictedPosition))
                {
                    if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                        predictedPosition.z >= 0f)
                    {
                        gameplay.HasPlayed(pieceColor);
                        isControllable = false;
                        transform.localPosition += new Vector3(+ 1.25f * d, 0,+2.5f * d);
                    }
                }
            }
        }
        else if(chessActions.MoveBackward.IsPressed())
        {
            if (chessActions.MoveLeft.WasPressedThisFrame())
            {
                Vector3 predictedPosition = transform.localPosition + new Vector3(-1.25f * d ,0 ,-2.5f * d);
                if (CanKillPiecesInPredictedPosition(predictedPosition))
                {
                    if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                        predictedPosition.z >= 0f)
                    {
                        gameplay.HasPlayed(pieceColor);
                        isControllable = false;
                        transform.localPosition += new Vector3(- 1.25f * d, 0, -2.5f * d);
                    }
                }
               
            }
            else if (chessActions.MoveRight.WasPressedThisFrame())
            {
                Vector3 predictedPosition = transform.localPosition + new Vector3(+1.25f * d ,0 ,-2.5f * d);
                if (CanKillPiecesInPredictedPosition(predictedPosition))
                {
                    if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                        predictedPosition.z >= 0f)
                    {
                        gameplay.HasPlayed(pieceColor);
                        isControllable = false;
                        transform.localPosition += new Vector3( + 1.25f * d, 0, -2.5f * d);
                    }
                }
                
            }
        }
    }
    private bool CanKillPiecesInPredictedPosition(Vector3 predictedPosition)
    {
        Vector3 halfExtents = new Vector3(0.4f, 0.4f, 0.4f);
        Collider[] hits = Physics.OverlapBox(predictedPosition, halfExtents);
        foreach (Collider col in hits)
        {
            ChessPieces killablePiece = col.gameObject.GetComponent<ChessPieces>();
            if (killablePiece == null) continue;
            if(killablePiece.transform.localPosition != predictedPosition) continue;
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
