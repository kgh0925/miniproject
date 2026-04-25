using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputReader : MonoBehaviour, IResettable
{
    private PlayerInput PlayerInput;
    #region InputAction
    private InputAction MoveAction;
    private InputAction JumpAction;
    private InputAction RunAction;
    private InputAction FlyAction;
    private InputAction GameMenuAction;
    #endregion
    [Header("Actions Name")]
    [SerializeField] private string MoveActionName;
    [SerializeField] private string JumpActionName;
    [SerializeField] private string RunActionName;
    [SerializeField] private string FlyActionName;
    [SerializeField] private string GameMenuActionName;

    public Vector2 MoveVector { get; private set; }
    public bool JumpPressedThisFrame { get; private set; }
    public bool RunIsPressed { get; private set; }
    public bool FlyIsPressed { get; private set; }
    public bool GameMenuPressedThisFrame { get; private set; }

    private bool IsRespawn;
    private void Awake()
    {
        if(PlayerInput == null)
        {
            PlayerInput = GetComponent<PlayerInput>();
        }
        ResolveActions();
    }
    private void OnEnable()
    {
        if (SavePointManager.Instance == null) return;
        SavePointManager.Instance.Register(this);
    }
    private void OnDisable()
    {
        if (SavePointManager.Instance == null) return;
        SavePointManager.Instance.Unregister(this);
    }
    private void Update()
    {
        if (IsRespawn)
        {
            MoveVector = Vector2.zero;
            JumpPressedThisFrame = false;
            GameMenuPressedThisFrame = false;
            FlyIsPressed = false;
            RunIsPressed = false;
        }
        else
        {
            MoveVector = MoveAction != null ? MoveAction.ReadValue<Vector2>() : Vector2.zero;
            JumpPressedThisFrame = JumpAction != null && JumpAction.WasPerformedThisFrame();
            RunIsPressed = RunAction != null && RunAction.IsPressed();
            FlyIsPressed = FlyAction != null && FlyAction.IsPressed();
            GameMenuPressedThisFrame = GameMenuAction != null && GameMenuAction.WasPerformedThisFrame();
        }

    }
    private void ResolveActions()
    {
        if(PlayerInput == null || PlayerInput.actions == null)
        {
            Debug.Log("PlayerInput == null || PlayerInput.actions == null");
            return;
        }

        MoveAction = FindAction(MoveActionName);
        RunAction = FindAction(RunActionName);
        FlyAction = FindAction(FlyActionName);
        JumpAction = FindAction(JumpActionName);
        GameMenuAction = FindAction(GameMenuActionName);

    }
    private InputAction FindAction(string actionName)
    {
        if (string.IsNullOrWhiteSpace(actionName))
        {
            Debug.Log("string.IsNullOrWhiteSpace(actionName)");
            return null;
        }
        InputAction m_Action = PlayerInput.actions.FindAction(actionName, false);
        if(m_Action == null)
        {
            Debug.Log("Not Found Action");
            return null;
        }
        return m_Action;
    }

    public void ResetToSavePoint()
    {
        StartCoroutine(RespawnSystem());
    }
    IEnumerator RespawnSystem()
    {
        IsRespawn = true;
        yield return new WaitForSecondsRealtime(1.5f);
        IsRespawn = false;
    }
}
