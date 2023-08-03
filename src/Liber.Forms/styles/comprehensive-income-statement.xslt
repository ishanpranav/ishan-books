<?xml version="1.0" encoding="utf-8"?>
<!--
comprehensive-income-statement.xslt
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
                <xsl:variable name="otherComprehensiveIncome" select="sum(company/account[type = 'OtherComprehensiveIncome']/balance)"/>
                <tbody>
                    <tr>
                        <th class="in-1 left">
                            <xsl:value-of select="liber:pngets('net-income', -$netIncome)"/>
                        </th>
                        <th class="right">
                            <xsl:value-of select="liber:fm(-$netIncome)"/>
                        </th>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type" select="'OtherComprehensiveIncome'"/>
                        <xsl:with-param name="balance" select="-$otherComprehensiveIncome"/>
                    </xsl:apply-templates>
                </tbody>
                <tfoot>
                    <tr>
                        <th class="left">
                            <xsl:value-of select="liber:gets('comprehensive-income')"/>
                        </th>
                        <td class="total right">
                            <xsl:value-of select="liber:fm(-($netIncome + $otherComprehensiveIncome))"/>
                        </td>
                    </tr>
                </tfoot>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
