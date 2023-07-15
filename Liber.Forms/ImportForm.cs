using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Liber.Forms.Accounts;

internal abstract partial class ImportForm : Form
{
    protected ImportForm(Company company, FormFactory factory)
    {
        InitializeComponent();

        new ComponentResourceManager(GetType()).ApplyResources(this, "$this");

        Company = company;
        Factory = factory;
    }

    protected Company Company { get; }
    protected FormFactory Factory { get; }

    protected abstract void CommitChanges();

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        CommitChanges();
        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
