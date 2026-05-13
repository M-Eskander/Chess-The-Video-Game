using UnityEngine;
using MainClasses;

namespace chessPieces.Pieces
{
    public class Kings : ChessPieces
    {
        private Vector3 _predictedPosition;
        private bool _played =  true;
        public override void Update()
        {   
            base.Update();
            if (!isControllable) return;
            DiagonalMovement();
            PerpendicularMovement();
            HandleMovement();
        }
        private void HandleMovement()
        {
            if (isPredictedPositionOnBoard(_predictedPosition) &&
                !_played && CanKillPiecesInPredictedPosition(_predictedPosition))
            {
                transform.localPosition = _predictedPosition;
                _played = true;
                baseGameplay.HasPlayed(pieceColor);
            }
        }
        private void PerpendicularMovement()
        {

            if (chessActions.MoveForward.WasReleasedThisFrame() && !chessActions.MoveRight.IsPressed() && !chessActions.MoveLeft.IsPressed())
            {
                    _predictedPosition = transform.localPosition + new Vector3(0, 0, dir * 1.25f);
                    _played = false;
            }

            if (chessActions.MoveBackward.WasReleasedThisFrame() && !chessActions.MoveRight.IsPressed() && !chessActions.MoveLeft.IsPressed())
            {
                    _predictedPosition = transform.localPosition + new Vector3(0, 0, -dir * 1.25f);
                    _played = false;
            }
            
            if (chessActions.MoveRight.WasReleasedThisFrame() && !chessActions.MoveForward.IsPressed() && !chessActions.MoveBackward.IsPressed())
            {
                    _predictedPosition = transform.localPosition + new Vector3(dir * 1.25f, 0, 0);
                    _played = false;
            }
            
            if (chessActions.MoveLeft.WasReleasedThisFrame() && !chessActions.MoveForward.IsPressed() && !chessActions.MoveBackward.IsPressed())
            {
                    _predictedPosition = transform.localPosition + new Vector3(-dir * 1.25f, 0, 0);
                    _played = false;
            }
        }
        private void DiagonalMovement()
        {
            if ((chessActions.MoveForward.IsPressed() && chessActions.MoveRight.WasReleasedThisFrame())
                     || (chessActions.MoveForward.WasReleasedThisFrame() && chessActions.MoveRight.IsPressed()))
            {
                    _predictedPosition = transform.localPosition + new Vector3(dir * 1.25f, 0, dir * 1.25f);
                    _played = false;
            }
            
            if ((chessActions.MoveForward.IsPressed() && chessActions.MoveLeft.WasReleasedThisFrame())
                     || (chessActions.MoveForward.WasReleasedThisFrame() && chessActions.MoveLeft.IsPressed()))
            {
                    _predictedPosition = transform.localPosition + new Vector3(-dir * 1.25f, 0, dir * 1.25f);
                    _played = false;
            }
            
            if ((chessActions.MoveBackward.IsPressed() && chessActions.MoveRight.WasReleasedThisFrame())
                || (chessActions.MoveBackward.WasReleasedThisFrame() && chessActions.MoveRight.IsPressed()))
            {
                    _predictedPosition = transform.localPosition + new Vector3(dir * 1.25f, 0, -dir * 1.25f);
                    _played = false;
            }


            if ((chessActions.MoveBackward.IsPressed() && chessActions.MoveLeft.WasReleasedThisFrame())
                     || (chessActions.MoveBackward.WasReleasedThisFrame() && chessActions.MoveLeft.IsPressed()))
            {
                    _predictedPosition = transform.localPosition + new Vector3(-dir * 1.25f, 0, -dir * 1.25f);
                    _played = false;
            }
        }
    }
}