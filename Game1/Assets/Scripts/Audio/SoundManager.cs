using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioClip m_PlayerAttack;
    public AudioSource m_AudioSource;
    public InputManager InputManagerTest;

    // Use this for initialization
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        InputManagerTest.GetComponent<InputManager>();
    }

    void TestScript()
    {
        if(InputManagerTest.PlayerAttack1())
        {
            Debug.Log("Attacking1");
        }
        
        //InputManagerTest.PlayerAttack1();
    }
}
