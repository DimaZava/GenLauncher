﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace GenLauncherNet
{
    public class GitHubCoreYamlReader : IDisposable
    {
        private readonly string _xmlUrl;

        private HttpClient _httpClient;

        public GitHubCoreYamlReader(string xmlUrl)
        {
            _xmlUrl = xmlUrl;
        }

        public async Task<ReposModsData> ReadCoreManifestYaml()
        {
            _httpClient = new HttpClient { Timeout = TimeSpan.FromDays(1) };

            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GenLauncher", "1"));
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

            using (var response = await _httpClient.GetAsync(_xmlUrl, HttpCompletionOption.ResponseHeadersRead))
                return await DownloadFileFromHttpResponseMessage(response);
        }

        private async Task<ReposModsData> DownloadFileFromHttpResponseMessage(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            var totalBytes = response.Content.Headers.ContentLength;

            using (var contentStream = await response.Content.ReadAsStreamAsync())
                return await ProcessContentStream(totalBytes, contentStream);
        }

        private async Task<ReposModsData> ProcessContentStream(long? totalDownloadSize, Stream contentStream)
        {
            var totalBytesRead = 0L;
            var readCount = 0L;
            var buffer = new byte[8192];
            var isMoreToRead = true;
            using (MemoryStream memStream = new MemoryStream())
            {
                do
                {
                    var bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        isMoreToRead = false;
                        continue;
                    }

                    await memStream.WriteAsync(buffer, 0, bytesRead);

                    totalBytesRead += bytesRead;
                    readCount += 1;


                }
                while (isMoreToRead);

                memStream.Seek(0, SeekOrigin.Begin);

                var deSerializer = new Deserializer();

                var reposData = deSerializer.Deserialize<ReposModsData>(new StreamReader(memStream));

                return reposData;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
