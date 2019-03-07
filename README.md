# homeseer-utils
A collection of utilities for HomeSeer 3

## Scripts

### MQTT Sync
This script will extract all configured MQTT device entries from the [MQTT Plugin](https://shop.homeseer.com/products/mqtt-software-plug-in-for-hs3), and publish their current values.

I use this as a sync mechanism between HomeSeer and Home-Assistant, so that Home-Assistant receives updated state values even if the device hasn't been update on the HomeSeer side in quite some time.

### Battery Check
This script will identify all battery operated Z-Wave devices, and generate an alert if any of the batteries are low. Senes an SMS notification via my [Twilio Messaging](https://github.com/legrego/HSPI_TwilioMessaging) plugin.