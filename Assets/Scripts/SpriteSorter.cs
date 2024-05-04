using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSorter : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SpriteRenderer sRenderer;

    // Start is called before the first frame update
    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sRenderer.sortingOrder = -(int)(transform.position.y * 10);
    }
}
