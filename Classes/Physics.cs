using System.Numerics;
using clockworks.Methods;

namespace clockworks.Classes
{
    public class Physics
    {
        private static void collisionResponse(Sprite sprite, Sprite otherSprite, BoxCollider collider)
        {
            if(!Functions.CheckMaskCollision(sprite.mask, sprite.position, otherSprite.mask, otherSprite.position)) return;
            float elasticity = (sprite.phys.elasticity + otherSprite.phys.elasticity) / 2;
            float correctionFactor = (sprite.phys.positionCorrectionFactor + otherSprite.phys.positionCorrectionFactor) / 2;
            // Calculate relative velocity
            Vector2 relativeVelocity = otherSprite.phys.velocity - sprite.phys.velocity;
            // Calculate collision normal
            Vector2 normal = Vector2.Normalize(otherSprite.position - sprite.position);
            // Calculate impulse
            float impulse = Vector2.Dot(relativeVelocity, normal);
            impulse *= (elasticity * 10);
            Vector2 spriteToOther = otherSprite.position - sprite.position;
            // Calculate the penetration depth along the collision normal
            float penetrationDepth = Vector2.Dot(spriteToOther, normal);
            Vector2 oldSpritePos = sprite.position;
            Vector2 oldOtherSpritePos = otherSprite.position;

            if (sprite.phys.use && !sprite.phys.bottomCollision) sprite.position -= (normal * (penetrationDepth * correctionFactor)) / (10000 / penetrationDepth);
            if (otherSprite.phys.use && !otherSprite.phys.bottomCollision) otherSprite.position += (normal * (penetrationDepth * correctionFactor)) / (10000 / penetrationDepth);

            // Handle bottom collision
            if (sprite.phys.velocity.Y > 0) {
                sprite.phys.velocity = new Vector2(sprite.phys.velocity.X, 0);; // Stop vertical movement
            }

            if (otherSprite.phys.velocity.Y > 0) {
                otherSprite.phys.velocity = new Vector2(otherSprite.phys.velocity.X, 0); // Stop vertical movement
            }

            // Update velocities based on impulse and masses
            if (sprite.phys.use && !sprite.phys.bottomCollision) {
                //sprite.phys.velocity += impulse * normal / sprite.phys.mass;
            }

            if (otherSprite.phys.use && !otherSprite.phys.bottomCollision) {
                //otherSprite.phys.velocity -= impulse * normal / otherSprite.phys.mass;
            }

            sprite.detailedCollisionInfo(collider.detailedCollision);
        }
        public static void ColisionThread(Sprite sprite, BoxCollider collider, SpriteRegister spriteRegister)
        {
            for (int j = 0; j < spriteRegister.reg.Count; j++) // Start from i + 1 to avoid redundant collision checks
            {
                Sprite otherSprite = spriteRegister.reg[j];
                if (collider.checkColisions(sprite, otherSprite) && sprite.ID != otherSprite.ID)
                {
                    collisionResponse(sprite, otherSprite, collider);
                }
            }
        }
        public static void FrictionThread(Sprite sprite, float friction)
        {
            if(((sprite.phys.velocity.X < friction) && (sprite.phys.velocity.X > 0)) || ((sprite.phys.velocity.X > -friction) && (sprite.phys.velocity.X < 0)))
            {
                sprite.phys.velocity = new Vector2(0, sprite.phys.velocity.Y);
            }
            if(((sprite.phys.velocity.Y < friction) && (sprite.phys.velocity.Y > 0)) || ((sprite.phys.velocity.Y > -friction) && (sprite.phys.velocity.Y < 0)))
            {
                sprite.phys.velocity = new Vector2(sprite.phys.velocity.X, 0);
            }
            if(sprite.phys.velocity.X > 0)
            {
                sprite.phys.velocity = new Vector2(sprite.phys.velocity.X - friction, sprite.phys.velocity.Y);
            }
            else if(sprite.phys.velocity.X < 0)
            {
                sprite.phys.velocity = new Vector2(sprite.phys.velocity.X + friction, sprite.phys.velocity.Y);
            }
            if(sprite.phys.velocity.Y > 0)
            {
                sprite.phys.velocity = new Vector2(sprite.phys.velocity.X, sprite.phys.velocity.Y - friction);
            }
            else if(sprite.phys.velocity.Y < 0)
            {
                sprite.phys.velocity = new Vector2(sprite.phys.velocity.X, sprite.phys.velocity.Y + friction);
            }
        }
    }
}