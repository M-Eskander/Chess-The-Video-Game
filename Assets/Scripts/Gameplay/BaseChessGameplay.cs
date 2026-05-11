using UnityEngine;

public class BaseChessGameplay : MonoBehaviour
{
    enum whoPlays {Black, White}
    private whoPlays Player;
    private Vector3 moveDir;
    private Controls chessControls;
    private Controls.ChessActions chessActions;
    private GameObject chessBoard;
    private ChessPieces lastPieceSelected;
    private bool QuitSelection; 
    private bool smthWasPressed;
    private bool movingPiece;
    
    void Awake()
    {
        chessControls = new Controls();
        chessActions = chessControls.Chess;
    }
    void OnDestroy()
    {
        chessControls.Dispose();                         
    }
    void OnEnable()
    {
        chessActions.Enable();                               
    }
    void OnDisable()
    {
        chessActions.Disable();                         
    }
    private void Start()
    {        
        chessBoard = GameObject.Find("ChessBoard");
        Player = whoPlays.White;
        ResetHighlightPos();
    }
    private void Update()
    {
        HandleChoosingInput();
        HighlightPieceSelection();
        RotateChessBoard();
    }
    
    Vector3 RoundDir(Vector3 dir)
    {
        if(Player == whoPlays.White) return -dir;
        else return dir;
    }
    private void ResetHighlightPos()
    {
        QuitSelection = true;
        if (lastPieceSelected != null)
            lastPieceSelected.transform.GetChild(0).gameObject.SetActive(false);
        if (Player == whoPlays.Black)
            transform.localPosition = new Vector3(3.75f, 0, 0);
        else
            transform.localPosition = new Vector3(3.75f, 0, 8.75f);
    }
    private void HandleChoosingInput()
    {
        if (chessActions.MoveRight.WasPressedThisFrame())
        {
            moveDir = RoundDir(transform.right);
            smthWasPressed = true;
        }
        if (chessActions.MoveLeft.WasPressedThisFrame())
        {
            moveDir = RoundDir(-transform.right);
            smthWasPressed = true;
        }
        if (chessActions.MoveForward.WasPressedThisFrame())
        {
            moveDir = RoundDir(transform.forward);
            smthWasPressed = true;
        }
        if (chessActions.MoveBackward.WasPressedThisFrame())
        {
            moveDir = RoundDir(-transform.forward);
            smthWasPressed = true;
        }
        if (chessActions.Exit.WasPressedThisFrame() || QuitSelection)
        {
            movingPiece = false;
            if (lastPieceSelected != null)
                lastPieceSelected.isControllable = false;
        }
        if (chessActions.Choose.WasPressedThisFrame())
        {
            lastPieceSelected.isControllable = true;
            movingPiece = true;
            QuitSelection = false;
        }
    }
    public void HasPlayed(ChessPieces.PieceColor whatMoved)
    {
        Player = whatMoved == ChessPieces.PieceColor.White ? whoPlays.Black : whoPlays.White;
        ResetHighlightPos();
    }
    private void RotateChessBoard()
    {
        Quaternion target = Player == whoPlays.White ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        chessBoard.transform.rotation = Quaternion.RotateTowards(chessBoard.transform.rotation, target, 100 * Time.deltaTime);
    }
    private void HighlightPieceSelection()
    {
        LayerMask chessMask = Player == whoPlays.White ? 
        LayerMask.GetMask("WhiteChess"): LayerMask.GetMask("BlackChess");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDir, out hit, 1.25f * 9, chessMask ))
        {
            if (movingPiece) return;
            ChessPieces piece = hit.collider.gameObject.GetComponent<ChessPieces>();
            if (piece != null && smthWasPressed)
            {
                if (lastPieceSelected != null)
                {
                    lastPieceSelected.transform.GetChild(0).gameObject.SetActive(false);
                }
                piece.transform.GetChild(0).gameObject.SetActive(true);
                transform.localPosition = piece.transform.localPosition;
                lastPieceSelected = piece;

                smthWasPressed = false;
            }
        }
    }

}
