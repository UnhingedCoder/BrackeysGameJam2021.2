using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectiles
{
    public override void AssignController(PlayerController controller_)
    {
        controller = controller_;
    }
}
