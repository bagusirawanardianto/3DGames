using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimation : MonoBehaviour
{
    public float speed = 4;
    public float rotSpeed = 80;
    public float rot = 0f;
    public float gravity = 8;
    Animator anim;
    Vector3 moveDir = Vector3.zero;
    CharacterController controller;

    //Create Keycodes that will be associated with each of our commands.
    //These can be accessed by any other script in our game
    public KeyCode jump { get; set; }
    public KeyCode forward { get; set; }
    public KeyCode backward { get; set; }
    public KeyCode left { get; set; }
    public KeyCode right { get; set; }
    public KeyCode shot { get; set; }

    void Awake()
    {
        /*Assign each keycode when the game starts.
         * Loads data from PlayerPrefs so if a user quits the game, 
         * their bindings are loaded next time. Default values
         * are assigned to each Keycode via the second parameter
         * of the GetString() function
         */
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
        shot = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shotKey", "M"));

    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
	    GetInput();
	    SwapAk47();
	    SwapShotgun();
    }

    void Movement() {
    if (controller.isGrounded) {
            //Walk
        if (Input.GetKeyDown(KeyCode.W)) {
                
            if(anim.GetBool("shot") == true) {
                return;
            }
            else if (anim.GetBool("shot") == false) {
                anim.SetBool("run", true);
                anim.SetInteger("condition", 1);
                moveDir = new Vector3 (0, 0, 1);
                moveDir *= speed;
                moveDir = transform.TransformDirection(moveDir);
            }
        }
        //run
        if (Input.GetKeyDown(KeyCode.W)) {
            anim.SetBool("run", false);
            anim.SetInteger("condition", 0);
            moveDir = new Vector3 (0, 0, 0);
        }
        rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3 (0, rot, 0);
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
        }
    }

    void GetInput() {
        //Fire
        if (Input.GetButtonDown("Fire1")) {
            if (anim.GetBool("run") == true) {
                anim.SetBool("run", false);
                anim.SetInteger("condition", 0);
            }
            if (anim.GetBool("run") == false) {
                Attack();
            }
        }
    }

    IEnumerator AttackRoutine() {
        anim.SetBool("shot", true);
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds(0.5f);
        anim.SetInteger("condition", 0);
        anim.SetBool("shot", false);
    }

    void Attack() {
        StartCoroutine(AttackRoutine());
    }

    void SwapAk47() {
        //Switch Weapon 1
    if (Input.GetKey(KeyCode.Alpha1) || Input.GetKeyDown(forward)) {
        anim.SetBool("switchWeapon", true);
        anim.SetInteger("condition", 3);
        }
    else if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyDown(forward)) {
        anim.SetBool("switchWeapon", false);
        anim.SetInteger("condition", 0);
        }
    }
 
    void SwapShotgun()
    {
        //Switch Weapon 2
        if (Input.GetKey(KeyCode.Alpha2)) {
        anim.SetBool("switchWeapon", true);
        anim.SetInteger("condition", 3);    
        }
    else if (Input.GetKeyUp(KeyCode.Alpha2)) {
        anim.SetBool("switchWeapon", false);
        anim.SetInteger("condition", 0);
        }
    }
}
