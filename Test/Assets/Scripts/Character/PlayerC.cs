using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    click
}

public class PlayerC : MonoBehaviour
{
    public float speed;
    public PlayerState currentState;
    private Rigidbody2D myRigidbody2D;
    private Vector3 vector;
    public Vector2 lastMotionVector;
    private Animator animator;
    [SerializeField] protected Joystick joystick;
    private float joyHorizontal;
    private float joyVertical;
    [SerializeField] bool isjoystick=false;
    //Use this for initialization
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Awake()
    {
        // Deleting all objects from the inventory
        foreach (ItemSlot itemSlot in GameM.instance.inventoryContainer.slots)
        {
            if (itemSlot != null)
            {
                if (itemSlot.item != null)
                {
                    GameM.instance.inventoryContainer.RemoveItem(itemSlot.item, itemSlot.count);
                }
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) { isjoystick = !isjoystick; }
        if (FindObjectOfType<Gameplay.ShipName.PlayerShip>()._Activated == true) return;
        vector = Vector3.zero;
        if (!isjoystick)
        {
            vector.x = Input.GetAxis("Horizontal");
            vector.y = Input.GetAxis("Vertical");
        }
        else {
            vector.x = joystick.Horizontal;
            vector.y = joystick.Vertical;
        }
        if (Input.GetMouseButtonDown(0) && currentState != PlayerState.click)
        {
            StartCoroutine(ClickCo());
        }
        else if (currentState == PlayerState.walk)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator ClickCo()
    {
        animator.SetBool("clicking", true);
        currentState = PlayerState.click;
        yield return null;
        animator.SetBool("clicking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }
    void UpdateAnimationAndMove()
    {
        if (vector != Vector3.zero)
        {
            Move();
            animator.SetFloat("moveX", vector.x);
            animator.SetFloat("moveY", vector.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
            if (FindObjectOfType<SoundMM>().SoundIsPlaying("Walk"))
            {
                FindObjectOfType<SoundMM>().Stop("Walk");
            }
        }
    }
    //Move character
    void Move()
    {
        myRigidbody2D.MovePosition(transform.position + vector * speed * Time.deltaTime);
        if (!FindObjectOfType<SoundMM>().SoundIsPlaying("Walk"))
        {
            FindObjectOfType<SoundMM>().Play("Walk");
        }
    }
}
