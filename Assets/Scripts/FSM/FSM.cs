/*
//状态机基类
*/
using UnityEngine;
using System.Collections.Generic;

public class FSM : MonoBehaviour
{
    //fsm中所有状态的列表
    private Dictionary<FSMStateID, FSMState> fsmStateMap;
    //当前人物的所有触发器
    //private List<TransTrigger> transTriggers;

    private List<FSMEvent> eventList = new List<FSMEvent>();

    public FSMRoleCategory FsmRoleCategory;
    public int instanceId;

    //当前状态编号
    private FSMStateID currentStateID;
    public FSMStateID CurrentStateID { get { return currentStateID; } }

    //当前状态;
    //private FSMState currentState;
    public FSMState CurrentState { get { return fsmStateMap[currentStateID]; } }

    private Animator animator;

    public FSM()
    {
        fsmStateMap = new Dictionary<FSMStateID, FSMState>();
        //transTriggers = new List<TransTrigger>();

    }

    /// <summary>
    /// 初始化
    /// </summary>
    protected virtual void Initialize()
    {

    }
    /// <summary>
    /// 每帧执行
    /// </summary>
    protected virtual void FSMUpdate() { }
    /// <summary>
    /// 固定时间执行
    /// </summary>
    protected virtual void FSMFixedUpdate() { }
    void Start()
    {

        FSMManager.GetInstance().AddFSM(FsmRoleCategory, this.instanceId, this);

        animator = GetComponent<Animator>();
        //读表加载状态


        //根据状态绑定触发器
        List<Transition> AllTransitions = new List<Transition>();
        foreach (FSMState state in fsmStateMap.Values)
        {
            List<Transition> tmp = state.GetTransitionArray();
            for (int j = 0; j < tmp.Count; j++)
            {
                if (!AllTransitions.Contains(tmp[j]))
                {
                    AllTransitions.Add(tmp[j]);
                }
            }
        }
        Initialize();


    }

    public void AddEvent(FSMEvent fsmEvent)
    {
        eventList.Add(fsmEvent);
    }

    void Update()
    {


        FSMUpdate();


        CurrentState.Act();

    }

    void FixedUpdate()
    {
        FSMFixedUpdate();

        if (eventList.Count > 0)
        {
            for (int i = 0; i < eventList.Count; i++)
            {
                FSMEvent fsmEvent = eventList[i];
                FSMStateID stateId = CurrentState.HandleEvent(fsmEvent);
                if (stateId != FSMStateID.None && stateId != CurrentStateID)
                {
                    CurrentState.Release();
                    currentStateID = stateId;
                    CurrentState.Begin(gameObject, animator);
                    break;
                }
            }
        }
        eventList.Clear();
    }
    /// <summary>
    /// 添加新状态
    /// </summary>
    /// <param name="fsmState"></param>
    public void AddFSMState(FSMState fsmState)
    {
        if (fsmState == null)
        {
            Debug.LogError("FSM ERROE:Null reference is not allowed");
            return;
        }

        if (fsmStateMap.Count == 0)
        {
            fsmStateMap.Add(fsmState.ID, fsmState);

            currentStateID = fsmState.ID;
            CurrentState.Begin(gameObject, animator);
            return;
        }

        if (fsmStateMap.ContainsKey(fsmState.ID))
        {
            Debug.LogError("FSM ERROR: Trying to add a state that was already inside the list");
            return;
        }
        fsmStateMap.Add(fsmState.ID, fsmState);
    }

    /// <summary>
    /// 删除状态
    /// </summary>
    /// <param name="fsmstate"></param>
    public void DeleteState(FSMStateID fsmstate)
    {
        if (CurrentState.ID == fsmstate)
        {
            return;
        }
        if (!fsmStateMap.ContainsKey(fsmstate))
        {
            Debug.LogError("FSM ERROR:The state passed was not on the list.Impossible to delete it");
            return;
        }

        fsmStateMap.Remove(fsmstate);
    }
    /// <summary>
    /// 通过条件添加触发器
    /// </summary>
    /// <param name="t"></param>
    public void HandleTriggers(Transition t)
    {
        //读表取到对应的触发器id,以及参数
        //TransTrigger tt;
        //if (!transtriggers.contains (tt)) {
        //    transtriggers.add (tt);
        //}
    }

    /// <summary>
    /// 根据当前状态进行转换
    /// </summary>
    /// <param name="trans"></param>
    public bool PerformTransition(TransTrigger trans)
    {
        //FSMStateID id = CurrentState.GetOutPutState(trans.mTransResult);
        ////有下一个状态
        //if (id != FSMStateID.None)
        //{
        //    if (CurrentState.IsCanChangeState(trans.GetConditionData()))
        //    {
        //        CurrentState.Release();
        //        currentStateID = id;
        //        CurrentState.Begin(gameObject, animator);
        //        return true;
        //    }
        //    else
        //    {
        //        Debug.LogWarning("CurrentState hasn't" + trans.mTransResult.ToString());
        //    }
        //}
        //else
        //{
        //    Debug.LogWarning("CurrentState hasn't" + trans.mTransResult.ToString());
        //}
        return false;
    }

}
