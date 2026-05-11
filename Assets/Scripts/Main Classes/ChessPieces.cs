using UnityEngine;

public class ChessPieces : MonoBehaviour
{
    private Controls chessControls;
    protected Controls.ChessActions chessActions;
    protected BaseChessGameplay gameplay;
    public enum PieceColor {White, Black}
    [HideInInspector] public PieceColor pieceColor;
    [HideInInspector] public bool isControllable;

    void Awake()
    {
        chessControls = new Controls();
        chessActions = chessControls.Chess;
        string[] requiredLayers = { "WhiteChess", "BlackChess"};
        foreach (string layer in requiredLayers)
        {
            if (LayerMask.NameToLayer(layer) == -1)
            {
                Debug.LogError($"Required layer '{layer}' is missing!");
                enabled = false;
                return;
            }
        }
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
    void Start()
    {
        gameplay = FindAnyObjectByType<BaseChessGameplay>();
        if (gameplay == null) Debug.LogError("Couldn't find gameplay");
        if(pieceColor == PieceColor.White) 
            gameObject.layer = LayerMask.NameToLayer("WhiteChess");
        else 
            gameObject.layer = LayerMask.NameToLayer("BlackChess");
    }
    public virtual void Update()
    {
        /*CheckForNearbyEnemies();
        HighlightPiece();*/
    }
    
    private void HighlightPiece() //not being used yet
    {
        if (isControllable)
            transform.GetChild(0).gameObject.SetActive(true);
        //add highlight possible positions
    }
    protected virtual void CheckForNearbyEnemies() //not being used
    {
        Vector3 center = transform.position;
        Vector3 halfExtents = new Vector3(1.2f, 1.2f, 1.2f);
        Collider[] hitColliders = Physics.OverlapBox(center, halfExtents, transform.rotation);

        foreach (Collider col in hitColliders)
        {
            if (col.gameObject == gameObject) continue;
            ChessPieces otherPiece = col.gameObject.GetComponent<ChessPieces>();
            if (otherPiece == null) continue;
            if (otherPiece.pieceColor == pieceColor) continue;
            Debug.Log("Danger is close: " + col.gameObject.name);
        }
    }
}