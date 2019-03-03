//css_reference System.Data.SQLite;/usr/local/HomeSeer/Bin/System.Data.SQLite.dll
using System.Data.SQLite;

public object Main(object[] Parms)
{
   hs.WriteLog("MQTT Sync", "Starting");

   System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("Data Source=/usr/local/HomeSeer/Data/HS3MQTT/hs3mqtt.sqlite;Version=$

   conn.Open();
   string sql = "SELECT dvRef, topic FROM publish WHERE publishType = 3";
   System.Data.SQLite.SQLiteCommand command = new System.Data.SQLite.SQLiteCommand(sql, conn);
   System.Data.SQLite.SQLiteDataReader reader = command.ExecuteReader();
   while (reader.Read()) {
     int deviceRef = int.Parse(reader["dvRef"].ToString());
     string topic = reader["topic"].ToString();

     object[] parameters = { topic, hs.DeviceValue(deviceRef) };
     hs.PluginFunction("MQTT", "", "publish", parameters);
   }

   hs.WriteLog("MQTT Sync", "Finished");
   return 0;
}