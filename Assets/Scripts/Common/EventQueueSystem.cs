using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueueSystem 
{
    private static EventQueueSystem _Instance;

    public static EventQueueSystem Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new EventQueueSystem();
            }
            return _Instance;
        }
    }
    private Queue<Delegate> eventQueue = new Queue<Delegate>();
    private Queue<ParameterData> parameterDatas = new Queue<ParameterData>();
    public void AddEvent<T, F, G>(UnityEngine.Events.UnityAction<T, F, G> unityAction,ParameterData parameterData)
    {
        //Debug.Log("eventQueue.Count++" + eventQueue.Count);
        eventQueue.Enqueue(unityAction);
        //Debug.Log("eventQueue.Count++ed" + eventQueue.Count);
        parameterDatas.Enqueue(parameterData);
    }
    public void PlayerEvent()
    {
      
        if (eventQueue.Count > 0)
        {
            //Debug.Log("PlayerEventCount++ing++" + eventQueue.Count);
            if (ProduceQiPaoManager.Instance.produceQiPaolist.Count <= FaHuo.Maxcount)
            {
                var func = eventQueue.Dequeue();
                var datas = parameterDatas.Dequeue();
                func.DynamicInvoke(datas.name, datas.count, datas.produce);
            }
            //Debug.Log("PlayerEventCount++" + eventQueue.Count);
        }
    }
    public bool isHaveEvent()
    {
        if (eventQueue.Count > 0)
            return true;
        else
        {
            return false;
        }
    }

}
[Serializable] 
public class ParameterData {
  public  string name;
    public int count;
    public Produce produce;
    public ParameterData(string name,int count,Produce produce)
    {
        this.name = name;
        this.count = count;
        this.produce = produce;
    }

}
