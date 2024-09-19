using UnityEngine;
using UnityEngine.Events;

public class Door : Interactable
{
    [Header("Message Properties")]
    [SerializeField] private string _doorLockedTextKey;
    [SerializeField] private string _doorOpenedTextKey;

    [Header("Audio Properties")]
    [SerializeField] private AudioClip _onDoorLockedClip;
    [SerializeField] private float _onDoorLockedVolume;
    [SerializeField] private AudioClip _onDoorOpenedClip;
    [SerializeField] private float _onDoorOpenedVolume;

    [Header("Key Requirements")]
    [SerializeField] private ItemName _keyName;
    [SerializeField] private bool _eventControlled;
    [SerializeField] private bool _noKeyRequired;

    [SerializeField] private UnityEvent _onDoorSlammed;
    [SerializeField] private UnityEvent _onDoorOpened;

    private bool _wasSlammed;

    private Animator _animator;

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();

        if (_eventControlled)
        {
            _canInteract = false;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (!_eventControlled) { }
        {
            if (IsPlayerInteracting() && !_messageShown && !GameManager.Instance.PlayerInventory.HasItem(_keyName))
            {
                if (!_noKeyRequired)
                {
                    string message = ((string)TextManager.GetText(_doorLockedTextKey)).Replace("{key}", _keyName.ToFormattedString());
                    MessageManager.Instance.ShowMessage(message, _messageType, _messageSpeed);
                    _messageShown = true;
                }
                SoundEffectsManager.Instance.PlaySoundEffect(_onDoorLockedClip, transform, _onDoorLockedVolume);

            }

            if (IsPlayerInteracting() && GameManager.Instance.PlayerInventory.TryConsumeKey(_keyName) || IsPlayerInteracting() && _noKeyRequired)
            {
                if (!_noKeyRequired)
                {
                    string message = ((string)TextManager.GetText(_doorOpenedTextKey)).Replace("{key}", _keyName.ToFormattedString());
                    MessageManager.Instance.ShowMessage(message, _messageType, _messageSpeed);
                }
                _canInteract = false;
                _animator.SetTrigger("DoorOpen");
                SoundEffectsManager.Instance.PlaySoundEffect(_onDoorOpenedClip, transform, _onDoorOpenedVolume);
                _onDoorOpened.Invoke();
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (!_wasSlammed)
        {
            SlamDoor();
            if (_onDoorSlammed != null)
            {
                _onDoorSlammed.Invoke();
            }
        }
    }

    public void OpenDoor()
    {
        _canInteract = false;
        _animator.SetTrigger("DoorOpen");
        SoundEffectsManager.Instance.PlaySoundEffect(_onDoorOpenedClip, transform, _onDoorOpenedVolume);
        _onDoorOpened.Invoke();
    }

    public void SlamDoor()
    {
        _animator.SetTrigger("DoorSlam");
        _canInteract = false;
        _wasSlammed = true;
    }
}
