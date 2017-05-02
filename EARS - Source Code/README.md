## Project To-do List

## Notes

#### Basic Database Queries

When accessing the database:

```C#
  if (dbConnection.OpenConnection()) // Check and open the database connection.
  {
    MySqlCommand command = new MySqlCommand(); // Used to create a MySQL command.
    
    command.Connection = dbConnection.getConnection(); // Assigns the database to the command.
    command.CommandText = "SELECT * from example"; // The command string.
    
    // OPTION #1: If reading from the database (query):  
    using (MySqlDataReader dr = command.ExecuteReader())
    {
      while (dr.Read())
      {
        String firstItem = dr[0].ToString();
        int secondItem = Int32.Parse(dr[1].ToString());
      }
    }
    
    // OPTION #2: If using other command like insert, update, etc:
    command.ExecuteNonQuery();
    
    dbConnection.CloseConnection(); // Close the connection.
    command.Dispose(); // Dispose of the command object after use.
  }    
```
