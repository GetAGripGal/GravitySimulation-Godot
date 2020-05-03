using Godot;
using System;

public class TestPlanet : GravityObject
{

    public override void _Ready()
    {
        _Init();
    }
    public override void _PhysicsProcess(float delta)
    {   
        
        UpdateVelocity(delta);
        MoveAndSlide(currentVelocity * delta);
    }
}
