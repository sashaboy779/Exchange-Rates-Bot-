# 🤖 Exchange Rates Bot
This is a Telegram bot that provide an exchange rate information (in relation to UAH) for selected date. The bot uses PrivatBank API to fetch exchange rates.

## Supported commands
| Command | Description|
| ------- | ----------|
|`/start`|start using the bot|
|`/tutorial`|show the tutorial|
|`/currencies`|list of supported currencies|
|`/setcurrency`|choose favourite currencies|
|`/today`|show exchange rate for today|
|`/yesterday`|show exchange rate for yesterday|
|`date`|show exchange rate for selected date|
|`/settings`|open settings|


## Prerequisites
Before you begin, ensure you have met the following requirements:

* You have a Windows machine
* You have installed [Visual Studio](https://visualstudio.microsoft.com/)
* You have installed [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Usage
### 1. Create a new bot by **BotFather**
1. In the Telegram find `@BotFather`
2. Send a `/newbot` command and follow instructions
3. Now you have a bot username and token. We will use them later

### 2. Configure **Ngrok**
1. Sign up at [ngrok](https://ngrok.com/)
2. Download ngrok
3. Connect your account
```
<path to ngrog>/ngrok authtoken <your authtoken>
```
> You can find the token [here](https://dashboard.ngrok.com/auth/your-authtoken)
4. Install [Ngrok Extensions](https://marketplace.visualstudio.com/items?itemName=DavidProthero.NgrokExtensions)
5. Run Visual Studio as an administrator and open this solution
6. Choose "Start ngrok Tunnel" from the "Tools" menu
7. In opened commnad prompt copy forwarding url address (with https)

### 3. Change **Web.config**
Replace default values with yours. Here is an example:
```xml
<add key="BotUrl"  value="https://05dc0f76f319.ngrok.io/{0}" />
<add key="BotName" value="mytestbot" />
<add key="BotKey"  value="1387850398:AA2S6fGvUh7p3PaTMK_D8Vc4jnyPz2Tti-4" />
