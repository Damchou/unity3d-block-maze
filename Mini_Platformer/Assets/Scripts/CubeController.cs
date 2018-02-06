using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class CubeController : MonoBehaviour {

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public Text countText;
    public Text winText;
    public AudioSource audioJump;

    private Vector3 moveDirection = Vector3.zero;
    public AudioSource audioCoin;
    public int count;

    // For coins
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            audioCoin.Play();
            Destroy(other.gameObject);
            count++;
            SetCountText();
        }
    }

    // Parents with platform so the player moves with it
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Platform")
            transform.parent = other.transform;
    }
    // Un-parents
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Platform")
            transform.parent = null;
    }

    // Victory text
    void SetCountText()
    {
        countText.text = "Coins x " + count.ToString();
        if (count >= 100)
            winText.text = "Congratsulations! You collected every coin!";
    }

    void Start()
    {
        count = 0;
        SetCountText();
        winText.text = "";
    }


    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                audioJump.Play();
            }

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

}
