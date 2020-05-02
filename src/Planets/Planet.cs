using Godot;
using System;

public class Planet : KinematicBody
{   
    Universe Universe;
    float G;
    Vector3 F = new Vector3(0,0,0);
    [Export] float M = 0;

    [Export] Vector3 initialVelocity;
    private Vector3 currentVelocity;

    KinematicBody Planets;
    public override void _Ready()
    {   
        Universe = (Universe)GetTree().Root.GetNode("Universe");
        G = Universe.G;
        currentVelocity = initialVelocity;
    }
    public override void _PhysicsProcess(float delta)
    {
        UpdateVelocity(delta);
        MoveAndSlide(currentVelocity * delta);
    }

    public void UpdateVelocity(float delta)
    {
        foreach(KinematicBody member in GetTree().GetNodesInGroup("Planets"))
        {
            if(member != this)
            {   
                float distance = Translation.DistanceSquaredTo(member.Translation);
                Vector3 direction = (member.Translation - Translation).Normalized();
                
                F = (direction * G * M * (float)member.Get("M")) / distance;
                Vector3 acceleration = F/M;
                currentVelocity += acceleration * delta;
            }
        }
    }


}