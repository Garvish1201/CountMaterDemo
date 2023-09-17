using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Animator playerAnimator;
    public Transform crowdHolder;
    [SerializeField] private PlatformManager chunnkManagerInstance;
    [SerializeField] private CrowdSystem crowdSystemInstance;

    [Header (" Settings ")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float sideSpeed;
    [SerializeField] private bool afterReset;
    public bool underAttack;
    [SerializeField] private float afetResetPosition;

    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;

    private float roadWidth;

    [HideInInspector]
    public bool finished = false;

    #region Add Listeners To Action
    private void OnEnable()
    {
        GameManager.GameStateChange += OnGameStartChange;
        ResetPyramid.ResetRunners += OnResetPyramid;
        EnemyHolder.AttackCompleted += OnAttackComplete;
    }
    private void OnDisable()
    {
        GameManager.GameStateChange -= OnGameStartChange;
        ResetPyramid.ResetRunners -= OnResetPyramid;
        EnemyHolder.AttackCompleted -= OnAttackComplete;
    }
    #endregion

    void OnResetPyramid()
    {
        afterReset = true;
    }

    void OnAttackComplete()
    {
        underAttack = false;
        playerSpeed = 8;
        crowdSystemInstance.SetPlayerLocalPosition();
    }

    // effect player according to games's state
    void OnGameStartChange(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.Menu)
        {
            playerSpeed = 0;
            sideSpeed = 0;
        }
        else if (gameState == GameManager.GameState.InGame)
        {
            playerSpeed = 8;
            sideSpeed = 5;
            playerAnimator.SetTrigger("Run");
        }
        else if (gameState == GameManager.GameState.LevelCompleted) 
        {
            enabled = false;
        }
    }

    // get thw width of the road
    private void Start() => roadWidth = chunnkManagerInstance.chunkWidth;

    private void Update()
    { 
        // if players are under attack
        if (underAttack)
            playerSpeed = 3;
        
        // move forward
        MoveForward();

        // check for player count
        if(crowdHolder.childCount <= 0)
            enabled = false;

        // if the player has crossed the finish line sliding wont work
        if (finished) return;
        MoveSideWays();
    }

    void MoveForward()
    {
        transform.position += Vector3.forward * Time.deltaTime * playerSpeed;
        
        if (afterReset)
        {
            Vector3 _endPosition = transform.position;
            _endPosition.y = afetResetPosition;
            transform.position = _endPosition;
        }
    }    

    private void MoveSideWays ()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            // get position when clicked 
            clickedScreenPosition = Input.mousePosition;
            clickedPlayerPosition = transform.position;
        }
        else if (Input.GetMouseButton(0))
        {
            float difference = Input.mousePosition.x - clickedScreenPosition.x;
            
            // for reponsive screen
            difference /= Screen.width;
            difference *= sideSpeed;

            Vector3 _position = transform.position;
            _position.x = clickedPlayerPosition.x + difference;

            // adjust movement according to the radius
            _position.x = Mathf.Clamp(_position.x, -roadWidth / 2 + crowdSystemInstance.GetCrowdRadius(), roadWidth / 2 - crowdSystemInstance.GetCrowdRadius());

            transform.position = _position;
        }
    }

    // check for player crossing finish line
    public void CrossedFinishLine()
    {
        finished = true;
    }
}
