using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // jump velocity coefficient
    public readonly float jvc = 8f;

    private Rigidbody2D rb2d;
    private Animator animator;

    public bool Grounded { get; private set; } = false;
    public bool Clinged { get; private set; } = false;

    private Jump jump = null;

    private Vector2 startPos;

    private DeathController deathController;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        startPos = rb2d.position;

        if (SceneManager.GetActiveScene().name != "MenuScene")
        {
            deathController = GameObject.Find("Level Controller").GetComponent<DeathController>();
        }
    }

    void Update()
    {
        CheckFreezeState();
    }

    void FixedUpdate()
    {
        if (jump != null) Jump();

        animator.SetFloat("Velocity Y", rb2d.velocity.y);
    }

    private void CheckFreezeState()
    {
        if (!Grounded && !Clinged && rb2d.velocity.Equals(Vector2.zero))
        {
            SetPlayerGrounded(true);
        }
    }

    private void Jump()
    {
        ChangeDirection();

        if (Grounded) SetPlayerGrounded(false);
        else if (Clinged) SetPlayerClinged(false);

        SetVelocity(jump.velocity);
        AudioController.instance.PlayJumpSFX();

        jump = null;
    }

    public void SetJump(Jump j)
    {
        jump = j;
    }

    public Jump GenerateJump(Vector2 distance)
    {
        float angle = CurveRenderer.CalculateAngle(distance);
        Vector2 velocity = distance * jvc;

        if (Grounded)
        {
            // vertical jump
            if (angle > 85 && angle < 95)
            {
                return new Jump(new Vector2(0, velocity.y), 90, JumpType.GROUND_VERTICAL);

            }
            else if (angle > 10 && angle < 170)
            {
                return new Jump(velocity, angle, JumpType.GROUND_NOT_VERTICAL);
            }
        }

        else if (Clinged)
        {
            // jump to the right
            if (transform.localScale.x < 0 && angle > -85 && angle < 65)
            {
                return new Jump(velocity, angle, JumpType.WALL_TO_RIGHT);
            }
            // jump to the left
            if (transform.localScale.x > 0 && (angle > 115 || angle < -95))
            {
                return new Jump(velocity, angle, JumpType.WALL_TO_LEFT);
            }
        }
        return null;
    }

    private void ChangeDirection()
    {
        if (jump.velocity.x * transform.localScale.x < 0)
        {
            Vector2 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }

    //private bool collided = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // there are two cases that the player touches two colliders at the same time
        // two callbacks will be called at once

        //if (collided) return;
        //collided = true;

        string tag = collision.gameObject.tag;
        if (tag == "Ground")
        {
            SetPlayerGrounded(true);
            // AudioController.instance.PlayCollideSFX();
        }

        // Math.Round is used here to remove the bug [wrong-drag]
        else if (tag == "Wall" && Math.Round(rb2d.velocity.x, 2) == 0)
        {
            SetPlayerClinged(true);
            // AudioController.instance.PlayCollideSFX();
        }

        else if (tag == "Spike")
        {
            SetDead(true);
            AudioController.instance.PlayHitSFX();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "Wall")
        {
            // touch the ground while clinging
            if (rb2d.velocity.magnitude == 0)
            {
                SetPlayerGrounded(true);
            }

            // falling down while being close to the wall
            else if (rb2d.velocity.y < 0 && !Clinged)
            {
                SetPlayerClinged(true);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Ground")
        {
            SetPlayerGrounded(false);
        }

        else if (tag == "Wall")
        {
            SetPlayerClinged(false);
        }

        //if (Grounded)
        //{
        //    SetPlayerGrounded(false);
        //}

        //else if (Clinged)
        //{
        //    SetPlayerClinged(false);
        //}

        //collided = false;
    }

    //void OnTriggerEnter2D(Collider2D collider)
    //{
    //    string tag = collider.tag;

    //    if (tag == "Spike")
    //    {
    //        animator.SetTrigger("Hit");
    //        GetComponent<Player>().enabled = false;
    //    }
    //}

    private void SetPlayerGrounded(bool status)
    {
        Grounded = status;
        animator.SetBool("Grounded", status);

        if (status)
        {
            SetPlayerClinged(false);
            SetVelocity(Vector2.zero);
            rb2d.drag = 0;
        }
    }

    private void SetPlayerClinged(bool status, float drag = 50)
    {
        Clinged = status;
        animator.SetBool("Clinged", status);

        if (status)
        {
            SetPlayerGrounded(false);
            rb2d.drag = drag;
        }
        else
        {
            rb2d.drag = 0;
        }
    }

    private void SetVelocity(Vector2 velocity)
    {
        rb2d.velocity = velocity;
        animator.SetFloat("Velocity Y", rb2d.velocity.y);
    }

    private void SetDead(bool status)
    {
        if (status)
        {
            rb2d.velocity = Vector2.zero;
            SetKinematicTrue();
            animator.SetTrigger("Dead");
            GetComponent<Player>().enabled = false;
        }
        else
        {
            SetKinematicFalse();
            GetComponent<Player>().enabled = true;
        }

    }

    public void Disappear()
    {
        SetDead(false);
        gameObject.SetActive(false);
        deathController.IncrementPlayerDeath();
        deathController.ReviveMe(gameObject, startPos, 1);
    }

    public void Appear()
    {
        rb2d.velocity = new Vector2(0, -0.5f); // to avoid FreezeState
        SetKinematicTrue();
    }

    public void SetKinematicTrue()
    {
        rb2d.isKinematic = true;
    }

    public void SetKinematicFalse()
    {
        rb2d.isKinematic = false;
    }

    // for main menu scene
    public void SetRun(bool status)
    {
        animator.SetBool("Run", status);
    }
}
