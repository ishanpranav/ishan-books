<?xml version="1.0" encoding="utf-8"?>
<!--
financial-statement.xslt
Copyright (c) 2023 Ishan Pranav. All rights reserved.
Licensed under the MIT License.
-->
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:liber="urn:liber"
    exclude-result-prefixes="msxsl">
    <xsl:output method="html" indent="yes"/>
    <xsl:template name="financial-statement">
        <xsl:param name="title"/>
        <xsl:param name="table"/>
        <xsl:param name="styles"/>
        <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
            <head>
                <meta charset="utf-8"/>
                <title>
                    <xsl:value-of select="$title"/>
                </title>
                <link rel="stylesheet" type="text/css" href="https://liber.example/styles/financial-statement.css"/>
                <xsl:copy-of select="$styles"/>
            </head>
            <body>
                <table>
                    <xsl:copy-of select="$table"/>
                </table>
            </body>
        </html>
    </xsl:template>
    <xsl:template match="company">
        <xsl:param name="type"/>
        <xsl:param name="balance"/>
        <xsl:param name="previous"/>
        <xsl:param name="indent"/>
        <xsl:param name="title" select="liber:gets($type)"/>
        <xsl:param name="subtitle" select="liber:pngets($type, $balance)"/>
        <xsl:param name="comparative" select="0"/>
        <xsl:choose>
            <xsl:when test="$balance != 0 or $previous != 0">
                <tr>
                    <th class="in-{$indent} left">
                        <xsl:value-of select="$title"/>
                    </th>
                    <th></th>
                </tr>
                <xsl:for-each select="//account[type = $type and (balance != 0 or previous != 0)]">
                    <tr>
                        <td class="in-{$indent + 2} left">
                            <xsl:value-of select="name"/>
                        </td>
                        <td class="right">
                            <xsl:value-of select="liber:fm($type, balance)"/>
                        </td>
                        <xsl:choose>
                            <xsl:when test="$comparative = 1">
                                <td class="right">
                                    <xsl:value-of select="liber:fm($type, previous)"/>
                                </td>
                            </xsl:when>
                        </xsl:choose>
                    </tr>
                </xsl:for-each>
            </xsl:when>
        </xsl:choose>
        <tr>
            <th class="in-{$indent} left">
                <xsl:value-of select="$subtitle"/>
            </th>
            <td class="subtotal right">
                <xsl:value-of select="liber:fm($balance)"/>
            </td>
            <xsl:choose>
                <xsl:when test="$comparative = 1">
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fm($previous)"/>
                    </td>
                </xsl:when>
            </xsl:choose>
        </tr>
    </xsl:template>
</xsl:stylesheet>
