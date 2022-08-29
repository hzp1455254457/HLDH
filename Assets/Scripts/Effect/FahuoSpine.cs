using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FahuoSpine : MonoBehaviour
{
    // Start is called before the first frame update
    public SkeletonGraphic fahuoAnim;
    public void Animation()
    {
        fahuoAnim.AnimationState.SetAnimation(0, "jingzhi", false).Complete += s => GameObjectPool.Instance.CollectObject(fahuoAnim.gameObject);
    }
}
