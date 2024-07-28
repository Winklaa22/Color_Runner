using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public PlayerControl Player { get; }

    public IdlePlayerState IdleState { get; }
    public RunningPlayerState RunningState { get; }
    public JumpPlayerState JumpState { get; }
    public SlidePlayerState SlideState { get; }
    public FallingPlayerState FallingState { get; }
    public DeathPlayerState DeathState { get; }

    public PlayerStateMachine(PlayerControl player)
    {
        Player = player;
        IdleState = new IdlePlayerState(this);
        RunningState = new RunningPlayerState(this);
        JumpState = new JumpPlayerState(this);
        SlideState = new SlidePlayerState(this);
        FallingState = new FallingPlayerState(this);
        DeathState = new DeathPlayerState(this);
    }
}
