<?xml version="1.0" encoding="utf-8"?>
<!--
reconciliation-report.xslt
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
    <xsl:include href="base/financial-statement.xslt"/>
    <xsl:output method="html" indent="yes"/>
    <xsl:key name="accounts-by-name" match="account" use="concat(generate-id(..), '|', name)"/>
    <xsl:template match="/report">
        <xsl:call-template name="financial-statement">
            <xsl:with-param name="title" select="title"/>
            <xsl:with-param name="table">
                <thead>
                    <tr>
                        <th colspan="4" class="subtitle">
                            <xsl:value-of select="company/name"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="4" class="title">
                            <xsl:value-of select="title"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="4" class="dateline">
                            <xsl:value-of select="liber:fdatel(posted)"/>
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
                        <th></th>
                        <th class="heading">
                            <xsl:value-of select="liber:gets('balance')"/>
                        </th>
                        <th class="heading">
                            <xsl:value-of select="liber:gets('reconciled-balance')"/>
                        </th>
                        <th class="heading">
                            <xsl:value-of select="liber:gets('difference')"/>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <xsl:for-each select="company/account[
                        generate-id() = generate-id(key('accounts-by-name', concat(generate-id(..), '|', name))[1])
                    ]">
                        <xsl:variable name="group" select="key('accounts-by-name', concat(generate-id(..), '|', name))"/>
                        <xsl:variable name="groupBalance" select="sum($group/balance)"/>
                        <xsl:variable name="groupReconciledBalance" select="sum($group/reconciled-balance)"/>
                        <xsl:if test="$groupReconciledBalance != 0">
                            <tr>
                                <td class="left account">
                                    <xsl:value-of select="name"/>
                                </td>
                                <td class="right">
                                    <xsl:value-of select="liber:fm(type, $groupBalance)"/>
                                </td>
                                <td class="right">
                                    <xsl:value-of select="liber:fm(type, $groupReconciledBalance)"/>
                                </td>
                                <td class="right">
                                    <xsl:value-of select="liber:fm(type, $groupBalance - $groupReconciledBalance)"/>
                                </td>
                            </tr>
                        </xsl:if>
                    </xsl:for-each>
                </tbody>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
