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

    [SerializeField] private UnityEvent _onDoorSlammed;

    private bool _wasSlammed;

    private Animator _animator;

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (!_eventControlled) { }
        {
            if (IsPlayerInteracting() && !_messageShown && !GameManager.Instance.PlayerInventory.HasItem(_keyName))
            {
                string message = ((string)TextManager.GetText(_doorLockedTextKey)).Replace("{key}", _keyName.ToFormattedString());

                _messageShown = true;
                SoundEffectsManager.Instance.PlaySoundEffect(_onDoorLockedClip, transform, _onDoorLockedVolume);
                MessageManager.Instance.ShowMessage(message, _messageType, _messageSpeed);
            }

            if (IsPlayerInteracting() && GameManager.Instance.PlayerInventory.TryConsumeKey(_keyName))
            {
                string message = ((string)TextManager.GetText(_doorOpenedTextKey)).Replace("{key}", _keyName.ToFormattedString());

                _canInteract = false;
                _animator.SetTrigger("DoorOpen");
                SoundEffectsManager.Instance.PlaySoundEffect(_onDoorOpenedClip, transform, _onDoorOpenedVolume);
                MessageManager.Instance.ShowMessage(message, _messageType, _messageSpeed);
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
    }

    public void SlamDoor()
    {
        _animator.SetTrigger("DoorSlam");
        _canInteract = false;
        _wasSlammed = true;
    }
}
