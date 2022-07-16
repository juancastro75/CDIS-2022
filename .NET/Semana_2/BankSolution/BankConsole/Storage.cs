using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BankConsole;

public static class Storage
{
    static string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\users.json";

    public static void AddUser(User user)
    {
        string json = "", usersInFile = "";

        if(File.Exists(filePath))
        { 
            usersInFile = File.ReadAllText(filePath);
        }
        
        var ListUsers = JsonConvert.DeserializeObject<List<Object>>(usersInFile);

        if(ListUsers == null)
        {
            ListUsers = new List<Object>();
        }
        ListUsers.Add(user);

        var settings = new JsonSerializerSettings();
        settings.Formatting = Formatting.Indented;
        
        json = JsonConvert.SerializeObject(ListUsers, settings);
        
        File.WriteAllText(filePath, json);
    }

    public static List<User> GetNewUsers()
    {
        string usersInFile = "";
        var ListUsers = new List<User>();

        if(File.Exists(filePath))
            usersInFile = File.ReadAllText(filePath);
        
        var ListObject = JsonConvert.DeserializeObject<List<Object>>(usersInFile);

        if(ListObject == null)
            return ListUsers;

        foreach (object obj in ListObject)
        {
            User newUser;
            JObject user = (JObject)obj;

            if(user.ContainsKey("TaxRegime"))
                newUser = user.ToObject<Client>();
            else
                newUser = user.ToObject<Employee>();

            ListUsers.Add(newUser);
        }

        var newUserList = ListUsers.Where(user => user.GetRegisterDate().Date.Equals(DateTime.Today)).ToList();

        return newUserList;
    }

    public static List<User> GetUsers()
    {
        string usersInFile = "";
        var ListUsers = new List<User>();

        if(File.Exists(filePath))
            usersInFile = File.ReadAllText(filePath);
        
        var ListObject = JsonConvert.DeserializeObject<List<Object>>(usersInFile);

        if(ListObject == null)
            return ListUsers;

        foreach (object obj in ListObject)
        {
            User newUser;
            JObject user = (JObject)obj;

            if(user.ContainsKey("TaxRegime"))
                newUser = user.ToObject<Client>();
            else
                newUser = user.ToObject<Employee>();

            ListUsers.Add(newUser);
        }

        var UserList = ListUsers.ToList();

        return UserList;
    }

    public static string DeleteUser(int ID)
    {
        string usersInFile = "";
        var ListUsers = new List<User>();

        if(File.Exists(filePath))
            usersInFile = File.ReadAllText(filePath);
        
        var ListObjects = JsonConvert.DeserializeObject<List<Object>>(usersInFile);

        if(ListObjects == null)
            return "There are no users in the file.";

        foreach (object obj in ListObjects)
        {
            User newUser;
            JObject user = (JObject)obj;

            if(user.ContainsKey("TaxRegime"))
                newUser = user.ToObject<Client>();
            else
                newUser = user.ToObject<Employee>();

            ListUsers.Add(newUser);
        }

        var userToDelete = ListUsers.Where(user => user.GetID() == ID).Single();

        ListUsers.Remove(userToDelete);

        var settings = new JsonSerializerSettings();
        settings.Formatting = Formatting.Indented;
        
        string json = JsonConvert.SerializeObject(ListUsers, settings);
        
        File.WriteAllText(filePath, json);

        return "Success";
    }
} 