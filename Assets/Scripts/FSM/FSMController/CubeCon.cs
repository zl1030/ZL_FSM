using UnityEngine;
using System.Collections;

public class CubeCon : FSM
{

    protected override void Initialize()
    {

        base.Initialize();


        AddFSMState(new PlayerIdle(null));
        AddFSMState(new PlayerRun(gameObject));

    }
}
