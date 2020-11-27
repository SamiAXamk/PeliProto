using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed = 1;
	public int hp = 2;
	public GameObject player, projectile;

	public const string IDLE = "Anim_Idle";
	public const string RUN = "Anim_Run";
	public const string ATTACK = "Anim_Attack";
	public const string DAMAGE = "Anim_Damage";
	public const string DEATH = "Anim_Death";

	Animation anim;
	public GameObject gameManager;
	GameManagerScript managerScript;

	void Start()
	{
		anim = GetComponent<Animation>();
		player = GameObject.FindGameObjectWithTag("Player");
		InvokeRepeating("Shoot", 2, 1);
		managerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
	}

    void Update()
    {
		transform.Translate(new Vector3(0,0,-1) * Time.deltaTime * speed, Space.World);

		if (player != null)
		{
			transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), Vector3.up);
		}
	}


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Projectile")
        {
			Debug.Log("HIT");
			hp--;
			Destroy(other.gameObject);
			if(hp <= 0)
            {
				managerScript.EnemyDeathSound();
				managerScript.EnemyKilled();
				Destroy(this.gameObject);
            }
        }
		if(other.gameObject.tag == "GameManager")
        {
			Destroy(this.gameObject);
        }
    }

	void Shoot()
    {
		if (player != null)
		{
			Instantiate(projectile, this.gameObject.transform.position, transform.rotation);
		}
	}



    public void IdleAni()
	{
		anim.CrossFade(IDLE);
	}

	public void RunAni()
	{
		anim.CrossFade(RUN);
	}

	public void AttackAni()
	{
		anim.CrossFade(ATTACK);
	}

	public void DamageAni()
	{
		anim.CrossFade(DAMAGE);
	}

	public void DeathAni()
	{
		anim.CrossFade(DEATH);
	}

}
