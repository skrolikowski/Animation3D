using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceBum
{
    public class InputController : MonoBehaviour, StandardControls.IMovementActions
    {
        [Serializable]
        public struct Sensitivity
        {
            [SerializeField, Range(0.2f, 2f), Tooltip("Look sensitivity for mouse.")]
            float mouseVertical;

            [SerializeField, Range(0.2f, 2f), Tooltip("Look sensitivity for mouse.")]
            float mouseHorizontal;

            public float MouseVerticalSensitivity => mouseVertical;
            public float MouseHorizontalSensitivity => mouseHorizontal;
        }

        [SerializeField, Tooltip("Vertical and Horizontal axis sensitivity")]
        Sensitivity cameraLookSensitivity;

        [SerializeField] bool invertX;
        [SerializeField] bool invertY;

        // Events

        public event Action recenterCamera;
        public event Action onCameraChange;
        public event Action jumpTerminated;
        public event Action jumpTriggered;
        public event Action strafeStarted;
        public event Action strafeEnded;
        
        // Public Accessors

        public Vector2 Axis => standardControls.Movement.Axis.ReadValue<Vector2>();
        public Vector3 LocalDirection => new Vector3(Axis.x, 0f, Axis.y);
        public bool HasAxisInput => Axis != Vector2.zero;
        public Vector2 LookAt { get; private set; }
        public Sensitivity CameraLookSensitivity => cameraLookSensitivity;
        public float LastGroundTouch { get; private set; }

        public bool JumpIsReady => LastGroundTouch < hangTime && jumpBuffer > 0;
        public bool IsJumpRequested => standardControls.Movement.Jump.ReadValue<float>() == 1;
        public bool IsStrafing { get; private set; }

        const float jumpBufferDelay = 0.2f;
        const float hangTime = 0.15f;

        // Prviate

        StandardControls standardControls;
        CharacterController character;
        float jumpBuffer;

        // Monobehaviors

        void Awake()
        {
            standardControls = new StandardControls();
            standardControls.Movement.SetCallbacks(this);

            character = GetComponent<CharacterController>();
        }

        void OnEnable()
        {
            standardControls.Enable();
        }

        void OnDisable()
        {
            standardControls.Disable();
        }

        void FixedUpdate()
        {
            jumpBuffer -= Time.deltaTime;

            if (character.isGrounded)
                LastGroundTouch = 0;
            else
                LastGroundTouch += Time.deltaTime;

            if (JumpIsReady)
            {
                jumpTriggered();
            }
        }

        /*********************
         * STANDARD CONTROLS *
         *********************/

        public void OnAxis(InputAction.CallbackContext context)
        {
            //...
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                jumpBuffer = jumpBufferDelay;
            }
            else
            {
                jumpTerminated();
            }
        }

        public void OnStrafe(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                IsStrafing = true;
                onCameraChange();
                strafeStarted();
            }
            else
            {
                IsStrafing = false;
                onCameraChange();
                strafeEnded();
            }
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            Vector2 axis = context.ReadValue<Vector2>();

            float xLookAt = invertX ? axis.x : -axis.x;
            float yLookAt = invertY ? axis.y : -axis.y;

            yLookAt *= cameraLookSensitivity.MouseVerticalSensitivity;
            xLookAt *= cameraLookSensitivity.MouseHorizontalSensitivity;

            LookAt = new Vector2(xLookAt, yLookAt);
        }

        public void OnRecenter(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                recenterCamera();
            }
        }
    }
}