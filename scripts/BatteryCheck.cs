//css_reference System.Collections;Scheduler.Classes.DeviceClass;Scheduler.Classes.clsDeviceEnumeration;

public object Main(object[] Parms)
{
   hs.WriteLog("Battery Check", "Checking all battery statuses");

   Scheduler.Classes.clsDeviceEnumeration devenum = hs.GetDeviceEnumerator() as Scheduler.Classes.clsDeviceEnumeration;

   System.Collections.IEnumerable enumerator = hs.GetDeviceEnumerator() as System.Collections.IEnumerable;

   while (!devenum.Finished) {
        Scheduler.Classes.DeviceClass dev = devenum.GetNext();
        string deviceType = dev.get_Device_Type_String(hs);
        if (deviceType == "Z-Wave Battery") {
          string displayName = string.Format("{0}_{1}_{2}", dev.get_Location(hs), dev.get_Location2(hs), dev.get_Name(hs));
          double value = dev.get_devValue(hs);

          if (value >= 255 || value <= 20) {
            hs.WriteLog("Battery Warning", displayName + ": " + value);
            // todo: alert via twilio plugin
          }
        }
    }

   hs.WriteLog("Battery Check", "Finished");
   return 0;
}