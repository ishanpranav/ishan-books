<?xml version="1.0" encoding="utf-8"?>
<!--
annual-report.xslt
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
    <xsl:import href="base/income-statement.xslt"/>
    <xsl:import href="base/comprehensive-income-statement.xslt"/>
    <xsl:template match="/report">
        <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
            <head>
                <meta charset="utf-8"/>
                <title>
                    <xsl:value-of select="title"/>
                </title>
                <link rel="stylesheet" type="text/css" href="https://liber.example/styles/financial-statement.css"/>
            </head>
            <body>
                <xsl:call-template name="income-statement">
                    <xsl:with-param name="title">Income Statement</xsl:with-param>
                </xsl:call-template>
                <xsl:call-template name="comprehensive-income-statement">
                    <xsl:with-param name="title">Comprehensive Income Statement</xsl:with-param>
                </xsl:call-template>
            </body>
        </html>
    </xsl:template>
    <xsl:output method="html" indent="yes"/>
</xsl:stylesheet>
