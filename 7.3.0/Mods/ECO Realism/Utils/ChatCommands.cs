﻿    using Eco.Gameplay.Players;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Chat;
    using System;
    using System.Collections.Generic;
    using System.Linq;
using Eco.Gameplay.Property;
using Eco.Shared.Math;
using Eco.Shared.Utils;
using Eco.Shared.Networking;
using Eco.Simulation.WorldLayers;
using Eco.Simulation.Types;
using Eco.Shared.Localization;
using Eco.Simulation.Time;
using Eco.Gameplay;
using Eco.Gameplay.Economy;

namespace EcoRealism.Utils
{
    public class ChatCommands : IChatCommandHandler
    {
        //private static string separator = "[#SEPARATOR#]";

        [ChatCommand("rules", "Displays the Server Rules")]
        public static void Rules(User user)
        {
            string x = IOUtils.ReadConfig("rules.txt");
            x = ChatUtils.AutoLink(x);

            user.Player.OpenInfoPanel("Rules", x);
        }

        [ChatCommand("modkit", "Display information about the modkit")]
        public static void Modkit(User user)
        {
            string x = IOUtils.ReadConfig("modkit.txt");
            x = ChatUtils.AutoLink(x);

            user.Player.OpenInfoPanel("Modkit", x);
        }

        [ChatCommand("changelog", "Displays the changelog of the modkit")]
        public static void Changelog(User user)
        {
            string x = IOUtils.ReadConfig("changelog.txt");
            x = ChatUtils.AutoLink(x);

            user.Player.OpenInfoPanel("Changelog", x);
        }

        [ChatCommand("report", "Report a player or an issue")]
        public static void Report(User user, string part1, string part2 = "", string part3 = "", string part4 = "", string part5 = "", string part6 = "", string part7 = "", string part8 = "", string part9 = "", string part10 = "", string eof = "")
        {
            if(eof != "")
            {
                user.Player.SendTemporaryErrorAlreadyLocalized("Sorry, too many commas (this command only supports up to 9");
                return;
            }
            string singlestringreport = part1 + "," + part2 + "," + part3 + "," + part4 + "," + part5 + "," + part6 + "," + part7 + "," + part8 + "," + part9 + "," + part10;
            singlestringreport = singlestringreport.TrimEnd(',');
            string texttoadd = string.Empty;

            foreach (User usercheck in UserManager.OnlineUsers)
            {
                if (usercheck.IsAdmin) usercheck.Player.OpenCustomPanel("New Report from " + user.Name, TextLinkManager.MarkUpText(singlestringreport), DateTime.Now.Ticks.ToString());
            }

            texttoadd = TextLinkManager.MarkUpText("<b>" + user.Name + " " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":</b>" + System.Environment.NewLine + singlestringreport + System.Environment.NewLine + System.Environment.NewLine);
            IOUtils.WriteToFile("./mods/ECO Realism/Reports/reports.txt", texttoadd);
            user.Player.SendTemporaryMessageAlreadyLocalized("Report sent");

        }

        [ChatCommand("reports", "Displays current Reports", level: ChatAuthorizationLevel.Admin)]
        public static void Reports(User user)
        {
            string x = IOUtils.ReadConfig("../Reports/reports.txt");
            user.Player.OpenInfoPanel("Recent Reports", x);
        }

        [ChatCommand("clearreports", "Deletes all current reports", level: ChatAuthorizationLevel.Admin)]
        public static void ClearReports(User user)
        {
            IOUtils.ClearFile("./mods/ECO Realism/Reports/reports.txt");
            user.Player.SendTemporaryMessageAlreadyLocalized("Reports cleared");
        }

