namespace WebApp.Models.Games
{
    public class GameDetailsViewModel
    {
        public int GameId { get; set; }
        public string GameName { get; set; } = "";
        public string GenreName { get; set; } = "";

        public List<PublisherInfo> Publishers { get; set; } = new();
    }

    public class PublisherInfo
    {
        public int? PublisherId { get; set; }
        public string PublisherName { get; set; } = "";

        public List<PlatformInfo> Platforms { get; set; } = new();
    }

    public class PlatformInfo
    {
        public string PlatformName { get; set; } = "";
        public int? ReleaseYear { get; set; }

        public List<RegionSaleInfo> RegionSales { get; set; } = new();
    }

    public class RegionSaleInfo
    {
        public string RegionName { get; set; } = "";
        public double? NumSales { get; set; }
    }
}