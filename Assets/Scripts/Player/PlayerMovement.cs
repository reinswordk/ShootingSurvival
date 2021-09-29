using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Singleton
    private static PlayerMovement _instance = null;
    public static PlayerMovement Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<PlayerMovement>();
                if(_instance == null)
                {
                    Debug.Log("Fatal Error: Player Movement");
                }
            }return _instance;
        }
    }
    #endregion
    public float speed = 6f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    private void Awake()
    {
        //nilai mask layer dari layer Floor
        floorMask = LayerMask.GetMask("Floor");

        //mendapatkan Animator
        anim = GetComponent<Animator>();

        //mendapatkan Rigidbody
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //mendapat nilai input horizontal
        float h = Input.GetAxisRaw("Horizontal");

        //mendapat nilai input vertical
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);    
        Turning();
        Animating(h, v);
    }

    //Player berjalan
    public void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        //move to position
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        //Buat Ray dari posisi mouse di layar
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        //Buat raycast untuk floorHit
        RaycastHit floorHit;
        
        //Lakukan raycast
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            //Mendapatkan vector dari posisi player dan posisi floorHit
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            
            //Mendapatkan look rotation baru ke hit position
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            
            //Rotasi player
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    public void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("isWalking", walking);
    }

    //Movement boost
    public void Boosting()
    {
       speed += 1;
    }
}
