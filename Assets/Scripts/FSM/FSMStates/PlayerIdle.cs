using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerIdle : FSMState
{
    private Animator Anim;

    public PlayerIdle(Animator a)
    {
        Anim = a;
        stateID = FSMStateID.PlayerIdle;
        //AddTransition(Transition.JoyStickPress, FSMStateID.PlayerRun);

        List<FSMEventTrigger> triggerList = new List<FSMEventTrigger>();

        FSMEventTrigger trigger = new FSMEventTrigger();
        trigger.transition = Transition.JoyStickPress;
        trigger.nextFsmStateId = FSMStateID.PlayerRun;
        triggerList.Add(trigger);

        triggerMapByTransition.Add(trigger.transition, triggerList);
    }

    public override void Act()
    {

    }

    public override void Begin(GameObject go, Animator anim, object data = null)
    {
        // throw new NotImplementedException ();
    }

    //public override bool IsCanChangeState(object data = null)
    //{
    //    return true;
    //}

    public override void Release()
    {

    }
}
