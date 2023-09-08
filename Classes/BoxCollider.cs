using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clockworks.Classes
{
    public class BoxCollider
    {
        public bool colision {get;set;}
        public Collision detailedCollision {get;set;}
        public string? colisionCallback {get;set;}
        public BoxCollider()
        {
            colision = false;
            detailedCollision = new Collision();
        }
        public bool checkColisions(Sprite sprite, Sprite sprt)
        {
            colision = false;
            detailedCollision.right = false;
            detailedCollision.left = false;
            detailedCollision.bottom = false;
            detailedCollision.top = false;
            if(
                (sprite.position.X + sprite.size.X >= sprt.position.X) &&
                (sprite.position.X <= sprt.position.X + sprt.size.X) &&
                (sprite.position.Y + sprite.size.Y >= sprt.position.Y) &&
                (sprite.position.Y <= sprt.position.Y + sprt.size.Y)
            )
            {
                // Calculate the overlap on both X and Y axes
                float overlapX = Math.Min(sprite.position.X + sprite.size.X, sprt.position.X + sprt.size.X) - Math.Max(sprite.position.X, sprt.position.X);
                float overlapY = Math.Min(sprite.position.Y + sprite.size.Y, sprt.position.Y + sprt.size.Y) - Math.Max(sprite.position.Y, sprt.position.Y);

                if (overlapX > overlapY)
                {
                    if (sprite.position.Y + sprite.size.Y > sprt.position.Y + sprt.size.Y)
                    {
                        // Collision is from the top
                        // Handle top collision here
                        detailedCollision.top = true;
                        //Console.WriteLine("top");
                    }
                    if (sprite.position.Y + sprite.size.Y < sprt.position.Y + sprt.size.Y)
                    {
                        // Collision is from the bottom
                        // Handle bottom collision here
                        detailedCollision.bottom = true;
                        //Console.WriteLine("bottom");
                    }
                }
                else
                {
                    if (sprite.position.X + sprite.size.X > sprt.position.X + sprt.size.X)
                    {
                        // Collision is from the left
                        // Handle left collision here
                        detailedCollision.left = true;
                        //Console.WriteLine("left");
                    }
                    if (sprite.position.X + sprite.size.X < sprt.position.X + sprt.size.X)
                    {
                        // Collision is from the right
                        // Handle right collision here
                        detailedCollision.right = true;
                        //Console.WriteLine("right");
                    }
                }

                colision = true;
                sprite.detailedCollisionInfo(detailedCollision);
            }

            return colision;
        }
    }
}