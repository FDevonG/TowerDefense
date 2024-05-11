using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.grey;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);

    TMP_Text label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    void Awake(){
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TMP_Text>();
        label.enabled = false;

        DisplayCoordinates();
    }

    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateTileName();
            label.enabled = true;
        }
        SetLabelColor();
        if(Input.GetKeyDown(KeyCode.C))
            ToggleCoordinates();
    }

    private void DisplayCoordinates()
    {
        if(gridManager == null) return;
        coordinates.x = (int)MathF.Round((int)transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = (int)MathF.Round((int)transform.position.z / gridManager.UnityGridSize);

        label.text = $"{coordinates.x},{coordinates.y}";
    }

    private void UpdateTileName()
    {
        transform.parent.name = coordinates.ToString();
    }

    private void SetLabelColor()
    {
        if(gridManager == null) return;

        Node node = gridManager.GetNode(coordinates);
        if(node == null) return;

        if(!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if(node.isPath)
        {      
            label.color = pathColor;
        }
        else if(node.isExplored)
        {
            label.color = exploredColor;
        }
    }

    void ToggleCoordinates()
    {
        label.enabled = !label.enabled;
    }
}
