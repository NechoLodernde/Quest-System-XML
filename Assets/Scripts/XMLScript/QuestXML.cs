using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public class QuestXML : MonoBehaviour
{
    public static QuestXML QuestXMLInstance { get; private set; }

    public QuestDatabase questDB;

    [SerializeField] private string objectID, filepath, prevPlayerName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool CheckFileLocation()
    {
        if (File.Exists(filepath)) return true;
        else return false;
    }

    public void SetPrevPlayerName(string setName)
    {
        prevPlayerName = setName;
    }

    public void ResetPrevPlayerName()
    {
        prevPlayerName = "";
    }
}

public class QuestDatabase
{
    public List<QuestEntry> list = new();
}

public class QuestEntry 
{
    public string questID;
}
