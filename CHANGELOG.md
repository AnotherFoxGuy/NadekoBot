# Changelog

Mostly based on [keepachangelog](https://keepachangelog.com/en/1.0.0/) except date format. a-c-f-r-o

## [5.3.8] - 27.01.2025

## Fixed

- `.temprole` now correctly adds a role
  - `.h temprole` also shows the correct overload now

## [5.3.7] - 21.01.2025

## Changed

- You can now run `.prune` in DMs
  - It deletes only bot messages
  - You can't specify a number of messages to delete (100 default)
- Updated command list

## [5.3.6] - 20.01.2025

## Added

- Added player skill stat when fishing
  - Starts at 0, goes up to 100
  - Every time you fish you have a chance to get an extra skill point
  - Higher skill gives you more chance to catch fish (and therefore less chance to catch trash)

## Changed

- Patrons no longer have `.timely` and `.fish` captcha on the public bot

## Fixed

- Fixed fishing spots again (Your channels will once again change a spot, last time hopefully)
  - There was a mistake in spot calculation for each channel

## [5.3.5] - 17.01.2025

## Fixed

- .sar rm will now accept role ids in case the role was deleted
- `.deletewaifus` should work again

## [5.3.4] - 14.01.2025

## Added

- Added `.fish` commands
  - `.fish` - Attempt to catch a fish - different fish live in different places, at different times and during different times of the day
  - `.fishlist` - Look at your fish catalogue - shows how many of each fish you caught and what was the highest quality - for each caught fish, it also shows its required spot, time of day and weather
  - `.fishspot` - Shows information about the current fish spot, time of day and weather

## Fixed

- `.timely` fixed captcha sometimes generating only 2 characters

## [5.3.3] - 15.12.2024

## Fixed

- `.notify` commands are no longer owner only, they now require Admin permissions
- `.notify` messages can now mention anyone

## [5.3.2] - 14.12.2024

## Fixed

- `.banner` should be working properly now with both server and global user banners

## [5.3.1] - 13.12.2024

## Changed

- `.translate` will now use 2 embeds, to allow for longer messages
- Added role icon to `.inrole`, if it exists
- `.honeypot` will now add a 'Honeypot' as a ban reason.

## Fixed

- `.winlb` looks better, has a title, shows 9 entries now
- `.sar ex` help updated
- `.banner` partially fixed, it still can't show global banners, but it will show guild ones correctly, in a good enough size
- `.sclr` will now show correct color hexes without alpha
- `.dmcmd` will now correctly block commands in dms, not globally

## [5.3.0] - 10.12.2024

## Added

- Added `.minesweeper` /  `.mw` command - spoiler-based minesweeper minigame. Just for fun
- Added `.temprole` command - add a role to a user for a certain amount of time, after which the role will be removed
- Added `.xplevelset` - you can now set a level for a user in your server
- Added `.winlb` command - leaderboard of top gambling wins
- Added `.notify` command
    - Specify an event to be notified about, and the bot will post the specified message in the current channel when the
      event occurs
    - A few events supported right now:
        - `UserLevelUp` when user levels up in the server
        - `AddRoleReward` when a role is added to a user through .xpreward system
        - `RemoveRoleReward` when a role is removed from a user through .xpreward system
        - `Protection` when antialt, antiraid or antispam protection is triggered
- Added `.banner` command to see someone's banner
- Selfhosters:
    - Added `.dmmod` and `.dmcmd` - you can now disable or enable whether commands or modules can be executed in bot's
      DMs

## Changed

- Giveaway improvements
    - Now mentions winners in a separate message
    - Shows the timestamp of when the giveaway ends
- Xp Changes
    - Removed awarded xp (the number in the brackets on the xp card)
    - Awarded xp, (or the new level set) now directly apply to user's real xp
    - Server xp notifications are now set by the server admin/manager in a specified channel
- `.sclr show` will now show hex code of the current color
- Queueing a song will now restart the playback if the queue is on the last track and stopped (there were no more tracks
  to play)
- `.translate` will now use 2 embeds instead of 1

## Fixed

- .setstream and .setactivity will now pause .ropl (rotating statuses)
- Fixed `.sar ex` help description

## Removed

- `.xpnotify` command, superseded by `.notify`, although as of right now you can't post user's level up in the same
  channel user last typed, because you have to specify a channel where the notify messages will be posted

## [5.2.4] - 27.11.2024

## Fixed

- More fixes for .sclr
- `.iamn` fixed

## [5.2.3] - 27.11.2024

## Fixed

- `.iam` Fixed
- `.sclr` will now properly change color on many commands it didn't work previously

### Changed

- `.rps` now also has bet amount in the result, like other gambling commands

## [5.2.2] - 27.11.2024

### Changed

- Button roles are now non-exclusive by default

### Fixed

- Fixed sar migration, again (this time correctly)
- Fixed `.sclr` not updating unless bot is restarted, the changes should be immediate now for warn and error
- Fixed group buttons exclusivity message always saying groups are exclusive

## [5.2.1] - 26.11.2024

### Fixed

- Fixed old self assigned missing

## [5.2.0] - 26.11.2024

### Added

- Added `.todo undone` command to unmark a todo as done
- Added Button Roles!
    - `.btr a` to add a button role to the specified message
    - `.btr list` to list all button roles on the server
    - `.btr rm` to remove a button role from the specified message
    - `.btr rma` to remove all button roles on the specified message
    - `.btr excl` to toggle exclusive button roles (only 1 role per message or any number)
    - Use `.h btr` for more info
- Added `.wrongsong` which will delete the last queued song.
    - Useful in case you made a mistake, or the bot queued a wrong song
    - It will reset after a shuffle or fairplay toggle, or similar events.
- Added Server color Commands!
    - Every Server can now set their own colors for ok/error/pending embed (the default green/red/yellow color on the
      left side of the message the bot sends)
    - Use `.h .sclr` to see the list of commands
    - `.sclr show` will show the current server colors
    - `.sclr ok <color hex>` to set ok color
    - `.sclr warn <color hex>` to set warn color
    - `.sclr error <color hex>` to set error color

### Changed

- Self Assigned Roles reworked! Use `.h .sar` for the list of commands
    - `.sar autodel`
        - Toggles the automatic deletion of the user's message and Nadeko's confirmations for .iam and .iamn commands.
    - `.sar ad`
        - Adds a role to the list of self-assignable roles. You can also specify a group.
        - If 'Exclusive self-assignable roles' feature is enabled (.sar exclusive), users will be able to pick one role
          per group.
    - `.sar groupname`
        - Sets a self assignable role group name. Provide no name to remove.
    - `.sar remove`
        - Removes a specified role from the list of self-assignable roles.
    - `.sar list`
        - Lists self-assignable roles. Shows 20 roles per page.
    - `.sar exclusive`
        - Toggles whether self-assigned roles are exclusive. While enabled, users can only have one self-assignable role
          per group.
    - `.sar rolelvlreq`
        - Set a level requirement on a self-assignable role.
    - `.sar grouprolereq`
        - Set a role that users have to have in order to assign a self-assignable role from the specified group.
    - `.sar groupdelete`
        - Deletes a self-assignable role group
    - `.iam` and `.iamn` are unchanged
- Removed patron limits from Reaction Roles. Anyone can have as many reros as they like.
- `.timely` captcha made stronger and cached per user.
- `.bsreset` price reduced by 90%

### Fixed

- Fixed `.sinfo` for servers on other shard

## [5.1.20] - 10.11.2024

### Added

- Added `.rakeback` command, get a % of house edge back as claimable currency
- Added `.snipe` command to quickly get a copy of a posted message as an embed
    - You can reply to a message to snipe that message
    - Or just type .snipe and the bot will snipe the last message in the channel with content or image
- Added `.betstatsreset` / `.bsreset` command to reset your stats for a fee
- Added `.gamblestatsreset` / `.gsreset` owner-only command to reset bot stats for all games
- Added `.waifuclaims` command which lists all of your claimed waifus
- Added and changed `%bot.time%` and `%bot.date%` placeholders. They use timestamp tags now

### Changed

- `.divorce` no longer has a cooldown
- `.betroll` has a 2% better payout
- `.slot` payout balanced out (less volatile), reduced jackpot win but increased other wins,
    - now has a new symbol, wheat
    - worse around 1% in total (now shares the top spot with .bf)

## [5.1.19] - 04.11.2024

### Added

- Added `.betstats`
    - See your own stats with .betstats
    - Target someone else:  .betstats @seraphe
    - You can also specify a game .betstats lula
    - Or both! .betstats seraphe br
- `.timely` can now have a server boost bonus
    - Configure server ids and reward amount in data/gambling.yml
    - anyone who boosts one of the sepcified servers gets the amount as base timely bonus

### Changed

- `.plant/pick` password font size will be slightly bigger
- `.race` will now have 82-94% payout rate based on the number of players playing (1-12, x0.01 per player).
    - Any player over 12 won't increase payout

### Fixed

- `.xplb` and `.xpglb` now have proper ranks after page 1
- Fixed boost bonus on shards different than the specified servers' shard

## [5.1.18] - 02.11.2024

### Added

- Added `.translateflags` / `.trfl` command.
    - Enable on a per-channel basis.
    - Reacting on any message in that channel with a flag emoji will post the translation of that message in the
      language of that country
    - 5 second cooldown per user
    - The message can only be translated once per language (counter resets every 24h)
- `.timely` now has a button. Togglable via `.conf gambling` it's called pass because previously it was a captcha, but
  captchas are too annoying

## Changed

- [public bot] Patreon reward bonus for flowers reduced. Timely bonuses stay the same
- discriminators removed from the databases. All users who had ???? as discriminator have been renamed to ??username.
    - all new unknown users will have ??Unknown as their name
- Flower currency generation will now have a strikeout to try combat the pickbots. This is the weakest but easiest
  protection to implement. There may be more options in the future

## Fixed

- nunchi join game message is now ok color instead of error color

## [5.1.17] - 29.10.2024

### Fixed

- fix: Bot will now not accept .aar Role if that Role is higher than or equal to bot's role. Previously bot would just
  fail silently, now there is a proper error message.

## [5.1.16] - 28.10.2024

## Added

- Added .ncanvas and related commands.
    - You can set pixel colors (and text) on a 500x350 canvas, pepega version of r/place
    - You use currency to set pixels.
    - Commands:
        - see the entire canvas: `.nc`
        - zoom: `.ncz <pos>` or `.ncz x y`
        - set pixel: `.ncsp <pos> <color> <text?>`
        - get pixel: `.ncp <pos>`
    - Owners can use .ncsetimg to set a starting image, use `.h .setimg` for instructions
    - Owners can reset the whole canvas via `.ncreset`

## [5.1.15] - 21.10.2024

## Added

- Added -c option for `.xpglb`
-

## Change

- Leaderboards will now show 10 users per page
- A lot of internal changes and improvements

## Fixed

- Fixed a big issue which caused several features to not get loaded on bot restart
- Alias collision fix `.qse` is now quotesearch, `.qs` will stay `.queuesearch`
- Fixed some migrations which would prevent users from updating from ancient versions
- Waifulb will no longer show #0000 discrims
- More `.greet` command fixes
- Author name will now be counted as content in embeds. Embeds can now only have author fields and still be valid
- Grpc api fixes, and additions

## [5.1.14] - 03.10.2024

## Changed

- Improved `.xplb -c`, it will now correctly only show users who are still in the server with no count limit

## Fixed

- Fixed medusa load error on startup

## [5.1.13] - 03.10.2024

### Fixed

- Grpc api server will no longer start unless enabled in creds
- Seq comment in creds fixed

## [5.1.12] - 03.10.2024

### Added

- Added support for `seq` for logging. If you fill in seq url and apiKey in creds.yml, bot will sends logs to it

### Fixed

- Fixed another bug in `.greet` / `.bye` system, which caused it to show wrong message on a wrong server occasionally

## [5.1.11] - 03.10.2024

### Added

- Added `%user.displayname%` placeholder. It will show users nickname, if there is one, otherwise it will show the
  username.
    - Nickname won't be shown in bye messages.
- Added initial version of grpc api. Beta

### Fixed

- Fixed a bug which caused `.bye` and `.greet` messages to be randomly disabled
- Fixed `.lb -c` breaking sometimes, and fixed pagination

### Changed

- Youtube now always uses `yt-dlp`. Dropped support for `youtube-dl`
    - If you've previously renamed your yt-dlp file to youtube-dl, please rename it back.
- ytProvider in data/searches.yml now also controls where you're getting your song streams from.
    - (Invidious support added for .q)

## [5.1.10] - 24.09.2024

### Fixed

- Fixed claimed waifu decay in `games.yml`

### Changed

- Added some logs for greet service in case there are unforeseen issues, for easier debugging

## [5.1.9] - 21.09.2024

### Fixed

- Fixed `.greettest`, and other `.*test` commands if you didn't have them enabled.
- Fixed `.greetdmtest` sending messages twice.
- Fixed a serious bug which caused greet messages to be jumbled up, and wrong ones to be sent for the wrong events.
    - There is no database issue, all greet messages are safe, the cache was caching any setting every 3 seconds with no
      regard for the type of the event
    - This also caused `.greetdm` messages to not be sent if `.greet` is enabled
    - This bug was introduced in 5.1.8. PLEASE UPDATE if you are on 5.1.8
- Selfhosters only: Fixed medusa dependency loading
    - Note: Make sure to not publish any other DLLs besides the ones you are sure you will need, as there can be version
      conflicts which didn't happen before.

## [5.1.8] - 19.09.2024

### Added

- Added `.leaveunkeptservers` which will make the bot leave all servers on all shards whose owners didn't run `.keep`
  command.
    - This is a dangerous and irreversible command, don't use it. Meant for use on the public bot.
- `.adpl` now supports custom statuses (you no longer need to specify Playing, Watching, etc...)

### Changed

- `.quote` commands cleaned up and improved
    - All quote commands now start with `.q<whatever>` and follow the same naming pattern as Expression commands
    - `.liqu` renamed to `.qli`
    - `.quotesearch` / `.qse` is now paginated for easier searching
- `.whosplaying` is now paginated
- `.img` is now paginated
- `.setgame` renamed to`.setactivity` and now supports custom text activity. You don't have to specify playing,
  listening etc before the activity
- Clarified and added some embed / placeholder links to command help where needed
- dev: A lot of code cleanup and internal improvements

### Fixed

- Fixed `.xpcurrew` breaking xp gain if user gains 0 xp from being in a voice channel
- Fixed a bug in `.gatari` command
- Fixed some waifu related strings
- Fixed `.quoteshow` and `.quoteid` commands
- Fixed some placeholders not working in `.greetdm`
- Fixed postgres support
- Fixed and clarified some command strings/parameter descriptions

### Removed

- Removed mysql support as it didn't work for a while, and requires some special handling/maintenance
    - Sqlite and Postgres support stays

## [5.1.7] - 08.08.2024

### Fixed

- Fixed some command groups incorrectly showing up as modules

## [5.1.6] - 07.08.2024

### Added

- `.serverlist` is now paginated

### Changed

- `.listservers` renamed to `.serverlist`

### Fixed

- `.afk` messages can no longer ping, and the response is moved to DMs to avoid abuse
- Possible fix for `.remind` timestamp

### Removed

- Removed old bloat / semi broken / dumb commands
    - `.memelist` / `.memegen` (too inconvenient to use)
    - `.activity` (useless owner-only command)
    - `.rafflecur` (Just use raffle and then award manually instead)
    - `.rollduel` (we had this command?)
- You can no longer bet on `.connect4`
- `.economy` Removed.
    - Was buggy and didn't really show the real state of the economy.
    - It might come back improved in the future
- `.mal` Removed. Useless information / semi broken

## [5.1.5] - 01.08.2024

### Added

- Added: Added a `.afk <msg>?` command which sets an afk message which will trigger whenever someone pings you
    - Message will when you type a message in any channel that the bot sees, or after 8 hours, whichever comes first
    - The specified message will be prefixed with "The user is afk: "
    - The afk message will disappear 30 seconds after being triggered

### Changed

- Bot now shows a message when .prune fails due to already running error
- Updated some bet descriptions to include 'all' 'half' usage instructions
- Updated some command strings
- dev: Vastly simplified medusa creation using dotnet templates, docs updated
- Slight refactor of .wiki, time, .catfact, .wikia, .define, .bible and .quran commands, no significant change in
  functionality

### Fixed

- .coins will no longer show double minus sign for negative changes
- You can once again disable cleverbot responses using fake 'cleverbot:response' module name in permission commands

### Removed

- Removed .rip command

## [5.1.4] - 13.07.2024

### Added

- Added `.coins` command which lists top 10 cryptos ordered by marketcap
- Added Clubs rank in the leaderboard to `.clubinfo`
- Bot owners can now check other people's bank balance (Not server owners, only bot owner, the person who is hosting the
  bot)
