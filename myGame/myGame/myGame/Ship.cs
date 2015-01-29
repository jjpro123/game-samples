using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceGame
{
    class Ship
    {

        public Model Model;
        public Matrix[] Transforms;

        //Position of the model in world space
        public Vector3 Position = Vector3.Zero;

        //Velocity of the model, applied each frams to the model's position
        public Vector3 Velocity = Vector3.Zero;

        public Matrix RotationMatrix = Matrix.CreateRotationX(MathHelper.PiOver2);

        private float rotation;

        public float Rotation
        {
            get { return rotation; }
            set 
            {
                float newVal = value;
                while(newVal >= MathHelper.TwoPi)
                {
                    newVal -= MathHelper.TwoPi;
                }
                while (newVal < 0)
                {
                    newVal += MathHelper.TwoPi;
                }

                if(rotation != newVal)
                {
                    rotation = newVal;
                    RotationMatrix = Matrix.CreateRotationY(rotation);
                }
            }
        }

        public void Update(GamePadState controllerState)
        {
         
            // Rotate the model using the left thumbstick, and scale it down.
            Rotation -= controllerState.ThumbSticks.Left.X * 0.10f;

            // Finally, add this vector to our velocity.
            Velocity += RotationMatrix.Forward * 1.0f *
                controllerState.Triggers.Right;
        }

        public void Update(KeyboardState currentKeyState)
        {
            if (currentKeyState.IsKeyDown(Keys.A))
                Rotation += 0.10f;
            else if (currentKeyState.IsKeyDown(Keys.D))
                Rotation -= 0.10f;

            if (currentKeyState.IsKeyDown(Keys.W))
            {
                Velocity += RotationMatrix.Forward * 2.0f; // 2.0f is too high
            }
            else if (currentKeyState.IsKeyDown(Keys.S))
            {
                Velocity += RotationMatrix.Backward  * 2.0f; // 2.0f is too high
            }
        }
    }
}
