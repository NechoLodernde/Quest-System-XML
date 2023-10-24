using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using TMPro;

public class PlayerXMLManager : MonoBehaviour
{
    public static PlayerXMLManager PlayerXMLInstance { get; private set; }

    [SerializeField] private string objectID;
    [SerializeField] private string filepath;

    private void Awake()
    {
        objectID = name + transform.position.ToString();
        filepath = Application.dataPath + @"/StreamingAssets/XML/player_data.xml";

        for (int i = 0; i < FindObjectsOfType<PlayerXMLManager>().Length; i++)
        {
            if (FindObjectsOfType<PlayerXMLManager>()[i] != this)
            {
                if (FindObjectsOfType<PlayerXMLManager>()[i].objectID == objectID)
                {
                    Destroy(gameObject);
                }
            }
        }

        PlayerXMLInstance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void InitializeFile()
    {
        XmlWriterSettings settings = new();
        settings.Encoding = System.Text.Encoding.GetEncoding("UTF-8");
        settings.Indent = true;
        settings.IndentChars = ("     ");
        settings.OmitXmlDeclaration = false;

        XmlWriter writer = XmlWriter.Create(filepath, settings);
        writer.WriteStartDocument();
        writer.WriteStartElement("PlayerDatabase");
        writer.WriteEndElement();
        writer.WriteEndDocument();
        writer.Flush();
    }
}

[System.Serializable]
public class PlayerEntry
{
    public string playerName;
    public Roles playerRole;
    public int playerLevel;
    public int playerExp;
}

public enum Roles
{
    Warrior, 
    Wizard, 
    Healer,
    Hero
}
