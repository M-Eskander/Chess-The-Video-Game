using UnityEngine;
using MainClasses;
namespace chessPieces.Pieces
{
    public class Bishops : ChessPieces
    {
        private Vector3 _predictedPosition;
        private bool _played =  true;
        public override void Update()
        {   //organise movement
            base.Update();
            if (!isControllable) return;
            if (chessActions.MoveForward.IsPressed() && chessActions.MoveRight.IsPressed())
            {
                if (CalculateWantedDistance())
                {
                    _predictedPosition = transform.localPosition + new Vector3(dir * wantedDistance, 0, dir * wantedDistance);
                    _played = false;
                }
            }
            else if ((chessActions.MoveForward.IsPressed() && chessActions.MoveRight.WasReleasedThisFrame())
                   || (chessActions.MoveForward.WasReleasedThisFrame() && chessActions.MoveRight.IsPressed()))
            {
                if (!CalculateWantedDistance())
                {
                    _predictedPosition = transform.localPosition + new Vector3(dir * 1.25f, 0, dir * 1.25f);
                    _played = false;
                }
            }
            
            if (chessActions.MoveForward.IsPressed() && chessActions.MoveLeft.IsPressed())
            {
                if (CalculateWantedDistance())
                {
                    _predictedPosition = transform.localPosition + new Vector3(-dir * wantedDistance, 0, dir * wantedDistance);
                    _played = false;
                }
            }
            else if ((chessActions.MoveForward.IsPressed() && chessActions.MoveLeft.WasReleasedThisFrame())
                  || (chessActions.MoveForward.WasReleasedThisFrame() && chessActions.MoveLeft.IsPressed()))
            {
                if (!CalculateWantedDistance())
                {
                    _predictedPosition = transform.localPosition + new Vector3(-dir * 1.25f, 0, dir * 1.25f);
                    _played = false;
                }
            }

            if (chessActions.MoveBackward.IsPressed() && chessActions.MoveRight.IsPressed())
            {
                if (CalculateWantedDistance())
                {
                    _predictedPosition = transform.localPosition + new Vector3(dir * wantedDistance, 0, -dir * wantedDistance);
                    _played = false;
                }
            }
            else if ((chessActions.MoveBackward.IsPressed() && chessActions.MoveRight.WasReleasedThisFrame())
                  || (chessActions.MoveBackward.WasReleasedThisFrame() && chessActions.MoveRight.IsPressed()))
            {
                if (!CalculateWantedDistance())
                {
                    _predictedPosition = transform.localPosition + new Vector3(dir * 1.25f, 0, -dir * 1.25f);
                    _played = false;
                }
            }

            if (chessActions.MoveBackward.IsPressed() && chessActions.MoveLeft.IsPressed())
            {
                if (CalculateWantedDistance())
                {
                    _predictedPosition = transform.localPosition + new Vector3(-dir * wantedDistance, 0, -dir * wantedDistance);
                    _played = false;
                }
            }
            else if ((chessActions.MoveBackward.IsPressed() && chessActions.MoveLeft.WasReleasedThisFrame())
                  || (chessActions.MoveBackward.WasReleasedThisFrame() && chessActions.MoveLeft.IsPressed()))
            {
                if (!CalculateWantedDistance())
                {
                    _predictedPosition = transform.localPosition + new Vector3(-dir * 1.25f, 0, -dir * 1.25f);
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