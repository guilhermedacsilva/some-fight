using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectFighter : MonoBehaviour {
    
    public string fighterName;

    public void play()
    {
        PlayerController.PLAYER_HERO_INDEX = HeroIndex.Warrior;
        SceneManager.LoadScene("Arena");
    }
    
}
