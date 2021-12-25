﻿global using Serilog;
global using Humanizer;

global using NadekoBot;

global using NadekoBot.Services;
global using NadekoBot.Common;
global using NadekoBot.Common.Attributes;
global using NadekoBot.Extensions;

global using Discord;
global using Discord.Commands;
global using Discord.Net;
global using Discord.WebSocket;

global using GuildPerm = Discord.GuildPermission;
global using ChannelPerm = Discord.ChannelPermission;
global using BotPermAttribute = Discord.Commands.RequireBotPermissionAttribute;
global using LeftoverAttribute = Discord.Commands.RemainderAttribute;

global using System.Collections.Concurrent;

global using JetBrains.Annotations;