- You can now send multiple waifu gifts at once to waifus. For example `.waifugift 3xRose @user` will give that user 3
  roses
    - The format is `<NUMBER>x<ITEM>`, no spaces
- Added `.boosttest` command
- Added support for any openai compatible api for the chatterbot feature change:
    - Changed games.yml to allow input of the apiUrl (needs to be openai compatible) and modelName as a string.

### Changed

- Updated command strings to clarify `.say` and `.send` usages

### Fixed

- Fixed `.waifugift` help string

### Removed

- Removed selfhost button from `.donate` command, no idea why it was there in the first place

## [5.1.3] - 06.07.2024

### Added

- Added `.quran` command, which will show the provided ayah in english and arabic, including recitation by Alafasy

### Changed

- Replying to the bot's message in the channel where chatterbot is enabled will also trigger the ai response, as if you
  pinged the bot. This only works for chatterbot, but not for nadeko ai command prompts

### Fixed

- Fixed `.stickeradd` it now properly supports 300x300 image uploads.
- Bot should now trim the invalid characters from chatterbot usernames to avoid openai errors
- Fixed prompt triggering chatterbot responses twice

## [5.1.2] - 29.06.2024

### Fixed

- Fixed `.honeypot` not unbanning and not pruning messages

## [5.1.1] - 27.06.2024

