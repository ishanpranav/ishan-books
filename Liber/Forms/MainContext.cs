using Liber.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace Liber.Forms;

internal sealed class MainContext
{
    private readonly Dictionary<Guid, Form> _forms = new Dictionary<Guid, Form>();
    private readonly SortedSet<Error> _errors = new SortedSet<Error>();

    private Company _company;

    public MainContext(MainForm form, Settings settings)
    {
        Form = form;
        Company = new Company();
        Settings = settings;
    }

    public MainForm Form { get; }
    public Settings Settings { get; }
    public string? Path { get; set; }

    public Company Company
    {
        get
        {
            return _company;
        }

        [MemberNotNull(nameof(_company))]
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (_company == value)
            {
                return;
            }

            Path = null;

            if (_company != null)
            {
                _company.NameChanged -= OnCompanyNameChanged;
            }

            value.NameChanged += OnCompanyNameChanged;
            _company = value;

            Form.UpdateCompany(value);
        }
    }

    public ICollection<Error> Errors
    {
        get
        {
            return _errors;
        }
    }

    private void OnCompanyNameChanged(object? sender, EventArgs e)
    {
        Form.UpdateCompanyName(_company);
    }

    public void Register(Guid key, Form value)
    {
        _forms.Add(key, value);

        value.MdiParent = Form;
        value.FormClosed += (_, _) => _forms.Remove(key);

        value.Show();
    }

    public void Kill(Guid key)
    {
        if (_forms.TryGetValue(key, out Form? form))
        {
            form.Close();
        }
    }

    public bool TryKill(Guid key)
    {
        if (!_forms.TryGetValue(key, out Form? form))
        {
            return false;
        }

        form.Activate();

        return true;
    }

    public void ReportErrors()
    {
        Guid id = new Guid("E4C44B74-0849-4A70-8EA5-16893E3F8762");

        ErrorsForm form;

        if (TryKill(id))
        {
            form = (ErrorsForm)_forms[id];
        }
        else
        {
            form = new ErrorsForm(context: this);

            Register(id, form);
        }

        form.InitializeErrors();
    }
}
