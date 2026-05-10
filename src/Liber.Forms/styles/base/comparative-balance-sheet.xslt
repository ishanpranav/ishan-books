<?xml version="1.0" encoding="utf-8"?>
<!--
comparative-balance-sheet.xslt
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
    <xsl:include href="balance-sheet.xslt"/>
    <xsl:output method="html" indent="yes"/>
    <xsl:template name="comparative-balance-sheet">
        <xsl:param name="title"/>
        <xsl:param name="commonSize" select="0"/>
        <xsl:param name="absoluteSize" select="1"/>
        <xsl:variable name="perPeriod">
            <xsl:choose>
                <xsl:when test="$absoluteSize = 1 and $commonSize = 1">2</xsl:when>
                <xsl:otherwise>1</xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:variable name="cols">
            <xsl:value-of select="1 + (2 * $perPeriod)"/>
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
                        <xsl:value-of select="liber:fdatel(posted)"/>
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
                            <xsl:value-of select="liber:fdates(posted)"/>
                        </th>
                    </xsl:if>
                    <xsl:if test="$commonSize = 1">
                        <th class="heading">%</th>
                    </xsl:if>
                    <xsl:if test="$absoluteSize = 1">
                        <th class="heading">
                            <xsl:value-of select="liber:fdates(started)"/>
                        </th>
                    </xsl:if>
                    <xsl:if test="$commonSize = 1">
                        <th class="heading">%</th>
                    </xsl:if>
                </tr>
            </thead>
            <xsl:call-template name="balance-sheet-internal">
                <xsl:with-param name="comparative"  select="1"/>
                <xsl:with-param name="commonSize"   select="$commonSize"/>
                <xsl:with-param name="absoluteSize" select="$absoluteSize"/>
            </xsl:call-template>
        </table>
    </xsl:template>
</xsl:stylesheet>
