using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Save : MonoBehaviour
{
    public static Save instance;
    public SaveData saveData;
    public string path = Application.dataPath + Path.DirectorySeparatorChar + "SaveData.json";
    public string key;
    public bool mainMenu;
    public Player player;

    public byte[] MakeKey() {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        string baseString = Convert.ToBase64String(keyBytes);
        byte[] aesKey = Encoding.UTF8.GetBytes(baseString);
        return aesKey;
    }
    public string EncryptString(string sd) {
        byte[] iv = new byte[16];
        byte[] array;
        
        using (Aes aes = Aes.Create()) {
            byte[] aesKey = MakeKey();
            aes.Key = aesKey;
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new()) {
                using (CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write)) {
                    using (StreamWriter streamWriter = new(cryptoStream)) {
                        streamWriter.Write(sd);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }
        var str = Convert.ToBase64String(array);
        return str;
    }
    public string DecryptString(string encryptedJson) {
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(encryptedJson);

        using (Aes aes = Aes.Create()) {
            byte[] aesKey = MakeKey();
            aes.Key = aesKey;
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(buffer)) {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)) {
                    using (StreamReader streamReader = new StreamReader(cryptoStream)) {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }
    private void Awake() {
        instance = this;
        path = Application.persistentDataPath + Path.DirectorySeparatorChar + "SaveData.json";        
        saveData = LoadData();
    }
    private void Start() {
        StartCoroutine(AutoSave());
        player = GetComponent<Player>();
    }
    public void SaveData() {
        path = Application.persistentDataPath + Path.DirectorySeparatorChar + "SaveData.json";
        saveData.money = player.Money;
        saveData.upgrades = UpgradeManager.Instance.upgrades;
        string json = JsonUtility.ToJson(saveData);
        string encryptedJson = EncryptString(json);

        using (StreamWriter sw = new StreamWriter(path)) {
            sw.Write(encryptedJson);
        }

    }

    private SaveData LoadData() {
        path = Application.persistentDataPath + Path.DirectorySeparatorChar + "SaveData.json";
        string json;
       
        SaveData data;
        if (File.Exists(path)) {
            using (StreamReader sr = new StreamReader(path)) {
                json = sr.ReadToEnd();
            }
            string decodedJson = DecryptString(json);
            data = JsonUtility.FromJson<SaveData>(decodedJson);
        }
        else {
            data = new SaveData();
        }
        
        return data;
    }

    private IEnumerator AutoSave() {
        yield return new WaitForSeconds(5);
        SaveData();
        StartCoroutine(AutoSave());
    }
    
    public void ClearData() {
        File.Delete(path);
    }
}