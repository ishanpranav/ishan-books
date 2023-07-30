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
                            <xsl:value-of select="liber:ftspanl(started, posted)"/>
                        </th>
                    </tr>
                    <tr>
                        <th></th>
                        <th class="heading">
                            <xsl:value-of select="liber:ftspans(started, posted)"/>
                        </th>
                    </tr>
                </thead>
                <xsl:variable name="income" select="sum(company/account[type = 'Income']/balance)"/>
                <xsl:variable name="cost" select="sum(company/account[type = 'Cost']/balance)"/>
                <xsl:variable name="expense" select="sum(company/account[type = 'Expense']/balance)"/>
                <xsl:variable name="otherIncomeExpense" select="sum(company/account[type = 'OtherIncomeExpense']/balance)"/>
                <xsl:variable name="incomeTaxExpense" select="sum(company/account[type = 'IncomeTaxExpense']/balance)"/>
                <tbody>
                    <tr>
                        <th class="in-1 left">
                            <xsl:value-of select="liber:gets('ordinary-income')"/>
                        </th>
                        <th></th>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">Income</xsl:with-param>
                        <xsl:with-param name="balance" select="-$income"/>
                        <xsl:with-param name="indent">3</xsl:with-param>
                    </xsl:apply-templates>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">Cost</xsl:with-param>
                        <xsl:with-param name="balance" select="$cost"/>
                        <xsl:with-param name="indent">3</xsl:with-param>
                    </xsl:apply-templates>
                    <tr>
                        <th class="in-2 left">
                            <xsl:value-of select="liber:pngets('gross-profit', -($income + $cost))"/>
                        </th>
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm(-($income + $cost))"/>
                        </td>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">Expense</xsl:with-param>
                        <xsl:with-param name="balance" select="$expense"/>
                        <xsl:with-param name="indent">3</xsl:with-param>
                    </xsl:apply-templates>
                    <tr>
                        <th class="in-1 left">
                            <xsl:value-of select="liber:pngets('ordinary-income', -($income + $cost + $expense))"/>
                        </th>
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm(-($income + $cost + $expense))"/>
                        </td>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">OtherIncomeExpense</xsl:with-param>
                        <xsl:with-param name="balance" select="$otherIncomeExpense"/>
                        <xsl:with-param name="indent">2</xsl:with-param>
                    </xsl:apply-templates>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">IncomeTaxExpense</xsl:with-param>
                        <xsl:with-param name="balance" select="$incomeTaxExpense"/>
                        <xsl:with-param name="indent">2</xsl:with-param>
                    </xsl:apply-templates>
                </tbody>
                <tfoot>
                    <tr>
                        <xsl:variable name="netIncome" select="$income + $otherIncomeExpense + $cost + $expense + $incomeTaxExpense"/>
                        <th class="left">
                            <xsl:value-of select="liber:pngets('net-income', -$netIncome)"/>
                        </th>
                        <td class="total right">
                            <xsl:value-of select="liber:fm(-$netIncome)"/>
                        </td>
                    </tr>
                </tfoot>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
