// TaxType.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;

namespace Liber;

public enum TaxType
{
    [LocalizedDescription(nameof(None))]
    None = 0,

    [LocalizedDescription(nameof(Other))]
    Other = 1,

    /// <remarks>Corresponds to Line 2b of Form 1040 (taxable interest). The account type should be <see cref="AccountType.Income"/>.</remarks>
    Form1040Line2B,

    /// <remarks>Corresponds to Line 16 of Form 1040 (tax). The account type should be <see cref="AccountType.IncomeTaxExpense"/>.</remarks>
    Form1040Line16,

    /// <remarks>This tax type records deferred federal income tax liabilities. The account type should be <see cref="AccountType.LongTermLiability"/>.</remarks>
    DeferredFederalIncomeTaxLiability,

    /// <remarks>Corresponds to Line 5a of Form 1040 Schedule A (state and local income taxes or general sales taxes). The account type should be <see cref="AccountType.IncomeTaxExpense"/>.</remarks>
    ScheduleALine5A,

    /// <remarks>This tax type records state income taxes withheld in excess of the state income tax liability. The account type should be <see cref="AccountType.OtherCurrentAsset"/>.</remarks>
    AdditionalStateIncomeTaxWithheld,

    /// <remarks>Corresponds to Box 1 of Form 1098-T (payments received for qualified tuition and related expenses). The account type should be <see cref="AccountType.Expense"/>.</remarks>
    Form1098TBox1,

    /// <remarks>Equivalent to <see cref="Form1040Line2B"/> for federal taxes. This tax type corresponds to Line 2b of Form 1040 (taxable interest), however it is not taxed by any state. The account type should be <see cref="AccountType.Income"/>.</remarks>
    USTreasuryQualifiedInterestIncome,

    /// <remarks>Corresponds to Box 1 of Form W-2 (wages, tips, and other compensation). The account type should be <see cref="AccountType.Income"/>.</remarks>
    FormW2Box1,

    /// <remarks>Corresponds to Box 2 of Form W-2 (federal income tax withheld). The account type should be <see cref="AccountType.OtherCurrentAsset"/>.</remarks>
    FormW2Box2,

    /// <remarks>Corresponds to Box 4 of Form W-2 (social security tax withheld). The account type should be <see cref="AccountType.OtherCurrentAsset"/>.</remarks>
    FormW2Box4,

    /// <remarks>Corresponds to Box 19 of Form W-2 (local income tax). The account type should be <see cref="AccountType.IncomeTaxExpense"/>.</remarks>
    FormW2Box19,

    /// <remarks>This tax type records social security taxes on wages earned. The account type should be <see cref="AccountType.IncomeTaxExpense"/>.</remarks>
    SocialSecurityTaxExpense,

    /// <remarks>Corresponds to Box 6 of Form W-2 (Medicare tax withheld). The account type should be <see cref="AccountType.OtherCurrentAsset"/>.</remarks>
    FormW2Box6,

    /// <remarks>This tax type records Medicare taxes on wages earned. The account type should be <see cref="AccountType.IncomeTaxExpense"/>.</remarks>
    MedicareTaxExpense,

    /// <remarks>Corresponds to Line 19 of Form 8863 (nonrefundable education credits). The account type should be a contra <see cref="AccountType.IncomeTaxExpense"/>.</remarks>
    Form8863Line19
}
