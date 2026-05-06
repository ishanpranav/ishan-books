<?xml version="1.0" encoding="utf-8"?>
<!--
income-statement.xslt
Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
Licensed under the MIT License.
-->
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:html="http://www.w3.org/1999/xhtml"
    xmlns:liber="urn:liber"
    exclude-result-prefixes="msxsl">
    <xsl:include href="financial-statement.xslt"/>
    <xsl:output method="html" indent="yes"/>
    <xsl:template name="income-statement">
        <xsl:param name="title"/>
        <table>
            <thead>
                <tr>
                    <th colspan="2" class="subtitle">
                        <xsl:value-of select="company/name"/>
                    </th>
                </tr>
                <tr>
                    <th colspan="2" class="title">
                        <xsl:value-of select="$title"/>
                    </th>
                </tr>
                <tr>
                    <th colspan="2" class="bar dateline">
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
                <xsl:apply-templates select="company">
                    <xsl:with-param name="type" select="'Income'"/>
                    <xsl:with-param name="balance" select="-$income"/>
                    <xsl:with-param name="indent" select="0"/>
                </xsl:apply-templates>
                <xsl:apply-templates select="company">
                    <xsl:with-param name="type" select="'Cost'"/>
                    <xsl:with-param name="balance" select="$cost"/>
                    <xsl:with-param name="indent" select="0"/>
                </xsl:apply-templates>
                <tr>
                    <td class="in-2 left">
                        <xsl:value-of select="liber:pngets('gross-profit', -($income + $cost))"/>
                    </td>
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fm(-($income + $cost))"/>
                    </td>
                </tr>
                <xsl:apply-templates select="company">
                    <xsl:with-param name="type" select="'Expense'"/>
                    <xsl:with-param name="balance" select="$expense"/>
                    <xsl:with-param name="indent" select="0"/>
                </xsl:apply-templates>
                <tr>
                    <td class="left">
                        <xsl:value-of select="liber:pngets('operating-income', -($income + $cost + $expense))"/>
                    </td>
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fm(-($income + $cost + $expense))"/>
                    </td>
                </tr>
                <xsl:apply-templates select="company">
                    <xsl:with-param name="type" select="'OtherIncomeExpense'"/>
                    <xsl:with-param name="balance" select="-$otherIncomeExpense"/>
                    <xsl:with-param name="indent" select="0"/>
                </xsl:apply-templates>
                <tr>
                    <td class="left">
                        <xsl:value-of select="liber:pngets('pretax-income', -($income + $cost + $expense + $otherIncomeExpense))"/>
                    </td>
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fm(-($income + $cost + $expense + $otherIncomeExpense))"/>
                    </td>
                </tr>
                <xsl:apply-templates select="company">
                    <xsl:with-param name="type">IncomeTaxExpense</xsl:with-param>
                    <xsl:with-param name="balance" select="$incomeTaxExpense"/>
                    <xsl:with-param name="indent" select="0"/>
                </xsl:apply-templates>
                <tr>
                    <td class="left">
                        <xsl:value-of select="liber:gets('net-income')"/>
                    </td>
                    <td class="grand-total right">
                        <xsl:value-of select="liber:fm(-$netIncome)"/>
                    </td>
                </tr>
            </tbody>
        </table>
    </xsl:template>
</xsl:stylesheet>
