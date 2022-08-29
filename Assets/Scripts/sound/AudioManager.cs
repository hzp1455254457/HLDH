

using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    private static AudioManager _Instance;

    public static AudioManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new AudioManager();
            }
            return _Instance;
        }
    }
    public bool isOpenShake;
    public bool isOpenMusic;
    public bool isOpenSound;
    public bool isOpenOtherSound = true;//播放红包音效时控制其他音效；
    /// <summary>
    /// 跑马灯喇叭
    /// </summary>
    private AudioSource MusicPlayer;
    //音乐播放器
    private AudioSource PaomadengPlayer;
    //音效播放器
    private List<AudioSource> SoundPlayerList;

    private Dictionary<string, AudioClip> AudioSoundClipDictionary = new Dictionary<string, AudioClip>();

    //public void AddMusicPlayer(MusicAudio musicAudio)
    //   {
    //       MusicPlayer = musicAudio.musicPlayer;
    //   }

    public void LoadAudioSource(Game game)
    {
        if (PlayerPrefs.HasKey("isOpenMusic") == false)
        {
            isOpenMusic = true;

        }
        else
        {
            isOpenMusic = PlayerPrefs.GetInt("isOpenMusic") == 0 ? false : true;//保存

        }


        if (PlayerPrefs.HasKey("isOpenSound") == false)
        {
            isOpenSound = true;
        }
        else
        {
            isOpenSound = PlayerPrefs.GetInt("isOpenSound") == 0 ? false : true;
        }

        if (PlayerPrefs.HasKey("isOpenShake") == false)
        {
            isOpenShake = true;
        }
        else
        {
            isOpenShake = PlayerPrefs.GetInt("isOpenShake") == 0 ? false : true;
        }

        SoundPlayerList = game.SoundPlayerList;
        MusicPlayer = game.MusicPlayer;
        PaomadengPlayer = game.PaoMaDengPlayer;
        InitSetMouandSan();
        if (!MusicPlayer.isPlaying)
            SetMusic(isOpenMusic);
        Debug.Log(isOpenMusic);

    }
    public void PlayPaoMaSound(string name)
    {
        if (this.isOpenSound == false)
            return;
       
        if (AudioSoundClipDictionary.ContainsKey(name) == true)
        {
            AudioClip clip = AudioSoundClipDictionary[name];
           
            AudioSource audioSource = PaomadengPlayer;
            if (audioSource)
            {
                audioSource.clip = clip;

                audioSource.PlayOneShot(clip);

                audioSource.Play();
            }


        }
        else
        {
            string path = string.Format("sound/{0}", name);
            AudioClip clip = Resources.Load<AudioClip>(path);

            if (clip == null)
            {
                Debug.LogError("没有音效：" + path);
                return;
            }

            AudioSoundClipDictionary[name] = clip;
            AudioSource audioSource = PaomadengPlayer;
            if (audioSource)
            {
                audioSource.clip = clip;

                audioSource.PlayOneShot(clip);

            }
        }
    }
        //播放音效
        public AudioSource PlaySound(string name, bool isContinue = false)
    {
        if (!isOpenOtherSound) return null;
        if (this.isOpenSound == false)
            return null;

        if (AudioSoundClipDictionary.ContainsKey(name) == true)
        {
            AudioClip clip = AudioSoundClipDictionary[name];

            AudioSource audioSource = GetSound();
            if (audioSource)
            {
                audioSource.clip = clip;
                if (!isContinue)
                    audioSource.PlayOneShot(clip);
                else
                {
                    audioSource.loop = true;
                    audioSource.Play();
                }

            }
            return audioSource;

        }
        else
        {
            string path = string.Format("sound/{0}", name);
            AudioClip clip = Resources.Load<AudioClip>(path);

            if (clip == null)
            {
                Debug.LogError("没有音效：" + path);
                return null;
            }

            AudioSoundClipDictionary[name] = clip;
            AudioSource audioSource = GetSound();
            if (audioSource)
            {
                audioSource.clip = clip;
                if (!isContinue)
                    audioSource.PlayOneShot(clip);
                else
                {
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }
            return audioSource;
        }

    }
    public void StopSound(AudioSource audioSource)
    {
        audioSource.Stop();
    }
    AudioSource GetSound()
    {

        AudioSource audioSource = SoundPlayerList.Find(s => s.isPlaying == false);
        if (audioSource != null)
            return audioSource;
        else
        {
          audioSource=  new GameObject("Audio").AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
           
            SoundPlayerList.Add(audioSource);
        }
        return null;

    }




    public void PlayMusic(string name)
    {
        if (!isOpenOtherSound) return;
        if (this.isOpenMusic == false)
            return;

        if (AudioSoundClipDictionary.ContainsKey(name) == true)
        {
            AudioClip clip = AudioSoundClipDictionary[name];
            MusicPlayer.clip = clip;
            MusicPlayer.loop = true;
            MusicPlayer.Play();
        }
        else
        {
            string path = string.Format("sound/{0}", name);
            AudioClip clip = Resources.Load<AudioClip>(path);
            AudioSoundClipDictionary[name] = clip;
            MusicPlayer.clip = clip;
            MusicPlayer.loop = true;
            MusicPlayer.Play();
        }
    }
    public void StopMusic()
    {
        if (MusicPlayer.clip != null)
        {
            MusicPlayer.Stop();
        }

    }

    public void SetMusic(bool isOpen)
    {
        isOpenMusic = isOpen;
        PlayerPrefs.SetInt("isOpenMusic", isOpenMusic == true ? 1 : 0);
        if (isOpen)
        {
            this.PlayMusic("bgm");
        }
        else
        {
            MusicPlayer.Stop();
        }
        //Debug.Log("音乐存储");
    }

    public void SetSound(bool isOpen)
    {
        isOpenSound = isOpen;
        PlayerPrefs.SetInt("isOpenSound", isOpenSound == true ? 1 : 0);
        //Debug.Log("声音存储");
    }
    public void SetShake(bool isopen)
    {
        isOpenShake = isopen;
        PlayerPrefs.SetInt("isOpenShake", isOpenShake == true ? 1 : 0);
        //Debug.Log("震动存储");
    }
    public void OpenShake(bool isopen)
    {
        isOpenShake = isopen;
        PlayerPrefs.SetInt("isOpenShake", isOpenShake == true ? 1 : 0);
        //Debug.Log("震动存储");
    }

    public void Shake(HapticTypes hapticTypes)
    {
        if (isOpenShake == false)
        {
            return;
        }
        //Handheld.Vibrate();
        MMVibrationManager.Haptic(hapticTypes);
    }
    public IEnumerator PlayShake(int count)
    {
        if (this.isOpenShake == false)
        { yield break; }
        int i = 0;
        while (true)
        {
            i++;
            Debug.Log(string.Format("震动次数{0}", i));
            MMVibrationManager.Vibrate();
            yield return new WaitForSeconds(0.1f);
            if (i >= count) { break; }
        }

    }

    public IEnumerator PlayShakes(HapticTypes hapticTypes, int count)
    {
        if (this.isOpenShake == false)
        { yield break; }
        int i = 0;
        while (true)
        {
            i++;
            Debug.Log(string.Format("震动次数{0}", i));
            MMVibrationManager.Haptic(hapticTypes);
            yield return new WaitForSeconds(0.1f);
            if (i >= count) { break; }
        }

    }
    public void InitSetMouandSan()
    {
        //if (PlayerPrefs.GetString("Isplaymusic") == "true")
        //{
        //    AudioManager.Instance.PlayMusic("bgm");
        //    AudioManager.Instance.SetMusic(true);
        //}
        //else
        //{
        //    AudioManager.Instance.SetMusic(false);
        //}
        //if (PlayerPrefs.GetString("IsOpenshake") == "true")
        //{
        //    AudioManager.Instance.OpenShake(true);
        //}
        //else
        //{
        //    AudioManager.Instance.OpenShake(false);
        //}

    }

    //public void PlayMslSound(int type)
    //{
    //    if (timer > 10)
    //    {
    //        if (type == 0)
    //        {
    //            PlaySound(string.Format("hl0{0}",Random.Range(1,4)));
    //        }
    //        else if (type == 1)
    //        {
    //            PlaySound(string.Format("hs0{0}", Random.Range(1, 4)));
    //        }
    //        else if (type == 2)
    //        {
    //            PlaySound(string.Format("ll0{0}", Random.Range(1, 4)));
    //        }
    //        else if (type == 3)
    //        {
    //            PlaySound(string.Format("ls0{0}", Random.Range(1, 4)));
    //        }
    //        timer = 0;
    //    }

    //    timer += 1;

    //    if (type == 5)
    //    {
    //        PlaySound(string.Format("hl0{0}", Random.Range(1, 4)));
    //    }
    //}
}
