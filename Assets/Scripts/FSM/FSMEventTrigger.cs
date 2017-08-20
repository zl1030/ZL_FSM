using UnityEngine;
using System.Collections;

public struct FSMEventTrigger
{

    public Transition transition;
    public int[] conditions;
    public FSMStateID nextFsmStateId;
}
