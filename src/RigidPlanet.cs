using Godot;
using System;

public class RigidPlanet : RigidGravityObject
{      
    public override void _Ready()
    {
        _Init();
    }
    public override void _PhysicsProcess(float delta)
    {   
        
        UpdateVelocity(delta);
        AddForce(currentVelocity * delta, Translation);
    }

}