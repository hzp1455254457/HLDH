using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunChuan : Transportation
{
    public override void Start()
    {
        base.Start();
        //if (!PlayerDate.Instance.Datas3D.IsJieSuoLunChuan)
        //{
        //    return;
        //}
        //TODO TEST
        if (!BigWorldData.IsJieSuoLunChuan)
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
