<?xml version="1.0" encoding="utf-8"?>
<!--
aging-summary.xslt
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
                <thead>
                    <tr>
                        <th colspan="7">
                            <xsl:value-of select="company/name"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="7" class="title">
                            <xsl:value-of select="title"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="7" class="bar">
                            <xsl:value-of select="liber:fdatel(posted)"/>
                        </th>
                    </tr>
                    <tr>
                        <th></th>
                        <th class="heading">
                            <xsl:value-of select="liber:gets('current')"/>
                        </th>
                        <th class="heading">
                            <xsl:value-of select="liber:fgets('days{0}{1}', 1, 30)"/>
                        </th>
                        <th class="heading">
                            <xsl:value-of select="liber:fgets('days{0}{1}', 31, 60)"/>
                        </th>
                        <th class="heading">
                            <xsl:value-of select="liber:fgets('days{0}{1}', 61, 90)"/>
                        </th>
                        <th class="heading">
                            <xsl:value-of select="liber:fgets('days{0}', 91)"/>
                        </th>
                        <th class="heading">
                            <xsl:value-of select="liber:gets('total')"/>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <xsl:for-each select="company/account[type = 'OtherCurrentAsset']">
                        <tr>
                            <td class="left">
                                <xsl:value-of select="name"/>
                            </td>
                            <td class="right">X</td>
                            <td class="right">X</td>
                            <td class="right">X</td>
                            <td class="right">X</td>
                            <td class="right">X</td>
                            <td class="right">X</td>
                        </tr>
                    </xsl:for-each>
                </tbody>
                <tfoot>
                    <tr>
                        <th class="left">
                            <xsl:value-of select="liber:gets('total')"/>
                        </th>
                        <th class="subtotal right">X</th>
                        <th class="subtotal right">X</th>
                        <th class="subtotal right">X</th>
                        <th class="subtotal right">X</th>
                        <th class="subtotal right">X</th>
                        <th class="total right">X</th>
                    </tr>
                </tfoot>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
