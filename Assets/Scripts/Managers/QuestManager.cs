using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager QuestManagerInstance { get; private set; }

    [SerializeField] private string objectID, prevPlayerName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
