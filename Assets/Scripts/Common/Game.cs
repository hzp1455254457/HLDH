using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;
public class Game:MonoBehaviour
{
    public List<SkeletonDataAsset> animatorList;
    public List<AudioSource> SoundPlayerList;
    public AudioSource MusicPlayer;
    public AudioSource PaoMaDengPlayer;
    private void Start()
    {
        ResourceManager.Instance.LoadGame(this);
        AudioManager.Instance.LoadAudioSource(this);
    }
}