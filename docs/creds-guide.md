## Creds Guide

This guide will show you how to create your own discord bot, invite it to your server, and copy it's credentials to your `creds.yml` in order to run your bot.

- Start by opening your creds.yml
  - If you're on a windows installer version, click on the creds button next to your bot's RUN button.
  - If you're on linux from source or windows from source version, open `nadekobot/output/creds.yml`. Please use visual studio code, notepad++ or another code editor. Usage of notepad is discouraged.

![Create a bot application and copy token to creds.yml file](https://cdn.nadeko.bot/tutorial/bot-creds-guide.gif)

1. Go to [the Discord developer application page][DiscordApp].
2. Log in with your Discord account.
3. Click **New Application**.
3. Fill out the `Name` field however you like, accept the terms, and confirm.
1. Go to the **Bot** tab on the left sidebar.
1. Click on the `Add a Bot` button and confirm that you do want to add a bot to this app.
1. **Optional:** Add bot's avatar and description.
1. Copy your Token to `creds.yml` as shown above.
1. Scroll down to the **`Privileged Gateway Intents`** section
    - Enable the following:
         - **PRESENCE INTENT**
         - **SERVER MEMBERS INTENT**
         - **MESSAGE CONTENT INTENT**

These are required for a number of features to function properly, and all should be on.

##### Getting Owner ID

- Go to your Discord server and attempt to mention yourself, but put a backslash at the start
  *(to make it slightly easier, add the backslash after the mention has been typed)*.
- For example, the message `\@fearnlj01#3535` will appear as `<@145521851676884992>` after you send the message.
- The message will appear as a mention if done correctly. Copy the numbers from it **`145521851676884992`** and replace the big number on the `OwnerIds` section with your user ID.
- Save the `creds.yml` file.
- If done correctly, you should now be the bot owner. You can add multiple owners by adding them below the first one. Indentation matters.

For a single owner, it should look like this:

```yml
OwnerIds:
  - 105635576866156544
```

For multiple owners, it should look like this:

```yml
OwnerIds:
  - 105635123466156544
  - 145521851676884992
  - 341420590009417729
```


#### Inviting your bot to your server

![Invite the bot to your server](https://cdn.nadeko.bot/tutorial/bot-invite-guide.gif)

- On the **General Information** tab, copy your `Application ID` from your [applications page][DiscordApp].
- Replace the `YOUR_CLIENT_ID_HERE` in this link:
  `https://discordapp.com/oauth2/authorize?client_id=YOUR_CLIENT_ID_HERE&scope=bot&permissions=66186303` with your `Client ID`
- The link should now look something like this:
  `https://discordapp.com/oauth2/authorize?client_id=123123123123&scope=bot&permissions=66186303`
- Access that newly created link, pick your Discord server, click `Authorize` and confirm with the captcha at the end
- The bot should now be in your server

That's it! You may now go back to the installation guide you were following before 🎉

[DiscordApp]: https://discordapp.com/developers/applications/me
