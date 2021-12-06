using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlRandomizer : MonoBehaviour
{
    public List<string> InputTags;
    public string inputTag;
    public int buttonSwapTime;
    public AudioClip GlitchSound;
    public float SoundVolume;
    public TextMeshProUGUI TimerText;
    int rand_int = 0;

    private void Start()
    {
        StartCoroutine(RandomizeTag());
    }

    IEnumerator RandomizeTag()
    {
        int time = buttonSwapTime;
        int temp = rand_int;
        rand_int = Random.Range(0, InputTags.Count-1);
        if (temp == rand_int) rand_int++;
        inputTag = InputTags[rand_int];
        for(int i=1; i<=buttonSwapTime; i++)
        {
            time--;
            TimerText.text = time.ToString();
            yield return new WaitForSeconds(1);
        }
        AudioSource.PlayClipAtPoint(GlitchSound, Camera.main.transform.position,SoundVolume);
        StartCoroutine(RandomizeTag());
    } 
}
