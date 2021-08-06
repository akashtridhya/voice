using System;
using voice.dapper;
using voice.models;

namespace voice.api.Helpers
{
    public class GenericHelpers
    {
        #region 1. Object Declarations and Constructor

        private BaseUrlConfigs Configs { get; set; }

        public IDapperRepository DapperRepository { get; set; }

        public GenericHelpers(
            BaseUrlConfigs configs = null,
            IDapperRepository DapperRepository = null)
        {
            this.Configs = configs;
            this.DapperRepository = DapperRepository;
        }

        #endregion 1. Object Declarations and Constructor

        #region 2. Bind Attachment URLs

        public string BindProfileAttachmentUrl(string attachment)
        {
            return BindAttachmentUrl(attachment, FolderEnums.profile.ToString());
        }

        public string BindGameAttachmentUrl(string attachment)
        {
            return BindAttachmentUrl(attachment, FolderEnums.games.ToString());
        }

        public string BindAdAttachmentUrl(string attachment)
        {
            return BindAttachmentUrl(attachment, FolderEnums.adImage.ToString());
        }

        public string BindAttachmentUrl(
            string attachment,
            string folder)
        {
            if (string.IsNullOrEmpty(attachment)
                || Configs == null) return string.Empty;

            return $"{Configs.ImageBase}{folder}/{attachment}";
        }

        #endregion 2. Bind Attachment URLs

        #region 4. Calculate Total Pages For Grid

        public int CalculateTotalPages(
            dynamic total,
            int? pageSize)
        {
            var pages = Convert.ToDecimal(total) / pageSize;
            var response = pages < 1 ? 1 : Convert.ToInt32(Math.Ceiling(pages));

            return response;
        }

        #endregion 4. Calculate Total Pages For Grid

        #region 5. Make Tiny URL

        public string MakeTinyUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;

            var address = new Uri("http://tinyurl.com/api-create.php?url=" + url);
            var client = new System.Net.WebClient();
            var response = client.DownloadString(address);

            return response;
        }

        #endregion 5. Make Tiny URL

        #region 6. URL Shortener

        internal async System.Threading.Tasks.Task<string> SetShortURLAsync(string url)
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;

            var randomURL = await DapperRepository.FindAsync<GetSourceURLRequest>(
                  SpConsts.Generic.PersistRandomUrl,
                  new
                  {
                      SourceURL = url
                  });

            return $"{Configs.BaseUrl}voice/{randomURL.ShortURL}";
        }

        #endregion 6. URL Shortener
    }
}