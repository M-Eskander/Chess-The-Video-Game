using UnityEngine;

public class Knights : ChessPieces
{

    public override void Update()
    {
        base.Update();
        if(!isControllable) return;
        float d = pieceColor == PieceColor.White ? -1f : 1f;
        if (moveRightAction.IsPressed())
        {
            if (moveForwardAction.WasPressedThisFrame())
            {
                Vector3 predictedPosition = transform.localPosition + new Vector3(2.5f * d ,0 ,1.25f * d);
                if (KillPiecesInPredictedPosition(predictedPosition))
                {
                    if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                        predictedPosition.z >= 0f)
                    {
                        transform.localPosition += new Vector3( 2.5f * d, 0, 1.25f * d);
                        turnBasedSetup.finishedTurn(pieceColor);
                        isControllable = false;
                    }
                }
                
            }
            else if (moveBackwardAction.WasPressedThisFrame())
            {
                Vector3 predictedPosition = transform.localPosition + new Vector3(2.5f * d ,0 ,-1.25f * d);
                if(KillPiecesInPredictedPosition(predictedPosition))
                {
                    if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                        predictedPosition.z >= 0f)
                    {
                        transform.localPosition += new Vector3( + 2.5f * d, 0,- 1.25f * d);
                        turnBasedSetup.finishedTurn(pieceColor);
                        isControllable = false;
                    }
                }
            }
        }
        else if (moveLeftAction.IsPressed())
        {
            if (moveForwardAction.WasPressedThisFrame())
            {
                Vector3 predictedPosition = transform.localPosition + new Vector3(-2.5f * d ,0 ,1.25f * d);
                if (KillPiecesInPredictedPosition(predictedPosition))
                {
                    if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                        predictedPosition.z >= 0f)
                    {
                        turnBasedSetup.finishedTurn(pieceColor);
                        isControllable = false;
                        transform.localPosition += new Vector3( - 2.5f * d, 0, + 1.25f * d);
                    }
                }
                
            }
            else if (moveBackwardAction.WasPressedThisFrame())
            {
                Vector3 predictedPosition = transform.localPosition + new Vector3(-2.5f * d ,0 ,-1.25f * d);
                if (KillPiecesInPredictedPosition(predictedPosition))
                {
                    if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                        predictedPosition.z >= 0f)
                    {
                        turnBasedSetup.finishedTurn(pieceColor);
                        isControllable = false;
                        transform.localPosition += new Vector3( -2.5f * d, 0,- 1.25f * d);
                    }
                }
               
            }
        }
        if (moveForwardAction.IsPressed())
        {
            if (moveLeftAction.WasPressedThisFrame())
            {
                Vector3 predictedPosition = transform.localPosition + new Vector3(-1.25f * d ,0 ,2.5f * d);
                if (KillPiecesInPredictedPosition(predictedPosition))
                {
                    if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                        predictedPosition.z >= 0f)
                    {
                        turnBasedSetup.finishedTurn(pieceColor);
                        isControllable = false;
                        transform.localPosition += new Vector3( - 1.25f * d, 0, +2.5f * d);
                    }
                }
            }
            else if (moveRightAction.WasPressedThisFrame())
            {
                Vector3 predictedPosition = transform.localPosition + new Vector3(+1.25f * d ,0 ,2.5f * d);
                if (KillPiecesInPredictedPosition(predictedPosition))
                {
                    if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                        predictedPosition.z >= 0f)
                    {
                        turnBasedSetup.finishedTurn(pieceColor);
                        isControllable = false;
                        transform.localPosition += new Vector3(+ 1.25f * d, 0,+2.5f * d);
                    }
                }
            }
        }
        else if(moveBackwardAction.IsPressed())
        {
            if (moveLeftAction.WasPressedThisFrame())
            {
                Vector3 predictedPosition = transform.localPosition + new Vector3(-1.25f * d ,0 ,-2.5f * d);
                if (KillPiecesInPredictedPosition(predictedPosition))
                {
                    if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                        predictedPosition.z >= 0f)
                    {
                        turnBasedSetup.finishedTurn(pieceColor);
                        isControllable = false;
                        transform.localPosition += new Vector3(- 1.25f * d, 0, -2.5f * d);
                    }
                }
               
            }
            else if (moveRightAction.WasPressedThisFrame())
            {
                Vector3 predictedPosition = transform.localPosition + new Vector3(+1.25f * d ,0 ,-2.5f * d);
                if (KillPiecesInPredictedPosition(predictedPosition))
                {
                    if (predictedPosition.x <= 8.75f && predictedPosition.x >= 0f && predictedPosition.z <= 8.75f &&
                        predictedPosition.z >= 0f)
                    {
                        turnBasedSetup.finishedTurn(pieceColor);
                        isControllable = false;
                        transform.localPosition += new Vector3( + 1.25f * d, 0, -2.5f * d);
                    }
                }
                
            }
        }
    }

    private bool KillPiecesInPredictedPosition(Vector3 predictedPosition)
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
