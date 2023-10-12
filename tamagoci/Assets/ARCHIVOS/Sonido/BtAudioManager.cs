using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioClip configMusic;

    public Button[] buttons;
    public AudioClip[] audioClips;

    private AudioSource audioSource;
    private AudioSource backgroundMusicSource;
    private bool isInConfigMenu = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;

        //BackGround Music
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Captura el valor de 'i' para que funcione en el Listener
            buttons[i].onClick.AddListener(() => PlayAudio(index));
        }
    }

    private void PlayAudio(int index)
    {
        if (index >= 0 && index < audioClips.Length)
        {
            if (!isInConfigMenu)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
                audioSource.clip = audioClips[index];
                audioSource.Play();
            }
        }
    }

    public void EnterConfigMenu()
    {
        backgroundMusicSource.volume = 0.3f; // Baja el volumen de la música de fondo
        backgroundMusicSource.Pause(); // Pausa la música de fondo

        // Reproduce la música de configuración
        audioSource.clip = configMusic;
        audioSource.Play();

        isInConfigMenu = true;
    }

    public void ExitConfigMenu()
    {
        audioSource.Stop();

        // Continúa reproduciendo la música de fondo
        backgroundMusicSource.Play();

        isInConfigMenu = false;
    }

}

