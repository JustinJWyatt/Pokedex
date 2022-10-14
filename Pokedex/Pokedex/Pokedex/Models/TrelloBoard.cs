using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Models
{
    public class BackgroundImageScaled
    {
        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class LabelNames
    {
        [JsonProperty("green")]
        public string Green { get; set; }

        [JsonProperty("yellow")]
        public string Yellow { get; set; }

        [JsonProperty("orange")]
        public string Orange { get; set; }

        [JsonProperty("red")]
        public string Red { get; set; }

        [JsonProperty("purple")]
        public string Purple { get; set; }

        [JsonProperty("blue")]
        public string Blue { get; set; }

        [JsonProperty("sky")]
        public string Sky { get; set; }

        [JsonProperty("lime")]
        public string Lime { get; set; }

        [JsonProperty("pink")]
        public string Pink { get; set; }

        [JsonProperty("black")]
        public string Black { get; set; }

        [JsonProperty("green_dark")]
        public string GreenDark { get; set; }

        [JsonProperty("yellow_dark")]
        public string YellowDark { get; set; }

        [JsonProperty("orange_dark")]
        public string OrangeDark { get; set; }

        [JsonProperty("red_dark")]
        public string RedDark { get; set; }

        [JsonProperty("purple_dark")]
        public string PurpleDark { get; set; }

        [JsonProperty("blue_dark")]
        public string BlueDark { get; set; }

        [JsonProperty("sky_dark")]
        public string SkyDark { get; set; }

        [JsonProperty("lime_dark")]
        public string LimeDark { get; set; }

        [JsonProperty("pink_dark")]
        public string PinkDark { get; set; }

        [JsonProperty("black_dark")]
        public string BlackDark { get; set; }

        [JsonProperty("green_light")]
        public string GreenLight { get; set; }

        [JsonProperty("yellow_light")]
        public string YellowLight { get; set; }

        [JsonProperty("orange_light")]
        public string OrangeLight { get; set; }

        [JsonProperty("red_light")]
        public string RedLight { get; set; }

        [JsonProperty("purple_light")]
        public string PurpleLight { get; set; }

        [JsonProperty("blue_light")]
        public string BlueLight { get; set; }

        [JsonProperty("sky_light")]
        public string SkyLight { get; set; }

        [JsonProperty("lime_light")]
        public string LimeLight { get; set; }

        [JsonProperty("pink_light")]
        public string PinkLight { get; set; }

        [JsonProperty("black_light")]
        public string BlackLight { get; set; }
    }

    public class TrelloListItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }

        [JsonProperty("idBoard")]
        public string IdBoard { get; set; }

        [JsonProperty("pos")]
        public int Pos { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }

        [JsonProperty("softLimit")]
        public object SoftLimit { get; set; }

        public int CardCount { get; set; }
    }

    public class Prefs
    {
        [JsonProperty("permissionLevel")]
        public string PermissionLevel { get; set; }

        [JsonProperty("hideVotes")]
        public bool HideVotes { get; set; }

        [JsonProperty("voting")]
        public string Voting { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }

        [JsonProperty("invitations")]
        public string Invitations { get; set; }

        [JsonProperty("selfJoin")]
        public bool SelfJoin { get; set; }

        [JsonProperty("cardCovers")]
        public bool CardCovers { get; set; }

        [JsonProperty("isTemplate")]
        public bool IsTemplate { get; set; }

        [JsonProperty("cardAging")]
        public string CardAging { get; set; }

        [JsonProperty("calendarFeedEnabled")]
        public bool CalendarFeedEnabled { get; set; }

        [JsonProperty("hiddenPluginBoardButtons")]
        public List<object> HiddenPluginBoardButtons { get; set; }

        [JsonProperty("switcherViews")]
        public List<SwitcherView> SwitcherViews { get; set; }

        [JsonProperty("background")]
        public string Background { get; set; }

        [JsonProperty("backgroundColor")]
        public object BackgroundColor { get; set; }

        [JsonProperty("backgroundImage")]
        public string BackgroundImage { get; set; }

        [JsonProperty("backgroundImageScaled")]
        public List<BackgroundImageScaled> BackgroundImageScaled { get; set; }

        [JsonProperty("backgroundTile")]
        public bool BackgroundTile { get; set; }

        [JsonProperty("backgroundBrightness")]
        public string BackgroundBrightness { get; set; }

        [JsonProperty("backgroundBottomColor")]
        public string BackgroundBottomColor { get; set; }

        [JsonProperty("backgroundTopColor")]
        public string BackgroundTopColor { get; set; }

        [JsonProperty("canBePublic")]
        public bool CanBePublic { get; set; }

        [JsonProperty("canBeEnterprise")]
        public bool CanBeEnterprise { get; set; }

        [JsonProperty("canBeOrg")]
        public bool CanBeOrg { get; set; }

        [JsonProperty("canBePrivate")]
        public bool CanBePrivate { get; set; }

        [JsonProperty("canInvite")]
        public bool CanInvite { get; set; }
    }

    public class TrelloBoard
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("descData")]
        public object DescData { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }

        [JsonProperty("idOrganization")]
        public string IdOrganization { get; set; }

        [JsonProperty("idEnterprise")]
        public object IdEnterprise { get; set; }

        [JsonProperty("pinned")]
        public bool Pinned { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("shortUrl")]
        public string ShortUrl { get; set; }

        [JsonProperty("prefs")]
        public Prefs Prefs { get; set; }

        [JsonProperty("labelNames")]
        public LabelNames LabelNames { get; set; }

        [JsonProperty("lists")]
        public List<TrelloListItem> Lists { get; set; }

        [JsonProperty("cards")]
        public List<TrelloCard> Cards { get; set; }
    }

    public class SwitcherView
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("viewType")]
        public string ViewType { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }


}
