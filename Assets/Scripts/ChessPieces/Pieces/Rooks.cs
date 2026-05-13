using UnityEngine;
using MainClasses;

namespace chessPieces.Pieces
{
    public class Rooks : ChessPieces
    {
        private Vector3 _predictedPosition;
        private bool _played =  true;
        public override void Update()
        {   //organise movement
            base.Update();
            if (!isControllable) return;
            if (chessActions.MoveForward.IsPressed())
            {
                if (CalculateWantedDistance())
                {
                    _predictedPosition = transform.localPosition + new Vector3(0, 0, dir * wantedDistance);
                    _played = false;
                }
            }
            else if (chessActions.MoveForward.WasReleasedThisFrame())
            {
                if (!CalculateWantedDistance())
                {
                    _predictedPosition = transform.localPosition + new Vector3(0, 0, dir * 1.25f);
                    _played = false;
                }
            }

            if (chessActions.MoveBackward.IsPressed())
            {
                if (CalculateWantedDistance())
                {
                    _predictedPosition = transform.localPosition + new Vector3(0, 0, -dir * wantedDistance);
                    _played = false;
                }
            }
            else if (chessActions.MoveBackward.WasReleasedThisFrame())
            {
                if (!CalculateWantedDistance())
                {
                    _predictedPosition = transform.localPosition + new Vector3(0, 0, -dir * 1.25f);
                    _played = false;
                }
            }

            if (chessActions.MoveRight.IsPressed())
            {
                if (CalculateWantedDistance())
                {
                    _predictedPosition = transform.localPosition + new Vector3(dir * wantedDistance, 0, 0);
                    _played = false;
                }
            }
            else if (chessActions.MoveRight.WasReleasedThisFrame())
            {
                if (!CalculateWantedDistance())
                {
                    _predictedPosition = transform.localPosition + new Vector3(dir * 1.25f, 0, 0);
                    _played = false;
                }
            }

            if (chessActions.MoveLeft.IsPressed())
            {
                if (CalculateWantedDistance())
                {
                    _predictedPosition = transform.localPosition + new Vector3(-dir * wantedDistance, 0, 0);
                    _played = false;
                }
            }
            else if (chessActions.MoveLeft.WasReleasedThisFrame())
            {
                if (!CalculateWantedDistance())
                {
                    _predictedPosition = transform.localPosition + new Vector3(-dir * 1.25f, 0, 0);
                    _played = false;
                }
            }

            Debug.Log(_predictedPosition);
            
            if (isPredictedPositionOnBoard(_predictedPosition) && 
            !_played && CanKillPiecesInPredictedPosition(_predictedPosition))
            {
                transform.localPosition = _predictedPosition;
                _played = true;
                baseGameplay.HasPlayed(pieceColor);
            }
        }

    }
}