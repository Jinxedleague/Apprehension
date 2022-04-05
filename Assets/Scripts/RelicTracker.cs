using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicTracker : MonoBehaviour
{
    public Sprite relicIcon;
    public Image relicImage;

    public Vector2 relicPosition
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }
}
