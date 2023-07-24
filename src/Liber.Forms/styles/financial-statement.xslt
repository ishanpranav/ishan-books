﻿<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    exclude-result-prefixes="msxsl">
    <xsl:output method="html" indent="yes"/>
    <xsl:template name="date-year">
        <xsl:param name="value"/>
        <xsl:value-of select="substring($value, 1, 4)"/>
    </xsl:template>
    <xsl:template name="date-long">
        <xsl:param name="value"/>
        <xsl:variable name="month" select="substring($value, 6, 2)"/>
        <xsl:choose>
            <xsl:when test="$month = '01'">January</xsl:when>
            <xsl:when test="$month = '02'">February</xsl:when>
            <xsl:when test="$month = '03'">March</xsl:when>
            <xsl:when test="$month = '04'">April</xsl:when>
            <xsl:when test="$month = '05'">May</xsl:when>
            <xsl:when test="$month = '06'">June</xsl:when>
            <xsl:when test="$month = '07'">July</xsl:when>
            <xsl:when test="$month = '08'">August</xsl:when>
            <xsl:when test="$month = '09'">September</xsl:when>
            <xsl:when test="$month = '10'">October</xsl:when>
            <xsl:when test="$month = '11'">November</xsl:when>
            <xsl:when test="$month = '12'">December</xsl:when>
        </xsl:choose>
        <xsl:text> </xsl:text>
        <xsl:value-of select="format-number(substring($value, 9, 2), '#')"/>
        <xsl:text>, </xsl:text>
        <xsl:call-template name="date-year">
            <xsl:with-param name="value" select="$value"/>
        </xsl:call-template>
    </xsl:template>
    <xsl:template name="date-simple">
        <xsl:param name="value"/>
        <xsl:value-of select="substring($value, 6, 2)"/>
        <xsl:text>/</xsl:text>
        <xsl:value-of select="substring($value, 9, 2)"/>
        <xsl:text>/</xsl:text>
        <xsl:call-template name="date-year">
            <xsl:with-param name="value" select="$value"/>
        </xsl:call-template>
    </xsl:template>
    <xsl:template name="number">
        <xsl:param name="value"/>
        <xsl:choose>
            <xsl:when test="$value != 0">
                <xsl:value-of select="format-number($value, ' #,##0.00 ;(#,##0.00)')"/>
            </xsl:when>
        </xsl:choose>
    </xsl:template>
    <xsl:template name="financial-statement">
        <xsl:param name="title"/>
        <xsl:param name="table"/>
        <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
            <head>
                <meta charset="utf-8"/>
                <title>
                    <xsl:value-of select="$title"/>
                </title>
                <link rel="stylesheet" type="text/css" href="https://sharp-books.example/styles/financial-statement.css"/>
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
        <xsl:param name="description"/>
        <xsl:param name="total"/>
        <xsl:param name="negative" select="$total"/>
        <xsl:param name="indent"/>
        <xsl:param name="sign" select="1"/>
        <xsl:choose>
            <xsl:when test="$balance != 0">
                <tr>
                    <th class="in-{$indent} left">
                        <xsl:value-of select="$description"/>
                    </th>
                    <th></th>
                </tr>
                <xsl:for-each select="//account[type = $type]">
                    <tr>
                        <td class="in-{$indent + 2} left">
                            <xsl:value-of select="name"/>
                        </td>
                        <td class="right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="$sign * (debit - credit)"/>
                            </xsl:call-template>
                        </td>
                    </tr>
                </xsl:for-each>
            </xsl:when>
        </xsl:choose>
        <tr>
            <th class="in-{$indent} left">
                <xsl:choose>
                    <xsl:when test="$sign * $balance &lt; 0">
                        <xsl:value-of select="$negative"/>
                    </xsl:when>
                    <xsl:otherwise>
                        <xsl:value-of select="$total"/>
                    </xsl:otherwise>
                </xsl:choose>
            </th>
            <td class="subtotal right">
                <xsl:call-template name="number">
                    <xsl:with-param name="value" select="$sign * $balance"/>
                </xsl:call-template>
            </td>
        </tr>
    </xsl:template>
    <xsl:template name="equity">
        <xsl:param name="type"/>
        <xsl:param name="value" select="0"/>
        <xsl:choose>
            <xsl:when test="type = 'Individual' and value &gt;= 0">Owner's equity</xsl:when>
            <xsl:when test="type = 'Parternship' and value &gt;= 0">Partners' equity</xsl:when>
            <xsl:when test="type = 'Corporation' and value &gt;= 0">Shareholders' equity</xsl:when>
            <xsl:when test="value &lt; 0">Deficit</xsl:when>
            <xsl:otherwise>Equity</xsl:otherwise>
        </xsl:choose>
    </xsl:template>
</xsl:stylesheet>