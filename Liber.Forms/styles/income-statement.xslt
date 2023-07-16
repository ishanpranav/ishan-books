<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:html="http://www.w3.org/1999/xhtml"
    exclude-result-prefixes="msxsl">
    <xsl:include href="financial-statement.xslt"/>
    <xsl:output method="html" indent="yes"/>
    <xsl:variable name="title">Income Statement</xsl:variable>
    <xsl:template match="/report">
        <xsl:call-template name="financial-statement">
            <xsl:with-param name="title" select="$title"/>
            <xsl:with-param name="table">
                <thead>
                    <tr>
                        <th colspan="2">
                            <xsl:value-of select="company/name"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="2" class="title">
                            <xsl:value-of select="$title"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="6" class="bar">
                            <xsl:call-template name="date-long">
                                <xsl:with-param name="value" select="started"/>
                            </xsl:call-template>
                            <xsl:text> &#x2013; </xsl:text>
                            <xsl:call-template name="date-long">
                                <xsl:with-param name="value" select="posted"/>
                            </xsl:call-template>
                        </th>
                    </tr>
                    <tr>
                        <th></th>
                        <th class="heading">
                            <xsl:call-template name="date-year">
                                <xsl:with-param name="value" select="posted"/>
                            </xsl:call-template>
                        </th>
                    </tr>
                </thead>
                <xsl:variable name="income" select="sum(company/account[type = 'Income']/debit) - sum(company/account[type = 'Income']/credit)"/>
                <xsl:variable name="cost" select="sum(company/account[type = 'Cost']/debit) - sum(company/account[type = 'Cost']/credit)"/>
                <xsl:variable name="expense" select="sum(company/account[type = 'Expense']/debit) - sum(company/account[type = 'Expense']/credit)"/>
                <xsl:variable name="otherIncome" select="sum(company/account[type = 'OtherIncome']/debit) - sum(company/account[type = 'OtherIncome']/credit)"/>
                <xsl:variable name="otherExpense" select="sum(company/account[type = 'OtherExpense']/debit) - sum(company/account[type = 'OtherExpense']/credit)"/>
                <xsl:variable name="incomeTaxExpense" select="sum(company/account[type = 'IncomeTaxExpense']/debit) - sum(company/account[type = 'IncomeTaxExpense']/credit)"/>
                <tbody>
                    <tr>
                        <th class="in-1 left">Ordinary revenue and expenses</th>
                        <th></th>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">Income</xsl:with-param>
                        <xsl:with-param name="balance" select="$income"/>
                        <xsl:with-param name="description">Revenues</xsl:with-param>
                        <xsl:with-param name="total">Total revenue</xsl:with-param>
                        <xsl:with-param name="indent">3</xsl:with-param>
                        <xsl:with-param name="sign">-1</xsl:with-param>
                    </xsl:apply-templates>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">Cost</xsl:with-param>
                        <xsl:with-param name="balance" select="$cost"/>
                        <xsl:with-param name="description">Costs of revenue</xsl:with-param>
                        <xsl:with-param name="total">Total cost of revenue</xsl:with-param>
                        <xsl:with-param name="indent">3</xsl:with-param>
                        <xsl:with-param name="sign">1</xsl:with-param>
                    </xsl:apply-templates>
                    <tr>
                        <th class="in-2 left">Gross profit</th>
                        <td class="subtotal right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="-$income - $cost"/>
                            </xsl:call-template>
                        </td>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">Expense</xsl:with-param>
                        <xsl:with-param name="balance" select="$expense"/>
                        <xsl:with-param name="description">Expenses</xsl:with-param>
                        <xsl:with-param name="total">Total expense</xsl:with-param>
                        <xsl:with-param name="indent">3</xsl:with-param>
                        <xsl:with-param name="sign">1</xsl:with-param>
                    </xsl:apply-templates>
                    <tr>
                        <th class="in-1 left">Net ordinary income</th>
                        <td class="subtotal right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="-$income - $cost - $expense"/>
                            </xsl:call-template>
                        </td>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">OtherIncome</xsl:with-param>
                        <xsl:with-param name="balance" select="$otherIncome"/>
                        <xsl:with-param name="description">Other income</xsl:with-param>
                        <xsl:with-param name="total">Total other income</xsl:with-param>
                        <xsl:with-param name="indent">2</xsl:with-param>
                        <xsl:with-param name="sign">-1</xsl:with-param>
                    </xsl:apply-templates>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">OtherExpense</xsl:with-param>
                        <xsl:with-param name="balance" select="$otherExpense"/>
                        <xsl:with-param name="description">Other expenses</xsl:with-param>
                        <xsl:with-param name="total">Total other expense</xsl:with-param>
                        <xsl:with-param name="indent">2</xsl:with-param>
                        <xsl:with-param name="sign">1</xsl:with-param>
                    </xsl:apply-templates>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">IncomeTaxExpense</xsl:with-param>
                        <xsl:with-param name="balance" select="$incomeTaxExpense"/>
                        <xsl:with-param name="description">Income tax expense</xsl:with-param>
                        <xsl:with-param name="total">Total income tax expense</xsl:with-param>
                        <xsl:with-param name="indent">2</xsl:with-param>
                        <xsl:with-param name="sign">1</xsl:with-param>
                    </xsl:apply-templates>
                </tbody>
                <tfoot>
                    <tr>
                        <th class="left">Net income</th>
                        <td class="total right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="-$income + -$otherIncome - $cost - $expense - $otherExpense - $incomeTaxExpense"/>
                            </xsl:call-template>
                        </td>
                    </tr>
                </tfoot>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
