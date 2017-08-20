using UnityEngine;
using System.Collections;
using System;

public class PlayerMov : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameObject go = GameObject.Find("Sphere");
            if (go != null)
            {
                FSMManager.GetInstance().AddFSMComponent(FSMRoleCategory.MONSTER, go);
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject go = GameObject.Find("Sphere");
            if (go != null)
            {
                FSMManager.GetInstance().RemoveFSMComponent(go);
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {

            FSMEvent fsmEvent = new FSMEvent(this.GetInstanceID(), Transition.JoyStickPress);
            FSMManager.GetInstance().SendEvent(FSMRoleCategory.MY_PLAYER, fsmEvent);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            FSMEvent fsmEvent = new FSMEvent(this.GetInstanceID(), Transition.JoyStickRelease);
            FSMManager.GetInstance().SendEvent(FSMRoleCategory.MY_PLAYER, fsmEvent);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {

            FSMEvent fsmEvent = new FSMEvent(this.GetInstanceID(), Transition.JoyStickPress);
            FSMManager.GetInstance().SendEvent(FSMRoleCategory.MONSTER, fsmEvent);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            FSMEvent fsmEvent = new FSMEvent(this.GetInstanceID(), Transition.JoyStickRelease);
            FSMManager.GetInstance().SendEvent(FSMRoleCategory.MONSTER, fsmEvent);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {

            FSMEvent fsmEvent = new FSMEvent(this.GetInstanceID(), Transition.JoyStickPress);
            FSMManager.GetInstance().SendEvent(FSMRoleCategory.ALL, fsmEvent);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            FSMEvent fsmEvent = new FSMEvent(this.GetInstanceID(), Transition.JoyStickRelease);
            FSMManager.GetInstance().SendEvent(FSMRoleCategory.ALL, fsmEvent);
        }
    }


}
