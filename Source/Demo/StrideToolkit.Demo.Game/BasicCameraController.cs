using System;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Engine.Events;
using Stride.Input;
using StrideToolkit.Engine;

namespace StrideToolkit.Demo
{
    /// <summary>
    /// A script that allows to move and rotate an entity through keyboard, mouse and touch input to provide basic camera navigation.
    /// </summary>
    /// <remarks>
    /// The entity can be moved using W, A, S, D, Q and E, arrow keys or dragging/scaling using multi-touch.
    /// Rotation is achieved using the Numpad, the mouse while holding the right mouse button, or dragging using single-touch.
    /// </remarks>
    public class BasicCameraController : SyncScript
    {
        private const float MaximumPitch = MathUtil.PiOverTwo * 0.99f;

        private Vector3 upVector;
        private Vector3 translation;
        private float yaw;
        private float pitch;

        private Entity target;

        private readonly EventReceiver<Entity> TargetAcquired
           = new EventReceiver<Entity>(CameraExtensionsDemo.EntitySelected);


        public Vector3 KeyboardMovementSpeed { get; set; } = new Vector3(5.0f);

        public Vector3 TouchMovementSpeed { get; set; } = new Vector3(40, 40, 20);

        public float SpeedFactor { get; set; } = 5.0f;

        public Vector2 KeyboardRotationSpeed { get; set; } = new Vector2(3.0f);

        public Vector2 MouseRotationSpeed { get; set; } = new Vector2(90.0f, 60.0f);

        public Vector2 TouchRotationSpeed { get; set; } = new Vector2(60.0f, 40.0f);



        public override void Start()
        {
            base.Start();

            // Default up-direction
            upVector = Vector3.UnitY;

           
        }

        public override void Update()
        {
            if(TargetAcquired.TryReceive(out var targetCandidate))
            {
                target = targetCandidate;
            }

            ProcessInput();
            UpdateTransform();
        }

        private void ProcessInput()
        {
            translation = Vector3.Zero;
            yaw = 0;
            pitch = 0;

            // Move with keyboard
            if (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.Up))
            {
                translation.Z = -KeyboardMovementSpeed.Z;
            }
            else if (Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.Down))
            {
                translation.Z = KeyboardMovementSpeed.Z;
            }

            if (Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.Left))
            {
                translation.X = -KeyboardMovementSpeed.X;
            }
            else if (Input.IsKeyDown(Keys.D) || Input.IsKeyDown(Keys.Right))
            {
                translation.X = KeyboardMovementSpeed.X;
            }

            if (Input.IsKeyDown(Keys.Q))
            {
                translation.Y = -KeyboardMovementSpeed.Y;
            }
            else if (Input.IsKeyDown(Keys.E))
            {
                translation.Y = KeyboardMovementSpeed.Y;
            }

            // Alternative translation speed
            if (Input.IsKeyDown(Keys.LeftShift) || Input.IsKeyDown(Keys.RightShift))
            {
                translation *= SpeedFactor;
            }


            if (target != null && Input.IsKeyDown(Keys.F))
            {
                Entity.Transform.LookAt(target.Transform);
                //Update();
            }
            else
            {
                // Rotate with keyboard
                if (Input.IsKeyDown(Keys.NumPad2))
                {
                    pitch = KeyboardRotationSpeed.X;
                }
                else if (Input.IsKeyDown(Keys.NumPad8))
                {
                    pitch = -KeyboardRotationSpeed.X;
                }

                if (Input.IsKeyDown(Keys.NumPad4))
                {
                    yaw = KeyboardRotationSpeed.Y;
                }
                else if (Input.IsKeyDown(Keys.NumPad6))
                {
                    yaw = -KeyboardRotationSpeed.Y;
                }

                // Rotate with mouse
                if (Input.IsMouseButtonDown(MouseButton.Right))
                {
                    Input.LockMousePosition();
                    Game.IsMouseVisible = false;

                    yaw = -Input.MouseDelta.X * MouseRotationSpeed.X;
                    pitch = -Input.MouseDelta.Y * MouseRotationSpeed.Y;
                }
                else
                {
                    Input.UnlockMousePosition();
                    Game.IsMouseVisible = true;
                }
            }

          
            
          
        }

        private void UpdateTransform()
        {
            var elapsedTime = Game.GetDeltaTime();

            translation *= elapsedTime;
            yaw *= elapsedTime;
            pitch *= elapsedTime;

            // Get the local coordinate system
            var rotation = Matrix.RotationQuaternion(Entity.Transform.Rotation);

            // Enforce the global up-vector by adjusting the local x-axis
            var right = Vector3.Cross(rotation.Forward, upVector);
            var up = Vector3.Cross(right, rotation.Forward);

            // Stabilize
            right.Normalize();
            up.Normalize();

            // Adjust pitch. Prevent it from exceeding up and down facing. Stabilize edge cases.
            var currentPitch = MathUtil.PiOverTwo - (float)Math.Acos(Vector3.Dot(rotation.Forward, upVector));
            pitch = MathUtil.Clamp(currentPitch + pitch, -MaximumPitch, MaximumPitch) - currentPitch;

            // Move in local coordinates
            //Entity.Transform.Position += Vector3.TransformCoordinate(translation, rotation);
            Entity.Transform.Translate(translation);

            if(target != null && Input.IsKeyDown(Keys.LeftCtrl))
            {
                Entity.Transform.LookAt(target.Transform,Game.GetDeltaTime() * 3);
            }
            else
            {
                // Yaw around global up-vector, pitch and roll in local space
                Entity.Transform.Rotation *= Quaternion.RotationAxis(right, pitch) * Quaternion.RotationAxis(upVector, yaw);
            }
            
        }
    }
}
