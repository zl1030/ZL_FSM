using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerRun : FSMState
{

    private PlayerMov mo;
    private Animator Anim;
    private GameObject mgo;
    public override void Act()
    {
        Debug.Log("PlayerRun.Act");
        mgo.transform.position +=
            new Vector3(0, 10 * Time.deltaTime, 0);
    }

    public PlayerRun(GameObject a)
    {
        mgo = a;
        stateID = FSMStateID.PlayerRun;
        //AddTransition(Transition.JoyStickRelease, FSMStateID.PlayerIdle);

        List<FSMEventTrigger> triggerList = new List<FSMEventTrigger>();

        FSMEventTrigger trigger = new FSMEventTrigger();
        trigger.transition = Transition.JoyStickRelease;
        trigger.nextFsmStateId = FSMStateID.PlayerIdle;
        triggerList.Add(trigger);

        triggerMapByTransition.Add(trigger.transition, triggerList);
    }

    public override void Begin(GameObject go, Animator anim, object data = null)
    {
        Debug.Log("PlayerRun.Begin");
    }

    //public override bool IsCanChangeState(object data = null)
    //{
    //    return true;
    //}

    public override void Release()
    {
        Debug.Log("PlayerRun.Release");
        mo = null;
    }
}
