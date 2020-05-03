using Godot;
using System;

public class GravityObject : KinematicBody
{
    Universe Universe;
    float G;
    Vector3 F = new Vector3(0,0,0);
    [Export] float M = 0;

    public float ClosestDistance;
    public GravityObject ClosestObject;

    [Export] Vector3 initialVelocity;
    public Vector3 currentVelocity;

    KinematicBody Planets;
    public void _Init()
    {   
        ClosestDistance = 0;
        Universe = (Universe)GetTree().Root.GetNode("Universe");
        G = Universe.G;
        currentVelocity = initialVelocity;
    }
    public override void _PhysicsProcess(float delta)
    {
        //UpdateVelocity(delta);
    }

    public void UpdateVelocity(float delta)
    {
        foreach(GravityObject member in GetTree().GetNodesInGroup("Planets"))
        {
            if(member != this)
            {   
                
                float distance = Translation.DistanceSquaredTo(member.Translation);
                Vector3 direction = (member.Translation - Translation).Normalized();
                
                F = (direction * G * M * (float)member.Get("M")) / distance;
                Vector3 acceleration = F/M;
                currentVelocity += acceleration * delta;

                if(ClosestDistance > Translation.DistanceTo(member.Translation))
                {   
                    GD.Print("Updating distance");
                    ClosestDistance = Translation.DistanceSquaredTo(member.Translation);
                    ClosestObject = member;
                }
            }
        }
    }


}

