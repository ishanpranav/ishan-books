using System.Windows.Forms;
using Liber.Forms.Properties;

namespace Liber.Forms.Saving;

public partial class SavingForm : Form
{
    public SavingForm()
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        Text = Resources.CancelCaption;
    }

    public int Progress
    {
        get
        {
            return progressBar1.Value;
        }
        set
        {
            progressBar1.Value = value;
        }
    }

    public int MaxProgress
    {
        get
        {
            return progressBar1.Maximum;
        }
        set
        {
            progressBar1.Maximum = value;
        }
    }
}
