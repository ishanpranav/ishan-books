<?xml version="1.0" encoding="utf-8"?>
<!--
income-statement.xslt
Copyright (c) 2023 Ishan Pranav. All rights reserved.
Licensed under the MIT License.
-->
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:html="http://www.w3.org/1999/xhtml"
    xmlns:liber="urn:liber"
    exclude-result-prefixes="msxsl">
    <xsl:include href="base/financial-statement.xslt"/>
    <xsl:output method="html" indent="yes"/>
    <xsl:variable name="title" select="liber:gets('income-statement')"/>
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
                        <th colspan="2" class="bar">
                            <xsl:value-of select="liber:ftspanl()"/>
                        </th>
                    </tr>
                    <tr>
                        <th></th>
                        <th class="heading">
                            <xsl:value-of select="liber:ftspans()"/>
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
                    </xsl:apply-templates>
                    <tr>
                        <xsl:variable name="netOrdinaryIncome" select="-$income - $cost - $expense"/>
                        <th class="in-1 left">
                            <xsl:choose>
                                <xsl:when test="$netOrdinaryIncome &lt; 0">Net ordinary loss</xsl:when>
                                <xsl:otherwise>Net ordinary income</xsl:otherwise>
                            </xsl:choose>
                        </th>
                        <td class="subtotal right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="$netOrdinaryIncome"/>
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
                    </xsl:apply-templates>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">IncomeTaxExpense</xsl:with-param>
                        <xsl:with-param name="balance" select="$incomeTaxExpense"/>
                        <xsl:with-param name="description">Income tax expense</xsl:with-param>
                        <xsl:with-param name="total">Total income tax expense</xsl:with-param>
                        <xsl:with-param name="indent">2</xsl:with-param>
                    </xsl:apply-templates>
                </tbody>
                <tfoot>
                    <tr>
                        <xsl:variable name="netIncome" select="-$income - $otherIncome - $cost - $expense - $otherExpense - $incomeTaxExpense"/>
                        <th class="left">
                            <xsl:choose>
                                <xsl:when test="$netIncome &lt; 0">Net loss</xsl:when>
                                <xsl:otherwise>Net income</xsl:otherwise>
                            </xsl:choose>
                        </th>
                        <td class="total right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="$netIncome"/>
                            </xsl:call-template>
                        </td>
                    </tr>
                </tfoot>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
