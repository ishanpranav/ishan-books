using Microsoft.Web.WebView2.Core;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Liber.Forms;

internal sealed partial class UrlForm : Form
{
    private readonly Uri? _uri;

    public UrlForm(Uri uri)
    {
        InitializeComponent();

        _uri = uri;
    }

    private async void OnLoad(object sender, EventArgs e)
    {
        _webView.Source = _uri;

        await _webView.EnsureCoreWebView2Async();

        _webView.CoreWebView2.DocumentTitleChanged += (_, _) => Text = _webView.CoreWebView2.DocumentTitle;
        _webView.CoreWebView2.FaviconChanged += async (_, _) =>
        {
            using Stream stream = await _webView.CoreWebView2.GetFaviconAsync(CoreWebView2FaviconImageFormat.Png);
            using Bitmap bitmap = new Bitmap(stream);

            bitmap.MakeTransparent(Color.White);

            Icon = Icon.FromHandle(bitmap.GetHicon());
        };
        _webView.CoreWebView2.ContextMenuRequested += (_, e) => e.MenuItems.Clear();
    }
}
