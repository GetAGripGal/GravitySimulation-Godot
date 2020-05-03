using Godot;
using System;

public class PlayerController : GravityObject
{
    public override void _PhysicsProcess(float delta)
    {   
        UpdateVelocity(delta);
        MoveAndSlide(currentVelocity * delta);
    }
}
