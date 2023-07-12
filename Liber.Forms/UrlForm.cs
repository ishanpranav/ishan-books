using System;
using System.Windows.Forms;

namespace Liber.Forms;

internal sealed partial class UrlForm : Form
{
    private readonly Uri _uri;

    public UrlForm(Uri uri)
    {
        InitializeComponent();

        _uri = uri;
    }

    private async void OnBrowserFormLoad(object sender, EventArgs e)
    {
        _webView.Source = _uri;

        await _webView.EnsureCoreWebView2Async();

        _webView.CoreWebView2.DocumentTitleChanged += (_, _) => Text = _webView.CoreWebView2.DocumentTitle;
    }
}
