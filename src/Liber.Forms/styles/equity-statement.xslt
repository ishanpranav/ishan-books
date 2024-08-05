<?xml version="1.0" encoding="utf-8"?>
<!--
equity-statement.xslt
Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
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
    <xsl:template match="/report">
        <xsl:call-template name="financial-statement">
            <xsl:with-param name="title" select="title"/>
            <xsl:with-param name="table">
                <thead>
                    <tr>
                        <th colspan="2">
                            <xsl:value-of select="company/name"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="2" class="title">
                            <xsl:value-of select="title"/>
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
                <xsl:variable name="equity1" select="sum(company/account[equity = 'true']/previous)"/>
                <xsl:variable name="equity2" select="sum(company/account[equity = 'true']/balance)"/>
                <xsl:variable name="otherEquity1" select="sum(company/account[other-equity = 'true']/previous)"/>
                <xsl:variable name="otherEquity2" select="sum(company/account[other-equity = 'true']/balance)"/>
                <tbody>
                    <tr>
                        <th class="in-1 left">
                            <xsl:value-of select="liber:gets('retained-earnings')"/>
                        </th>
                    </tr>
                    <tr>
                        <td class="in-2 left">
                            <xsl:value-of select="concat(liber:pngets('retained-earnings', -$equity1), ', ', liber:fdates(started))"/>
                        </td>
                        <td class="right">
                            <xsl:value-of select="liber:fm(-$equity1)"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="in-2 left">
                            <xsl:value-of select="liber:pngets('net-income', -$netIncome)"/>
                        </td>
                        <td class="right">
                            <xsl:value-of select="liber:fm(-$netIncome)"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="in-2 left">
                            <xsl:value-of select="concat(liber:pngets('retained-earnings', -$equity2), ', ', liber:fdates(posted))"/>
                        </td>
                        <td class="right">
                            <xsl:value-of select="liber:fm(-$equity2)"/>
                        </td>
                    </tr>
                    <tr>
                        <th class="in-1 left">
                            <xsl:value-of select="liber:gets('accumulated-other-comprehensive-income')"/>
                        </th>
                    </tr>
                    <tr>
                        <td class="in-2 left">
                            <xsl:value-of select="concat(liber:pngets('accumulated-other-comprehensive-income', -$otherEquity1), ', ', liber:fdates(started))"/>
                        </td>
                        <td class="right">
                            <xsl:value-of select="liber:fm(-$otherEquity1)"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="in-2 left">
                            <xsl:value-of select="liber:pngets('other-comprehensive-income', -($otherEquity2 - $otherEquity1))"/>
                        </td>
                        <td class="right">
                            <xsl:value-of select="liber:fm(-($otherEquity2 - $otherEquity1))"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="in-2 left">
                            <xsl:value-of select="concat(liber:pngets('accumulated-other-comprehensive-income', -$otherEquity2), ', ', liber:fdates(posted))"/>
                        </td>
                        <td class="right">
                            <xsl:value-of select="liber:fm(-$otherEquity2)"/>
                        </td>
                    </tr>
                    <tr>
                        <xsl:variable name="equity" select="sum(company/account[type = 'Equity']/balance)"/>
                        <th class="left">
                            <xsl:value-of select="liber:fgets('total-equity{0}', liber:pngets(company/type, -$equity2 - $otherEquity2))"/>
                        </th>
                        <th class="total right">
                            <xsl:value-of select="liber:fm(-$equity2 - $otherEquity2)"/>
                        </th>
                    </tr>
                </tbody>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
