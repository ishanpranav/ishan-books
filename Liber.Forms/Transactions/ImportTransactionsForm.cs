using Liber.Forms.Accounts;
using System.Collections.Generic;
using System.ComponentModel;

namespace Liber.Forms.Transactions;

internal sealed class ImportTransactionsForm : ImportForm
{
    private readonly List<Transaction> _transactions = new List<Transaction>();
    private readonly BindingList<Line> _lines = new BindingList<Line>();

    public ImportTransactionsForm(Company company) : base(company)
    {
        _dataGridView.DataSource = _lines;
    }

    protected override void CommitChanges()
    {
        foreach (Transaction transaction in _transactions)
        {
            Company.AddTransaction(transaction);
        }
    }
}
