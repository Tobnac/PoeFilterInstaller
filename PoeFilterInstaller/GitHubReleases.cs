using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var gitHubReleases = GitHubReleases.FromJson(jsonString);

namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
    using R = Newtonsoft.Json.Required;
    using N = Newtonsoft.Json.NullValueHandling;

    public partial class GitHubReleases
    {
        [J("url")] public string Url { get; set; }
        [J("assets_url")] public string AssetsUrl { get; set; }
        [J("upload_url")] public string UploadUrl { get; set; }
        [J("html_url")] public string HtmlUrl { get; set; }
        [J("id")] public long Id { get; set; }
        [J("tag_name")] public string TagName { get; set; }
        [J("target_commitish")] public TargetCommitish TargetCommitish { get; set; }
        [J("name")] public string Name { get; set; }
        [J("draft")] public bool Draft { get; set; }
        [J("author")] public Author Author { get; set; }
        [J("prerelease")] public bool Prerelease { get; set; }
        [J("created_at")] public DateTimeOffset CreatedAt { get; set; }
        [J("published_at")] public DateTimeOffset PublishedAt { get; set; }
        [J("assets")] public object[] Assets { get; set; }
        [J("tarball_url")] public string TarballUrl { get; set; }
        [J("zipball_url")] public string ZipballUrl { get; set; }
        [J("body")] public string Body { get; set; }
    }

    public partial class Author
    {
        [J("login")] public Login Login { get; set; }
        [J("id")] public long Id { get; set; }
        [J("avatar_url")] public AvatarUrl AvatarUrl { get; set; }
        [J("gravatar_id")] public GravatarId GravatarId { get; set; }
        [J("url")] public Url Url { get; set; }
        [J("html_url")] public HtmlUrl HtmlUrl { get; set; }
        [J("followers_url")] public FollowersUrl FollowersUrl { get; set; }
        [J("following_url")] public FollowingUrl FollowingUrl { get; set; }
        [J("gists_url")] public GistsUrl GistsUrl { get; set; }
        [J("starred_url")] public StarredUrl StarredUrl { get; set; }
        [J("subscriptions_url")] public SubscriptionsUrl SubscriptionsUrl { get; set; }
        [J("organizations_url")] public OrganizationsUrl OrganizationsUrl { get; set; }
        [J("repos_url")] public ReposUrl ReposUrl { get; set; }
        [J("events_url")] public EventsUrl EventsUrl { get; set; }
        [J("received_events_url")] public ReceivedEventsUrl ReceivedEventsUrl { get; set; }
        [J("type")] public TypeEnum Type { get; set; }
        [J("site_admin")] public bool SiteAdmin { get; set; }
    }

    public enum AvatarUrl { HttpsAvatars3GithubusercontentComU2942999V4 };

    public enum EventsUrl { HttpsApiGithubComUsersNeverSinkDevEventsPrivacy };

    public enum FollowersUrl { HttpsApiGithubComUsersNeverSinkDevFollowers };

    public enum FollowingUrl { HttpsApiGithubComUsersNeverSinkDevFollowingOtherUser };

    public enum GistsUrl { HttpsApiGithubComUsersNeverSinkDevGistsGistId };

    public enum GravatarId { Empty };

    public enum HtmlUrl { HttpsGithubComNeverSinkDev };

    public enum Login { NeverSinkDev };

    public enum OrganizationsUrl { HttpsApiGithubComUsersNeverSinkDevOrgs };

    public enum ReceivedEventsUrl { HttpsApiGithubComUsersNeverSinkDevReceivedEvents };

    public enum ReposUrl { HttpsApiGithubComUsersNeverSinkDevRepos };

    public enum StarredUrl { HttpsApiGithubComUsersNeverSinkDevStarredOwnerRepo };

    public enum SubscriptionsUrl { HttpsApiGithubComUsersNeverSinkDevSubscriptions };

    public enum TypeEnum { User };

    public enum Url { HttpsApiGithubComUsersNeverSinkDev };

    public enum TargetCommitish { Master };

    public partial class GitHubReleases
    {
        public static GitHubReleases[] FromJson(string json) => JsonConvert.DeserializeObject<GitHubReleases[]>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this GitHubReleases[] self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new SubscriptionsUrlConverter(),
                new TypeEnumConverter(),
                new UrlConverter(),
                new AvatarUrlConverter(),
                new TargetCommitishConverter(),
                new EventsUrlConverter(),
                new FollowersUrlConverter(),
                new FollowingUrlConverter(),
                new GistsUrlConverter(),
                new GravatarIdConverter(),
                new HtmlUrlConverter(),
                new LoginConverter(),
                new OrganizationsUrlConverter(),
                new ReceivedEventsUrlConverter(),
                new ReposUrlConverter(),
                new StarredUrlConverter(),
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class SubscriptionsUrlConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(SubscriptionsUrl) || t == typeof(SubscriptionsUrl?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "https://api.github.com/users/NeverSinkDev/subscriptions")
            {
                return SubscriptionsUrl.HttpsApiGithubComUsersNeverSinkDevSubscriptions;
            }
            throw new Exception("Cannot unmarshal type SubscriptionsUrl");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (SubscriptionsUrl)untypedValue;
            if (value == SubscriptionsUrl.HttpsApiGithubComUsersNeverSinkDevSubscriptions)
            {
                serializer.Serialize(writer, "https://api.github.com/users/NeverSinkDev/subscriptions"); return;
            }
            throw new Exception("Cannot marshal type SubscriptionsUrl");
        }
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "User")
            {
                return TypeEnum.User;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (TypeEnum)untypedValue;
            if (value == TypeEnum.User)
            {
                serializer.Serialize(writer, "User"); return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }
    }

    internal class UrlConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Url) || t == typeof(Url?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "https://api.github.com/users/NeverSinkDev")
            {
                return Url.HttpsApiGithubComUsersNeverSinkDev;
            }
            throw new Exception("Cannot unmarshal type Url");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Url)untypedValue;
            if (value == Url.HttpsApiGithubComUsersNeverSinkDev)
            {
                serializer.Serialize(writer, "https://api.github.com/users/NeverSinkDev"); return;
            }
            throw new Exception("Cannot marshal type Url");
        }
    }

    internal class AvatarUrlConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(AvatarUrl) || t == typeof(AvatarUrl?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "https://avatars3.githubusercontent.com/u/2942999?v=4")
            {
                return AvatarUrl.HttpsAvatars3GithubusercontentComU2942999V4;
            }
            throw new Exception("Cannot unmarshal type AvatarUrl");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (AvatarUrl)untypedValue;
            if (value == AvatarUrl.HttpsAvatars3GithubusercontentComU2942999V4)
            {
                serializer.Serialize(writer, "https://avatars3.githubusercontent.com/u/2942999?v=4"); return;
            }
            throw new Exception("Cannot marshal type AvatarUrl");
        }
    }

    internal class TargetCommitishConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TargetCommitish) || t == typeof(TargetCommitish?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "master")
            {
                return TargetCommitish.Master;
            }
            throw new Exception("Cannot unmarshal type TargetCommitish");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (TargetCommitish)untypedValue;
            if (value == TargetCommitish.Master)
            {
                serializer.Serialize(writer, "master"); return;
            }
            throw new Exception("Cannot marshal type TargetCommitish");
        }
    }

    internal class EventsUrlConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(EventsUrl) || t == typeof(EventsUrl?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "https://api.github.com/users/NeverSinkDev/events{/privacy}")
            {
                return EventsUrl.HttpsApiGithubComUsersNeverSinkDevEventsPrivacy;
            }
            throw new Exception("Cannot unmarshal type EventsUrl");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (EventsUrl)untypedValue;
            if (value == EventsUrl.HttpsApiGithubComUsersNeverSinkDevEventsPrivacy)
            {
                serializer.Serialize(writer, "https://api.github.com/users/NeverSinkDev/events{/privacy}"); return;
            }
            throw new Exception("Cannot marshal type EventsUrl");
        }
    }

    internal class FollowersUrlConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FollowersUrl) || t == typeof(FollowersUrl?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "https://api.github.com/users/NeverSinkDev/followers")
            {
                return FollowersUrl.HttpsApiGithubComUsersNeverSinkDevFollowers;
            }
            throw new Exception("Cannot unmarshal type FollowersUrl");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (FollowersUrl)untypedValue;
            if (value == FollowersUrl.HttpsApiGithubComUsersNeverSinkDevFollowers)
            {
                serializer.Serialize(writer, "https://api.github.com/users/NeverSinkDev/followers"); return;
            }
            throw new Exception("Cannot marshal type FollowersUrl");
        }
    }

    internal class FollowingUrlConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FollowingUrl) || t == typeof(FollowingUrl?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "https://api.github.com/users/NeverSinkDev/following{/other_user}")
            {
                return FollowingUrl.HttpsApiGithubComUsersNeverSinkDevFollowingOtherUser;
            }
            throw new Exception("Cannot unmarshal type FollowingUrl");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (FollowingUrl)untypedValue;
            if (value == FollowingUrl.HttpsApiGithubComUsersNeverSinkDevFollowingOtherUser)
            {
                serializer.Serialize(writer, "https://api.github.com/users/NeverSinkDev/following{/other_user}"); return;
            }
            throw new Exception("Cannot marshal type FollowingUrl");
        }
    }

    internal class GistsUrlConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(GistsUrl) || t == typeof(GistsUrl?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "https://api.github.com/users/NeverSinkDev/gists{/gist_id}")
            {
                return GistsUrl.HttpsApiGithubComUsersNeverSinkDevGistsGistId;
            }
            throw new Exception("Cannot unmarshal type GistsUrl");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (GistsUrl)untypedValue;
            if (value == GistsUrl.HttpsApiGithubComUsersNeverSinkDevGistsGistId)
            {
                serializer.Serialize(writer, "https://api.github.com/users/NeverSinkDev/gists{/gist_id}"); return;
            }
            throw new Exception("Cannot marshal type GistsUrl");
        }
    }

    internal class GravatarIdConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(GravatarId) || t == typeof(GravatarId?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "")
            {
                return GravatarId.Empty;
            }
            throw new Exception("Cannot unmarshal type GravatarId");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (GravatarId)untypedValue;
            if (value == GravatarId.Empty)
            {
                serializer.Serialize(writer, ""); return;
            }
            throw new Exception("Cannot marshal type GravatarId");
        }
    }

    internal class HtmlUrlConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(HtmlUrl) || t == typeof(HtmlUrl?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "https://github.com/NeverSinkDev")
            {
                return HtmlUrl.HttpsGithubComNeverSinkDev;
            }
            throw new Exception("Cannot unmarshal type HtmlUrl");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (HtmlUrl)untypedValue;
            if (value == HtmlUrl.HttpsGithubComNeverSinkDev)
            {
                serializer.Serialize(writer, "https://github.com/NeverSinkDev"); return;
            }
            throw new Exception("Cannot marshal type HtmlUrl");
        }
    }

    internal class LoginConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Login) || t == typeof(Login?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "NeverSinkDev")
            {
                return Login.NeverSinkDev;
            }
            throw new Exception("Cannot unmarshal type Login");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Login)untypedValue;
            if (value == Login.NeverSinkDev)
            {
                serializer.Serialize(writer, "NeverSinkDev"); return;
            }
            throw new Exception("Cannot marshal type Login");
        }
    }

    internal class OrganizationsUrlConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(OrganizationsUrl) || t == typeof(OrganizationsUrl?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "https://api.github.com/users/NeverSinkDev/orgs")
            {
                return OrganizationsUrl.HttpsApiGithubComUsersNeverSinkDevOrgs;
            }
            throw new Exception("Cannot unmarshal type OrganizationsUrl");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (OrganizationsUrl)untypedValue;
            if (value == OrganizationsUrl.HttpsApiGithubComUsersNeverSinkDevOrgs)
            {
                serializer.Serialize(writer, "https://api.github.com/users/NeverSinkDev/orgs"); return;
            }
            throw new Exception("Cannot marshal type OrganizationsUrl");
        }
    }

    internal class ReceivedEventsUrlConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ReceivedEventsUrl) || t == typeof(ReceivedEventsUrl?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "https://api.github.com/users/NeverSinkDev/received_events")
            {
                return ReceivedEventsUrl.HttpsApiGithubComUsersNeverSinkDevReceivedEvents;
            }
            throw new Exception("Cannot unmarshal type ReceivedEventsUrl");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ReceivedEventsUrl)untypedValue;
            if (value == ReceivedEventsUrl.HttpsApiGithubComUsersNeverSinkDevReceivedEvents)
            {
                serializer.Serialize(writer, "https://api.github.com/users/NeverSinkDev/received_events"); return;
            }
            throw new Exception("Cannot marshal type ReceivedEventsUrl");
        }
    }

    internal class ReposUrlConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ReposUrl) || t == typeof(ReposUrl?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "https://api.github.com/users/NeverSinkDev/repos")
            {
                return ReposUrl.HttpsApiGithubComUsersNeverSinkDevRepos;
            }
            throw new Exception("Cannot unmarshal type ReposUrl");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ReposUrl)untypedValue;
            if (value == ReposUrl.HttpsApiGithubComUsersNeverSinkDevRepos)
            {
                serializer.Serialize(writer, "https://api.github.com/users/NeverSinkDev/repos"); return;
            }
            throw new Exception("Cannot marshal type ReposUrl");
        }
    }

    internal class StarredUrlConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(StarredUrl) || t == typeof(StarredUrl?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "https://api.github.com/users/NeverSinkDev/starred{/owner}{/repo}")
            {
                return StarredUrl.HttpsApiGithubComUsersNeverSinkDevStarredOwnerRepo;
            }
            throw new Exception("Cannot unmarshal type StarredUrl");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (StarredUrl)untypedValue;
            if (value == StarredUrl.HttpsApiGithubComUsersNeverSinkDevStarredOwnerRepo)
            {
                serializer.Serialize(writer, "https://api.github.com/users/NeverSinkDev/starred{/owner}{/repo}"); return;
            }
            throw new Exception("Cannot marshal type StarredUrl");
        }
    }
}
