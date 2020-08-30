using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class AudioMan : MonoBehaviour
{

    public static AudioMan a_Instance;
    int songNumber = 0;
    #region Debug Menu

    #endregion

    string _currentSong;
    #region AudioMixer
    [SerializeField] AudioMixer MainMix;
    [SerializeField] AudioMixerGroup musicGroup;
    [SerializeField] AudioMixerGroup sfxGroup;
    #endregion

    #region AudioSources
    [SerializeField] AudioSource levelMusic;
    [SerializeField] AudioSource sfxSource;
    #endregion

    #region Audio Clips
    public SfxClip[] sfx;
    public LevelMusic[] lvl_Music;
    #endregion

    readonly float levelMusicDelay = 0.4f;
    
    private void Awake()
    {
        //levelMusic = GameObject.FindGameObjectWithTag("m_Level").GetComponent<AudioSource>();
        //sfxSource = GameObject.FindGameObjectWithTag("sfx").GetComponent<AudioSource>();
        if (a_Instance == null)
            a_Instance = this;
        else if (a_Instance != this)
            Destroy(a_Instance);
    }
    private void OnEnable()
    {
        if(SceneManager.GetActiveScene().name == "MenuScene")
        {
            StartCoroutine(PlayMusic("Menu"));

        }
        else if (SceneManager.GetActiveScene().name == "PuzzleOne")
        {
            StartCoroutine(PlayMusic("LevelOne"));

        }
        else if (SceneManager.GetActiveScene().name == "PuzzleTwo")
        {
            StartCoroutine(PlayMusic("LevelTwo"));

        }
        else if (SceneManager.GetActiveScene().name == "PuzzleX")
        {
            StartCoroutine(PlayMusic("LevelThree"));

        }
        else
        StartCoroutine(PlayMusic("Random"));
        
    }
    private void Update()
    {

    }

    public IEnumerator PlayMusic(string clipName)
    {
        StopMusic();
        yield return new WaitForSeconds(levelMusicDelay);
        foreach (LevelMusic song in lvl_Music) { if (song.name == clipName) levelMusic.clip = song.clip; levelMusic.Play(); }
        levelMusic.Play();
        levelMusic.loop = true;
    }

    public void NextSong()
    {
        if (songNumber >= lvl_Music.Length) songNumber = 0;
        StopMusic();
        levelMusic.PlayOneShot(lvl_Music[songNumber].clip);
        songNumber += 1;
    }

    public void PlayOneShotByName(string sound)
    { foreach (SfxClip clip in sfx) if (clip.name == sound) sfxSource.PlayOneShot(clip.clip); }

    void FadeMusic()
    {
        //   levelMusic.
    }

    public void StopMusic()
    {
        levelMusic.Stop();
    }

    [System.Serializable]
    public struct BattleType { public string name; public AudioClip clip; }

    [System.Serializable]
    public struct LevelMusic { public string name; public AudioClip clip; }

    [System.Serializable]
    public struct SfxClip { public string name; public AudioClip clip; }
}