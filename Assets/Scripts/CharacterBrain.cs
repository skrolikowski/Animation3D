using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceBum
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(InputController))]
    public class CharacterBrain : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] CharacterController character;
        [SerializeField] InputController input;

        [Header("Ground Movement")]
        [SerializeField] float groundTurnSpeed = 300f;
        [SerializeField] float maxSpeed = 5f;

        [Header("Aerial Movement")]
        [SerializeField] float aerialTurnSpeed = 300f;
        [SerializeField] float jumpHeight = 4f;
        [SerializeField] float timeToApex = 0.4f;

        Camera mainCamera;
        Animator animator;

        Vector3 fallDirection;

        AerialMovementState aerialState = AerialMovementState.Grounded;
        AnimatorState animatorState = AnimatorState.Locomotion;
        MotorMovementMode movementMode = MotorMovementMode.Strafe;
        GroundMovementState groundMovementState = GroundMovementState.Walking;

        public bool IsGroundedState => animatorState == AnimatorState.Locomotion;
        public float NForwardSpeed { get; private set; }
        public float NLateralSpeed { get; private set; }
        public float NTurningSpeed { get; private set; }
        public float NVerticalSpeed { get; private set; }
        public float Gravity => -(2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
        public float JumpSpeed => Mathf.Abs(Gravity) * timeToApex;

        const float turnSpeedDecay = 5f;
        const float turnSpeedScale = 1.4f;
        const float turnSpeedGrowth = 2f;
        const float fallingForwardSpeed = 3f;
        const float fallDirectionChange = 0.025f;

        // Animator states
        int hashForwardSpeed;
        int hashLateralSpeed;
        int hashTurningSpeed;
        int hashVerticalSpeed;
        int hashIsStrafing;

        void Awake()
        {
            mainCamera = Camera.main;

            InitAnimator();
        }

        void OnEnable()
        {
            input.jumpTriggered += OnJumpTriggered;
            input.jumpTerminated += OnJumpTerminated;
            input.strafeStarted += OnStrafeStarted;
            input.strafeEnded += OnStrafeEnded;
        }

        void OnDisable()
        {
            input.jumpTriggered -= OnJumpTriggered;
            input.jumpTerminated -= OnJumpTerminated;
            input.strafeStarted -= OnStrafeStarted;
            input.strafeEnded -= OnStrafeEnded;
        }

        void Update()
        {
            UpdateAnimator();

            switch (movementMode)
            {
                case MotorMovementMode.Exploration:
                    CalculateForwardMovement();
                    break;
                case MotorMovementMode.Strafe:
                    CalculateStrafeMovement();
                    break;
            }
        }

        void LateUpdate()
        {
            SetLookDirection();
        }

        void OnAnimatorMove()
        {
            if (IsGroundedState)
            {
                // Ground --------
                Vector3 groundMovementVector = animator.deltaPosition;
                groundMovementVector.y = 0.0f;

                character.Move(groundMovementVector * maxSpeed);
            }
            else
            {
                // Aerial --------
                if (NVerticalSpeed <= 0 || aerialState != AerialMovementState.Grounded)
                {
                    //UpdateFallForwardSpeed();
                }

                Vector3 moveDirection = movementMode == MotorMovementMode.Exploration ? transform.forward : CalculateLocalInputDirection();
                fallDirection = Vector3.Lerp(fallDirection, moveDirection, fallDirectionChange);

                character.Move(NForwardSpeed * Time.deltaTime * fallDirection);
            }
        }

        #region AIRBORNE

        void OnJumpTriggered()
        {
            animator.SetFloat(hashVerticalSpeed, 1.0f);

            aerialState = AerialMovementState.Jumping;
            fallDirection = CalculateLocalInputDirection();

            if (IsIdleForwardJump())
            {
                switch (movementMode)
                {
                    case MotorMovementMode.Exploration:
                        NForwardSpeed = 1f;
                        break;
                    case MotorMovementMode.Strafe:
                        NLateralSpeed = input.Axis.x;
                        NForwardSpeed = input.Axis.y;
                        break;
                }
            }
        }

        bool IsIdleForwardJump()
        {
            return !input.HasAxisInput;
        }

        void OnJumpTerminated()
        {
            //TODO:
        }

        //void UpdateFallForwardSpeed()
        //{
        //    float maxFallForward = fallingForwardSpeed;
        //    float target = maxFallForward * Mathf.Clamp01(input.Axis.magnitude);
        //    float time =

        //    NForwardSpeed = Mathf.Sign(NForwardSpeed) *;
        //}

        #endregion

        #region STRAFE

        void OnStrafeStarted()
        {
            movementMode = MotorMovementMode.Strafe;
        }

        void OnStrafeEnded()
        {
            movementMode = MotorMovementMode.Exploration;
        }

        #endregion

        #region ROTATION/LOOK

        void SetLookDirection()
        {
            switch (movementMode)
            {
                case MotorMovementMode.Exploration:
                    SetExplorationLookDirection();
                    break;
                case MotorMovementMode.Strafe:
                    SetStrafeLookDirection();
                    break;
            }

            if (Mathf.Approximately(input.LookAt.magnitude, 0f))
            {
                NTurningSpeed = Mathf.Lerp(NTurningSpeed, 0f, Time.deltaTime * turnSpeedDecay);
            }
        }

        void SetExplorationLookDirection()
        {
            if (!input.HasAxisInput)
            {
                NTurningSpeed = 0f;
                return;
            }

            float turnSpeed = character.isGrounded ? Mathf.Deg2Rad * groundTurnSpeed : aerialTurnSpeed;

            Quaternion targetRotation = CalculateTargetRotation(input.LocalDirection);
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            SetNormalizedTurningSpeed(transform.rotation, newRotation);

            transform.rotation = newRotation;
        }

        void SetStrafeLookDirection()
        {
            Quaternion targetRotation = CalculateTargetRotation(Vector3.forward);
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, groundTurnSpeed * Time.deltaTime);

            SetNormalizedTurningSpeed(transform.rotation, newRotation);

            transform.rotation = newRotation;
        }

        Quaternion CalculateTargetRotation(Vector3 direction)
        {
            Vector3 flatForward = mainCamera.transform.forward;
            flatForward.y = 0f;
            flatForward.Normalize();

            Quaternion cameraToInputOffset = Quaternion.FromToRotation(Vector3.forward, direction);
            cameraToInputOffset.eulerAngles = new Vector3(0f, cameraToInputOffset.eulerAngles.y, 0f);

            return Quaternion.LookRotation(cameraToInputOffset * flatForward);
        }

        //TODO: ???
        void SetNormalizedTurningSpeed(Quaternion currentRotation, Quaternion newRotation)
        {
            float currentY = currentRotation.eulerAngles.y;
            float newY = newRotation.eulerAngles.y;
            float difference = Wrap180(newY - currentY) / Time.deltaTime;

            float targetSpeed = Mathf.Clamp(difference / groundTurnSpeed * turnSpeedScale, -1, 1);

            NTurningSpeed = Mathf.Lerp(NTurningSpeed, targetSpeed, Time.deltaTime * turnSpeedGrowth);
        }

        // Wraps float between -180 and 180
        float Wrap180(float value)
        {
            value %= 360f;

            if (value < -180f)
                value += 360f;
            else if (value > 180f)
                value -= 360f;

            return value;
        }

        #endregion

        #region TRANSFORM

        void CalculateForwardMovement()
        {
            Vector2 axis = input.Axis;
            float magnitude = axis.magnitude;

            if (magnitude > 1)
            {
                axis.Normalize();
            }

            NLateralSpeed = 0f;
            NForwardSpeed = Mathf.Approximately(magnitude, 0f) ? 0f : magnitude;
        }

        void CalculateStrafeMovement()
        {
            NForwardSpeed = Mathf.Approximately(input.Axis.y, 0f) ? 0f : input.Axis.y;
            NLateralSpeed = Mathf.Approximately(input.Axis.x, 0f) ? 0f : input.Axis.x;
        }

        Vector3 CalculateLocalInputDirection()
        {
            Vector3 direction = new Vector3(input.Axis.x, 0f, input.Axis.y);

            return Quaternion.AngleAxis(mainCamera.transform.eulerAngles.y, Vector3.up) * direction.normalized;
        }

        #endregion

        #region ANIMATOR

        void InitAnimator()
        {
            animator = GetComponent<Animator>();

            hashForwardSpeed = Animator.StringToHash(AnimationParameters.forwardSpeed);
            hashLateralSpeed = Animator.StringToHash(AnimationParameters.lateralSpeed);
            hashTurningSpeed = Animator.StringToHash(AnimationParameters.turningSpeed);
            hashVerticalSpeed = Animator.StringToHash(AnimationParameters.verticalSpeed);
            hashIsStrafing = Animator.StringToHash(AnimationParameters.isStrafing);
        }

        void UpdateAnimator()
        {
            animator.SetFloat(hashTurningSpeed, NTurningSpeed);
            animator.SetBool(hashIsStrafing, input.IsStrafing);

            bool fullyGrounded = character.isGrounded && animatorState != AnimatorState.Landing;

            if (fullyGrounded || animatorState == AnimatorState.Falling)
            {
                animator.SetFloat(hashLateralSpeed, NLateralSpeed, 1f, Time.deltaTime);
                animator.SetFloat(hashForwardSpeed, NForwardSpeed, 1f, Time.deltaTime);
            }
        }

        #endregion

        #region FSM CALLBACKS

        public void OnGroundedAnimationEnter()
        {
            if (animatorState == AnimatorState.Falling)
            {
                animatorState = AnimatorState.Locomotion;
            }
            else if (animatorState == AnimatorState.Jump)
            {
                animatorState = AnimatorState.JumpLanding;
            }
        }

        public void OnLandAnimationExit()
        {
            if (character.isGrounded)
            {
                animatorState = AnimatorState.Locomotion;
            }

            //animator.speed = m_CachedAnimatorSpeed;
        }

        public void OnLandAnimationEnter(bool adjustAnimationSpeedBasedOnForwardSpeed)
        {
            animatorState = AnimatorState.Landing;

            //if (adjustAnimationSpeedBasedOnForwardSpeed)
            //{
            //    animator.speed = m_Configuration.landSpeedAsAFactorSpeed.Evaluate(NForwardSpeed);
            //}
        }

        public void OnJumpAnimationEnter()
        {
            animatorState = AnimatorState.Jump;
        }

        public void OnJumpAnimationExit()
        {
            if (animatorState == AnimatorState.Jump || animatorState == AnimatorState.JumpLanding)
            {
                animatorState = AnimatorState.Locomotion;
            }
        }

        public void OnFallingLoopAnimationEnter()
        {
            animatorState = AnimatorState.Falling;
        }

        #endregion

        #region ENUMS

        public struct AnimationStates
        {
            public const string locomotion = "Locomotion";
        }

        public struct AnimationParameters
        {
            public const string forwardSpeed = "ForwardSpeed";
            public const string lateralSpeed = "LateralSpeed";
            public const string verticalSpeed = "VerticalSpeed";
            public const string turningSpeed = "TurningSpeed";
            public const string isStrafing = "IsStrafing";
        }

        public enum AnimatorState
        {
            Locomotion,
            Jump,
            Falling,
            Landing,
            JumpLanding
        }

        public enum AerialMovementState
        {
            Grounded,
            Jumping,
            Falling
        }

        public enum GroundMovementState
        {
            Walking,
            Running
        }

        public enum MotorMovementMode
        {
            Exploration,
            Strafe
        }

        #endregion
    }
}
