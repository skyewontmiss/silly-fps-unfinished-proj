using UnityEngine;
using Boxfight2.Client.Weapons;

namespace Boxfight2.Client.Player
{
    public class BoxfightController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float sprintMultiplier = 2f;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private float mouseSensitivity = 100f;
        [SerializeField] Camera camera;
        private Rigidbody rb;
        private bool isGrounded = false;
        private bool isSprinting = false;

        private float xRotation = 0f;
        private GunController gun;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        void FixedUpdate()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            float sprintMultiplier = isSprinting ? this.sprintMultiplier : 1f;
            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed * sprintMultiplier * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + transform.TransformDirection(movement));

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.Rotate(Vector3.up * mouseX);
            camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        }

        #region GunManagement
        public void UpdateGunController(GunController gunController)
        {
            gun = gunController;
        }

        #endregion

    }
}
