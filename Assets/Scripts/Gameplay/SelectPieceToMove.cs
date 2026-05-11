using UnityEngine;
using UnityEngine.InputSystem;

public class SelectPieceToMove : MonoBehaviour
{
    public enum whoPlays
    {
        Black, White
    }
    public whoPlays Player;
    public InputAction moveRightAction;
    public InputAction moveLeftAction;
    public InputAction moveUpAction;
    public InputAction moveDownAction;
    public InputAction select;
    public InputAction quitSelection;
    public bool QuitSelection = false; 
    public ChessPieces lastPieceSelected;
    public Vector3 moveDir;
    bool smthWasPressed = false;
    bool movingPiece = false;
    Vector3 RoundDir(Vector3 dir)
    {
        return new Vector3(Mathf.Round(dir.x), Mathf.Round(dir.y), Mathf.Round(dir.z));
    }
    
    private void OnEnable()
    {
        moveRightAction.Enable();
        moveLeftAction.Enable();
        moveUpAction.Enable();
        moveDownAction.Enable();
        select.Enable();
        quitSelection.Enable();
    }

    private void Start()
    {
        if (Player == whoPlays.Black)
            transform.localPosition = new Vector3(3.75f, 0, 0);
        else
            transform.localPosition = new Vector3(3.75f, 0, 8.75f);
    }

    public void resetHighlightPos()
    {
        if (Player == whoPlays.Black)
            transform.localPosition = new Vector3(3.75f, 0, 0);
        else
            transform.localPosition = new Vector3(3.75f, 0, 8.75f);
    }
    
    private void Update()
    {
        if (moveRightAction.WasPressedThisFrame())
        {
            moveDir = RoundDir(transform.right);
            smthWasPressed = true;
        }
        if (moveLeftAction.WasPressedThisFrame())
        {
            moveDir = RoundDir(-transform.right);
            smthWasPressed = true;
        }
        if (moveUpAction.WasPressedThisFrame())
        {
            moveDir = RoundDir(transform.forward);
            smthWasPressed = true;
        }
        if (moveDownAction.WasPressedThisFrame())
        {
            moveDir = RoundDir(-transform.forward);
            smthWasPressed = true;
        }
        if (quitSelection.WasPressedThisFrame() || QuitSelection)
        {
            movingPiece = false;
            lastPieceSelected.isControllable = false;
        }
        if (select.WasPressedThisFrame())
        {
            Debug.Log(lastPieceSelected.gameObject.name + " was selected");
            lastPieceSelected.isControllable = true;
            movingPiece = true;
            QuitSelection = false;
        }

        if (Player == whoPlays.White)
        {
            moveDir = -moveDir;
        }


        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDir, out hit, 1.25f * 9))
        {
            if (movingPiece) return;
            ChessPieces piece = hit.collider.gameObject.GetComponent<ChessPieces>();
            if (piece != null && smthWasPressed)
            {
                if(piece.pieceColor.ToString() != Player.ToString()) return;
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
