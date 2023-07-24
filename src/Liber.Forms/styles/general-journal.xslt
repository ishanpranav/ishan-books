<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:html="http://www.w3.org/1999/xhtml"
    exclude-result-prefixes="msxsl">
    <xsl:include href="financial-statement.xslt"/>
    <xsl:output method="html" indent="yes"/>
    <xsl:variable name="title">General Journal</xsl:variable>
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
                            <xsl:call-template name="date-long">
                                <xsl:with-param name="value" select="started"/>
                            </xsl:call-template>
                            <xsl:text> &#x2013; </xsl:text>
                            <xsl:call-template name="date-long">
                                <xsl:with-param name="value" select="posted"/>
                            </xsl:call-template>
                        </th>
                    </tr>
                    <tr>
                        <th class="left">Date</th>
                        <th class="left">Number</th>
                        <th class="left">Name</th>
                        <th></th>
                        <th>Debit</th>
                        <th>Credit</th>
                    </tr>
                    <tr>
                        <th class="heading" colspan="2"></th>
                        <td class="heading left">Description</td>
                        <th class="heading">Account</th>
                        <th class="heading" colspan="2"></th>
                    </tr>
                </thead>
                <tbody>
                    <xsl:for-each select="company/transaction">
                        <tr>
                            <td class="left">
                                <xsl:call-template name="date-simple">
                                    <xsl:with-param name="value" select="posted"/>
                                </xsl:call-template>
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
                                    <xsl:call-template name="number">
                                        <xsl:with-param name="value" select="debit"/>
                                    </xsl:call-template>
                                </td>
                                <td class="right">
                                    <xsl:call-template name="number">
                                        <xsl:with-param name="value" select="credit"/>
                                    </xsl:call-template>
                                </td>
                            </tr>
                        </xsl:for-each>
                    </xsl:for-each>
                </tbody>
                <tfoot>
                    <tr>
                        <th class="left" colspan="4">Total</th>
                        <td class="total right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="sum(company/transaction/line/debit)"/>
                            </xsl:call-template>
                        </td>
                        <td class="total right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="sum(company/transaction/line/credit)"/>
                            </xsl:call-template>
                        </td>
                    </tr>
                </tfoot>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
