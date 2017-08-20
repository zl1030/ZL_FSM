using UnityEngine;
using System.Collections;

public class FSMEvent
{

    public FSMEvent(int instanceId, Transition type)
    {
        senderInstanceId = instanceId;
        eventType = type;
    }

    private int senderInstanceId;
    public int SenderInstanceId { get { return senderInstanceId; } }

    private Transition eventType;
    public Transition EventType { get { return eventType; } }

    public int[] EventData { get; set; }
}

