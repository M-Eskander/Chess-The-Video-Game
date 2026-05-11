using UnityEngine;
using MainClasses;
namespace Gameplay
{
    public class BaseChessGameplay : MonoBehaviour
    {
        enum WhoPlays {Black,White}
        private WhoPlays _player;
        private Vector3 _moveDir;
        private Controls _chessControls;
        private Controls.ChessActions _chessActions;
        private GameObject _chessBoard;
        private ChessPieces _lastPieceSelected;
        private bool _quitSelection;
        private bool _smthWasPressed;
        private bool _movingPiece;

        void Awake()
        {
            _chessControls = new Controls();
            _chessActions = _chessControls.Chess;
        }
        void OnDestroy()
        {
            _chessControls.Dispose();
        }
        void OnEnable()
        {
            _chessActions.Enable();
        }
        void OnDisable()
        {
            _chessActions.Disable();
        }
        void Start()
        {
            _chessBoard = GameObject.Find("ChessBoard");
            _player = WhoPlays.White;
            ResetHighlightPos();
        }
        void Update()
        {
            HandleChoosingInput();
            HighlightPieceSelection();
            RotateChessBoard();
        }
        
        private Vector3 RoundDir(Vector3 dir)
        {
            if (_player == WhoPlays.White) return -dir;
            else return dir;
        }
        private void ResetHighlightPos()
        {
            _quitSelection = true;
            if (_lastPieceSelected != null)
                _lastPieceSelected.transform.GetChild(0).gameObject.SetActive(false);

            transform.localPosition =
                _player == WhoPlays.Black ? new Vector3(3.75f, 0, 0) : new Vector3(3.75f, 0, 8.75f);
        }
        private void HandleChoosingInput()
        {
            if (_chessActions.MoveRight.WasPressedThisFrame())
            {
                _moveDir = RoundDir(transform.right);
                _smthWasPressed = true;
            }

            if (_chessActions.MoveLeft.WasPressedThisFrame())
            {
                _moveDir = RoundDir(-transform.right);
                _smthWasPressed = true;
            }

            if (_chessActions.MoveForward.WasPressedThisFrame())
            {
                _moveDir = RoundDir(transform.forward);
                _smthWasPressed = true;
            }

            if (_chessActions.MoveBackward.WasPressedThisFrame())
            {
                _moveDir = RoundDir(-transform.forward);
                _smthWasPressed = true;
            }

            if (_chessActions.Exit.WasPressedThisFrame() || _quitSelection)
            {
                _movingPiece = false;
                if (_lastPieceSelected != null)
                    _lastPieceSelected.isControllable = false;
            }

            if (_chessActions.Choose.WasPressedThisFrame())
            {
                _lastPieceSelected.isControllable = true;
                _movingPiece = true;
                _quitSelection = false;
            }
        }
        public void HasPlayed(ChessPieces.PieceColor whatMoved)
        {
            _player = whatMoved == ChessPieces.PieceColor.White ? WhoPlays.Black : WhoPlays.White;
            ResetHighlightPos();
        }
        private void RotateChessBoard()
        {
            Quaternion target = _player == WhoPlays.White ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
            _chessBoard.transform.rotation =
                Quaternion.RotateTowards(_chessBoard.transform.rotation, target, 100 * Time.deltaTime);
        }
        private void HighlightPieceSelection()
        {
            LayerMask chessMask = _player == WhoPlays.White
                ? LayerMask.GetMask("WhiteChess")
                : LayerMask.GetMask("BlackChess");
            if (Physics.Raycast(transform.position, _moveDir, out RaycastHit hit, 1.25f * 9, chessMask))
            {
                if (_movingPiece) return;
                ChessPieces piece = hit.collider.gameObject.GetComponent<ChessPieces>();
                if (piece != null && _smthWasPressed)
                {
                    if (_lastPieceSelected != null)
                    {
                        _lastPieceSelected.transform.GetChild(0).gameObject.SetActive(false);
                    }

                    piece.transform.GetChild(0).gameObject.SetActive(true);
                    transform.localPosition = piece.transform.localPosition;
                    _lastPieceSelected = piece;

                    _smthWasPressed = false;
                }
            }
        }
    }
}
