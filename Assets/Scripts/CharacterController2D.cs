using System;
using UnityEngine;
using UnityEngine.Events;


// IMPORTANT NOTE: THIS SCRIPT IS A MODIFIED (MODIFIED TO FIT PROJECT NEEDS) VERSION OF BRACKEY'S CHARACTER 2D CONTROLLER SCRIPT FROM YOUTUBE

// Comments denoted by !!! signify that the code was taken from an open source script by DawnoSaurDev (https://github.com/DawnosaurDev/platformer-movement/blob/main/Scripts/Run%20Only/PlayerRun.cs) 
// This script allows for momentum based adjustments

public class CharacterController2D : MonoBehaviour
{
    public PlayerData Data;

    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.

    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;   // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.

    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);

    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private bool m_CanDoubleJump;

    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    private Vector2 m_moveInput;
    public float LastOnGroundTime { get; private set; }
    public bool IsJumping { get; private set; }
    public Rigidbody2D RB { get; private set; }

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    public int shock_multiplier = 1;
    private float shock_timer = 0.0f;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }


    private void Update()
    {
        /* !!! */
        LastOnGroundTime -= Time.deltaTime;

        m_moveInput.x = Input.GetAxisRaw("Horizontal") * shock_multiplier;
        if (m_moveInput.x != 0)
            CheckDirectionToFace(m_moveInput.x > 0);

        if (IsJumping && m_Rigidbody2D.velocity.y < 0)
            IsJumping = false;

        //Ground Check
        if (Physics2D.OverlapBox(m_GroundCheck.position, _groundCheckSize, 0, _groundLayer)) //checks if set box overlaps with ground
            LastOnGroundTime = 0.1f;
        //Check if player still shocked
        if (shock_multiplier == 0)
        {
            shock_timer += Time.deltaTime;
            if (shock_timer > 2)
            {
                shock_multiplier = 1;
                shock_timer = 0.0f;
            }
        }
    }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        RB = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                m_CanDoubleJump = true;;

                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool jump, bool canDoubleJump)
    {


        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            Run();

            // Move the character by finding the target velocity
            /*Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);*/

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (jump)
        {
            if (m_Grounded)
            {
                IsJumping = true;
                Jump();
            }
            else if (m_CanDoubleJump)
            {
                m_CanDoubleJump = false;

                Jump();
            }
        }
    }


    private void Jump()
    {
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
        m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
    }

    /* !!! */
    private void Run()
    {
        float targetSpeed = m_moveInput.x * Data.runMaxSpeed;
        float accelerate;

        if (LastOnGroundTime > 0)
            accelerate = (Math.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
        else
            accelerate = (Math.Abs(targetSpeed) > 0.01f)
                ? Data.runAccelAmount * Data.accelInAir
                : Data.runDeccelAmount * Data.deccelInAir;

        if (IsJumping && Math.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
        {
            accelerate *= Data.jumpHangAccelerationMult;
            targetSpeed *= Data.jumpHangMaxSpeedMult;
        }

        if (Data.doConserveMomentum && Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeed) &&
            Mathf.Sign(RB.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f &&
            LastOnGroundTime < 0)
            accelerate = 0;

        //Calculate difference between current velocity and desired velocity
        float speedDif = targetSpeed - RB.velocity.x;

        float movement = speedDif * accelerate;

        RB.AddForce(movement * Vector2.right, ForceMode2D.Force);


    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    /* !!! */
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != m_FacingRight)
            Flip();
    }
}