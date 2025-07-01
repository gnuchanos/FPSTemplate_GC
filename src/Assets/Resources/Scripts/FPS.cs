using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    // Player Default Settings
    public Transform head;
    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    public float jumpHeight = 1.5f;
    public float gravity = -15f;
    public float mouseSensitivity = 200f;



    // Player Speed
    public float CurrentSpeed = 4f;
    public float walkSpeed = 4f;
    public float sprintSpeed = 7f;
    public float crouchSpeed = 2f;

    public bool BodyDuck = false;

    // Player and Head/Camera Height change if player crouch
    private float BodyHeight;
    public float crouchHeight = 1f;
    public float standHeight = 2f;

    private float HeadHeight;
    private float HeadHeight_min = 0.5f;
    private float HeadHeight_max = 1.3f;


    // Raycast Face and Head
    public Ray faceRay;
    public Ray headRay;

    // Simple Player Settings
    public bool playerCanJump = true;
    public bool playerCanrun = true;

    public bool headRayHit = false;
    public bool faceRayHit = false;

    private float FaceRayDistance = 5f;
    private float HeadRayDistance = 2f;

    public Color rayColor = Color.green;
    public Color rayColor_face = Color.green;

    void Start() {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        if (head == null) {
            Debug.LogError("Head atanmadı!");
        }
    }

    void LookAround() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        head.localEulerAngles = new Vector3(xRotation, 0f, 0f);
    }

    void UpdateFaceRay() {
        Vector3 faceOrigin = head.position;
        Vector3 faceDirection = head.forward;

        faceRay = new Ray(faceOrigin, faceDirection);
        faceRayHit = Physics.Raycast(faceRay, out RaycastHit hit, FaceRayDistance);


        if (faceRayHit) {
            rayColor_face = Color.red;
        } else {
            rayColor_face = Color.green;
        }

        Debug.DrawRay(faceOrigin, faceDirection * FaceRayDistance, rayColor_face);
    }

    void UpdateHeadRay() {
        Vector3 rayDirection = Vector3.up;
        Vector3 headOrigin = head.position;

        headRay = new Ray(headOrigin, rayDirection);
        headRayHit = Physics.Raycast(headRay, out RaycastHit hit, HeadRayDistance);

        if (headRayHit) {
            rayColor = Color.red;
        } else {
            rayColor = Color.green;
        }

        Debug.DrawRay(headOrigin, rayDirection * HeadRayDistance, rayColor);
    }

    void Walk() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * x + transform.forward * z).normalized * CurrentSpeed;
        move.y = velocity.y;
        controller.Move(move * Time.deltaTime);        
    }


    void HandleCrouch() {
        if (Input.GetKey(KeyCode.LeftControl)) {
            BodyHeight = crouchHeight;
            HeadHeight = HeadHeight_min;
            CurrentSpeed = crouchSpeed;
            BodyDuck = true;
        } else {
            if (!headRayHit) {
                BodyHeight = standHeight;
                HeadHeight = HeadHeight_max;
                CurrentSpeed = walkSpeed;
                BodyDuck = false;
            }
        }

        controller.height = Mathf.Lerp(controller.height, BodyHeight, Time.deltaTime * 8f);

        Vector3 camPos = head.localPosition;
        camPos.y = Mathf.Lerp(camPos.y, HeadHeight, Time.deltaTime * 8f);
        head.localPosition = camPos;
    }

    void Run() {
        if (Input.GetKey(KeyCode.LeftShift) && !BodyDuck){
            CurrentSpeed = sprintSpeed;
        }
    }


    void Jump() {
        if (controller.isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        if (controller.isGrounded && Input.GetButtonDown("Jump")){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
    }

    void Update() {
        LookAround();
        UpdateFaceRay();
        UpdateHeadRay();
        HandleCrouch();
        Walk();
        Run();
        Jump();
    }
}


