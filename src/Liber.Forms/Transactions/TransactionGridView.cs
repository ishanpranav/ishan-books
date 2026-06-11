// TransactionGridView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Liber.MathEngine.Expressions;

namespace Liber.Forms.Transactions;

internal class TransactionGridView : DataGridViewEx
{
    private Color _companyColor;
    private int _debitColumnIndex = -1;
    private int _creditColumnIndex = -1;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color CompanyColor
    {
        get
        {
            return _companyColor;
        }
        set
        {
            if (_companyColor == value)
            {
                return;
            }

            _companyColor = value;
            AlternatingRowsDefaultCellStyle.BackColor = value;
            AlternatingRowsDefaultCellStyle.ForeColor = value.GetForeColor();

            if (AlternatingRowsDefaultCellStyle.SelectionForeColor == Color.Empty)
            {
                AlternatingRowsDefaultCellStyle.SelectionForeColor = DefaultCellStyle.SelectionForeColor;
            }

            if (AlternatingRowsDefaultCellStyle.SelectionBackColor == Color.Empty)
            {
                AlternatingRowsDefaultCellStyle.SelectionBackColor = DefaultCellStyle.SelectionBackColor;
            }

            if (value == DefaultCellStyle.SelectionBackColor)
            {
                DefaultCellStyle.SelectionBackColor = DefaultCellStyle.SelectionBackColor.Shade(0.95);
            }

            if (value == AlternatingRowsDefaultCellStyle.SelectionBackColor)
            {
                AlternatingRowsDefaultCellStyle.SelectionBackColor = AlternatingRowsDefaultCellStyle.SelectionBackColor.Shade(0.95);
            }
        }
    }

    [DefaultValue(-1)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int DebitColumnIndex
    {
        get
        {
            return _debitColumnIndex;
        }
        set
        {
            if (_debitColumnIndex == value)
            {
                return;
            }

            if (value != -1)
            {
                DataGridViewColumn debitColumn = Columns[value];

                debitColumn.ValueType = typeof(IExpression);
                debitColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
            }

            _debitColumnIndex = value;
        }
    }

    [DefaultValue(-1)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int CreditColumnIndex
    {
        get
        {
            return _creditColumnIndex;
        }
        set
        {
            if (_creditColumnIndex == value)
            {
                return;
            }

            if (value != -1)
            {
                DataGridViewColumn creditColumn = Columns[value];

                creditColumn.ValueType = typeof(IExpression);
                creditColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
            }

            _creditColumnIndex = value;
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Func<decimal>? Remainder { get; set; }

    private static bool TryEvaluateExpression(DataGridViewRow row, int columnIndex, out decimal value)
    {
        object? cellValue = row.Cells[columnIndex].Value;

        if (cellValue == null || cellValue is DBNull)
        {
            value = 0;

            return true;
        }

        try
        {
            value = ((IExpression)cellValue).Evaluate();

            return true;
        }
        catch (DivideByZeroException divideByZeroException)
        {
            row.ErrorText = divideByZeroException.Message;
            value = 0;

            return false;
        }
    }

    public bool TryGetDebit(DataGridViewRow row, out decimal result)
    {
        return TryEvaluateExpression(row, DebitColumnIndex, out result);
    }

    public bool TryGetCredit(DataGridViewRow row, out decimal result)
    {
        return TryEvaluateExpression(row, CreditColumnIndex, out result);
    }

    public bool TryGetBalance(DataGridViewRow row, out decimal result)
    {
        if (!TryGetDebit(row, out decimal debit) ||
            !TryGetCredit(row, out decimal credit))
        {
            result = 0;

            return false;
        }

        result = debit - credit;

        return true;
    }

    public void SetBalance(DataGridViewRow row, decimal value)
    {
        if (value >= 0)
        {
            row.Cells[DebitColumnIndex].Value = new DecimalExpression(value);
            row.Cells[CreditColumnIndex].Value = null;
        }
        else
        {
            row.Cells[DebitColumnIndex].Value = null;
            row.Cells[CreditColumnIndex].Value = new DecimalExpression(-value);
        }
    }

    protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
    {
        base.OnCellEndEdit(e);

        int columnIndex = e.ColumnIndex;
        int rowIndex = e.RowIndex;

        if (columnIndex == -1 || rowIndex == -1 ||
            (columnIndex != DebitColumnIndex && columnIndex != CreditColumnIndex))
        {
            return;
        }

        DataGridViewColumn column = Columns[columnIndex];

        if (column.ValueType != typeof(IExpression) ||
            this[columnIndex, rowIndex].Value is not IExpression expression)
        {
            return;
        }

        decimal value;

        try
        {
            value = expression.Evaluate();

            this[columnIndex, rowIndex].Value = new DecimalExpression(value);
        }
        catch (DivideByZeroException)
        {
            return;
        }

        DataGridViewRow row = Rows[rowIndex];

        if (!TryGetDebit(row, out decimal debit) ||
            !TryGetCredit(row, out decimal credit))
        {
            return;
        }

        if (debit - credit == 0)
        {
            if (columnIndex == DebitColumnIndex)
            {
                SetBalance(row, debit);
            }
            else
            {
                SetBalance(row, -credit);
            }

            return;
        }

        decimal remainder = debit - credit;

        if (Remainder != null)
        {
            remainder += Remainder();
        }

        if (debit == remainder && credit != 0)
        {
            SetBalance(row, -credit);

            return;
        }

        if (-credit == remainder && debit != 0)
        {
            SetBalance(row, debit);

            return;
        }

        SetBalance(row, debit - credit);
    }

    protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
    {
        if (e.RowIndex == -1)
        {
            return;
        }

        Rows[e.RowIndex].ErrorText = e.Exception?.Message ?? string.Empty;
    }
}