        [ChatCommand("houseranking", "Displays the users with the highest housing rates")]
        public static void HouseRanking(User user)
        {

            int usercount = UserManager.Users.Count<User>();
            int max = 10;
            int i = 0;
            string output = string.Empty;
            List<KeyValuePair< string, float>> userlist = new List<KeyValuePair<string, float>>(usercount);
            string tmpuser = string.Empty;

            foreach (User userentry in UserManager.Users)
            {
                userlist.Add(new KeyValuePair<string,float>(userentry.Name, (float)Math.Round(userentry.CachedHouseValue.HousingSkillRate,2)));
            }

            userlist.Sort(new MyComparer());           

            if (usercount < max) max = usercount;
            for(i=0; i<max; i++)
            {
                tmpuser = userlist[i].Key;
                output = output + (i + 1).ToString() + ". <link=\"User:" + tmpuser + "\"><style=\"Warning\">" + tmpuser + "</style></link>: <link=\"CachedHouseValue:" + tmpuser +"\"><style=\"Positive\">" + userlist[i].Value.ToString() + "</style></link> skill/day <br>";
            }
            output = output + "<br> Your " + user.HouseValue;

            user.Player.OpenInfoPanel("House Ranking", output);
        }

        [ChatCommand("avatar","Displays information about you or the given Player")]
        public static void Avatarcmd(User user, string playername = "")
        {
            Player targetplayer;
            User targetuser;
            if (playername == "")
            {
                targetplayer = user.Player;
                targetuser = user;
            }
            else
            {
                targetuser = UserManager.FindUserByName(playername);
                if (targetuser == null)
                {
                    user.Player.SendTemporaryErrorAlreadyLocalized("Player " + playername + " not found!");
                    return;
                }
                targetplayer = targetuser.Player;
            }

            string title = "Stats for " + targetuser.Name ;
            string housinginfo;
            string foodinfo;
            string totalsp;
            string onlineinfo;
            string professioninfo;
            string currencyinfo = string.Empty;
            string propertyinfo;
            string newline = "<br>";


            foreach(Currency currency in EconomyManager.Currency.Currencies)
            {
                if (currency.HasAccount(targetuser.Name))
                {
                    if (currency.GetAccount(targetuser.Name).Val > 0f)
                    {
                        currencyinfo += currency.GetAccount(targetuser.Name).ToString() + newline;
                    }
                }
            }


            float foodsp = targetuser.SkillRate;
            float housesp = targetuser.CachedHouseValue.HousingSkillRate;

            currencyinfo = targetuser.Currency.UserReport(targetuser.Name);
            
            housinginfo = "House SP: " + targetuser.CachedHouseValue.UILink();
            foodinfo =  "Food SP: " + Math.Round(foodsp,2);
            totalsp = "Total SP: " + Math.Round(housesp + foodsp, 2);
            propertyinfo = "Owns " + MiscUtils.CountPlots(targetuser) * 25 + "m² of land.";
            onlineinfo = targetuser.LoggedIn ? "Logged in. Located at " + new Vector3Tooltip(targetuser.Position).UILink() : "Logged out. Last online " + TimeFormatter.FormatSpan(WorldTime.Seconds - targetuser.LogoutTime) + " ago";
            //currencyinfo = targetuser.Currency.UserReport(targetuser.Name);

            user.Player.OpenInfoPanel(title,targetuser.UILink() + " " + onlineinfo + newline + newline + foodinfo + newline + housinginfo + newline + totalsp + newline + newline + propertyinfo + newline + currencyinfo);

        }

        [ChatCommand("donate", "Donates Money to the Treasury")]
        public static void Donate(User user, float amount, string currencytype)
        {
            Currency currency = EconomyManager.Currency.GetCurrency(currencytype);
            if(currency==null)
            {
                user.Player.SendTemporaryErrorAlreadyLocalized("Currency " + currencytype + " not found.");
                return;
            }
           
            if (amount <= 0)
            {
                user.Player.SendTemporaryErrorLoc("Must specify an amount greater than 0.");
                return;
            }
            Legislation.Government.PayTax(currency, user, amount, "Voluntary Donation");
            user.Player.SendTemporaryMessageAlreadyLocalized("You donated " + currency.UILink(amount) + " to the Treasury. Thank you!");

        }

        [ChatCommand("superskillshelp", "Displays an infobox about Super Skills")]
        public static void SuperSkillhelp(User user)
        {
            SkillUtils.ShowSuperSkillInfo(user.Player);
        }

