<?xml version="1.0" encoding="utf-8"?>
<!--
customer-report.xslt
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
                <xsl:variable name="debit" select="sum(company/transaction/line[account = //account/name]/debit) - sum(company/transaction/line[account = //account/name]/credit)"/>
                <thead>
                    <tr>
                        <th colspan="4" class="title left">
                            <xsl:value-of select="liber:pngets('customer-report', $debit)"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="3" class="subtitle left">
                            <xsl:value-of select="company/name"/>
                        </th>
                        <th colspan="1" class="subtitle dateline right">
                            <xsl:value-of select="liber:fdatel(posted)"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="4" class="subtitle left bar">
                            <xsl:value-of select="title"/>
                        </th>
                    </tr>
                    <tr>
                        <th class="left">
                            <xsl:value-of select="liber:gets('date')"/>
                        </th>
                        <th class="left">
                            <xsl:value-of select="liber:gets('name')"/>
                        </th>
                        <th class="left">
                            <xsl:value-of select="liber:gets('description')"/>
                        </th>
                        <th class="right">
                            <xsl:value-of select="liber:gets('balance')"/>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <xsl:for-each select="company/transaction">
                        <xsl:for-each select="line[account = //account/name]">
                            <tr>
                                <td class="left">
                                    <xsl:value-of select="liber:fdates(../posted)"/>
                                </td>
                                <td class="left" colspan="1">
                                    <xsl:value-of select="../name"/>
                                </td>
                                <td class="left">
                                    <xsl:value-of select="description"/>
                                </td>
                                <td class="right">
                                    <xsl:value-of select="liber:fm(debit - credit)"/>
                                </td>
                            </tr>
                        </xsl:for-each>
                    </xsl:for-each>
                </tbody>
                <tfoot>
                    <tr>
                        <th class="left" colspan="3">
                            <xsl:value-of select="liber:gets('total')"/>
                        </th>
                        <td class="total right">
                            <xsl:value-of select="liber:fm($debit)"/>
                        </td>
                    </tr>
                </tfoot>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
