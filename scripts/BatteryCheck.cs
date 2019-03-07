
/**
 * Generates low battery warnings for all Z-Wave battery devices.
 * Logs warnings to the HomeSeer Log, and sends an SMS via twilio to the configured number.
 * NOTE: Assumes my Twilio Messaging plugin is also installed: https://github.com/legrego/HSPI_TwilioMessaging
 */
public object Main(object[] Parms)
{
    // *******************************
    // ** Configuration Options
    // *******************************
    // phone number to receive sms notifications
    string phoneNumber = "5558675309";
    // low battery threshold (%)
    int lowBatteryThreshold = 20;


    hs.WriteLog("Battery Check", "Checking all battery statuses");

    Scheduler.Classes.clsDeviceEnumeration devenum = hs.GetDeviceEnumerator() as Scheduler.Classes.clsDeviceEnumeration;

    string smsMessageHeader = "Low Battery Warning(s):";
    // I would use a List, but I can't get HomeSeer to compile this script when using System.Collections.Generic.List
    string messages = "";

    while (!devenum.Finished)
    {
        Scheduler.Classes.DeviceClass dev = devenum.GetNext();
        string deviceType = dev.get_Device_Type_String(hs);
        if (deviceType == "Z-Wave Battery")
        {
            string displayName = string.Format("{0}_{1}_{2}", dev.get_Location(hs), dev.get_Location2(hs), dev.get_Name(hs));
            double value = dev.get_devValue(hs);

            if (value >= 255 || value <= lowBatteryThreshold)
            {
                string displayValue = value >= 255 ? "LOW" : value.ToString();
                string message = displayName + ": " + displayValue;
                hs.WriteLog("Battery Warning", message);
                messages = messages + "\n" + message;

            }
        }
    }

    if (messages.Length > 0)
    {
        string messageBody = smsMessageHeader + "\n" + messages;
        object[] parameters = { phoneNumber, messageBody };
        hs.PluginFunction("Twilio Messaging", "", "SendMessage", parameters);
    }

    hs.WriteLog("Battery Check", "Finished");
    return 0;
}