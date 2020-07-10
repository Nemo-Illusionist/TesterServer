using System;
using System.Net;
using System.Net.Http;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using Refit;
using TesterUI.Helpers.Api;
using TesterUI.MVVM.ViewModels;

namespace TesterUI.MVVM.Models
{
    public class Context : BaseViewModel
    {
        public string Login { get; set; }
        public IAuthApi AuthApi { get; set; }
        public IBrokerApi BrokerApi { get; set; }
        private HttpClientHandler HttpClientHandler { get; set; } = new HttpClientHandler {CookieContainer = new CookieContainer()};

        public Context()
        {
            var settingRefit = new RefitSettings
            {
                CollectionFormat = CollectionFormat.Multi,
            };

            AuthApi = RestService.For<IAuthApi>(BuildHttpClient(new Uri(App.Configuration.ConnectionStrings.Auth)),
                settingRefit);
            BrokerApi = RestService.For<IBrokerApi>(BuildHttpClient(new Uri(App.Configuration.ConnectionStrings.Broker)),
                settingRefit);
        }

        private HttpClient BuildHttpClient(Uri baseUrl)
        {
            var baseAddress = baseUrl;
            var client = new HttpClient(HttpClientHandler) {BaseAddress = baseAddress};

            return client;
        }

        public void SetToken(string token)
        {
            HttpClientHandler.CookieContainer.Add(new Uri(App.Configuration.ConnectionStrings.Broker), new Cookie("accept_token", token));
        }

        public Page CurrentPage { get; set; }

        public void SetPage(Page page)
        {
            CurrentPage = page;
        }

        public DialogHost MainDialog { get; set; }
    }
}