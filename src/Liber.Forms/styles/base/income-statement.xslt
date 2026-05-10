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
        <xsl:param name="absoluteSize" select="1"/>
        <xsl:param name="commonSize" select="0"/>
        <xsl:variable name="income"           select="sum(company/account[type = 'Income']/balance)"/>
        <xsl:variable name="cost"             select="sum(company/account[type = 'Cost']/balance)"/>
        <xsl:variable name="expense"          select="sum(company/account[type = 'Expense']/balance)"/>
        <xsl:variable name="otherIncomeExpense" select="sum(company/account[type = 'OtherIncomeExpense']/balance)"/>
        <xsl:variable name="incomeTaxExpense" select="sum(company/account[type = 'IncomeTaxExpense']/balance)"/>
        <xsl:variable name="revenue"          select="-$income"/>
        <xsl:variable name="perPeriod">
            <xsl:choose>
                <xsl:when test="$absoluteSize = 1 and $commonSize = 1">2</xsl:when>
                <xsl:otherwise>1</xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:variable name="cols">
            <xsl:value-of select="1 + $perPeriod"/>
        </xsl:variable>
        <table>
            <thead>
                <tr>
                    <th colspan="{$cols}" class="subtitle">
                        <xsl:value-of select="company/name"/>
                    </th>
                </tr>
                <tr>
                    <th colspan="{$cols}" class="title">
                        <xsl:value-of select="$title"/>
                    </th>
                </tr>
                <tr>
                    <th colspan="{$cols}" class="dateline">
                        <xsl:value-of select="liber:ftspanl(started, posted)"/>
                    </th>
                </tr>
                <xsl:if test="company/multiple != ''">
                    <tr>
                        <th colspan="{$cols}" class="dateline">
                            <xsl:value-of select="company/multiple"/>
                        </th>
                    </tr>
                </xsl:if>
                <tr class="overline">
                    <th/>
                    <xsl:if test="$absoluteSize = 1">
                        <th class="heading">
                            <xsl:value-of select="liber:ftspans(started, posted)"/>
                        </th>
                    </xsl:if>
                    <xsl:if test="$commonSize = 1">
                        <th class="heading">%</th>
                    </xsl:if>
                </tr>
            </thead>
            <tbody>
                <xsl:apply-templates select="company">
                    <xsl:with-param name="type"         select="'Income'"/>
                    <xsl:with-param name="balance"      select="-$income"/>
                    <xsl:with-param name="indent"       select="0"/>
                    <xsl:with-param name="cols"         select="$cols"/>
                    <xsl:with-param name="absoluteSize" select="$absoluteSize"/>
                    <xsl:with-param name="commonSize"   select="$commonSize"/>
                    <xsl:with-param name="denominator"  select="$revenue"/>
                </xsl:apply-templates>
                <xsl:apply-templates select="company">
                    <xsl:with-param name="type"         select="'Cost'"/>
                    <xsl:with-param name="balance"      select="$cost"/>
                    <xsl:with-param name="indent"       select="0"/>
                    <xsl:with-param name="cols"         select="$cols"/>
                    <xsl:with-param name="absoluteSize" select="$absoluteSize"/>
                    <xsl:with-param name="commonSize"   select="$commonSize"/>
                    <xsl:with-param name="denominator"  select="$revenue"/>
                </xsl:apply-templates>
                <tr>
                    <td class="in-2 left">
                        <xsl:value-of select="liber:pngets('gross-profit', -($income + $cost))"/>
                    </td>
                    <xsl:if test="$absoluteSize = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm(-($income + $cost))"/>
                        </td>
                    </xsl:if>
                    <xsl:if test="$commonSize = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fp(-($income + $cost) div $revenue)"/>
                        </td>
                    </xsl:if>
                </tr>
                <xsl:apply-templates select="company">
                    <xsl:with-param name="type"         select="'Expense'"/>
                    <xsl:with-param name="balance"      select="$expense"/>
                    <xsl:with-param name="indent"       select="0"/>
                    <xsl:with-param name="cols"         select="$cols"/>
                    <xsl:with-param name="absoluteSize" select="$absoluteSize"/>
                    <xsl:with-param name="commonSize"   select="$commonSize"/>
                    <xsl:with-param name="denominator"  select="$revenue"/>
                </xsl:apply-templates>
                <tr>
                    <td class="left">
                        <xsl:value-of select="liber:pngets('operating-income', -($income + $cost + $expense))"/>
                    </td>
                    <xsl:if test="$absoluteSize = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm(-($income + $cost + $expense))"/>
                        </td>
                    </xsl:if>
                    <xsl:if test="$commonSize = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fp(-($income + $cost + $expense) div $revenue)"/>
                        </td>
                    </xsl:if>
                </tr>
                <xsl:apply-templates select="company">
                    <xsl:with-param name="type"         select="'OtherIncomeExpense'"/>
                    <xsl:with-param name="balance"      select="-$otherIncomeExpense"/>
                    <xsl:with-param name="indent"       select="0"/>
                    <xsl:with-param name="cols"         select="$cols"/>
                    <xsl:with-param name="absoluteSize" select="$absoluteSize"/>
                    <xsl:with-param name="commonSize"   select="$commonSize"/>
                    <xsl:with-param name="denominator"  select="$revenue"/>
                </xsl:apply-templates>
                <tr>
                    <td class="left">
                        <xsl:value-of select="liber:pngets('pretax-income', -($income + $cost + $expense + $otherIncomeExpense))"/>
                    </td>
                    <xsl:if test="$absoluteSize = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm(-($income + $cost + $expense + $otherIncomeExpense))"/>
                        </td>
                    </xsl:if>
                    <xsl:if test="$commonSize = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fp(-($income + $cost + $expense + $otherIncomeExpense) div $revenue)"/>
                        </td>
                    </xsl:if>
                </tr>
                <xsl:apply-templates select="company">
                    <xsl:with-param name="type">IncomeTaxExpense</xsl:with-param>
                    <xsl:with-param name="balance"      select="$incomeTaxExpense"/>
                    <xsl:with-param name="indent"       select="0"/>
                    <xsl:with-param name="cols"         select="$cols"/>
                    <xsl:with-param name="absoluteSize" select="$absoluteSize"/>
                    <xsl:with-param name="commonSize"   select="$commonSize"/>
                    <xsl:with-param name="denominator"  select="$revenue"/>
                </xsl:apply-templates>
                <tr>
                    <td class="left">
                        <xsl:value-of select="liber:gets('net-income')"/>
                    </td>
                    <xsl:if test="$absoluteSize = 1">
                        <td class="grand-total right">
                            <xsl:value-of select="liber:fm(-$netIncome)"/>
                        </td>
                    </xsl:if>
                    <xsl:if test="$commonSize = 1">
                        <td class="grand-total right">
                            <xsl:value-of select="liber:fp(-$netIncome div $revenue)"/>
                        </td>
                    </xsl:if>
                </tr>
            </tbody>
        </table>
    </xsl:template>
</xsl:stylesheet>
