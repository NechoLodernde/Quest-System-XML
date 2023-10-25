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

    public PlayerDatabase playerDB;

    [SerializeField] private string objectID;
    [SerializeField] private string filepath, prevPlayerName;

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

    public void SavePlayerData()
    {
        XmlDocument xmlDoc = new();
        if (CheckFileLocation())
        {
            xmlDoc.Load(filepath);
            XmlElement elmRoot = xmlDoc.DocumentElement;
            XmlElement elmNew = xmlDoc.CreateElement("PlayerEntry");
            XmlElement playerName = xmlDoc.CreateElement("playerName");
            XmlElement playerRole = xmlDoc.CreateElement("playerRole");
            XmlElement playerGender = xmlDoc.CreateElement("playerGender");
            XmlElement playerLevel = xmlDoc.CreateElement("playerLevel");
            XmlElement playerExp = xmlDoc.CreateElement("playerExp");

            playerName.InnerText = playerDB.list.ToArray()[0].playerName;
            playerRole.InnerText = playerDB.list.ToArray()[0].playerRole.ToString();
            playerGender.InnerText = playerDB.list.ToArray()[0].playerGender.ToString();
            playerLevel.InnerText = playerDB.list.ToArray()[0].playerLevel.ToString();
            playerExp.InnerText = playerDB.list.ToArray()[0].playerExp.ToString();

            elmNew.AppendChild(playerName);
            elmNew.AppendChild(playerRole);
            elmNew.AppendChild(playerGender);
            elmNew.AppendChild(playerLevel);
            elmNew.AppendChild(playerExp);
            elmRoot.AppendChild(elmNew);

            xmlDoc.Save(filepath);
        }
        else
        {
            InitializeFile();
        }
    }

    public void LoadPlayerData()
    {
        XmlDocument xmlDoc = new();
        if (CheckFileLocation())
        {
            xmlDoc.Load(filepath);

            XmlNodeList playerList = xmlDoc.GetElementsByTagName("PlayerEntry");

            foreach (XmlNode playerInfo in playerList)
            {
                XmlNodeList playerContent = playerInfo.ChildNodes;
                PlayerEntry newEntry = new();
                string prevName = "";
                foreach (XmlNode playerItems in playerContent)
                {
                    if (playerItems.Name.Equals("playerName"))
                    {
                        prevName = playerItems.InnerText;
                        if (playerItems.InnerText.Equals(prevPlayerName))
                        {
                            newEntry.playerName = playerItems.InnerText;
                        }
                    }
                    else if (playerItems.Name.Equals("playerRole"))
                    {
                        if (prevName.Equals(prevPlayerName)){
                            newEntry.playerRole = GetRole(playerItems.InnerText);
                        }
                    }
                    else if (playerItems.Name.Equals("playerGender"))
                    {
                        if (prevName.Equals(prevPlayerName))
                        {
                            newEntry.playerGender = GetGender(playerItems.InnerText);
                        }
                    }
                    else if (playerItems.Name.Equals("playerLevel"))
                    {
                        if (prevName.Equals(prevPlayerName))
                        {
                            int.TryParse(playerItems.InnerText, out int level);
                            int pLevel = level;
                            newEntry.playerLevel = pLevel;
                        }
                    }
                    else if (playerItems.Name.Equals("playerExp"))
                    {
                        if (prevName.Equals(prevPlayerName))
                        {
                            int.TryParse(playerItems.InnerText, out int exp);
                            int pExp = exp;
                            newEntry.playerExp = pExp;
                        }
                    }
                }
                playerDB.list.Add(newEntry);
            }
        }
        else
        {
            InitializeFile();
        }
    }

    public void ModifyPlayerData(string pName, string pRole, string pGender, int pLevel, int pExp)
    {

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

    private bool CheckFileLocation()
    {
        if (File.Exists(filepath))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<string> GetRolesData()
    {
        List<string> list = new();

        foreach (Roles role in System.Enum.GetValues(typeof(Roles)))
        {
            list.Add(role.ToString());
        }

        return list;
    }

    public Roles GetRole(string input)
    {
        Roles baseRole = Roles.Warrior;
        foreach (Roles role in System.Enum.GetValues(typeof(Roles)))
        {
            if (input.Equals(role.ToString()))
            {
                baseRole = role;
                return baseRole;
            }
        }

        return baseRole;
    }

    public Genders GetGender(string input)
    {
        Genders baseGender = Genders.Male;
        foreach (Genders gender in System.Enum.GetValues(typeof(Genders)))
        {
            if (input.Equals(gender.ToString()))
            {
                baseGender = gender;
                return baseGender;
            }
        }

        return baseGender;
    }

    public void SetPrevPlayerName(string setName)
    {
        prevPlayerName = setName;
    }
}

[System.Serializable]
public class PlayerDatabase
{
    public List<PlayerEntry> list = new();
}

[System.Serializable]
public class PlayerEntry
{
    public string playerName;
    public Roles playerRole;
    public Genders playerGender;
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

public enum Genders
{
    Male,
    Female
}
