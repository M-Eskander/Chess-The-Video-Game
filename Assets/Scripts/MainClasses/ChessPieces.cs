using UnityEngine;

namespace MainClasses
{
    public class ChessPieces : MonoBehaviour
    {
        private Controls _chessControls;
        protected Controls.ChessActions chessActions;
        protected Gameplay.BaseChessGameplay baseGameplay;
        public enum PieceColor {White,Black}
        public PieceColor pieceColor;
        [HideInInspector] public bool isControllable;
        protected float wantedDistance;
        protected float dir;
        
        void Awake()
        {
            _chessControls = new Controls();
            chessActions = _chessControls.Chess;
            string[] requiredLayers = { "WhiteChess", "BlackChess" };
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
            _chessControls.Dispose();
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
            baseGameplay = FindAnyObjectByType<Gameplay.BaseChessGameplay>();
            if (baseGameplay == null) Debug.LogError("Couldn't find gameplay");
            gameObject.layer = pieceColor == PieceColor.White? LayerMask.NameToLayer("WhiteChess") : LayerMask.NameToLayer("BlackChess");
        }
        public virtual void Update()
        {
            dir = pieceColor == PieceColor.White ? -1f : 1f;
            /*CheckForNearbyEnemies();
            HighlightPiece();*/
        }
        protected bool CalculateWantedDistance()
        {
            var numKeys = new[]
            {
                chessActions.Num2,
                chessActions.Num3,
                chessActions.Num4,
                chessActions.Num5,
                chessActions.Num6,
                chessActions.Num7
            };
            for (int i = 0; i < numKeys.Length; i++)
            {
                if (numKeys[i].WasPressedThisFrame())
                {
                    wantedDistance = (i + 2) * 1.25f;
                    return true;
                }
            }
            return false;
        }
        protected virtual bool CanKillPiecesInPredictedPosition(Vector3 predictedPosition)
        {
            Vector3 originPos = transform.localPosition;
            Vector3 directedPosition = predictedPosition - transform.localPosition;
            float distance = directedPosition.magnitude;
            
            if(pieceColor == PieceColor.White)
                originPos = new Vector3(8.75f - originPos.x, originPos.y, 8.75f - originPos.z);

            
            if (Physics.Raycast(originPos, dir * directedPosition, out RaycastHit hit, distance))
            {
                ChessPieces killablePiece = hit.collider.gameObject.GetComponent<ChessPieces>();
                if (killablePiece == null) return true;
                if (killablePiece.transform.localPosition != predictedPosition) return false;
                if (killablePiece.pieceColor != pieceColor)
                {
                    Destroy(killablePiece.gameObject);
                    return true;
                }
                else{return false;}
            } return true;
        }
        protected bool isPredictedPositionOnBoard(Vector3 predictedPosition)
        {
            float tolerance = 0.01f;
            return predictedPosition.x <= 8.75f + tolerance && predictedPosition.x >= -tolerance &&
                   predictedPosition.z <= 8.75f + tolerance && predictedPosition.z >= -tolerance;
        }

        /*private void HighlightPiece() //not being used yet
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
        }*/
    }
}