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
        <table>
            <thead>
                <tr>
                    <th colspan="3" class="subtitle">
                        <xsl:value-of select="company/name"/>
                    </th>
                </tr>
                <tr>
                    <th colspan="3" class="title">
                        <xsl:value-of select="$title"/>
                    </th>
                </tr>
                <tr>
                    <th colspan="3" class="bar dateline">
                        <xsl:value-of select="liber:fdatel(posted)"/>
                    </th>
                </tr>
                <tr>
                    <th></th>
                    <th class="heading">
                        <xsl:value-of select="liber:fdates(posted)"/>
                    </th>
                    <th class="heading">
                        <xsl:value-of select="liber:fdates(started)"/>
                    </th>
                </tr>
            </thead>
            <xsl:call-template name="balance-sheet-internal">
                <xsl:with-param name="comparative" select="1"/>
            </xsl:call-template>
        </table>
    </xsl:template>
</xsl:stylesheet>
