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

    [SerializeField] private string objectID, filepath, prevQuestID;

    private void Awake()
    {
        objectID = name + transform.position.ToString();
        filepath = Application.dataPath + @"/StreamingAssets/XML/quest_data.xml";
        QuestXMLInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void InitializeFile()
    {
        XmlWriterSettings settings = new();
        settings.Encoding = System.Text.Encoding.GetEncoding("UTF-8");
        settings.Indent = true;
        settings.IndentChars = ("     ");
        settings.OmitXmlDeclaration = true;

        XmlWriter writer = XmlWriter.Create(filepath, settings);
        writer.WriteStartDocument();
        writer.WriteStartElement("QuestDatabase");
        writer.WriteEndElement();
        writer.WriteEndDocument();
        writer.Flush();
    }

    public void ResetData()
    {
        XmlDocument xmlDoc = new();
        
        if (CheckFileLocation())
        {
            xmlDoc.Load(filepath);
            XmlElement elmRoot = xmlDoc.DocumentElement;
            elmRoot.RemoveAll();
            xmlDoc.Save(filepath);
        }
    }

    public void QuestIDGenerator()
    {

    }

    public void ClearList()
    {
        questDB.list.Clear();
    }

    private bool CheckFileLocation()
    {
        if (File.Exists(filepath)) return true;
        else return false;
    }

    public void SetPrevQuestID(string setID)
    {
        prevQuestID = setID;
    }

    public void ResetPrevQuestID()
    {
        prevQuestID = "";
    }
}

public class QuestDatabase
{
    public List<QuestEntry> list = new();
}

public class QuestEntry 
{
    public string questID;
    public QuestType questType;
    public string questTitle;
    public string questDescription;
    public int experienceReward;
}

public enum QuestType
{
    Kill,
    Gathering,
    Interacting
}
