/*
//状态机 状态基类
*/

using UnityEngine;
using System.Collections.Generic;

public abstract class FSMState
{

    public FSMState()
    {
        //读表加载本状态的 条件|状态 表
    }
    //字典，字典中的每一项都记录了一个   转换-状态 对的信息;
    protected Dictionary<Transition, FSMStateID> map = new Dictionary<Transition, FSMStateID>();

    protected Dictionary<Transition, List<FSMEventTrigger>> triggerMapByTransition = new Dictionary<Transition, List<FSMEventTrigger>>();

    //状态编号ID;
    protected FSMStateID stateID;
    public FSMStateID ID { get { return stateID; } }

    /// <summary>
    /// 改状态下所有可能触发的条件
    /// </summary>
    /// <returns></returns>
    public List<Transition> GetTransitionArray()
    {
        return new List<Transition>(map.Keys);
    }
    /// <summary>
    /// 添加转换条件
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="id"></param>
    public void AddTransition(Transition trans, FSMStateID id)
    {
        if (map.ContainsKey(trans))
        {
            Debug.LogError("FSMstate　ERROR:transition is already inside the map");
            return;
        }
        map.Add(trans, id);
        Debug.Log("Added:" + trans + "with ID:" + id);
    }
    /// <summary>
    /// 删除转换条件
    /// </summary>
    /// <param name="trans"></param>
    public void DeleteTransition(Transition trans)
    {
        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }

        Debug.LogError("FSMState ERROR:Transition passed was not on this State's List");
    }
    /// <summary>
    /// 查找转换条件
    /// </summary>
    /// <param name="trans"></param>
    /// <returns></returns>
    public FSMStateID GetOutPutState(Transition trans)
    {
        if (map.ContainsKey(trans))
            return map[trans];
        else
            return FSMStateID.None;
    }

    public FSMStateID HandleEvent(FSMEvent fsmEvent)
    {
        Debug.Log("HandleEvent:" + fsmEvent.EventType);
        if (triggerMapByTransition.ContainsKey(fsmEvent.EventType))
        {
            List<FSMEventTrigger> triggerList = triggerMapByTransition[fsmEvent.EventType];
            for (int i = 0; i < triggerList.Count; i++) {
                FSMEventTrigger trigger = triggerList[i];
                //解析条件trigger.conditions , fsmEvent.EventData
                return trigger.nextFsmStateId;
            }
        }
        return FSMStateID.None;
    }

    /// <summary>
    /// 状态机开始时的初始化
    /// </summary>
    public abstract void Begin(GameObject go, Animator anim, object data = null);
    /// <summary>
    /// 离开当前状态时调用
    /// </summary>
    /// <param name="player"></param>
    /// <param name="npc"></param>
    public abstract void Release();
    /// <summary>
    /// 定义了本状态的角色行为
    /// </summary>
    /// <param name="player"></param>
    /// <param name="npc"></param>
    public abstract void Act();
    /// <summary>
    /// 是否满足切换条件
    /// </summary>
    /// <returns></returns>
    //public abstract bool IsCanChangeState(object data = null);
}