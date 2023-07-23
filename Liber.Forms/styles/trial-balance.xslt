<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:html="http://www.w3.org/1999/xhtml"
    exclude-result-prefixes="msxsl">
    <xsl:include href="financial-statement.xslt"/>
    <xsl:output method="html" indent="yes"/>
    <xsl:variable name="title">Trial Balance</xsl:variable>
    <xsl:template match="/report">
        <xsl:call-template name="financial-statement">
            <xsl:with-param name="title" select="$title"/>
            <xsl:with-param name="table">
                <thead>
                    <tr>
                        <th colspan="3">
                            <xsl:value-of select="company/name"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="3" class="title">
                            <xsl:value-of select="$title"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="3" class="bar">
                            <xsl:text>As of </xsl:text>
                            <xsl:call-template name="date-long">
                                <xsl:with-param name="value" select="posted"/>
                            </xsl:call-template>
                        </th>
                    </tr>
                    <tr>
                        <th></th>
                        <th colspan="2" class="heading">
                            <xsl:call-template name="date-year">
                                <xsl:with-param name="value" select="posted"/>
                            </xsl:call-template>
                        </th>
                    </tr>
                    <tr>
                        <th></th>
                        <th class="heading">Debit</th>
                        <th class="heading">Credit</th>
                    </tr>
                </thead>
                <tbody>
                    <xsl:for-each select="company/account">
                        <tr>
                            <td class="left">
                                <xsl:value-of select="name"/>
                            </td>
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
                </tbody>
                <tfoot>
                    <tr>
                        <th class="left">Total</th>
                        <td class="total right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="sum(company/account/debit)"/>
                            </xsl:call-template>
                        </td>
                        <td class="total right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="sum(company/account/credit)"/>
                            </xsl:call-template>
                        </td>
                    </tr>
                </tfoot>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>