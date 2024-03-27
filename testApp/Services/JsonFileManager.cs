using Newtonsoft.Json;
using testApp;


// this class is responsble for parsing c# object of type - TranlationKeyValue
// and adding it to a json file. 

public class JsonAppender
{
    private string _filePath;

    public JsonAppender(string ID)
    {
        _filePath = GetFullPath("Data/JsonData/" + ID) + ".json";
    }

    // side note: using SerializeObject and DeserializeObject for edting the jsin whould be the safer way,
    // But I'm not sure I fully understand working with dynamic objects in c#, as we do not know the stracture of the object,
    // as we store it in a key_value json, not an array(to be able to add languages dynamically).
    public void AppendJson(TranlationKeyValue translationKeyValue)
    {
        try
        {
            // just some string manipulations for creating a valid json.
            string newJsonData = GetJsonString(translationKeyValue).Trim();
            string existingJsonData = File.ReadAllText(_filePath);
            string combinedJsonData = existingJsonData.Trim();
            if (combinedJsonData.Length > 0)
                combinedJsonData = combinedJsonData.Substring(0, combinedJsonData.Length - 1);

            if (existingJsonData.Length > 0)
                combinedJsonData += ",\n";

            if (combinedJsonData.Length > 0)
                newJsonData = newJsonData.Substring(1);

            combinedJsonData += newJsonData;
            File.WriteAllText(_filePath, combinedJsonData);
        }
        catch
        {
            throw new Exception("Error while editing JSON. ");
        }
    }

    // when creating a new app, we need to create a file to store further keys deployments.
    public void CreateEmptyFile()
    {
        try
        {
            File.WriteAllText(_filePath, "");
        }
        catch
        {
            throw new Exception("Error while creating JSON. ");
        }
    }

    // used for getting the current data from the file (for exporting to the client)
    public string getFileString()
    {
        string existingJsonData = File.ReadAllText(_filePath);
        return existingJsonData;
    }

    // helpers
    private static string GetFullPath(string relativePath)
    {
        string baseDirectory = Directory.GetCurrentDirectory();
        string fullPath = Path.Combine(baseDirectory, relativePath);
        return fullPath;
    }

    // converting this type TranlationKeyValue to a json string (I wish I was using JS now)
    private string GetJsonString(TranlationKeyValue translationKeyValue)
    {
        // Create a dictionary to hold language-value pairs
        var languageValues = new Dictionary<string, string>();
        foreach (var item in translationKeyValue.ValueArray)
        {
            languageValues[item.language] = item.value;
        }

        // Create a dictionary with the key and the nested language-value pairs
        var result = new Dictionary<string, Dictionary<string, string>>();
        result[translationKeyValue.Key] = languageValues;

        // Convert to JSON string
        return JsonConvert.SerializeObject(result, Formatting.Indented);
    }

}