### Added

- Added `.honeypot` command, which automatically softbans (ban and immediate unban) any user who posts in that channel.
    - Useful to auto softban bots who spam every channel upon joining
    - Users who run commands or expressions won't be softbanned.
    - Users who have ban member permissions are also excluded.

### Fixed

- Fixed `.betdraw` not respecting maxbet
- Fixed `.xpshop` pagination for real this time?

## [5.1.0] - 25.06.2024

### Added

- Added `.prompt` command, Nadeko Ai Assistant
    - You can send natural language questions, queries or execute commands. For example "@Nadeko how's the weather in
      paris" and it will return `.we Paris` and run it for you.
    - In case the bot can't execute a command using your query, It will fall back to your chatter bot, in case you have
      it enabled in data/games.yml. (Cleverbot or chatgpt)
    - (It's far from perfect so please don't ask the bot to do dangerous things like banning or pruning)
    - Requires Patreon subscription, after which you'll be able to run it on global @Nadeko bot.
        - Selfhosters: If you're selfhosting, you also will need to acquire the api key
          from <https://dashy.nadeko.bot/me> after pledging on patreon and put it in nadekoAiToken in creds.yml
- Added support for `gpt-4o` in `data/games.yml`

### Changed

- Remind will now show a timestamp tag for durations
- Only `Gpt35Turbo` and `Gpt4o` are valid inputs in games.yml now
- `data/patron.yml` changed. It now has limits. The entire feature limit system has been reworked. Your previous
  settings will be reset
