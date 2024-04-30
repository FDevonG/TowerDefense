using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

[ExecuteAlways]
public class CoordinateLabler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.grey;

    TMP_Text label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;

    void Awake(){
        label = GetComponent<TMP_Text>();
        label.enabled = false;
        
        DisplayCoordinates();
        waypoint = GetComponentInParent<Waypoint>();
    }

    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateTileName();
        }
        ColorCoordinates();
        if(Input.GetKeyDown(KeyCode.C))
            ToggleCoordinates();
    }

    private void DisplayCoordinates()
    {
        coordinates.x = (int)MathF.Round((int)transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = (int)MathF.Round((int)transform.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = $"{coordinates.x},{coordinates.y}";
    }

    private void UpdateTileName()
    {
        transform.parent.name = coordinates.ToString();
    }

    private void ColorCoordinates()
    {
        if(waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
        }
    }

    void ToggleCoordinates()
    {
        label.enabled = !label.enabled;
    }
}
