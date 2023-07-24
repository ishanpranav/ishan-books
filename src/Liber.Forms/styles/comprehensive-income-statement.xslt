<?xml version="1.0" encoding="utf-8"?>
<!--
comprehensive-income-statement.xslt
Copyright (c) 2023 Ishan Pranav. All rights reserved.
Licensed under the MIT License.
-->
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:html="http://www.w3.org/1999/xhtml"
    exclude-result-prefixes="msxsl">
    <xsl:include href="financial-statement.xslt"/>
    <xsl:output method="html" indent="yes"/>
    <xsl:variable name="title">Comprehensive Income Statement</xsl:variable>
    <xsl:template match="/report">
        <xsl:call-template name="financial-statement">
            <xsl:with-param name="title" select="$title"/>
            <xsl:with-param name="table">
                <thead>
                    <tr>
                        <th colspan="2">
                            <xsl:value-of select="company/name"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="2" class="title">
                            <xsl:value-of select="$title"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="2" class="bar">
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
                        <th></th>
                        <th class="heading">
                            <xsl:call-template name="date-year">
                                <xsl:with-param name="value" select="posted"/>
                            </xsl:call-template>
                        </th>
                    </tr>
                </thead>
                <xsl:variable name="netIncome" select="sum(company/account[type = 'Income' or type = 'OtherIncome' or type = 'Cost' or type = 'Expense' or type = 'OtherExpense' or type = 'IncomeTaxExpense']/debit) - sum(company/account[type = 'Income' or type = 'OtherIncome' or type = 'Cost' or type = 'Expense' or type = 'OtherExpense' or type = 'IncomeTaxExpense']/credit)"/>
                <xsl:variable name="otherComprehensiveIncome" select="sum(company/account[type = 'OtherComprehensiveIncome']/debit) - sum(company/account[type = 'OtherComprehensiveIncome']/credit)"/>
                <tbody>
                    <tr>
                        <th class="in-1 left">Net income</th>
                        <th>
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="-$netIncome"/>
                            </xsl:call-template>
                        </th>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">OtherComprehensiveIncome</xsl:with-param>
                        <xsl:with-param name="balance" select="$otherComprehensiveIncome"/>
                        <xsl:with-param name="description">Other comprehensive income</xsl:with-param>
                        <xsl:with-param name="total">Total other comprehensive income</xsl:with-param>
                        <xsl:with-param name="negative">Total other comprehensive loss</xsl:with-param>
                        <xsl:with-param name="sign">-1</xsl:with-param>
                    </xsl:apply-templates>
                </tbody>
                <tfoot>
                    <tr>
                        <th class="left">Comprehensive income</th>
                        <td class="total right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="-$netIncome -$otherComprehensiveIncome"/>
                            </xsl:call-template>
                        </td>
                    </tr>
                </tfoot>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
