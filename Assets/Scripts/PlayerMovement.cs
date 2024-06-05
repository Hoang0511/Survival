using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {


        public CharacterController controller;
        public float speed = 12f;
        public float gravity = -9.81f * 2;
        public float jumpHeight = 3f;

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        Vector3 velocity;

        bool isGrounded;

        private Vector3 lastPosition = new Vector3(0f, 0f, 0f);
        public bool isMoving;

        void Update()
        {
            if(StorageManager.Instance.storageUIOpen == false && CampfireUIManager.Instance.isUiOpen == false)
            {
                Movement();
            }
        }


    public void Movement()
    {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);


            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);


            if (lastPosition != gameObject.transform.position && isGrounded == true)
            {
                isMoving = true;
                SoundManager.Instance.PlaySound(SoundManager.Instance.grassWalkSound);
            }
            else
            {
                isMoving = false;
                SoundManager.Instance.grassWalkSound.Stop();
            }
            lastPosition = gameObject.transform.position;
        }
    }
}