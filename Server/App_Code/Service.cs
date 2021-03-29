using System;
using System.Collections.Generic;
using System.IO;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    /// <summary>
    /// Full path of credentials.csv
    /// </summary>
    public string CredentialsFilePath { get; set; }
    
    /// <summary>
    /// Default Constructor
    /// </summary>
    public Service()
    {
        CredentialsFilePath = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "credentials.csv");   
    }

    /// <summary>
    /// Method for authorization/authentication of a user(username != admin)
    /// </summary>
    /// <param name="userName">received username from client</param>
    /// <param name="password">received password from client</param>
    /// <returns>true if login is successful; false otherwise</returns>
    public bool MakeLogin(string userName, string password)
    {
        string decodedPassword = DecodeFrom64(password);

        string[] lines = File.ReadAllLines(CredentialsFilePath);
        foreach (string line in lines)
        {
            string[] credentials = line.Split(',');
            if (userName == credentials[0] && decodedPassword == credentials[1])
                return true;
        }
        return false;
    }

    /// <summary>
    /// Retrieves a list of all users from credentials.csv
    /// </summary>
    /// <returns>List of all user names</returns>
    public List<string> GetAllUsers()
    {
        List<string> result = new List<string>();
        
        string[] lines = File.ReadAllLines(CredentialsFilePath);
        foreach (string line in lines)
        {
            string[] credentials = line.Split(',');
            result.Add(credentials[0]);
        }
        return result;
    }

    /// <summary>
    /// Deletes users with specific usernames from credentials.csv
    /// </summary>
    /// <param name="usersToDelete">List of users to delete</param>
    /// <returns>Number of deleted users</returns>
    public int DeleteUsers(List<string> usersToDelete)
    {
        string[] lines = File.ReadAllLines(CredentialsFilePath);
        List<string> updatedLines = new List<string>();
        int deletedUsers = 0;
        foreach (string line in lines)
        {
            string[] credentials = line.Split(',');
            if (!usersToDelete.Contains(credentials[0]))
                updatedLines.Add(line);
            else
                deletedUsers++;
        }
        File.WriteAllText(CredentialsFilePath, string.Empty);
        foreach (var item in updatedLines)
        {
            File.AppendAllText(CredentialsFilePath, item + "\n");
        }
        return deletedUsers;
    }

    /// <summary>
    /// Decodes an input string from base64(used for password decoding in server side)
    /// </summary>
    /// <param name="encodedData">string to decode</param>
    /// <returns>decoded string</returns>
    private string DecodeFrom64(string encodedData)
    {
        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        System.Text.Decoder utf8Decode = encoder.GetDecoder();
        byte[] todecode_byte = Convert.FromBase64String(encodedData);
        int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        char[] decoded_char = new char[charCount];
        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        string result = new String(decoded_char);
        return result;
    }
}
