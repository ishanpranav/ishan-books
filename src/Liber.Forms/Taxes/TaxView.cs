using System.ComponentModel;

namespace Liber.Forms.Taxes;

internal class TaxView : IntervalView
{
    public TaxView(Tax tax)
    {
        Tax = tax;
    }

    [Browsable(false)]
    public Tax Tax { get; }
}
