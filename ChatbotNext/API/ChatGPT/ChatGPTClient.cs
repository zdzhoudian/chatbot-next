using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatbotNext.API.ChatGPT
{
    public class ChatGPTClient : IDisposable
    {
        private HttpClient _http;

        public ChatGPTConfig Config { get; } = new ChatGPTConfig();

        public bool IsDisposed { get; private set; }

        public ChatGPTClient()
        {
            _http = new HttpClient();
        }

        public void Init()
        {
            _http = new HttpClient();
            _http.DefaultRequestHeaders.Remove("Authorization");
            _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {Config.ApiKey}");
        }

        public async Task<ChatGPTResponse> SendAsync(ChatGPTRequest request)
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(nameof(ChatGPT));
            }
            var reqJson = JsonSerializer.Serialize(request);
            using (var content = new StringContent(reqJson, Encoding.UTF8, "application/json"))
            using (var reqMsg = new HttpRequestMessage(HttpMethod.Post, Config.ApiUrl))
            {
                reqMsg.Content = content;
                reqMsg.Headers.Add("Authorization", $"Bearer {Config.ApiKey}");
                using (var resMsg = await _http.SendAsync(reqMsg))
                {
                    var str = await resMsg.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ChatGPTResponse>(str);
                }
            }
        }

        #region IDisposable
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            IsDisposed = true;
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    if (_http != null)
                    {
                        _http.Dispose();
                        _http = null;
                    }
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~ChatGPT()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
