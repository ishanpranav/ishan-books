<!--
tax-reconciliation.xslt
Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
Licensed under the MIT License.
-->
<xsl:stylesheet
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:html="http://www.w3.org/1999/xhtml"
    xmlns:liber="urn:liber"
    version="1.0"
    exclude-result-prefixes="msxsl">
    <xsl:include href="financial-statement.xslt"/>
    <xsl:output method="html" indent="yes"/>
    <xsl:key name="accounts-by-type-and-name-and-taxtype"
             match="account"
             use="concat(type, '|', name, '|', tax-type)"/>
    <xsl:template name="tax-reconciliation">
        <xsl:param name="title"/>

        <!-- Section 0: Income tax expense -->
        <xsl:variable name="taxExpenseCurrent"  select="sum(company/account[type = 'IncomeTaxExpense' and tax-type = 'true']/balance)"/>
        <xsl:variable name="taxExpenseDeferred" select="sum(company/account[type = 'IncomeTaxExpense' and tax-type = 'false']/balance)"/>
        <xsl:variable name="taxExpenseTotal"    select="$taxExpenseCurrent + $taxExpenseDeferred"/>

        <!-- Section 1: Income statement accounts -->
        <xsl:variable name="incomeCurrent"   select="sum(company/account[type = 'Income'             and tax-type = 'true']/balance)"/>
        <xsl:variable name="incomeDeferred"  select="sum(company/account[type = 'Income'             and tax-type = 'false']/balance)"/>
        <xsl:variable name="costCurrent"     select="sum(company/account[type = 'Cost'               and tax-type = 'true']/balance)"/>
        <xsl:variable name="costDeferred"    select="sum(company/account[type = 'Cost'               and tax-type = 'false']/balance)"/>
        <xsl:variable name="expenseCurrent"  select="sum(company/account[type = 'Expense'            and tax-type = 'true']/balance)"/>
        <xsl:variable name="expenseDeferred" select="sum(company/account[type = 'Expense'            and tax-type = 'false']/balance)"/>
        <xsl:variable name="otherCurrent"    select="sum(company/account[type = 'OtherIncomeExpense' and tax-type = 'true']/balance)"/>
        <xsl:variable name="otherDeferred"   select="sum(company/account[type = 'OtherIncomeExpense' and tax-type = 'false']/balance)"/>

        <xsl:variable name="pretaxIncome"         select="-sum(company/account[type = 'Income' or type = 'Cost' or type = 'Expense' or type = 'OtherIncomeExpense']/balance)"/>
        <xsl:variable name="currentTaxableIncome" select="-($incomeCurrent + $costCurrent + $expenseCurrent + $otherCurrent)"/>

        <table>
            <thead>
                <tr>
                    <th colspan="4" class="subtitle">
                        <xsl:value-of select="company/name"/>
                    </th>
                </tr>
                <tr>
                    <th colspan="4" class="title">
                        <xsl:value-of select="$title"/>
                    </th>
                </tr>
                <tr>
                    <th colspan="4" class="dateline">
                        <xsl:value-of select="liber:ftspanl(started, posted)"/>
                    </th>
                </tr>
                <xsl:if test="company/multiple != ''">
                    <tr>
                        <th colspan="4" class="dateline">
                            <xsl:value-of select="company/multiple"/>
                        </th>
                    </tr>
                </xsl:if>
                <tr class="overline">
                    <th/>
                    <th class="heading">
                        <xsl:value-of select="liber:gets('tax-current')"/>
                    </th>
                    <th class="heading">
                        <xsl:value-of select="liber:gets('tax-deferred')"/>
                    </th>
                    <th class="heading">
                        <xsl:value-of select="liber:gets('tax-total')"/>
                    </th>
                </tr>
            </thead>
            <tbody>

                <!-- Section 0: Income tax expense -->
                <tr>
                    <th class="left" colspan="4">
                        <xsl:value-of select="liber:gets('IncomeTaxExpense')"/>
                    </th>
                </tr>
                <xsl:for-each select="//account[
                    type = 'IncomeTaxExpense'
                    and tax-type = 'true'
                ]">
                    <xsl:variable name="current" select="number(balance)"/>
                    <xsl:variable name="deferred">
                        <xsl:choose>
                            <xsl:when test="key('accounts-by-type-and-name-and-taxtype', concat(type, '|', name, '|false'))">
                                <xsl:value-of select="number(key('accounts-by-type-and-name-and-taxtype', concat(type, '|', name, '|false'))/balance)"/>
                            </xsl:when>
                            <xsl:otherwise>0</xsl:otherwise>
                        </xsl:choose>
                    </xsl:variable>
                    <xsl:variable name="total" select="$current + $deferred"/>
                    <xsl:if test="$current != 0 or $deferred != 0">
                        <tr>
                            <td class="in-1 left account">
                                <xsl:value-of select="name"/>
                            </td>
                            <td class="right">
                                <xsl:value-of select="liber:fm('IncomeTaxExpense', $current)"/>
                            </td>
                            <td class="right">
                                <xsl:value-of select="liber:fm('IncomeTaxExpense', $deferred)"/>
                            </td>
                            <td class="right">
                                <xsl:value-of select="liber:fm('IncomeTaxExpense', $total)"/>
                            </td>
                        </tr>
                    </xsl:if>
                </xsl:for-each>
                <tr>
                    <td class="in-2 left">
                        <xsl:value-of select="liber:pngets('IncomeTaxExpense', $taxExpenseTotal)"/>
                    </td>
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fm('IncomeTaxExpense', $taxExpenseCurrent)"/>
                    </td>
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fm('IncomeTaxExpense', $taxExpenseDeferred)"/>
                    </td>
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fm('IncomeTaxExpense', $taxExpenseTotal)"/>
                    </td>
                </tr>

                <!-- Section 1: Income statement accounts -->
                <tr>
                    <th class="left" colspan="4">
                        <xsl:value-of select="liber:gets('income-statement')"/>
                    </th>
                </tr>
                <xsl:for-each select="//account[
                    (type = 'Income' or type = 'Cost' or type = 'Expense' or type = 'OtherIncomeExpense')
                    and tax-type = 'true'
                ]">
                    <xsl:variable name="current" select="number(balance)"/>
                    <xsl:variable name="deferred">
                        <xsl:choose>
                            <xsl:when test="key('accounts-by-type-and-name-and-taxtype', concat(type, '|', name, '|false'))">
                                <xsl:value-of select="number(key('accounts-by-type-and-name-and-taxtype', concat(type, '|', name, '|false'))/balance)"/>
                            </xsl:when>
                            <xsl:otherwise>0</xsl:otherwise>
                        </xsl:choose>
                    </xsl:variable>
                    <xsl:variable name="total" select="$current + $deferred"/>
                    <xsl:if test="$current != 0 or $deferred != 0">
                        <tr>
                            <td class="in-1 left account">
                                <xsl:value-of select="name"/>
                            </td>
                            <td class="right">
                                <xsl:value-of select="liber:fm(string(type), $current)"/>
                            </td>
                            <td class="right">
                                <xsl:value-of select="liber:fm(string(type), $deferred)"/>
                            </td>
                            <td class="right">
                                <xsl:value-of select="liber:fm(string(type), $total)"/>
                            </td>
                        </tr>
                    </xsl:if>
                </xsl:for-each>
                <tr>
                    <td class="in-2 left">
                        <xsl:value-of select="liber:gets('pretax-income')"/>
                    </td>
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fm($currentTaxableIncome)"/>
                    </td>
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fm(-($incomeDeferred + $costDeferred + $expenseDeferred + $otherDeferred))"/>
                    </td>
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fm($pretaxIncome)"/>
                    </td>
                </tr>

                <!-- Section 2: Balance sheet accounts -->
                <tr>
                    <th class="left" colspan="4">
                        <xsl:value-of select="liber:gets('balance-sheet')"/>
                    </th>
                </tr>
                <xsl:for-each select="//account[
                    (type = 'OtherCurrentAsset' or type = 'OtherAsset' or type = 'OtherCurrentLiability' or type = 'LongTermLiability')
                    and tax-type = 'true'
                ]">
                    <xsl:variable name="current" select="number(balance)"/>
                    <xsl:variable name="deferred">
                        <xsl:choose>
                            <xsl:when test="key('accounts-by-type-and-name-and-taxtype', concat(type, '|', name, '|false'))">
                                <xsl:value-of select="number(key('accounts-by-type-and-name-and-taxtype', concat(type, '|', name, '|false'))/balance)"/>
                            </xsl:when>
                            <xsl:otherwise>0</xsl:otherwise>
                        </xsl:choose>
                    </xsl:variable>
                    <xsl:variable name="total" select="$current + $deferred"/>
                    <xsl:if test="$current != 0 or $deferred != 0">
                        <tr>
                            <td class="in-1 left account">
                                <xsl:value-of select="name"/>
                            </td>
                            <td class="right">
                                <xsl:value-of select="liber:fm(string(type), $current)"/>
                            </td>
                            <td class="right">
                                <xsl:value-of select="liber:fm(string(type), $deferred)"/>
                            </td>
                            <td class="right">
                                <xsl:value-of select="liber:fm(string(type), $total)"/>
                            </td>
                        </tr>
                    </xsl:if>
                </xsl:for-each>

                <!-- Reconciliation -->
                <tr>
                    <th class="left" colspan="4">
                        <xsl:value-of select="liber:gets('tax-reconciliation')"/>
                    </th>
                </tr>
                <tr class="overline">
                    <th/>
                    <th class="heading">
                        <xsl:value-of select="liber:gets('financial-statement')"/>
                    </th>
                    <th class="heading">
                        <xsl:value-of select="liber:gets('adjustment')"/>
                    </th>
                    <th class="heading">
                        <xsl:value-of select="liber:gets('tax-return')"/>
                    </th>
                </tr>
                <tr>
                    <td class="left">
                        <xsl:value-of select="liber:gets('pretax-income')"/>
                    </td>
                    <td class="right">
                        <xsl:value-of select="liber:fm($pretaxIncome)"/>
                    </td>
                    <td class="right">
                        <xsl:value-of select="liber:fm($currentTaxableIncome - $pretaxIncome)"/>
                    </td>
                    <td class="right">
                        <xsl:value-of select="liber:fm($currentTaxableIncome)"/>
                    </td>
                </tr>
                <tr>
                    <td class="left">
                        <xsl:value-of select="liber:gets('IncomeTaxExpense')"/>
                    </td>
                    <td class="right">
                        <xsl:value-of select="liber:fm('IncomeTaxExpense', $taxExpenseTotal)"/>
                    </td>
                    <td class="right">
                        <xsl:value-of select="liber:fm('IncomeTaxExpense', $taxExpenseCurrent - $taxExpenseTotal)"/>
                    </td>
                    <td class="right">
                        <xsl:value-of select="liber:fm('IncomeTaxExpense', $taxExpenseCurrent)"/>
                    </td>
                </tr>
            </tbody>
        </table>
    </xsl:template>
</xsl:stylesheet>
