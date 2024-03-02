using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCustomizer : MonoBehaviour
{
    [SerializeField] private List<Outfit> m_outfits = new List<Outfit>();
}
[System.Serializable]
public class Outfit
{
    [SerializeField] private Color32 m_primary;
    [SerializeField] private Color32 m_secondary;
    [SerializeField] private Color32 m_tertiary;
}