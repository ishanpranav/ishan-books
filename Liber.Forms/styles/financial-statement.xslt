<?xml version="1.0" encoding="utf-8"?>
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
            <xsl:when test="$value = 0">-</xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="format-number($value, ' #,##0.00 ;(#,##0.00)')"/>
            </xsl:otherwise>
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
</xsl:stylesheet>
