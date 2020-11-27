using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    bool gameOver = false;
    public int hp = 5, kills;
    public Canvas menu;
    Vector3 spawn = new Vector3(0,1,14);
    public GameObject[] enemies;
    //public GameObject[] hpUI;
    public Button[] hpBar;
    public AudioSource audioData;
    public AudioClip[] audioFiles;
    public Text killAmount;

    // Start is called before the first frame update
    void Start()
    {
        menu.gameObject.SetActive(false);
        InvokeRepeating("SpawnEnemy", 2, 2);
        audioData = GetComponent<AudioSource>();
        kills = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver == true)
        {
            menu.gameObject.SetActive(true);
        }
        killAmount.text = kills.ToString();
    }

    public void SetGameOver(bool x)
    {
        gameOver = x;
    }

    public void Replay()
    {
        SceneManager.LoadScene("FirstLevel", LoadSceneMode.Single);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    void SpawnEnemy()
    {
        int x = Random.Range(0, enemies.Length);
        float randomSpawn = Random.Range(-11, 11);
        spawn.x = randomSpawn;
        Instantiate(enemies[x], spawn, enemies[x].transform.rotation);
    }
    public void ShootSound()
    {
        audioData.PlayOneShot(audioFiles[0]);
    }
    public void DeathSound()
    {
        audioData.PlayOneShot(audioFiles[1]);
    }
    public void HitSound()
    {
        audioData.PlayOneShot(audioFiles[2]);
    }
    public void EnemyDeathSound()
    {
        audioData.PlayOneShot(audioFiles[3]);
    }
    public void EnemyKilled()
    {
        kills++;
    }
}
