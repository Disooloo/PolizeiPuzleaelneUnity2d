using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class ParkourMovement : MonoBehaviour
{
    
    [Header("Language")]
    [SerializeField] private int language;

    [Header("Movement")]
    [SerializeField] public float moveSpeed;
    public float acceleration;
    public float decceleration;
    public float velPower;

    [Header("Jump")]
    public Vector2 jumpForce;
    public int jumpCounter;
    private int jumpTimeStore;
    public AudioSource _audioJamp;

    [Header("Dash")]
    public float dashForce;
    public float startDashTimer;
    float currentDashTimer;
    float dashDirection;
    bool isDashing;

    [Header("Crouch")]
    public float crouchHeightPercentage;
    public GameObject standingSprite;
    public GameObject crouchingSprite;

    private Vector2 standColiderSize;
    private Vector2 standColliderOffset;
    private Vector2 crouchColliderSize;
    private Vector2 crouchColliderOffset;

    [Header("Grappling Hook")]
    public Camera mainCamera;
    public LineRenderer linedRend;
    public DistanceJoint2D joint;

    [Header("Particle Effects")]
    public GameObject jumpParticleSpawnPos;
    public GameObject jumpEffect;
    public GameObject DangerEffect;

    [Header("Customizable")]
    public CinemachineVirtualCamera vcam;
    public bool smoothMovement;
    public bool canDash;
    public bool canCrouch;
    public bool canGrapple;
    public AudioSource _damageSound;

    [Header("Private Variables")]
    private Rigidbody2D rb;
    private float movX;
    private BoxCollider2D playerCollider;
    bool crouch;

    [Header("UI")]
    [SerializeField] private GameObject _GameOver;

    private bool camZooming;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vcam.m_Lens.OrthographicSize = 8;

        jumpTimeStore = jumpCounter;

        joint.enabled = false;

        standingSprite.SetActive(true);
        crouchingSprite.SetActive(false);

        playerCollider = GetComponent<BoxCollider2D>();
        standColiderSize = playerCollider.size;
        standColliderOffset = playerCollider.offset;

        crouchColliderSize = new Vector2(standColiderSize.x, standColiderSize.y * crouchHeightPercentage);
        crouchColliderOffset = new Vector2(standColliderOffset.x, standColliderOffset.y * crouchHeightPercentage);
        _GameOver.gameObject.SetActive(false);

        language = PlayerPrefs.GetInt("language", language);
    }

    // Update is called once per frame
    void Update()
    {
        movX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if(jumpCounter > 0)
            {
                JumpHandler();

            }
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash == true && movX != 0)
        {
            DashHandler();
        }

       

        GrappleManager();
        CrouchManager();
    }

    private void FixedUpdate()
    {
        MovementHandler();
    }


    private void MovementHandler()
    {
        if (smoothMovement == true)
        {
            #region MovementSmooth
            float targetSpeed = movX * moveSpeed;
            float speedDif = targetSpeed - rb.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
            rb.AddForce(movement * Vector2.right);
            #endregion
        }
        else
        {
            #region MovementNormal
            rb.velocity = new Vector2(movX * moveSpeed, rb.velocity.y);
            #endregion
        }
    }

    private void JumpHandler()
    {
        rb.AddForce(jumpForce * 1.15f, ForceMode2D.Impulse);
        Instantiate(jumpEffect, jumpParticleSpawnPos.transform.position, Quaternion.identity);
        jumpCounter -= 1;
        _audioJamp.Play();
    }

    private void CrouchManager()
    {
        if(canCrouch)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                crouch = true;
            }
            else if (Input.GetKeyUp(KeyCode.C))
            {
                crouch = false;
            }

            if (crouch == true)
            {
                playerCollider.size = crouchColliderSize;
                playerCollider.offset = crouchColliderOffset;
                standingSprite.SetActive(false);
                crouchingSprite.SetActive(true);
                jumpCounter = 0;
                if(vcam.m_Lens.OrthographicSize > 5)
                {
                    vcam.m_Lens.OrthographicSize -= Time.deltaTime;
                    camZooming = true;
                }
                else
                {
                    vcam.m_Lens.OrthographicSize = 5;
                    camZooming = false;
                }
            }

            if (crouch == false)
            {
                playerCollider.size = standColiderSize;
                playerCollider.offset = standColliderOffset;
                if (vcam.m_Lens.OrthographicSize < 8)
                {
                    vcam.m_Lens.OrthographicSize += Time.deltaTime * 1.4f;
                    camZooming = true;
                    StartCoroutine(camWait());
                }

                standingSprite.SetActive(true);
                crouchingSprite.SetActive(false);
            }
        }
    }

    private void DashHandler()
    {
        isDashing = true;
        currentDashTimer = startDashTimer;
        rb.velocity = Vector2.zero;
        dashDirection = movX;

        if(isDashing)
        {
            rb.velocity = transform.right * dashDirection * dashForce;
            currentDashTimer -= Time.deltaTime;
            if (currentDashTimer <= 0)
            {
                isDashing = false;
            }
        }
    }

    private void GrappleManager()
    {
        if(Input.GetMouseButtonDown(0) && canGrapple == true)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
                linedRend.SetPosition(0, mousePos);
                linedRend.SetPosition(1, transform.position);
                joint.connectedAnchor = mousePos;
                joint.enabled = true;
                linedRend.enabled = true;
            }

        }
        else if(Input.GetMouseButtonUp(0) && canGrapple == true)
        {
            joint.enabled = false;
            linedRend.enabled = false;
        }

        if(joint.enabled)
        {
            linedRend.SetPosition(1, transform.position);
            if (vcam.m_Lens.OrthographicSize < 12)
            {
                vcam.m_Lens.OrthographicSize += Time.deltaTime * 1.8f;
                camZooming = true;
            }
            else
            {
                vcam.m_Lens.OrthographicSize = 12;
                camZooming = false;
            }
        }
        else
        {
            if (vcam.m_Lens.OrthographicSize > 8)
            {
                vcam.m_Lens.OrthographicSize -= Time.deltaTime * 1.7f;
                camZooming = true;
                StartCoroutine(camWait());
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCounter = jumpTimeStore;
        }
    }

    IEnumerator camCorrection()
    {
        yield return new WaitForSeconds(0.05f);
        vcam.m_Lens.OrthographicSize = 8;
        camZooming = false;
    }

    IEnumerator camWait()
    {
        yield return new WaitForSeconds(1.5f);
        if (crouch == false && joint.enabled == false && camZooming == false)
        {
            StartCoroutine(camCorrection());
        }
    }

   

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DangerLevelTrigger")
        {
            //gameObject.SetActive(false);
           // Invoke("LoadScene", 1.5f);
            _damageSound.Play();
            Instantiate(DangerEffect, jumpParticleSpawnPos.transform.position, Quaternion.identity);
            _GameOver.gameObject.SetActive(true);

        }
    }
    /*
    public void RussianLanguage()
    {
        language = 0;
        PlayerPrefs.SetInt("language", language);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EnglishLanguage()
    {
        language = 1;
        PlayerPrefs.SetInt("language", language);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    */

}
