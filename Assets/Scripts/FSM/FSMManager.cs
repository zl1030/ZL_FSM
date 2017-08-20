using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;

public class FSMManager
{

    private static FSMManager _instance = null;

    private Dictionary<int, FSM> fsmMapByInstanceId = new Dictionary<int, FSM>();
    private Dictionary<FSMRoleCategory, List<int>> instanceIdListMapByRoleCategory = new Dictionary<FSMRoleCategory, List<int>>();

    public static FSMManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new FSMManager();
        }
        return _instance;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private static void CreateInstance()
    {
        if (_instance == null)
        {
            _instance = new FSMManager();
        }
    }

    private static bool IsRoleCategoryMatch(FSMRoleCategory category1, FSMRoleCategory category2)
    {
        return (category1 & category2) > 0;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public bool AddFSM(FSMRoleCategory category, int instanceId, FSM fsm)
    {
        if (fsmMapByInstanceId.ContainsKey(instanceId))
        {
            RemoveFSM(instanceId);
        }



        bool success = false;
        foreach (FSMRoleCategory c in Enum.GetValues(typeof(FSMRoleCategory)))
        {
            if (IsRoleCategoryMatch(c, category))
            {

                List<int> instanceIdList = null;
                if (!instanceIdListMapByRoleCategory.ContainsKey(c))
                {
                    instanceIdList = new List<int>();
                    instanceIdListMapByRoleCategory.Add(c, instanceIdList);
                }
                else
                {
                    instanceIdList = instanceIdListMapByRoleCategory[c];
                }
                instanceIdList.Add(instanceId);
                success = true;
            }
        }
        if (success)
        {
            fsmMapByInstanceId.Add(instanceId, fsm);
        }

        return success;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public bool RemoveFSM(FSMRoleCategory category)
    {
        if (instanceIdListMapByRoleCategory.ContainsKey(category))
        {
            foreach (FSMRoleCategory c in Enum.GetValues(typeof(FSMRoleCategory)))
            {
                if (IsRoleCategoryMatch(c, category))
                {
                    List<int> list = instanceIdListMapByRoleCategory[c];
                    for (int i = 0; i < list.Count; i++)
                    {
                        int instanceId = list[i];
                        RemoveFSM(instanceId);
                    }
                }
            }
            instanceIdListMapByRoleCategory.Remove(category);
            return true;
        }
        return false;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public bool RemoveFSM(int instanceId)
    {
        if (fsmMapByInstanceId.ContainsKey(instanceId))
        {
            foreach (FSMRoleCategory c in Enum.GetValues(typeof(FSMRoleCategory)))
            {
                if (!instanceIdListMapByRoleCategory.ContainsKey(c))
                    continue;

                List<int> instanceIdList = instanceIdListMapByRoleCategory[c];
                if (instanceIdList.Contains(instanceId))
                {
                    instanceIdList.Remove(instanceId);
                }
            }
            fsmMapByInstanceId.Remove(instanceId);
            return true;
        }
        return false;
    }

    public FSM GetFSM(int instanceId)
    {
        if (fsmMapByInstanceId.ContainsKey(instanceId))
        {
            return fsmMapByInstanceId[instanceId];
        }
        return null;
    }

    public List<FSM> GetFSM(FSMRoleCategory category)
    {
        if (instanceIdListMapByRoleCategory.ContainsKey(category))
        {
            List<FSM> fsmList = new List<FSM>();
            List<int> instanceIdList = instanceIdListMapByRoleCategory[category];
            for (int i = 0; i < instanceIdList.Count; i++)
            {
                int instanceId = instanceIdList[i];
                fsmList.Add(fsmMapByInstanceId[instanceId]);
            }
            return fsmList;
        }
        return null;
    }

    public void SendEvent(FSMRoleCategory category, FSMEvent fsmEvent)
    {
        if (instanceIdListMapByRoleCategory.ContainsKey(category))
        {
            List<int> instanceIdList = instanceIdListMapByRoleCategory[category];
            for (int i = 0; i < instanceIdList.Count; i++)
            {
                int instanceId = instanceIdList[i];
                SendEvent(instanceId, fsmEvent);
            }
        }
    }

    public void SendEvent(int instanceId, FSMEvent fsmEvent)
    {
        if (fsmMapByInstanceId.ContainsKey(instanceId))
        {
            FSM fsm = fsmMapByInstanceId[instanceId];
            fsm.AddEvent(fsmEvent);
        }
    }

    public void AddFSMComponent(FSMRoleCategory category, GameObject go)
    {
        if (go.GetComponent<FSM>() == null)
        {
            go.AddComponent<CubeCon>();
            FSM fsm = go.GetComponent<CubeCon>();
            fsm.FsmRoleCategory = category;
            fsm.instanceId = 2;
        }
    }

    public void RemoveFSMComponent(GameObject go)
    {
        if (go != null)
        {
            if (go.GetComponent<FSM>() != null)
            {
                if (RemoveFSM(go.GetComponent<FSM>().instanceId))
                {
                    GameObject.DestroyImmediate(go.GetComponent<FSM>());
                }
            }
        }

    }
}
