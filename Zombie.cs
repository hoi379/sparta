using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    [SerializeField] private Rigidbody2D _rig;

    private bool _isDead = false;



    public void Init(Vector3 pos)
    {
        this.transform.position = pos;

        _isDead = false;
    }

    #region Move

    [SerializeField] private LayerMask _layer;

    [SerializeField] private bool _jumpAble = true;
    [SerializeField] private bool _jump = false;
    [SerializeField] private bool _up = false;
    private bool _upFront = false;
    [SerializeField] private bool _hero = false;
    [SerializeField] private bool _zombie = false;
    [SerializeField] private bool _down = false;
    private float _frontZombieSpeed;

    private Vector2 _force = Vector2.zero;

    private void Move()
    {
        if (!Physics2D.OverlapBox(new Vector2(-0.65f + this.transform.position.x, 0.6f + this.transform.position.y), new Vector2(0.1f, 0.7f), 0))
        {
            _hero = false;

            _zombie = false;
        }
        else
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(-0.65f + this.transform.position.x, 0.6f + this.transform.position.y), new Vector2(0.1f, 0.7f), 0);

            foreach(Collider2D col in colliders)
            {
                if(col.tag == "Hero")
                {
                    _hero = true;

                    _zombie = false;
                }

                if(col.tag == "Zombie")
                {
                    _frontZombieSpeed = col.GetComponent<Zombie>()._force.x;

                    _zombie = true;

                    _hero = false;
                }
            }
        }

        if (Physics2D.OverlapBox(new Vector2(-0.25f + this.transform.position.x, 1.1f + this.transform.position.y), new Vector2(0.7f, 0.1f), 0))
        {
            if(Physics2D.OverlapBox(new Vector2(-0.25f + this.transform.position.x, 1.1f + this.transform.position.y), new Vector2(0.7f, 0.1f), 0).tag == "Zombie")
            {
                _up = true;

                if(Physics2D.OverlapBox(new Vector2(-0.25f + this.transform.position.x, 1.1f + this.transform.position.y), new Vector2(0.7f, 0.1f), 0).transform.position.x 
                    <= this.transform.position.x + 0.2f)
                {
                    _upFront = true;
                }
            }
        }
        else
        {
            _up = false;

            _upFront = false;
        }
        

        if(Physics2D.OverlapBox(new Vector2(-0.35f + this.transform.position.x, -0.1f + this.transform.position.y), new Vector2(0.5f, 0.1f), 0))
        {
            _down = true;
        }
        else
        {
            _down = false;
        }


        _force = new Vector2(-1.5f, 0f);

        /*if (!_down && !_jump)
        {
            _force.y = -5f;
        }*/

        if(_hero)
        {
            _anim.SetBool("IsAttacking", true);

            _force.x = 0f;
        }
        else
        {
            _anim.SetBool("IsAttacking", false);
        }

        if(_zombie)
        {
            if(_jumpAble)
            {
                _jump = true;
            }
            else
            {
                _force.x = _frontZombieSpeed;
            }
        }
        
        if(_jump)
        {
            _force.y = 5f;

            if(_up)
            {
                _jump = false;

                StartCoroutine(JumpCoolTime());
            }

            if(!_down)
            {
                _jump = false;
            }
        }

        if(_upFront)
        {
            if(!_zombie && _down)
            {
                _force.x = 1.5f;
            }
        }

        this.transform.Translate(_force * Time.deltaTime);
    }



    IEnumerator JumpCoolTime()
    {
        _jumpAble = false;

        yield return new WaitForSeconds(2f);

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

        Gizmos.DrawWireCube(new Vector2(-0.65f +this.transform.position.x, 0.6f + this.transform.position.y), new Vector2(0.1f, 0.7f));

        Gizmos.DrawWireCube(new Vector2(-0.25f + this.transform.position.x, 1.1f + this.transform.position.y), new Vector2(0.7f, 0.1f));

        Gizmos.DrawWireCube(new Vector2(-0.35f + this.transform.position.x, -0.1f + this.transform.position.y), new Vector2(0.5f, 0.1f));

        Gizmos.DrawWireCube(new Vector2(-1f + this.transform.position.x, 0.6f + this.transform.position.y), new Vector2(0.1f, 0.1f));
    }
}
