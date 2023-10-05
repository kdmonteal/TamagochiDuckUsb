using Firebase.Database;

using UnityEngine;
using UnityEngine.UI;

public class DataBaseManager : MonoBehaviour
{
    public InputField InputFieldName;
    public InputField InputFieldAge;

    private string userID;
    private DatabaseReference dbReference;

    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CreateUser()
    {
        User newUser = new User(InputFieldName.text, int.Parse(InputFieldAge.text));
        string json = JsonUtility.ToJson(newUser);

        object value = dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }
}