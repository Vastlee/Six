# Six

Six is our beloved bot that lives in #DYEL.

## Getting started

Get started developing Six in a few easy steps!

### Step 1: Create an app

Head to the [Discord developer portal](https://discord.com/developers/applications) and create an application.

Inside of your app head to settings > bot. Copy your token (for step 2) and then scroll down an enable all of **Privileged Gateway Intents** options.


### Step 2: Install the bot

Still in your discord application head to Settings -> Oauth2 -> Url Generator. Select the **Bot** scope and the **Administrator** permissions.

Copy the url at the bottom and install the bot on your server.

### Step 3: Configuration

Set the following environment variables on your system:

```
SIX_TOKEN=YourDiscordBotToken
SIX_SERVER_ID=YourDiscordServerID
SIX_CHANNEL_ID=YourDiscordChannelID
SIX_TEST_MODE=true
```

### Step 4: Run the bot

Open Six.App/Six.App.csproj and press the play button. Test by typing `!DYEL` in your discord channel.