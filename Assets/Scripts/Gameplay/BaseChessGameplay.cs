using UnityEngine;
using MainClasses;
namespace Gameplay
{
    public class BaseChessGameplay : MonoBehaviour
    {
        enum WhoPlays {Black,White}
        private WhoPlays _player;
        private Vector3 _moveDir;
        private Vector3 _hoverPos;
        private Controls _chessControls;
        private Controls.ChessActions _chessActions;
        private GameObject _chessBoard;
        private ChessPieces _lastPieceSelected;
        private bool _smthWasPressed;
        private bool _movingPiece;
        private bool _chooseActionPressed;
        private bool _quitSelection;
        
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
            //HighlightPieceSelection();
            RotateChessBoard();
        }
        
        private Vector3 RoundDir(Vector3 dir)
        {
            if (_player == WhoPlays.White) return -dir;
            else return dir;
        }
        private void ResetHighlightPos()
        {
            _movingPiece = false;
            if (_lastPieceSelected != null)
                _lastPieceSelected.transform.GetChild(0).gameObject.SetActive(false);

            transform.localPosition = _hoverPos =
                _player == WhoPlays.Black ? new Vector3(3.75f, 0, 0) : new Vector3(3.75f, 0, 8.75f);
            _chooseActionPressed = false;
            _quitSelection = true;
        }
        private void HandleChoosingInput()
        {
            if (!_movingPiece)
            {
                if (_chessActions.MoveRight.WasPressedThisFrame())
                {
                    _hoverPos = transform.localPosition + RoundDir(new Vector3(1.25f,0,0));
                }
                if (_chessActions.MoveLeft.WasPressedThisFrame())
                {
                    _hoverPos = transform.localPosition +RoundDir(new Vector3(-1.25f, 0, 0));
                }
                if (_chessActions.MoveForward.WasPressedThisFrame())
                {
                    _hoverPos = transform.localPosition + RoundDir(new Vector3(0, 0, 1.25f));
                }
                if (_chessActions.MoveBackward.WasPressedThisFrame())
                {
                    _hoverPos = transform.localPosition + RoundDir(new Vector3(0, 0, -1.25f));
                }
                if (_hoverPos.x >= 0 && _hoverPos.x <= 8.75f && _hoverPos.z >= 0 && _hoverPos.z <= 8.75f)
                {
                    transform.localPosition = _hoverPos;
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                if (_chessActions.Choose.WasPressedThisFrame())
                {
                    _chooseActionPressed = true;
                }
            }
            if (_chessActions.Exit.WasPressedThisFrame() || _quitSelection)
            {
                ResetHighlightPos();
                _movingPiece = false;
                if (_lastPieceSelected != null)
                    _lastPieceSelected.isControllable = false;
                _lastPieceSelected = null;
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
        private void OnTriggerStay(Collider other)
        {
            if (_movingPiece) return;
            ChessPieces piece = other.gameObject.GetComponent<ChessPieces>();
            if(piece.pieceColor.ToString() != _player.ToString())
            {_chooseActionPressed = false; return;}
            Debug.Log("Piece: " + piece);
            if (piece != null)
            {
                if (_lastPieceSelected != null)
                {
                    _lastPieceSelected.transform.GetChild(0).gameObject.SetActive(false);
                }

                if (_chooseActionPressed)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    piece.transform.GetChild(0).gameObject.SetActive(true);
                    transform.localPosition = piece.transform.localPosition;
                    _lastPieceSelected = piece;
                    _lastPieceSelected.isControllable = true;
                    _movingPiece = true;
                }
            }
        }
    }
}
