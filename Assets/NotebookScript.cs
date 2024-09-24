using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookScript : MonoBehaviour
{
    public void Collect()
    {
        GameManager.current.notebooks += 1;
        gameObject.SetActive(false);
    }
}
