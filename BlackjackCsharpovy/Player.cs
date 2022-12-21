using Newtonsoft.Json;

namespace BlackjackCsharpovy;

public class Player : User
{
    public string Name;
    public int Money;

    internal Player(string name)
    {
        Name = name;
        ReadMoney();
    }

    // Check stats.json if exist
    // false -> create new one
    public void StatsExist()
    {
        try
        {
            JsonConvert.DeserializeObject(File.ReadAllText("stats.json"));
        }
        catch (Exception e)
        {
            File.Create("stats.json").Close();
            File.WriteAllText("stats.json", "{}");
        }
    }
    
    public void SaveStats()
    {
        StatsExist();
        string jsonString = File.ReadAllText("stats.json");
        dynamic jsonFile =  JsonConvert.DeserializeObject(jsonString);

        if (jsonFile.ContainsKey(Name))
        {
            jsonFile[Name] = Money;
        }
        else
        {
            jsonFile.Add(Name, Money);
        }
		
        jsonFile = JsonConvert.SerializeObject(jsonFile);
        File.WriteAllText("stats.json", jsonFile);
    }

    public void ReadMoney()
    {
        StatsExist();
        string jsonString = File.ReadAllText("stats.json");
        dynamic jsonFile =  JsonConvert.DeserializeObject(jsonString);
        Money = jsonFile.ContainsKey(Name) ? jsonFile[Name] == 0 ? 420 : jsonFile[Name] : 420;
    }
}