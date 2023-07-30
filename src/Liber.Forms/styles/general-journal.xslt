<?xml version="1.0" encoding="utf-8"?>
<!--
general-journal.xslt
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
    <xsl:variable name="title" select="liber:gets('general-journal')"/>
    <xsl:template match="/report">
        <xsl:call-template name="financial-statement">
            <xsl:with-param name="title" select="$title"/>
            <xsl:with-param name="table">
                <thead>
                    <tr>
                        <th colspan="6">
                            <xsl:value-of select="company/name"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="6" class="title">
                            <xsl:value-of select="$title"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="6" class="bar">
                            <xsl:value-of select="liber:ftspanl(started, posted)"/>
                        </th>
                    </tr>
                    <tr>
                        <th class="left">
                            <xsl:value-of select="liber:gets('date')"/>
                        </th>
                        <th class="left">
                            <xsl:value-of select="liber:gets('number')"/>
                        </th>
                        <th class="left">
                            <xsl:value-of select="liber:gets('name')"/>
                        </th>
                        <th></th>
                        <th>
                            <xsl:value-of select="liber:gets('debit')"/>
                        </th>
                        <th>
                            <xsl:value-of select="liber:gets('credit')"/>
                        </th>
                    </tr>
                    <tr>
                        <th class="heading" colspan="2"></th>
                        <td class="heading left">
                            <xsl:value-of select="liber:gets('description')"/>
                        </td>
                        <th class="heading">
                            <xsl:value-of select="liber:gets('account')"/>
                        </th>
                        <th class="heading" colspan="2"></th>
                    </tr>
                </thead>
                <tbody>
                    <xsl:for-each select="company/transaction">
                        <tr>
                            <td class="left">
                                <xsl:value-of select="liber:fdates(posted)"/>
                            </td>
                            <td class="right">
                                <xsl:value-of select="number"/>
                            </td>
                            <td class="left" colspan="2">
                                <xsl:value-of select="name"/>
                            </td>
                            <td class="subtotal" colspan="2"></td>
                        </tr>
                        <xsl:for-each select="line">
                            <tr>
                                <td colspan="2"></td>
                                <td class="left">
                                    <xsl:value-of select="description"/>
                                </td>
                                <xsl:choose>
                                    <xsl:when test="credit = 0">
                                        <td class="left">
                                            <xsl:value-of select="account"/>
                                        </td>
                                    </xsl:when>
                                    <xsl:otherwise>
                                        <td class="left in-2">
                                            <xsl:value-of select="account"/>
                                        </td>
                                    </xsl:otherwise>
                                </xsl:choose>
                                <td class="right">
                                    <xsl:value-of select="liber:fm(debit)"/>
                                </td>
                                <td class="right">
                                    <xsl:value-of select="liber:fm(credit)"/>
                                </td>
                            </tr>
                        </xsl:for-each>
                    </xsl:for-each>
                </tbody>
                <tfoot>
                    <tr>
                        <th class="left" colspan="4">
                            <xsl:value-of select="liber:gets('total')"/>
                        </th>
                        <td class="total right">
                            <xsl:value-of select="liber:fm(sum(company/transaction/line/debit))"/>
                        </td>
                        <td class="total right">
                            <xsl:value-of select="liber:fm(sum(company/transaction/line/credit))"/>
                        </td>
                    </tr>
                </tfoot>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
