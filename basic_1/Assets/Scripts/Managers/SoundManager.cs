using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MP3 Player(���� ������ ����� ������) -> AudioSource(���� �����ġ)
// MP3 ����(� ������ ����� ������)   -> AudioClip(����)
// ��»�� -> AudioListener

public class SoundManager
{
    // Bgm, �׳����� �뵵�� �ϴ� 2���� �з�
    AudioSource[] _audioSources = new AudioSource[(int)Defines.Sound.MaxCount];

    // ĳ�� ����
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };            
            Object.DontDestroyOnLoad(root); // ���ο� ������ �Ѿ���� SoundManager ������Ʈ�� �ı����� ����

            string[] soundNames = System.Enum.GetNames(typeof(Defines.Sound));
            for(int i=0; i<soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform; // root�� ������ ���Եǰ� ����
            }

            _audioSources[(int)Defines.Sound.Bgm].loop = true;
        }
    }

    // ������ ���� ����(������ ��ġ�ϴ� ��θ� ������ ����)
    // �ش� ��ġ�� �ִ� ������ �����ͼ� ������ �ִ� ������ ȣ��
    public void Play(string path, Defines.Sound type = Defines.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    // ������ �ִ� ���� 
    // ������ ������ ���� �÷��̾� ��ġ�� �÷��� �÷���
    public void Play(AudioClip audioClip, Defines.Sound type = Defines.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null) return;

        if (type == Defines.Sound.Bgm)
        {              
            AudioSource audioSource = _audioSources[(int)Defines.Sound.Bgm];

            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {            
            AudioSource audioSource = _audioSources[(int)Defines.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    // ���� ����ɶ� ������ ������ �ִ� ���ݵ��� ��� ����� �޸� �뷮�� ���� ��ƸԱ� ������ �����ϰ� �����͸� ������ߵ�
    // ĳ�ø� ���� �۾��� ����
    public void Clear()
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

        _audioClips.Clear();        
    }

    AudioClip GetOrAddAudioClip(string path, Defines.Sound type = Defines.Sound.Effect)
    {
        // ���� ��θ� ã�� �κ� 
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;
        if (type == Defines.Sound.Bgm)
        {
            // ������ �������� �κ� 
            audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)            
                Debug.Log($"Audio Clip Missing! {path}");
        }
        else
        {
            // �Ź� �ش� �������� �� ������ ã�Ƽ� �������� �۾��� �������� ��츦 ����ؼ� ĳ�� �۾��� �ϵ��� ����
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                if (audioClip == null)
                    Debug.Log($"Audio Clip Missing! {path}");
                else                
                    _audioClips.Add(path, audioClip);                                                 
            }            
        }        
        
        return audioClip;
    }
}
