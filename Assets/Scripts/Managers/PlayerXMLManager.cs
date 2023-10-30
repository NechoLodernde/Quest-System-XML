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
                    switch (playerItems.Name)
                    {
                        case "playerName":
                            prevName = playerItems.InnerText;
                            if (CheckNames(playerItems.InnerText, prevPlayerName))
                            {
                                newEntry.playerName = playerItems.InnerText;
                            }
                            break;
                        case "playerRole":
                            if (CheckNames(prevName, prevPlayerName))
                            {
                                newEntry.playerRole = GetRole(playerItems.InnerText);
                            }
                            break;
                        case "playerGender":
                            if (CheckNames(prevName, prevPlayerName))
                            {
                                newEntry.playerGender = GetGender(playerItems.InnerText);
                            }
                            break;
                        case "playerLevel":
                            if (CheckNames(prevName, prevPlayerName))
                            {
                                int.TryParse(playerItems.InnerText, out int level);
                                int pLevel = level;
                                newEntry.playerLevel = pLevel;
                            }
                            break;
                        case "playerExp":
                            if (CheckNames(prevName, prevPlayerName))
                            {
                                int.TryParse(playerItems.InnerText, out int exp);
                                int pExp = exp;
                                newEntry.playerExp = pExp;
                            }
                            break;
                        default:
                            Debug.Log("End of the line");
                            break;
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

    public void ModifyPlayerData(string pName, int pLevel, int pExp)
    {
        XmlDocument xmlDoc = new();
        if (CheckFileLocation())
        {
            xmlDoc.Load(filepath);

            XmlNodeList playerList = xmlDoc.GetElementsByTagName("PlayerEntry");

            foreach (XmlNode playerInfo in playerList)
            {
                XmlNodeList playerContent = playerInfo.ChildNodes;
                string prevName = "";

                foreach (XmlNode playerItems in playerContent)
                {
                    switch (playerItems.Name)
                    {
                        case "playerName":
                            prevName = playerItems.InnerText;
                            if (CheckNames(playerItems.InnerText, prevPlayerName))
                            {
                                prevName = playerItems.InnerText;
                            }
                            break;
                        case "playerLevel":
                            if (CheckNames(pName, prevName))
                            {
                                playerItems.InnerText = pLevel.ToString();
                            }
                            break;
                        case "playerExp":
                            if (CheckNames(pName, prevName))
                            {
                                playerItems.InnerText = pExp.ToString();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            xmlDoc.Save(filepath);
        }
        else
        {
            InitializeFile();
        }
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

    public void ResetPrevPlayerName()
    {
        prevPlayerName = "";
    }

    private bool CheckNames(string playerName, string prevPlayerName)
    {
        bool matches = false;

        if (prevPlayerName.Equals(playerName))
        {
            matches = true;
            return matches;
        }
        return matches;
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
