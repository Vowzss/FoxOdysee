using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTextured : MonoBehaviour
{
    public Texture2D curorsTexture;
    private void Start()
    {
        Cursor.SetCursor(curorsTexture, Vector2.zero, CursorMode.ForceSoftware);
    }
}
