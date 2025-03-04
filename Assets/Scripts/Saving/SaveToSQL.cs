using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySqlConnector;

public class SaveToSQL : MonoBehaviour
{
  private MySqlConnection _connection;

  private string _connectionString =
    "Server=mysql-18178143-periode3game.c.aivencloud.com;User ID=avnadmin;Password=AVNS_dAEa2xIwS49ooiv6K8Z;Database=defaultdb;Connection Timeout=30;Port=14788";
  void Start() {
    _connection = new MySqlConnection(_connectionString);
    _connection.Open();
    GetData();
  }

  void GetData() {
    MySqlCommand command = new MySqlCommand();
    command.Connection = _connection;
    command.CommandText = @"SELECT * FROM Saves";
    
    if (_connection.State != System.Data.ConnectionState.Open) {
      _connection.Open();
    }

    using (var myReader = command.ExecuteReader()) {
      while (myReader.Read()) {
        var n = myReader["Name"];
        print(n);
      }
    }
    
    _connection.Close();
  }

}
