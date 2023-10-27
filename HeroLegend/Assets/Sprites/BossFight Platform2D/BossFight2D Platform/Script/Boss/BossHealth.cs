using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
	[SerializeField]
	private ShakeCamera shakeCamera;
	public Boss_Walk bossWalk;
	[SerializeField]
	private SpawnRock spawnRock;
	private Animator anim;
	[SerializeField]
	private BoxCollider2D col;
	[SerializeField]
	private SpriteRenderer sr;
	public Color colorDamage;
	public Color startColor;

	public float currentSongTime;


	public int health = 500;
	public bool isInvulnerable = false;

    private void Start()
    {
		anim = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
	}

    public void Update()
    {
		if (health <= 0)
		{
			anim.SetBool("Hit", false);
			anim.SetTrigger("BigHit");
			anim.SetBool("Song", false);
			gameObject.layer = 12;
		}

		if (health > 0 && Time.time - currentSongTime > 2f)
        {
		    anim.SetBool("Song", true);

			spawnRock.SpawnRockEffect();
			currentSongTime = Time.time;
        }
    }

	public void Shake()
    {
		shakeCamera.ShakeCameraThree();
	}

	public void SongOf()
    {
		anim.SetBool("Song", false);
    }
    public void TakeDamage(int damage)
	{


		if (isInvulnerable)
			return;

		health -= damage;

		
		anim.SetBool("Hit", true);
		StartCoroutine("DamageColor");

		

	}



	IEnumerator DamageColor()
	{
		isInvulnerable = true;
		sr.color = colorDamage;
		yield return new WaitForSeconds(0.1f);
		sr.color = startColor;
		yield return new WaitForSeconds(0.1f);
		sr.color = colorDamage;
		yield return new WaitForSeconds(0.1f);
		sr.color = startColor;
		isInvulnerable = false;
		anim.SetBool("Hit", false);
	}



}
