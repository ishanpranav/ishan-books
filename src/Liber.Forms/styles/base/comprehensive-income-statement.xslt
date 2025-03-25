<?xml version="1.0" encoding="utf-8"?>
<!--
comprehensive-income-statement.xslt
Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
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
    <xsl:template name="comprehensive-income-statement">
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
            <xsl:variable name="otherComprehensiveIncome" select="sum(company/account[other-equity = 'true']/balance) - sum(company/account[other-equity = 'true']/previous)"/>
            <tbody>
                <tr>
                    <th class="in-1 left">
                        <xsl:value-of select="liber:pngets('net-income', -$netIncome)"/>
                    </th>
                    <th class="right">
                        <xsl:value-of select="liber:fm(-$netIncome)"/>
                    </th>
                </tr>

                <tr>
                    <th class="in-1 left">
                        <xsl:value-of select="liber:pngets('other-comprehensive-income', -$otherComprehensiveIncome)"/>
                    </th>
                    <th class="right">
                        <xsl:value-of select="liber:fm(-$otherComprehensiveIncome)"/>
                    </th>
                </tr>
                <tr>
                    <th class="left">
                        <xsl:value-of select="liber:pngets('comprehensive-income', -($netIncome + $otherComprehensiveIncome))"/>
                    </th>
                    <td class="total right">
                        <xsl:value-of select="liber:fm(-($netIncome + $otherComprehensiveIncome))"/>
                    </td>
                </tr>
            </tbody>
        </table>
    </xsl:template>
</xsl:stylesheet>
