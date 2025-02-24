﻿namespace Gpm.WebView.Internal
{
    using System.Collections.Generic;
    using UnityEngine;

    public class DefaultWebView : IWebView
    {
        public bool CanGoBack => false;

        public bool CanGoForward => false;

        [System.Obsolete("This method is deprecated.")]
        public void ShowUrl(
            string url,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent,
            GpmWebViewCallback.GpmWebViewPageLoadDelegate pageLoadCallback)
        {
            Debug.LogWarning("Not supported method in the editor");
        }

        [System.Obsolete("This method is deprecated.")]
        public void ShowHtmlFile(
            string fileName,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent,
            GpmWebViewCallback.GpmWebViewPageLoadDelegate pageLoadCallback)
        {
            Debug.LogWarning("Not supported method in the editor");
        }

        [System.Obsolete("This method is deprecated.")]
        public void ShowHtmlString(
            string source,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewErrorDelegate openCallback,
            GpmWebViewCallback.GpmWebViewErrorDelegate closeCallback,
            List<string> schemeList,
            GpmWebViewCallback.GpmWebViewDelegate<string> schemeEvent,
            GpmWebViewCallback.GpmWebViewPageLoadDelegate pageLoadCallback)
        {
            Debug.LogWarning("Not supported method in the editor");
        }

        public void ShowUrl(string url,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewDelegate callback,
            List<string> schemeList)
        {
            Debug.LogWarning("Not supported method in the editor");
        }

        public void ShowHtmlFile(
            string fileName,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewDelegate callback,
            List<string> schemeList)
        {
            Debug.LogWarning("Not supported method in the editor");
        }

        public void ShowHtmlString(
            string source,
            GpmWebViewRequest.Configuration configuration,
            GpmWebViewCallback.GpmWebViewDelegate callback,
            List<string> schemeList)
        {
            Debug.LogWarning("Not supported method in the editor");
        }

        public void ShowSafeBrowsing(
            string url,
            GpmWebViewRequest.ConfigurationSafeBrowsing configuration = null,
            GpmWebViewCallback.GpmWebViewDelegate callback = null)
        {
            Debug.LogWarning("Not supported method in the editor");
        }

        public void Close()
        {
            Debug.LogWarning("Not supported method in the editor");
        }

        public bool IsActive()
        {
            Debug.LogWarning("Not supported method in the editor");
            return false;
        }

        public void ExecuteJavaScript(string script)
        {
            Debug.LogWarning("Not supported method in the editor");
        }

        public void SetFileDownloadPath(string path)
        {
            Debug.LogWarning("Not supported method in the editor");
        }

        public void GoBack()
        {
            Debug.LogWarning("Not supported method in the editor");
        }

        public void GoForward()
        {
            Debug.LogWarning("Not supported method in the editor");
        }

        public void SetPosition(int x, int y)
        {
            Debug.LogWarning("Not supported method in the editor");
        }

        public void SetSize(int width, int height)
        {
            Debug.LogWarning("Not supported method in the editor");
        }

        public void SetMargins(int left, int top, int right, int bottom)
        {
            Debug.LogWarning("Not supported method in the editor");
        }

        public int GetX()
        {
            Debug.LogWarning("Not supported method in the editor");
            return 0;
        }

        public int GetY()
        {
            Debug.LogWarning("Not supported method in the editor");
            return 0;
        }

        public int GetWidth()
        {
            Debug.LogWarning("Not supported method in the editor");
            return 0;
        }

        public int GetHeight()
        {
            Debug.LogWarning("Not supported method in the editor");
            return 0;
        }
    }
}
