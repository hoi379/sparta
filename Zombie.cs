using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Zombie : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    [SerializeField] private Rigidbody2D _rig;

    private bool _isDead = false;

    [SerializeField] private SortingGroup _sort;


    public void Init(Vector3 pos, int line)
    {
        this.transform.position = pos;

        _isDead = false;

        this.gameObject.layer = line + 7;

        _sort.sortingOrder = 5 - line;

        _jumpAble = true;
        _jump = false;
        _up = false;
        _hero = false;
        _zombie = false;
        _down = false;

        this.gameObject.name = Random.Range(0, 1000).ToString();
    }

    #region Move

    private bool _jumpAble = true;
    private bool _jump = false;
    private bool _up = false;
    private bool _upFront = false;
    private bool _hero = false;
    private bool _zombie = false;
    private bool _down = false;
    private float _frontZombieSpeed;

    private Vector2 _force = Vector2.zero;

    private void Move()
    {
        _zombie = false;

        _hero = false;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(-0.65f + this.transform.position.x, 0.5f + this.transform.position.y), new Vector2(0.1f, 0.9f), 0);

        foreach (Collider2D col in colliders)
        {
            if (col.tag == "Hero")
            {
                _hero = true;
            }

            if (col.tag == "Zombie" && col.gameObject.layer == this.gameObject.layer)
            {
                _frontZombieSpeed = col.GetComponent<Zombie>()._force.x;

                _zombie = true;
            }
        }
        

        
        _up = false;

        _upFront = false;

        colliders = Physics2D.OverlapBoxAll(new Vector2(-0.25f + this.transform.position.x, 1.1f + this.transform.position.y), new Vector2(0.7f, 0.1f), 0);

        foreach (Collider2D col in colliders)
        {
            if (col.tag == "Zombie" && this.gameObject.layer == col.gameObject.layer)
            {
                _up = true;

                if (col.transform.position.x <= this.transform.position.x + 0.2f)
                {
                    _upFront = true;

                    break;
                }
            }
        }
        

        
        _down = false;

        colliders = Physics2D.OverlapBoxAll(new Vector2(-0.4f + this.transform.position.x, -0.1f + this.transform.position.y), new Vector2(0.4f, 0.1f), 0);

        foreach (Collider2D col in colliders)
        {
            if (this.gameObject.layer == col.gameObject.layer)
            {
                _down = true;

                break;
            }
        }



        _force = new Vector2(-1.5f, 0f);

        if (_hero)
        {
            _anim.SetBool("IsAttacking", true);

            _force.x = 0f;
        }
        else
        {
            _anim.SetBool("IsAttacking", false);
        }

        if (_zombie)
        {
            if (_jumpAble)
            {
                _jump = true;
            }
            else
            {
                _force.x = _frontZombieSpeed;
            }
        }

        if (_jump)
        {
            _force.y = 5f;

            if (_up)
            {
                _jump = false;

                StartCoroutine(JumpCoolTime());
            }

            if (!_down)
            {
                _jump = false;
            }
        }

        if (_upFront)
        {
            if (!_zombie && _down)
            {
                _force.x = 1.2f;
            }
        }

        this.transform.Translate(_force * Time.deltaTime);
    }



    IEnumerator JumpCoolTime()
    {
        _jumpAble = false;

        yield return new WaitForSeconds(1.5f);

        _jumpAble = true;
    }

    #endregion

    private void FixedUpdate()
    {
        if (!_isDead)
        {
            Move();
        }
    }

    public void OnAttack()
    {
        Collider2D col;

        if(col = Physics2D.OverlapBox(new Vector2(-1f + this.transform.position.x, 0.6f + this.transform.position.y), new Vector2(0.1f, 0.1f), 0))
        {
            if(col.tag == "Hero")
            {
                col.GetComponent<BoxHp>().GetDamage(10f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(new Vector2(-0.65f +this.transform.position.x, 0.5f + this.transform.position.y), new Vector2(0.1f, 0.9f));

        Gizmos.DrawWireCube(new Vector2(-0.25f + this.transform.position.x, 1.1f + this.transform.position.y), new Vector2(0.7f, 0.1f));

        Gizmos.DrawWireCube(new Vector2(-0.4f + this.transform.position.x, -0.1f + this.transform.position.y), new Vector2(0.4f, 0.1f));

        Gizmos.DrawWireCube(new Vector2(-1f + this.transform.position.x, 0.6f + this.transform.position.y), new Vector2(0.1f, 0.1f));
    }
}