        [ChatCommand("confirmsuperskill", "Confirm that you read the warning about Super Skills")]
        public static void ConfirmSuperSkill(User user)
        {
            foreach(string id in SkillUtils.superskillconfirmed)
            {
                if (id == user.ID)
                {
                    user.Player.SendTemporaryErrorAlreadyLocalized("You already unlocked Super Skills");
                    return;
                }                   
            }
            user.Player.SendTemporaryMessageAlreadyLocalized("You can now level up a Skill to level 10");
            SkillUtils.superskillconfirmed.Add(user.ID);
        }

        [ChatCommand("unclaimselect", "Selects the owner of the plot you're standing on for unclaimconfirm", level: ChatAuthorizationLevel.Admin)]
        public static void UnclaimPlayer(User user)
        {
            User owner = PropertyManager.GetPlot(user.Position.XZi).Owner;
            if (owner == null)
            {
                user.Player.SendTemporaryMessageAlreadyLocalized("Player not Found");
                return;
            }

            if (UtilsClipboard.UnclaimSelector.ContainsKey(user)) UtilsClipboard.UnclaimSelector.Remove(user);
            UtilsClipboard.UnclaimSelector.Add(user, owner);

            UtilsClipboard.UnclaimSelector.TryGetValue(user, out owner);
            user.Player.SendTemporaryMessageAlreadyLocalized(TextLinkManager.MarkUpText("Selected " + owner.Name + " as target! "));
            //ChatUtils.SendMessage(user, "Then: " + owner.OfflineInfo.LoggoutTime.ToString());
            //ChatUtils.SendMessage(user, "Now: " + TimeUtil.Ticks);
            //ChatUtils.SendMessage(user, "Diff: " + TimeUtil.DaysHoursMinutes(TimeUtil.TicksToSeconds((TimeUtil.Ticks - (long)owner.OfflineInfo.LoggoutTime))));

        }

        [ChatCommand("unclaimconfirm", "Unclaims all property of the selected player", level: ChatAuthorizationLevel.Admin)]
        public static void UnclaimConfirm(User user)
        {
            User target = null;
            int totalplotcount = 0, deedcount = 0, plotcountdeed = 0, vehiclecount = 0;
            IEnumerable<Vector2i> positions;
            string tmp = string.Empty;


            if (!UtilsClipboard.UnclaimSelector.TryGetValue(user, out target))
            {
                user.Player.SendTemporaryErrorAlreadyLocalized("no User selected");
                return;
            }
            IEnumerable<AuthorizationController> authorizationControllers = PropertyManager.GetAuthBelongingTo(target);
            foreach (AuthorizationController auth in authorizationControllers)
            {
                plotcountdeed = 0;
                if (auth.Type == "Property")
                {
                    deedcount++;
                    positions = PropertyManager.PositionsForId(auth.Id);
                    foreach (Vector2i pos in positions)
                    {
                        plotcountdeed++;
                        totalplotcount++;
                        //ChatUtils.SendMessage(user, TextLinkManager.MarkUpText("Try unclaiming: (" + pos.X + "," + pos.Y + ")"));
                        PropertyManager.UnclaimProperty(pos);
                    }
                    ChatUtils.SendMessage(user, "Unclaimed: " + auth.Name + " (" + plotcountdeed.ToString() + " Plots)");
                }
                else if (auth.Type == "Vehicle")
                {
                    vehiclecount++;
                    ChatUtils.SendMessage(user, "Found Vehicle: " + auth.Name);

                    //(auth.AttachedWorldObjects[0].Object as WorldObject).Destroy();

                    //foreach (WorldObjectHandle objhandle in auth.AttachedWorldObjects)
                    //{
                    //    tmp = objhandle.Object.Name;
                    //    (objhandle.Object as WorldObject).Destroy();
                    //    ChatUtils.SendMessage(user, "Destroyed " + tmp);
                    //}                    

                }
            }

            ChatUtils.SendMessage(user, totalplotcount.ToString() + " Plots on " + deedcount.ToString() + " Deeds unclaimed. Also found " + vehiclecount.ToString() + " Vehicles (those aren't handled by this command yet)");
        }
    }


}