<?xml version="1.0" encoding="utf-8"?>
<!--
trial-balance.xslt
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
                        <th colspan="3">
                            <xsl:value-of select="company/name"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="3" class="title">
                            <xsl:value-of select="title"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="3" class="bar">
                            <xsl:value-of select="liber:fdatel(posted)"/>
                        </th>
                    </tr>
                    <tr>
                        <th></th>
                        <th colspan="2" class="heading">
                            <xsl:value-of select="liber:fdates(posted)"/>
                        </th>
                    </tr>
                    <tr>
                        <th></th>
                        <th class="heading">
                            <xsl:value-of select="liber:gets('debit')"/>
                        </th>
                        <th class="heading">
                            <xsl:value-of select="liber:gets('credit')"/>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <xsl:for-each select="company/account">
                        <tr>
                            <td class="left">
                                <xsl:value-of select="name"/>
                            </td>
                            <td class="right">
                                <xsl:value-of select="liber:fm(debit)"/>
                            </td>
                            <td class="right">
                                <xsl:value-of select="liber:fm(credit)"/>
                            </td>
                        </tr>
                    </xsl:for-each>
                </tbody>
                <tfoot>
                    <tr>
                        <th class="left">
                            <xsl:value-of select="liber:gets('total')"/>
                        </th>
                        <td class="total right">
                            <xsl:value-of select="liber:fm(sum(company/account/debit))"/>
                        </td>
                        <td class="total right">
                            <xsl:value-of select="liber:fm(sum(company/account/credit))"/>
                        </td>
                    </tr>
                </tfoot>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