- A lot of updates to bot strings (thanks Ene)
- Improved cleanup command to delete a lot more data once cleanup is ran, not only guild configs (please don't use this
  command unless you have your database bakced up and you know 100% what you're doing)

### Fixed

- Fixed xp bg buy button not working, and possibly some other buttons too
- Fixed shopbuy %user% placeholders and updated help text
- All .feed overloads should now work"
- `.xpexclude` should will now work with forums too. If you exclude a forum you won't be able to gain xp in any of the
  threads.
- Fixed remind not showing correct time (thx cata)

### Removed

- Removed PoE related commands
- dev: Removed patron quota data from the database, it will now be stored in redis

## [5.0.8] - 21.05.2024

### Added

- Added `.setserverbanner` and `.setservericon` commands (thx cata)
- Added overloads section to `.h command` which will show you all versions of command usage with param names
- You can now check commands for submodules, for example `.cmds SelfAssignedRoles` will show brief help for each of the
  commands in that submodule
- Added dropdown menus for .mdls and .cmds (both module and group versions) which will give you the option to see more
  detailed help for each specific module, group or command respectively
- Self-Hosters only:
    - Added a dangerous cleanup command that you don't have to know about

### Changed

- Quotes will now use alphanumerical ids (like expressions)

### Fixed

- `.verbose` will now be respected for expression errors
- Using `.pick` will now correctly show the name of the user who picked the currency
- Fixed `.h` not working on some commands
- `.langset` and `.langsetd` should no longer allow unsupported languages and nonsense to be typed in

## [5.0.7] - 15.05.2024

### Fixed

- `.streammessage` will once again be able to mention anyone (as long as the user setting the message has the permission
  to mention everyone)
- `.streammsgall` fixed
- `.xplb` and `.xpglb` pagination fixed
- Fixed page number when the total number of elements is unknown

## [5.0.6] - 14.05.2024

### Changed

- `.greet` and `.bye` will now be automatically disabled if the bot losses permissions to post in the specified channel
- Removed response replies from `.blackjack` and `.pick` as the original message will always be deleted

### Fixed

- Fixed `.blackjack` response string as it contained no user name
- Fixed `.ttt` and `.gift` strings not mentioning the user

## [5.0.5] - 11.05.2024

### Fixed

- `%server.members%` placeholder fixed
- `.say #channel <message>` should now be working properly again
- `.repeat`, `.greet`, `.bye` and `.boost` command can now once again mention anyone

## [5.0.4] - 10.05.2024

### Added

- Added `.shopadd command` You can now sell commands in the shop. The command will execute as if you were the one
  running it when someone buys it
    - type `.h .shopadd` for more info
- Added `.stickyroles` Users leaving the server will have their roles saved to the database and reapplied if they rejoin
  within 30 days.
- Giveaway commands
    - `.ga start <duration> <text>` starts the giveway with the specified duration and message (prize). You may have up
      to 5 giveaways on the server at once
    - `.ga end <id>` prematurely ends the giveaway and selects a winner
    - `.ga cancel <id>` cancels the giveaway and doesn't select a winner
    - `.ga list` lists active giveaways on the current server
    - `.ga reroll <id>` rerolls the winner on the completed giveaway. This only works for 24 hours after the giveaway
      has ended, or until the bot restarts.
    - Users can join the giveaway by adding a :tada: reaction
- Added Todo Commands
    - `.todo add <name>` - adds a new todo
    - `.todo delete <id>` - deletes a todo item
    - `.todo done <id>` - completes a todo (marks it with a checkmark)
    - `.todo list` - lists all todos
    - `.todo edit <id> <new message>` - edits a todo item message
    - `.todo show <id>` - Shows the text of the specified todo item
    - In addition to that, there are also Todo archive commands
        - `.todo archive add <name>` - adds all current todos (completed and not completed) to the archived list, your
          current todo list will become cleared
        - `.todo archive list` - lists all your archived todo lists
        - `.todo archive show <id>` - shows the todo items from one of your archived lists
        - `.todo archive delete <id>` - deletes and archived todo list
- Added `.queufairplay` / `.qfp` (music feature) re-added but it works differently
    - Once you run it, it will reorganize currently queued songs so that they're in a fair order.
- Added `.clubrename` command to uh rename your club
- For self-hosters:
    - Added `.sqlselectcsv` which will return results in a csv file instead of an embed.
    - You can set whether nadeko ignores other bots in `bot.yml`
    - You can set shop sale cut in `gambling.yml`
- Added a page parameter to `.feedlist`
- Added seconds/sec/s to `.convert` command
- Added `.prunecancel` to cancel an active prune
- Added progress reporting when using `.prune`.
- Added audit log reason for `.setrole` and some other features

### Changed

- Users who have manage messages perm in the channel will now be excluded from link and invite filtering (`.sfi`
  and `.sfl`)
- `.send` command should work consistently and correctly now. You can have targets from other shards too. The usage has
  been changed. refer to `.h .send` for more info
- `.serverinfo` no longer takes a server name. It only takes an id or no arguments
- You can now target a different channel with .repeat
- `.cmds <module name>`, `.cmds <group name` and `.mdls` looks better
- The bot will now send a discord Reply to every command
- `.queuesearch` / `.qs` will now show the results with respective video thumbnails
- A lot of code cleanup (still a lot to be done) and Quality of Life improvements
- `.inrole` will now show mentions primarily, and use a spoiler to show usernames

### Fixed

- `.feed` should now correctly accept (and show) the message which can be passed as the third parameter
- `.say` will now correctly report errors if the user or the bot don't have sufficent perms to send a message in the
  targeted channel
- Fixed `.invitelist` not paginating correctly
- `.serverinfo` will now correctly work for other shards
- `.send` will now correctly work for other shards
- `.translate` command will no longer fail if the user capitalizes the language name
- Fixed xp card user avatar not showing for some users

### Removed

- `.poll` commands removed as discord added polls
- `.scpl` and other music soundcloud commands have been removed as soundcloud isn't issuing new api tokens for years now
- Removed a lot of useless and nsfw commands
- Removed log voice presence TTS
- Cleanup: Removed a lot of obsolete aliases from aliases.yml

## [4.3.22] - 23.04.2024

### Added

- Added `.setbanner` command (thx cata)

### Fixed

- Fixed pagination error due to a missing emoji

## [4.3.21] - 19.04.2024

### Fixed

- Possible fix for a duplicate in `.h bank`
- Fixed `.stock` command
- Fixed `.clubapply` and `.clubaccept`
- Removed some redundant discriminators

## [4.3.20] - 20.01.2024

### Fixed

- Fixed `.config searches followedStreams.maxCount` not working

## [4.3.19] - 20.01.2024

### Added

- Added `followedStreams.maxCount` to `searches.yml` which lets bot owners change the default of 10 per server

### Changed

- Improvements to GPT ChatterBot (thx alexandra)
- Add a personality prompt to tweak the way chatgpt bot behaves
- Added Chat history support to chatgpt ChatterBot
- Chatgpt token usage now correctly calculated
- More chatgpt configs in `games.yml`

## [4.3.18] - 26.12.2023

### Added

- Added `.cacheusers` command (thx Kotz)
- Added `.clubreject` which lets you reject club applications

### Changed

- Updated discord lib, there should be less console errors now

### Fixed

- Fixed `icon_url` when using `.showembed`
- Fixed `.quoteshow` not showing sometimes (thx Cata)
- Notifications will no longer be sent if dms are off when using `.give`
- Users should no longer be able to apply to clubs while in a club already (especially not to the same club they're
  already in)

### Removed

- `.revimg` and `.revav` as google removed reverse image search

## [4.3.17] - 06.09.2023

### Fixed

- Fix to waifu gifts being character limited
- Fixes UserUpdated and UserPresence not correctly ignoring users that are logignored
- Added Trim() to activity names since apparently some activities have trailing spaces.

## [4.3.16] - 24.05.2023

### Fixed

- Fixed missing events from `.logevents`
- Fixed `.log` thread deleted and thread created events not working properly

## [4.3.15] - 21.05.2023

### Fixed

- Fixed -w 0 in trivia
- Fixed `.rps` amount field in the response
- Fixed `.showembed` output
- Fixed bank award's incorrect output message

## [4.3.14] - 02.04.2023

### Fixed

- Fixed voice hearbeat issue
- `.banktake` had ok/error responses flipped. No functional change
- PermRole should deny messages in threads todo
- Fixed chucknorris jokes
- `.logserver` will now

## [4.3.13] - 20.02.2023

### Fixed

- Fixed `.log userpresence`
- `.q` will now use `yt-dlp` if anything other than `ytProvider: Ytdl` is set in `data/searches.yml`
- Fixed Title links on some embeds

## [4.3.12] - 12.02.2023

### Fixed

- Fixed `.betstats` not working on european locales
- Timed `.ban` will work on users who are not in the server
- Fixed some bugs in the medusa system

## [4.3.11] - 21.01.2023

### Added

- Added `.doas` Bot owner only command
- Added `.stickeradd` command

### Changed

- `.waifuinfo` optimized
- You can now specify an optional custom message in `.feed` and `.yun` which will be posted along with an update
- Greet/bye messages will now get disabled if they're set to a deleted/unknown channel
- Updated response strings
- `.translate` now supports many more languages
- `.translangs` prettier output

### Fixed

- Added logging for thread events
- Fixed a bug for `.quotedeleteauthor` causing the executing user to delete own messages
- Fixed TimeOut punishment not alklowing duration
- Fixed a nullref in streamrole service
- Fixed some potential causes for ratelimit due to default message retry settings
- Fixed a patron rewards bug caused by monthly donation checking not accounting for year increase
- Fixed a patron rewards bug for users who connected the same discord account with multiple patreon accounts
- `.deletecurrency` will now also reset banked currency
- Fixed DMHelpText reply
- `.h` command show now properly show both channel and server user permission requirements
- Many fixes and improvements to medusa system
- Fixed trivia --nohint
- `.joinrace` will no longer fail if the user isn't in the database yet

## [4.3.10] - 10.11.2022

### Added

- `.filterlist` / `.fl` command which lists link and invite filtering channels and status
- Added support for `%target%` placeholder in `.alias` command
- Added .forwardtochannel which will forward messages to the current channel. It has lower priority than fwtoall
- Added .exprtoggleglobal / .extg which can be used to toggle usage of global expressions on the server

### Changed

- .meload and .meunload are now case sensitive. Previously loaded medusae may need to be reloaded or
  data/medusae/medusa.yml may need to be edited manually
- Several club related command have their error messages improved
- Updated help text for .antispam and .antiraid
- You can now specify time and date (time is optional) in `.remind` command instead of relative time, in the
  format `HH:mm dd.MM.YYYY`
- OwnerId will be automatically added to `creds.yml` at bot startup if it's missing

### Fixed

- Fixed `.cmdcd` console error
- Fixed an error when currency is add per xp
- Fixed an issue preventing execution of expressions starting with @Bot when cleverbot is enabled on the server
- Fixed `.feedadd`
- Fixed `.prune @target` not working
- Medusa modules (sneks) should now inherit medusa description when listed in .mdls command
- Fixed command cooldown calculation

## [4.3.9] - 12.10.2022

### Added

- `.betstats` shows sum of all bets, payouts and the payout rate in %. Updates once an hour

### Changed

- `.betstats` looks way better (except on Mac)
- `.feedadd` errors clarified and separated in individual error messages for each issue.
- `.clubban` and `.clubunban` errors clarified and separated in individual error messages for each issue.
- `.clubapply` better error messages

### Fixed

- `.timely` 'Remind' button fixed in DMs
- `.cmdcd` database bugs fixed
- Fixed bugged mysql and postgresql migrations
- Fixed issues with lodaing medusae due to strict versioning

### Removed

- `.slotstats` Superseded by `.betstats`

## [4.3.8] - 02.10.2022

### Added

- Added `.autopublish` command which will automatically publish messages posted in the channel.
- Added `--after <messageid>` option to prune which will make prune only delete messages after the specified message id.

### Changed

- `.prune` options `--after` and `--safe` are now proper command options, and will show in .h help
- `.cmdcd` code mostly rewritten, slight QoL improvements.
- Clarified `.remind` permission requirements in help text
- `.cmdcds` looks a little better, and is paginated

### Fixed

- Fixed trivia bugs
- Fixed `.yun` not working with channels with underscore in the name

## [4.3.7] - 14.09.2022

### Added

- Added `.exprdelserv` (.exds) to completement .exas. Deletes an expression on the current server and is susceptible to
  .dpo, unlike .exd
- Added `.shopreq` which lets you set role requirement for specific shop items
- Added `.shopbuy` alias to `.buy`

### Fixed

- Fixed `.convertlist` showing currencies twice (this may not apply to existing users and it may require you to manually
  remove all currencies from units.json)

### Removed

- Removed `Viewer` field from stream online notification as it is (almost?) always 0.

## [4.3.6] - 08.09.2022

### Added

- Added `.expraddserver` (.exas) which will server as a server-only alternative to '.exa' in case users want to override
  default Admin permissions with .dpo
- Added .banprune command which sets how many days worth of messages will be pruned when bot (soft)bans a person either
  through a command or another punishment feature.
- Added .qdelauth - Delete all quotes by the specified author on this server. If you target yourself - no permission
  required
- Added `.timeout` command
- Added an option to award currency based on received xp

### Changed

- Reminders now have embed support, but plaintext field is not supported.
- User friendlier errors when parsing a number in a command fails

### Fixed

- Awarded xp is now correctly used in level up calculations

## [4.3.5] - 17.08.2022

### Added

- Added a 'Use' button when a user already owns an item
- Added a 'Pull Again' button to slots
- Added `.roleinfo` command
- Added `.emojiremove` command
- Added `.threadcreate` and `.threaddelete` commands
- Added `.bank seize` / `.bank award` owner only commands

### Changed

- Running a .timely command early now shows a pending color
- .xp system is once again no longer opt in for servers
    - It's still opt-in for global and requires users to run .xp at least once in order to start gaining global xp

### Fixed

- Fixed users not getting club xp

## [4.3.4] - 07.08.2022

### Fixed

- Fixed users getting XP out of nowhere while voice xp is enabled

## [4.3.3] - 06.08.2022

### Added

- Added `betroll` option to `.bettest` command
- Added `.xpshopbuy` and `.xpshopuse` convenience commands
- Added an optional preview url to teh xp shop item config model which will be shown instead of the real Url

### Changed

- Updated position of Username and Club name on the .xp card
- Improved text visibility on the .xp card

### Fixed

- Possibly fixed .trivia not stopping bug
- Fixed very low payout rate on `.betroll`
- Fixed an issue with youtube song resolver which caused invalid data to be cached
- Added client id to the cache key as a potential fix for VoiceXp 'bug'. The solution may be to use different redis
  instances for each bot, or to switch from botCache: from 'redis' to 'memory' in creds.yml
- Bot owner should now be able to buy items from the xpshop when patron requirement is set
- Fixed youtube-dl caching invalid data. Please use yt-dlp instead

## [4.3.2] - 28.07.2022

### Fixed

- Fixed Reaction Roles not working properly with animated emojis
- Fixed `.slot` alignment
- Fixed `mysql` and `postgresql` reactionrole migration
- Fixed repeat loop with `postgresql` db provider
- Fixed `.bank withdraw <expression>` will now correctly use bank amount for calculation
- [dev] Fixed medusa Reply*LocalizedAsync not working with placeholders

## [4.3.1] - 27.07.2022

### Changed

- Check for updates will run once per hour as it was supposed to

## [4.3.0] - 27.07.2022

### Added

- Added `.bettest` command which lets you test many gambling commands
    - Better than .slottest
    - Counts win/loss streaks too
    - Doesn't count 1x returns as neither wins nor losses
    - multipliers < 1 are considered losses, > 1 considered wins
- Added `.betdraw` command which lets you guess red/black and/or high/low for a random card
    - They payouts are very good, but seven always loses
- Added `.lula` command. Plays the same as `.wof` but looks much nicer, and is easily customizable from gambling.yml
  without any changes to the sourcecode needed.
- Added `.repeatskip` command which makes the next repeat trigger not post anything
- Added `.linkonly` which will make the bot only allow link posts in the channel. Exclusive with `.imageonly`
- Added release notifications. Bot owners will now receive new release notifications in dms if they
  have `checkForUpdates` set to `true` in data/bot.yml
    - You can also configure it via `.conf bot checkfor
    - updates <true/false>`
- Added `.xpshop` which lets bot owners add xp backgrounds and xp frames for sale by configuring `data/xp.yml`
    - You can also toggle xpshop feature via `.conf xp shop.is_enabled`

### Changed

- `.t` Trivia code cleaned up, added ALL pokemon generations

- `.xpadd` will now work on roles too. It will add the specified xp to each user (visible to the bot) in the role
- Improved / cleaned up / modernized how most gambling commands look
    - `.roll`
    - `.rolluo`
    - `.draw`
    - `.flip`
    - `.slot`
    - `.betroll`
    - `.betflip`
    - Try them out!
- `.draw`, `.betdraw` and some other card commands (not all) will use the new, rewritten deck system
- Error will be printed to the console if there's a problem in `.plant`
- [dev] Split Nadeko.Common into a separate project
    - [dev] It will contain classes/utilities which can be shared across different nadeko related projects
- [dev] Split Nadeko.Econ into a separate project
    - [dev] It should be home for the backend any gambling/currency/economy feature
    - [dev] It will contain most gambling games and any shared logic
- [dev] Compliation should take less time and RAM
    - [dev] No longer using generator and partial methods for commands

### Fixed

- `.slot` will now show correct multipliers if they've been modified
- Fix patron errors showing up even with permissions disabling the command
- Fixed an issue with voice xp breaking xp gain.

### Removed

- Removed `.slottest`, replaced by `.bettest`
- Removed `.wof`, replaced by `.lula`
- [dev] Removed a lot of unused methods
- [dev] Removed several unused response strings

## [4.2.15] - 12.07.2022

### Fixed

- Fixed `.nh*ntai` nsfw command
- Xp Freezes may have been fixed
- `data/images.yml` should once again support local file paths
- Fixed multiword aliases

## [4.2.14] - 03.07.2022

### Added

- Added `.log userwarned` (Logging user warnings)
- Claiming `.timely` will now show a button which you can click to set a reminder
- Added `%server.icon%` placeholder
- Added `warn` punishment action for protection commands (it won't work with `.warnp`)

### Changed

- `.log userbanned` will now have a ban reason
- When `.die` is used, bot will try to update it's status to `Invisible`

### Fixed

- Fixed elipsis character issue with aliases/quotes. You should now be able to set an elipsis to be an alias
  of `.quoteprint`

## [4.2.13] - 30.06.2022

### Fixed

- Fixed `.cash` bank interaction not being ephemeral anymore

## [4.2.12] - 30.06.2022

### Fixed

- Fixed `.trivia --pokemon` showing incorrect pokemons

## [4.2.11] - 29.06.2022

### Fixed

- Fixed `.draw` command

## [4.2.10] - 29.06.2022

- Fixed currency generation working only once

## [4.2.9] - 25.06.2022

### Fixed

- Fixed `creds_example.yml` misssing from output directory

## [4.2.8] - 24.06.2022

### Fixed

- `.timely` should be fixed

## [4.2.7] - 24.06.2022

### Changed

- New cache abstraction added
    - 2 implemenations: redis and memory
    - All current bots will stay on redis cache, all new bots will use **in-process memory cache by default**
    - This change removes bot's hard dependency on redis
    - Configurable in `creds.yml` (please read the comments)
    - You **MUST** use 'redis' if your bot runs on more than 1 shard (2000+ servers)
- [dev] Using new non-locking ConcurrentDictionary

### Fixed

- `.xp` will now show default user avatars too

### Removed

- Removed `.imagesreload` as images are now lazily loaded on request and then cached

## [4.2.6] - 22.06.2022

### Fixed

- Patron system should now properly by disabled on selfhosts by default.

## [4.2.5] - 18.06.2022

### Fixed

- Fixed `.crypto`, you will still need coinmarketcapApiKey in `creds.yml` in order to make it run consistently as the
  key is shared

## [4.2.3] - 17.06.2022

### Fixed

- Fixed `.timely` nullref bug and made it nicer
- Fixed `.streamrole` not updating in real time!
- Disabling specific Global Expressions should now work with `.sc` (and other permission commands)

## [4.2.2] - 15.06.2022

### Fixed

- Added missing Patron Tiers and fixed Patron pledge update bugs
- Prevented creds_example.yml error in docker containers from crashing it

### Changed

- Rss feeds will now show error counter before deletion

## [4.2.1] - 14.06.2022

### Added

- Localized strings updated

### Fixed

- Fixed `.exexport`, `.savechat`, and `.quoteexport`
- Fixed plaintext-only embeds
- Fixed greet message footer not showing origin server

## [4.2.0] - 14.06.2022

### Added

- Added `data/searches.yml` file which configures some of the new search functionality
  The file comments explaining what each property does.
  Explained briefly here:
  ```yml
  # what will be used for .google command. Either google (official api) or searx
  webSearchEngine: Google
  # what will be used for .img command. Either google (official api) or searx
  imgSearchEngine: Google
  # how will yt results be retrieved: ytdataapi or ytdl or ytdlp
  ytProvider: YtDataApiv3
  # in case web or img search is set to searx, the following instances will be used:
  searxInstances: []
  # in case ytProvider is set to invidious, the following instances will be used
  invidiousInstances: []
  ```
- Added new properties to `creds.yml`. google -> searchId and google -> searchImageId.
- These properties are used as `cx` (google api query parameter) in case you've setup your `data/searches.yml` to use
  the official google api.
  `searchId` is used for web search
  `searchimageId` is used for image search
  ```yml
  google:
      searchId: ""
      searchImageId: ""
  ```
- Check `creds_example.yml` for comments explaining how to obtain them.

#### Patronage system added

- Added `data/patron.yml` for configuration
- Implemented only for patreon so far
- Patreon subscription code completely rewritten
- Users who pledge on patreon get benefits based on the amount they pledged
- Public nadeko only. But selfhosters can adapt it to their own patreon pages by configuring their patreon credentials
  in `creds.yml` and enabling the system in `data/patron.yml` file.
    - Most of the patronage system strings are hardcoded atm, so if you wish to use this system on selfhosts, you will
      have to modify the source
- Pledge amounts are split into tiers. This is not configurable atm.
    - Tier I - 1$ - 4.99$ a month
    - Tier V - 5$ - 9.99$ a month
    - Tier X - 10$ - 19.99$ a month
    - Tier XX - 20$ - 49.99$ a month
    - Tier L - 50$ - 99.99$ a month
    - Tier C - 100$+ a month
- Rewards and command quotas for each of the tiers are configurable
- Limitations to certain features are also configurable. ex:

```yml
quotas:
  features:
    "rero:max_count":
      x: 50
```

- ^ this setting would set the maximum number of reaction roles to be 50 for a user who is in Patron Tier X
- Read the comments in the .yml file for (much) more info
- Quota system allows the owner to set up hourly, daily and monthly quota usage for each tier
- Quota system applies to entire server owner by a patron
    - Patron spends own quota by using the commands on any server
    - Any user on *any* server owned by a patron spends that patron's quota
- When users subscribe to patreon they will receive a welcome message
    - If you're enabling patron system for a selfhost, you will want to edit it

Added `.patron` and `.patronmessage` commands

- `.patron` checks your patronage status, and quotas. Requires patron system to be enabled.
- `.patronmessage` (owner only) sends message to all patrons with the specified tier or higher. Supports embeds

- Added a fake `.cmdcd` command `cleverbot:response` which can be used to limit how often users can talk to the
  cleverbot.

### Changed

- CurrencyReward now support adding additional flowers to patrons.
- `.donate` command completely reworked.
    - Works only on public bot (OnlyPublicBotAttribute)
    - Guides user on how to donate to support the project
    - Added interaction explaining selfhosting

- `.google` reimplemented. It now has 2 modes configurable in `data/searches.yml` under the `webSearchengine` property
    - If set to `google`, official custom search api will be used. You will need to set googleapikey and google.searchId
      in `creds.yml`
    - if set to `searx` one of the instances specified in the `searxInstances:` property will be randomly chosen for
      each request
        - instances must have `format=json` allowed (public ones usually don't allow it)
        - instances are specified as a fully qualified url, example: `https://my.cool.searx.instance.io`
- `.image` reimplemented. Same as `.google` - it uses either `google` official api (in which case it
  uses `google.searchImageId` from `creds.yml`) or `searx`

- `.youtube` reimplemented. It will use a `ytProvider:` property from `data/searches.yml` to determine how to retrieve
  results
    - `ytdataapi` will use the official google api (requires `GoogleApiKey` specified in `creds.yml`) and YoutubeDataApi
      enabled in the dev console
    - `ytdl` will use `youtube-dl` program from the host machine. It must be downloaded and it's location must be added
      to path env variable.
    - `ytdlp` will use `yt-dlp` program from the host machine. Same as `youtube-dl` - must be in path env variable.
    - `invidious` will use one of invidious instances specified in the `invidiousInstances` property. Very good.

- `.google`, `.youtube` and `.image` moved to the new Search group

Note: Results of each `.youtube` query will be cached for 1 hour to improve perfomance

- Removed 30 second `.ping` ratelimit on public nadeko

- xp image generation changes
    - In case you have default settings, your xp image will look slightly different
    - If you've modified xp_template.json, your xp image might look broken. Your old template will be saved in
      xp_template.json.old
    - Xp number outline is now slightly thicker
    - Xp number will now have Center vertical and horizontal alignment
    - LastLevelUp no longer supported

- Some commands will now use timestamp tags for better user experience
- `.prune` was slightly slowed down to avoid ratelimits
- `.wof` moved from it's own group to the default Gambling group
- `.feed` urls which error for more than 100 times will be automatically removed.
- `.ve` is now enabled by default

- [dev] nadeko interaction slightly improved to make it less nonsense (they still don't make sense)
- [dev] RewardedUsers table slightly changed to make it more general
- [dev] renamed `// todo`s which aren't planned soon to `// FUTURE`
- [dev] currency rewards have been reimplemented and moved to a separate service

### Fixed

- `.rh` no longer needs quotes for multi word roles
- `.deletexp` will now properly delete server xp too
- Fixed `.crypto` sparklines
- [dev] added support for configs to properly parse enums without case sensitivity (ConfigParsers.InsensitiveEnum)
- [dev] Fixed a bug in .gencmdlist
- [dev] small fixes to creds provider

### Removed

- `.ddg` removed.
- [dev] removed some dead code and comments

## [4.1.6] - 14.05.2022

### Fixed

- Fixed windows release and updated packages

## [4.1.5] - 11.05.2022

### Changed

- `.clubdesc <msg>` will now have a nicer response

### Fixed

- `.give` DM will once again show an amount
- Fixed an issue with filters not working and with custom reactions no longer being able to override commands.
- Fixed `.stock` command

## [4.1.4] - 06.05.2022

### Fixed

- Fixed `.yun`

## [4.1.3] - 06.05.2022

### Added

- Added support for embed arrays in commands such as .say, .greet, .bye, etc...
    - Website to create them is live at eb.nadeko.bot (old one is moved to oldeb.nadeko.bot)
    - Embed arrays don't have a plainText property (it's renamed to 'content')
    - Embed arrays use color hex values instead of an integer
    - Old embed format will still work
    - There shouldn't be any breaking changes
- Added `.stondel` command which, when toggled, will make the bot delete online stream messages on the server when the
  stream goes offline
- Added a simple bank system.
    - Users can deposit, withdraw and check the balance of their currency in the bank.
    - Users can't check other user's bank balances.
- Added a button on a .$ command which, when clicked, sends you a message with your bank balance that only you can see.
- Added `.h <command group>`
    - Using this command will list all commands in the specified group
    - Atm only .bank is a proper group (`.h bank`)
- Added "Bank Accounts" entry to `.economy`

### Changed

- Reaction roles rewritten completely
    - Supports multiple exclusivity groups per message
    - Supports level requirements
    - However they can only be added one by one
    - Use the following commands for more information
        - `.h .reroa`
        - `.h .reroli`
        - `.h .rerot`
        - `.h .rerorm`
        - `.h .rerodela`
- Pagination is now using buttons instead of reactions
- Bot will now support much higher XP values for global and server levels
- [dev] Small change and generation perf improvement for the localized response strings

### Fixed

- Fixed `.deletexp` command
- `.give` command should send DMs again
- `.modules` command now has a medusa module description

## [4.1.2] - 16.04.2022

### Fixed

- Fixed an issue with missing `.dll` files in release versions

## [4.1.0] - 16.04.2022

### Added

- NadekoBot now supports mysql, postgresql and sqlite
    - To change the db nadeko will use, simply change the `db type` in `creds.yml`
    - There is no migration code right now, which means that if you want to switch to another system you'll either have
      to manually export/import your database or start fresh
- Medusa system
    - A massive new feature which allows developers to create custom modules/plugins/cogs
    - They can be load/unloaded/updated at runtime without restarting the bot

### Changed

- Minor club rework
    - Clubs names are now case sensitive (owo and OwO can be 2 different clubs)
    - Removed discriminators
        - Current discriminators which are greater than 1 are appended to clubnames to avoid duplicates, you can rename
          your club with `.clubrename` to remove it
        - Most of the clubs with #1 discriminator no longer have it (For example MyClub#1 will now just be MyClub)
- [dev] A lot of refactoring and slight functionality changes within Nadeko's behavior system and command handler which
  were required in order to support the medusa system

### Removed

- Removed `.clublevelreq` command as it doesn't serve much purpose

## [4.0.6] - 21.03.2022

### Fixed

- Fixed voice presence logging
- Fixed .clubaccept, .clubban, .clubkick and .clubunban commands

## [4.0.5] - 21.03.2022

### Fixed

- Fixed several bugs in the currency code
- Fixed some potential memory leaks
- Fixed some response strings

## [4.0.4] - 04.03.2022

### Fixed

- Fixed the `id` which shows up when you add a new Expression
- Fixed some strings which were still referring to "CustomReaction(s)" instead of "Expression(s)"

## [4.0.3] - 04.03.2022

### Fixed

- Console should no longer spam numbers when `.antispam` is enabled

## [4.0.2] - 03.03.2022

### Fixed

- Fixed `.rero` not working due to a bug introduced in 4.0

## [4.0.1] - 03.03.2022

### Added

- Added `usePrivilegedIntents` to creds.yml if you don't have or don't want (?) to use them
- Added a human-readable, detailed error message if logging in fails due to missing privileged intents

## [4.0.0] - 02.03.2022

### Added

- Added `.deleteemptyservers` command
- Added `.curtr <id>` which lets you see full information about one of your own transactions with the specified id
- Added trovo.live support for stream notifications (`.stadd`)
- Added unclaimed waifu decay functionality
    - Added 3 new settings to `data/gambling.yml` to control it:
        - waifu.decay.percent - How much % to subtract from unclaimed waifu
        - waifu.decay.hourInterval - How often to decay the price
        - waifu.decay.minPrice - Unclaimed waifus with price lower than the one specified here will not be affected by
          the decay
- Added `currency.transactionsLifetime` to `data/gambling.yml` Any transaction older than the number of days specified
  will be automatically deleted
- Added `.stock` command to check stock prices and charts
- Re-added `.qap / .queueautoplay`

### Changed

- CustomReactions module (and customreactions db table) has been renamed to Expressions.
    - This was done to remove confusion about how it relates to discord Reactions (it doesn't, it was created and named
      before discord reactions existed)
    - Expression command now start with ex/expr and end with the name of the action or setting.
    - For example `.exd` (`.dcr`) is expression delete, `.exa` (`.acr`)
    - Permissions (`.lp`) be automatically updated with "ACTUALEXPRESSIONS", "EXPRESSIONS" instead of "
      ACTUALCUSTOMREACTIONS" and "CUSTOMREACTIONS"
    - Permissions for `.ecr` (now `.exe`), `.scr` (now `.exs`), `.dcr` (now `.exd`), `.acr` (now `.exa`), `.lcr` (
      now `.exl`) will be automatically updated
    - If you have custom permissions for other CustomReaction commands
    - Some of the old aliases like `.acr` `.dcr` `.lcr` and a few others have been kept
- Currency output format improvement (will use guild locale now for some commands)
- `.crypto` will now also show CoinMarketCap rank
- Waifus can now be claimed for much higher prices (int -> long)
- Several strings and commands related to music have been changed
    - Changed `.ms / .movesong` to `.tm / .trackmove` but kept old aliases
    - Changed ~~song~~ -> `track` throughout music module strings
- Improved .curtrs (It will now have a lot more useful data in the database, show Tx ids, and be partially localized)
    - [dev] Reason renamed to Note
    - [dev] Added Type, Extra, OtherId fields to the database
- [dev] CommandStrings will now use methodname as the key, and **not** the command name (first entry in aliases.yml)
    - In other words aliases.yml and commands.en-US.yml will use the same keys (once again)
- [dev] Reorganized module and submodule folders
- [dev] Permissionv2 db table renamed to Permissions
- [dev] Moved FilterWordsChannelId to a separate table

### Fixed

- Fixed twitch stream notifications (rewrote it to use the new api)
- Fixed an extra whitespace in usage part of command help if the command has no arguments
- Possible small fix for `.prune` ratelimiting
- `.gvc` should now properly trigger when a user is already in a gvc and changes his activity
- `.gvc` should now properly detect multiple activities
- Fixed reference to non-existent command in bot.yml
- Comment indentation in .yml files should now make more sense
- Fixed `.warn` punishments not being applied properly when using weighted warnings
- Fixed embed color when disabling `.antialt`

### Removed

- Removed `.bce` - use `.config` or `.config bot` specifically for bot config
- Removed obsolete placeholders: %users% %servers% %userfull% %username% %userdiscrim% %useravatar% %id% %uid% %chname%
  %cid% %sid% %members% %server_time% %shardid% %time% %mention%
- Removed some obsolete commands and strings
- Removed code which migrated 2.x to v3 credentials, settings, etc...

## [3.0.13] - 14.01.2022

### Fixed

- Fixed `.greetdm` causing ratelimits during raids
- Fixed `.gelbooru`

## [3.0.12] - 06.01.2022

### Fixed

- `.smch` Fixed
- `.trans` command will now work properly with capitilized language names
- Ban message color with plain text fixed
- Fixed some grpc coordinator bugs
- Fixed a string in `.xpex`
- Google version of .img will now have safe search enabled
- Fixed a small bug in `.hangman`

## [3.0.11] - 17.12.2021

### Added

- `.remindl` and `.remindrm` commands now supports optional 'server' parameter for Administrators which allows them to
  delete any reminder created on the server
- Added slots.currencyFontColor to gambling.yml
- Added `.qexport` and `.qimport` commands which allow you to export and import quotes just like `.crsexport`
- Added `.showembed <msgid>` and `.showembed #channel <msgid>` which will show you embed json from the specified message

### Changed

- `.at` and `.atl` commands reworked
    - Persist restarts
    - Will now only translate non-commands
    - You can switch between `.at del` and `.at` without clearing the user language registrations
    - Disabling `.at` will clear all user language registrations on that channel
    - Users can't register languages if the `.at` is not enabled
    - Looks much nicer
        - Bot will now reply to user messages with a translation if `del` is disabled
        - Bot will make an embed with original and translated text with user avatar and name if `del` is enabled
    - If the bot is unable to delete messages while having `del` enabled, it will reset back to the no-del behavior for
      the current session

### Fixed

- `.crypto` now supports top 5000 coins

## [3.0.10] - 01.12.2021

### Changed

- `.warn` now supports weighted warnings
- `.warnlog` will now show current amount and total amount of warnings

### Fixed

- `.xprewsreset` now has correct permissions

### Removed

- Removed slot.numbers from `images.yml` as they're no longer used

## [3.0.9] - 21.11.2021

### Changed

- `.ea` will now use an image attachments if you omit imageUrl

### Added

- Added `.emojiadd` with 3 overloads
    - `.ea :customEmoji:` which copies another server's emoji
    - `.ea newName :customEmoji:` which copies emoji under a different name
    - `.ea emojiName <imagelink.png>` which creates a new emoji from the specified image
- Patreon Access and Refresh Tokens should now be automatically updated once a month as long as the user has provided
  the necessary credentials in creds.yml file:
    - `Patreon.ClientId`
    - `Patreon.RefreshToken` (will also get updated once a month but needs an initial value)
    - `Patreon.ClientSecret`
    - `Patreon.CampaignId`

### Fixed

- Fixed an error that would show up in the console when a club image couldn't be drawn in certain circumstances

## [3.0.8] - 03.11.2021

### Added

- Created VotesApi project nad re-worked vote rewards handling
    - Updated votes entries in creds.yml with explanations on how to set up vote links

### Fixed

- Fixed adding currency to users who don't exist in the database
- Memory used by the bot is now correct (thanks to kotz)
- Ban/kick will no longer fail due to too long reasons
- Fixed some fields not preserving inline after string replacements

### Changed

- `images.json` moved to `images.yml`
    - Links will use the new cdn url
    - Heads and Tails images will be updated if you haven't changed them already
- `.slot` redesigned (and updated entries in `images.yml`)
- Reduced required permissions for .qdel (thanks to tbodt)

## [3.0.7] - 05.10.2021

### Added

- `.streamsclear` re-added. It will remove all followed streams on the server.
- `.gifts` now have 3 new ✂️ Haircut 🧻 ToiletPaper and 🥀 WiltedRose which **reduce** waifu's value
    - They are called negative gifts
    - They show up at the end of the `.gifts` page and are marked with a broken heart
    - They have a separate multiplier (`waifu.multi.negative_gift_effect` default 0.5, changeable via `.config gambling`
      or `data/gambling.yml`)
    - When gifted, the waifu's price will be reduced by the `price * multiplier`
    - Negative gifts don't show up in `.waifuinfo` nor is the record of them kept in the database

### Fixed

- Fixed `%users%` and `%shard.usercount%` placeholders not showing correct values

## [3.0.6] - 27.09.2021

### Added

- .logignore now supports ignoring users and channels. Use without parameters to see the ignore list

### Changed

- Hangman rewrite
    - Hangman categories are now held in separate .yml files in data/hangman/XYZ.yml where XYZ is the category name

### Fixed

- Fixed an exception which caused repeater queue to break
- Fixed url field not working in embeds

## [3.0.5] - 20.09.2021

### Fixed

- Fixed images not automatically reloading on startup if the keys don't exist
- Fixed `.logserver` - it should no longer throw an exception if you had no logsettings previously

## [3.0.4] - 16.09.2021

### Added

- Fully translated to Brazilian Portuguese 🎉
- Added `%server.boosters%` and `%server.boost_level%` placeholders
- Added `DmHelpTextKeywords` to `data/bot.yml`
    - Bot now sends dm help text ONLY if the message contains one of the keywords specified
    - If no keywords are specified, bot will reply to every DM (like before)

### Fixed

- Possible fix for `.repeat` bug
    - Slight adjustment for repeater logic
    - Timer should no longer increase on some repeaters
    - Repeaters should no longer have periods when they're missing from the list
- Fixed several commands which used error color for success confirmation messages

## [3.0.3] - 15.09.2021

### Added

- Added `.massban` to ban multiple people at once. 30 second cooldown
- Added `.youtubeuploadnotif` / `.yun` as a shortcut for subscribing to a youtube channel's rss feed
- Added `.imageonlychannel` / `.imageonly` to prevent users from posting anything but images in the channel
- Added `.config games hangman.currency_reward` and a property with the same name in games.yml
    - If set, users will gain the specified amount of currency for each hangman win
- Fully translated to Spanish, Russian and Ukrainian 🎉

### Changed

- Ban `.warnp` will now prune user's messages

### Fixed

- `.boostmsg` will now properly show boost, and not greet message

## [3.0.2] - 12.09.2021

### Added

- `.rero` now optionally takes a message id to which to attach the reaction roles
- Fully translated to German 🎉
- Added `.boost`, `.boostmsg` and `.boostdel` commands which allow you to have customizable messages when someone boosts
  your server, with auto-deletion support

### Changed

- Updated `.greetmsg` and `.byemsg` command help to match the new `.boost` command help
- Updated response embed colors in greet commands
    - Success -> green
    - Warning or Disable -> yellow.

### Fixed

- `.timely` will now correctly use `Ok` color
- Fixed `.log` commands

### Removed

- Removed `.novel` command as it no longer works

## [3.0.1] - 10.09.2021

### Fixed

- Fixed some issues with the embeds not showing the correct data

## [3.0.0] - 06.09.2021

### Changed

- Renamed `credentials.json` to `creds.yml` (example in `creds_example.yml`)
    - Most of the credentials from 2.x will be automatically migrated
    - Explanations on how to get the keys are added as the comments
- Code cleanup
    - Command attributes cleaned up
        - Removed dummy Remarks and Usages attributes as hey were unused for a few patches but stayed in the code to
          avoid big git diffsmigration code has ran and it can be safely removed
    - There are 2 projects: NadekoBot and NadekoBot.Coordinator
        - You can directly run NadekoBot as the regular bot with one shard
        - Run NadekoBot.Coordinator if you want more control over your shards and a grpc api for coordinator with which
          you can start, restart, kill and see status of shards
    - Small performance improvements
    - Db Migrations squashed
    - A lot of cleanup all around
- Many guides reworked
    - Guides now instruct users to set build output to nadekobot/output instead of running from nadekobot/src/NadekoBot

### Fixed

- Fixed many response strings which were formatted or used incorrectly

### Removed

- Removed All database migrations and data (json file) migrations
    - As updating to the latest 2.x version before switching over to v3 is mandated (or fresh v3 install), that means
      all

## [2.46.2] - 14.07.2021

### Fixed

- Fixed .save for local songs
- Fixed .lq for local songs if the song names are too long
- Fixed hierarchy check for .warnpunish with role argument

## [2.46.1] - 21.06.2021

### Fixed

- Fixed some response strings (thx Ala)
- Fixed repeaters having 5 global limit, instead of 5 server limit (thx cata)

## [2.46.0] - 17.06.2021

### Added

- Added some nsfw commands

### Changed

- `.aar` reworked. Now supports multiple roles, up to 3.
    - Toggle roles that are added to newly joined users with `.aar RoleName`
    - Use `.aar` to list roles which will be added
    - Roles which are deleted are automatically cleaned up from `.aar`
- `.inrole` now also shows user ids
- Blacklist commands (owner only) `.ubl` `.sbl` and `.cbl` will now list blacklisted items when no argument (or a page
  number) is provided
- `.cmdcd` now works with customreactions too
- `.xprr` usage changed. It now takes add/rm parameter to add/remove a role ex. You can only take or remove a single
  role, adding and removing a role at the same level doesn't work (yet?)
    - example: `.xprr 5 add Member` or `.xprr 1 rm Newbie`

## [2.45.2] - 14.06.2021

### Added

- Added `.duckduckgo / .ddg` search

### Changed

- `.invlist` shows expire time and is slightly prettier

### Fixed

- `.antialt` will be properly cleaned up when the bot leaves the server

## [2.45.1] - 12.06.2021

### Added

- Added many new aliases to custom reaction commands in the format ex + "action" to prepare for the future rename from
  CustomReactions to Expressions
- You can now `.divorce` via username#discrim even if the user no longer exists

### Changed

- DmHelpText should now have %prefix% and %bot.prefix% placeholders available
- Added squares which show enabled features for each cr in `.lcr`
- Changed CustomReactions' IDs to show, and accept base 32 unambigous characters instead of the normal database IDs (
  this will result in much shorter cr IDs in case you have a lot of them)
- Improved `.lcr` helptext to explain what's shown in the output
- `.rolecolor <color> <role>` changed to take color, then the role, to make it easier to set color for roles with
  multiple words without mentioning the role
- `.acmdcds` alias chanaged to `.cmdcds`
- `.8ball` will now cache results for a day
- `.chatmute` and `.voicemute` now support timed mutes

### Fixed

- Fixed `.config <conf> <prop>` exceeding embed field character limit

## [2.45.0] - 10.06.2021

### Added

- Added `.crsexport` and `.crsimport`
    - Allows for quick export/import of server or global custom reactions
    - Requires admin permissions for server crs, and owner for global crs
    - Explanation of the fields is in the comment at the top of the `.crsexport` .yml file
- Added `.mquality` / `.musicquality` - Set encoding quality. Has 4 presets - Low, Medium, High, Highest. Default is
  Highest
- Added `.xprewsreset` which resets all currently set xp level up rewards
- Added `.purgeuser @User` which will remove the specified from the database completely. Removed settings include: Xp,
  clubs, waifu, currency, etc...
- Added `.config xp txt.per_image` and xpFromImage to xp.yml - Change this config to allow xp gain from posting images.
  Images must be 128x128 or greater in size
- Added `.take <amount> <role>` to complement `.award <amount> role`
- Added **Fans** list to `.waifuinfo` which shows how many people have their affinity set to you
- Added `.antialt` which will punish any user whose account is younger than specified threshold

### Changed

- `.warne` with no args will now show current state
- .inrole` will now lists users with no roles if no role is provided
- Music suttering fixed on some systems
- `.say` moved to utility module
- Re-created GuildRepeaters table and renamed to Repeaters
- confirmation prompts will now use pending color from bot config, instead of okcolor
- `.mute` can now have up to 49 days mute to match .warnp
- `.warnlog` now has proper pagination (with reactions) and checking your own warnings past page 1 works correctly now
  with `.warnlog 2`

### Fixed

- obsolete_use string fixed
- Fixed `.crreact`

## [2.44.4] - 06.06.2021

### Added

- Re-added `%music.playing%` and `%music.queued%` (#290)
- Added `%music.servers%` which shows how many servers have a song queued up to play
  ℹ️ ^ Only available to `.ropl` / `.adpl` feature atm
- `.autodc` re-added
- `.qrp`, `.vol`, `.smch` `.autodc` will now persist

### Changed

- Using `.commands` / `.cmds` without a module will now list modules
- `.qrp` / `.queuerepeat` will now accept one of 3 values
    - `none` - don't repeat queue
    - `track` - repeat single track
    - `queue` (or ommit) - repeat entire queue
- your old `.defvol` and `.smch` settings will be reset

### Fixed

- Fixed `.google` / `.g` command
- Removing last song in the queue will no longer reset queue index
- Having `.rpl` disabled will now correctly stop after the last song, closes #292

### Removed

- `.sad` removed. It's more or less useless. Use `.qrp` and `.autodc` now for similar effect

### Obsolete

- `.rcs` is obsolete, use `.qrp s` or `.qrp song`
- `.defvol` is obsolete, use `.vol`

## [2.44.3] - 04.06.2021

### Changed

- Minor perf improvement for filter checks

### Fixed

- `.qs` result urls are now valid
- Custom reactions with "`-`" as a response should once again disable that custom reaction completely
- Fixed `.acrm` out of range string
- Fixed `.sclist` and `.aclist` not showing correct indexes past page 1

## [2.44.2] - 02.06.2021

### Added

- Music related commands reimplemented with custom code, **considered alpha state**
- Song and playlist caching (faster song queue after first time)
- Much faster starting and skipping once the songs are in the queue
- Higher quality audio (no stuttering too!)
- Local tracks will now have durations if you have ffprobe installed (comes with ffmpeg)
- Bot supports joining a different vc without skipping the song if you use `.j`
    - ⚠️ **DO NOT DRAG THE BOT** to another vc, as it's not properly supported atm, and you will have to do `.play`
      after dragging it)
- `.j` makes the bot join your voice channel
- `.p` is now alias of play, pause is `.pause`
- `.qs` should work without google api key now for most users as it is using a custom loader
- Added `.clubs` alias for `.clublb`

### Changed

- `.ms` no longer takes `>` between arguments (`.ms 1 5` now, was `.ms 1>5` before)
- FlowerShop renamed to Shop

### Fixed

- Fixed decay bug giving everyone 1 flower every 24h
- Fixed feeds which have rss media items without a type
- Fixed `.acrm` index not working
- Fixed and error reply when a waifu item doesn't exist
- Disabled colored console on windows as they were causing issues for some users
- Fixed/Updated some strings and several minor bugfixes

### Removed

- Removed admin requirement on `.scrm` as it didn't make sense
- Some Music commands are removed because of the complexity they bring in with little value (if you *really* want them
  back, you can open an issue and specify your *good* reason)
