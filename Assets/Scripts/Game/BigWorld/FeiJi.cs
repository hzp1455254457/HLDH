using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeiJi : Transportation
{
    public override void Start()
    {
        base.Start();
        //if (!PlayerDate.Instance.Datas3D.IsJieSuoFeiJi)
        //{
        //    return;
        //}
        //TODO TEST
        if (BigWorldData.IsJieSuoFeiJi)
        {
            return;
        }
    }
    public override void Move()
    {
        base.Move();
    }

    public override void OnSceneLoaded()
    {
        throw new System.NotImplementedException();
    }

    public override void Reset()
    {
        base.Reset();
    }

    public override void Stop()
    {
        base.Stop();
    }
    
}
