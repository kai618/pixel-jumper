using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JumpType { GROUND_NOT_VERTICAL, WALL_TO_LEFT, WALL_TO_RIGHT, GROUND_VERTICAL };
public class Jump
{
    public Vector2 velocity { get; private set; }
    public JumpType type { get; private set; }
    public float angle { get; private set; }

    public Jump(Vector2 velocity, float angle,
    JumpType type)
    {
        this.velocity = velocity;
        this.type = type;
        this.angle = angle;
    }
}
