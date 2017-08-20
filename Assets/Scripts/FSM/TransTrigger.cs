/*
//状态机 感受器基类
*/
using UnityEngine;
using System.Collections;

public interface TransTrigger{


    FSMTriggerEnum TriggerID { get; set; }
    //条件判断
     Transition mTransResult {
        get;set;
    }
    object GetConditionData ();


}

public enum FSMTriggerEnum {
    None=-1
}