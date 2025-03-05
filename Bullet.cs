using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trail;

    private bool _isBomb;

    public void Init(Vector2 muzzle, float rotation)
    {
        this.transform.position = muzzle;

        this.transform.eulerAngles = new Vector3(0, 0, rotation);

        StartCoroutine(lifeTimer());

        _isBomb = false;
    }

    IEnumerator lifeTimer()
    {
        yield return new WaitForSeconds(1f);

        _trail.Clear();

        this.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        this.transform.Translate(Vector2.right * Time.deltaTime * 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            if (!_isBomb)
            {
                _isBomb = true;

                collision.GetComponent<ZombieHp>().GetDamage(70f);

                _trail.Clear();

                this.gameObject.SetActive(false);
            }
        }
    }
}
