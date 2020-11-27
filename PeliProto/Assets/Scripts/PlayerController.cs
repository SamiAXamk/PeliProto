using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject gameManager;
    private Animator anim;
    public float speed = 1;
    //public int hp = 3;
    public GameObject projectile, gunMuzzle;
    Vector3 aimTarget;
    GameManagerScript managerScript;
    public ParticleSystem playerDeathParticles;
    public int particleRate, particleSize;

    void Start()
    {
        anim = GetComponent<Animator>();
        managerScript = gameManager.GetComponent<GameManagerScript>();
        var em = playerDeathParticles.emission;
        em.rateOverTime = particleRate;
        //playerDeathParticles.startSize = particleSize;
    }

    void Update()
    {
        aimTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimTarget.y = transform.position.y;

        transform.LookAt(aimTarget, Vector3.up);

        // Move up
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Run", true);
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        }
        else
        {
            anim.SetBool("Run", false);
        }

        // Move left
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("RunLeft", true);
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        }
        else
        {
            anim.SetBool("RunLeft", false);
        }

        // Move right
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("RunRight", true);
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        }
        else
        {
            anim.SetBool("RunRight", false);
        }

        // Move down
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("RunBack", true);
            transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
        }
        else
        {
            anim.SetBool("RunBack", false);
        }

        // Shoot
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //anim.SetBool("Attack", true);
            anim.Play("Base Layer.Attack", 0, 0.25f);
            Instantiate(projectile, gunMuzzle.transform.position, transform.rotation);
            //audioData.Play(0);
            //managerScript.audioData.PlayOneShot(audioFiles[0]);
            managerScript.ShootSound();
        }
        else
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                anim.SetBool("Attack", false);
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyProjectile")
        {
            Debug.Log("PLAYER HIT");
            managerScript.HitSound();
            //gameManager.GetComponent<GameManagerScript>().hp--;
            managerScript.hpBar[managerScript.hp-1].image.fillCenter = false;
            managerScript.hp--;
            Destroy(other.gameObject);
            if (managerScript.hp <= 0)
            {
                Debug.Log("GAME OVER!");
                //managerScript.audioData.PlayOneShot(audioFiles[1]);
                playerDeathParticles.transform.position = transform.position;
                playerDeathParticles.Play();
                managerScript.DeathSound();
                Destroy(this.gameObject);
                gameManager.GetComponent<GameManagerScript>().SetGameOver(true);
            }
        }
    }
}
