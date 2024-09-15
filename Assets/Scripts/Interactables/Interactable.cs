using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected MessageType _messageType;
    [SerializeField] protected float _messageSpeed;

    protected InputAction _interactInput;
    protected bool _isPlayerNearby;
    protected bool _messageShown;

    protected virtual void Awake()
    {
        _isPlayerNearby = false;
        _interactInput = new PlayerInput().Player.Interact;
    }

    protected virtual void Start()
    {

    }

    protected virtual void OnEnable()
    {
        _interactInput.Enable();
    }

    protected virtual void OnDisable()
    {
        _interactInput.Disable();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _isPlayerNearby = true;
            _messageShown = false;
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _isPlayerNearby = false;
            MessageManager.Instance.ShowMessageBox(false, _messageType);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _isPlayerNearby = true;
            _messageShown = false;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _isPlayerNearby = false;
            MessageManager.Instance.ShowMessageBox(false, _messageType);
        }
    }

    protected virtual void Update()
    {

    }

    protected bool IsPlayerInteracting()
    {
        return _isPlayerNearby && _interactInput.WasPressedThisFrame();
    }
}
