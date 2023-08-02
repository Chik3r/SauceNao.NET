using System.Text.Json.Serialization;

namespace SauceNao.NET.Model; 

public record Data(
        [property: JsonPropertyName("ext_urls")] IReadOnlyList<string> ExtUrls,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("pixiv_id")] int PixivId,
        [property: JsonPropertyName("member_name")] string MemberName,
        [property: JsonPropertyName("member_id")] int MemberId,
        [property: JsonPropertyName("published")] DateTime? Published,
        [property: JsonPropertyName("service")] string Service,
        [property: JsonPropertyName("service_name")] string ServiceName,
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("user_id")] string UserId,
        [property: JsonPropertyName("user_name")] string UserName,
        [property: JsonPropertyName("company")] string Company,
        [property: JsonPropertyName("getchu_id")] string GetchuId,
        [property: JsonPropertyName("source")] string Source,
        [property: JsonPropertyName("creator")] object Creator,
        [property: JsonPropertyName("eng_name")] string EngName,
        [property: JsonPropertyName("jp_name")] string JpName,
        [property: JsonPropertyName("path")] string Path,
        [property: JsonPropertyName("creator_name")] string CreatorName,
        [property: JsonPropertyName("author_name")] string AuthorName,
        [property: JsonPropertyName("author_url")] string AuthorUrl,
        [property: JsonPropertyName("anidb_aid")] int? AnidbAid,
        [property: JsonPropertyName("mal_id")] int? MalId,
        [property: JsonPropertyName("anilist_id")] int? AnilistId,
        [property: JsonPropertyName("part")] string Part,
        [property: JsonPropertyName("year")] string Year,
        [property: JsonPropertyName("est_time")] string EstTime,
        [property: JsonPropertyName("da_id")] string DaId,
        [property: JsonPropertyName("fa_id")] int? FaId,
        [property: JsonPropertyName("as_project")] string AsProject,
        [property: JsonPropertyName("mu_id")] int? MuId,
        [property: JsonPropertyName("type")] string Type,
        [property: JsonPropertyName("imdb_id")] string ImdbId
    );