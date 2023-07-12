using Liber.Commands.Companies;
using Liber.Forms;
using Liber.Forms.Accounts;
using Liber.Forms.Companies;
using System.Drawing;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Liber.Commands;

[JsonDerivedType(typeof(CloseCommand), "close")]
[JsonDerivedType(typeof(NewCompanyCommand), "new-company")]
[JsonDerivedType(typeof(OpenCompanyCommand), "open-company")]
[JsonDerivedType(typeof(SaveCompanyCommand), "save-company")]
[JsonDerivedType(typeof(SaveCompanyAsCommand), "save-company-as")]
[JsonDerivedType(typeof(OpenRecentCompanyCommand), "open-recent-company")]
[JsonDerivedType(typeof(SettingsCommand), "settings")]
[JsonDerivedType(typeof(FormCommand<AccountsForm>), "accounts")]
[JsonDerivedType(typeof(FormCommand<EditCompanyForm>), "edit-company")]
[JsonDerivedType(typeof(FormCommand<NewAccountForm>), "new-account")]
[JsonDerivedType(typeof(FormCommand<JournalForm>), "journal")]
[JsonDerivedType(typeof(ImportCommand), "import")]
[JsonDerivedType(typeof(UrlCommand), "url")]
[JsonDerivedType(typeof(ErrorsCommand), "errors")]
internal abstract class Command
{
    [JsonIgnore]
    public virtual string DisplayName
    {
        get
        {
            return FormattedStrings.GetString(GetType().Name + "Name");
        }
    }

    [JsonIgnore]
    public virtual Image? Image
    {
        get
        {
            return FormattedStrings.GetImage(GetType().Name + "Image");
        }
    }

    public virtual Task ExecuteAsync(MainContext context)
    {
        return Task.CompletedTask;
    }
}
