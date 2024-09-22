using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum StalkerScriptedEventCompleteResponse
{
    Chase,
    Reset,
    Hold
}

[CreateAssetMenu(fileName = "NewScriptedEventSO", menuName = "ScriptableObjects/ScriptedEventSO")]
public class ScriptedEventSO : ScriptableObject
{
    public bool IsPlayerMovementDisabled;

    [Space(20)]

    public bool ShouldControlPlayerMovement;
    public Vector2 PlayerStart;
    public Vector2 PlayerEnd;
    public float PlayerSpeed;

    [Space(20)]

    public bool ShouldControlStalkerMovement;
    public Vector2 StalkerStart;
    public Vector2 StalkerEnd;
    public float StalkerSpeed;

    [Space(20)]

    public StalkerScriptedEventCompleteResponse StalkerOnComplete;
    public Vector2 StalkerHoldPosition;

    [Space(20)]

    public bool ShouldScreenShake;

    [Space(20)]

    public bool ShouldDestroyTiles;
    public bool ShouldPlayParticleSystemOnDestroy;
    public Vector3Int[] DestroyTiles;

    [Space(20)]

    public bool ShouldControlSirens;
    public bool SetSirensActive;
    public string[] AffectedSirens;

    [Space(20)]

    public bool HasDelayOnComplete;
    public float DelayTime;

    [Space(20)]

    public bool ShouldControlDoors;
    public bool SetDoorsOpen;
    public string[] AffectedDoors;

    [Space(20)]
    public bool ShouldStopBackgroundMusic;
    public bool ShouldChangeBackgroundMusic;
    public bool ShouldChangeBackgroundMusicOnComplete;
    public AudioClip NewBackgroundMusic;

    [Space(20)]

    public ScriptedEventSO LinkedEvent;

    [HideInInspector]
    public UnityEvent<ScriptedEventSO> Event;

    private void OnEnable()
    {
        if (Event == null)
        {
            Event = new UnityEvent<ScriptedEventSO>();
        }
    }

    private void OnDisable()
    {
        if (Event != null )
        {
            Event.RemoveAllListeners();
        }
    }

    public void StartEvent()
    {
        Event.Invoke(this);
    }
}
