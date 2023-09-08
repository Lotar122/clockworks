using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace clockworks.Classes
{
    public class PhysicsObject
    {
        public float mass {get;set;}
        public Vector2 acceleration {get;set;}
        public Vector2 force {get;set;}
        public Vector2 velocity {get;set;}
        public float elasticity {get;set;}
        public float positionCorrectionFactor {get;set;}
        public float friction {get;set;}
        public bool use {get;set;}
        public bool bottomCollision {get;set;}
        public PhysicsObject(float _mass)
        {
            use = true;
            mass = _mass;
            acceleration = Vector2.Zero;
            force = Vector2.Zero;
            velocity = Vector2.Zero;
            elasticity = 0.7f;
            positionCorrectionFactor = 0.1f;
        }
        public PhysicsObject(float _mass, float _elasticity)
        {
            use = true;
            mass = _mass;
            acceleration = Vector2.Zero;
            force = Vector2.Zero;
            velocity = Vector2.Zero;
            elasticity = _elasticity;
            positionCorrectionFactor = 0.1f;
        }
        public PhysicsObject(float _mass, float _elasticity, float _positionCorrectionFactor)
        {
            use = true;
            mass = _mass;
            acceleration = Vector2.Zero;
            force = Vector2.Zero;
            velocity = Vector2.Zero;
            elasticity = _elasticity;
            positionCorrectionFactor = _positionCorrectionFactor;
        }
        public void Update()
        {
            if(!use) return;
            if(!bottomCollision) acceleration = (force + new Vector2(0, mass * 0.5f)) / mass;
            if(bottomCollision) 
            {
                if(force.Y > 0) acceleration = new Vector2(force.X, 0) / mass;
                else acceleration = force / mass;
            }
            velocity += acceleration;
        }
        public void setForce(Vector2 _force)
        {
            force = _force;
        }
        public void setFriction(float _friction)
        {
            friction = _friction;
        }
        public void setFriction(double _friction)
        {
            friction = (float)_friction;
        }
        public void setFriction(int _friction)
        {
            friction = (float)_friction;
        }
    }
}