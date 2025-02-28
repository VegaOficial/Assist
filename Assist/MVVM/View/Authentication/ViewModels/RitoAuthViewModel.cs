﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Assist.MVVM.ViewModel;
using Microsoft.Web.WebView2.Wpf;
using Serilog;

namespace Assist.MVVM.View.Authentication.ViewModels
{
    internal class RitoAuthViewModel : ViewModelBase
    {
        public Cookie AuthCookie;
        public async Task GetCookies(WebView2 webView)
        {
            var result = await webView.CoreWebView2.CookieManager.GetCookiesAsync(null);
            var c = result.Find(_c => _c.Name == "ssid");

            if (c != null)
            {
                Log.Debug("Found SSID Cookie from Authentication");
                var cook = new Cookie
                {
                    Name = c.Name,
                    Value = c.Value,
                    Path = c.Path,
                    Secure = c.IsSecure,
                    HttpOnly = c.IsHttpOnly,
                    Domain = c.Domain,
                };
                if (c.Expires.ToString().Contains("1/1/0001"))
                    cook.Expires = DateTime.Now.AddMonths(1);
                else
                    cook.Expires = c.Expires;

                webView.CoreWebView2.CookieManager.DeleteAllCookies();
                AuthCookie = cook;

            }


        }


    }
}
