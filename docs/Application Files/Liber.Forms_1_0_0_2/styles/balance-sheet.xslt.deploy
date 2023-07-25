<?xml version="1.0" encoding="utf-8"?>
<!--
balance-sheet.xslt
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
    <xsl:variable name="title">Balance Sheet</xsl:variable>
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
                            <xsl:text>As of </xsl:text>
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
                <tbody>
                    <xsl:variable name="bank" select="sum(company/account[type = 'Bank']/debit) - sum(company/account[type = 'Bank']/credit)"/>
                    <xsl:variable name="otherCurrentAsset" select="sum(company/account[type = 'OtherCurrentAsset']/debit) - sum(company/account[type = 'OtherCurrentAsset']/credit)"/>
                    <xsl:variable name="fixedAsset" select="sum(company/account[type = 'FixedAsset']/debit) - sum(company/account[type = 'FixedAsset']/credit)"/>
                    <xsl:variable name="otherAsset" select="sum(company/account[type = 'OtherAsset']/debit) - sum(company/account[type = 'OtherAsset']/credit)"/>
                    <xsl:variable name="equity" select="sum(company/account[type = 'Equity']/debit) - sum(company/account[type = 'Equity']/credit)"/>
                    <xsl:variable name="currentLiability" select="sum(company/account[type = 'CurrentLiability']/debit) - sum(company/account[type = 'CurrentLiability']/credit)"/>
                    <xsl:variable name="otherCurrentLiability" select="sum(company/account[type = 'OtherCurrentLiability']/debit) - sum(company/account[type = 'OtherCurrentLiability']/credit)"/>
                    <xsl:variable name="longTermLiability" select="sum(company/account[type = 'LongTermLiability']/debit) - sum(company/account[type = 'LongTermLiability']/credit)"/>
                    <tr>
                        <th class="left">Assets</th>
                        <th></th>
                    </tr>
                    <tr>
                        <th class="left in-1">Current assets</th>
                        <th></th>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">Bank</xsl:with-param>
                        <xsl:with-param name="balance" select="$bank"/>
                        <xsl:with-param name="description">Cash and cash equivalents</xsl:with-param>
                        <xsl:with-param name="total">Total cash and cash equivalents</xsl:with-param>
                        <xsl:with-param name="indent">2</xsl:with-param>
                    </xsl:apply-templates>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">OtherCurrentAsset</xsl:with-param>
                        <xsl:with-param name="balance" select="$otherCurrentAsset"/>
                        <xsl:with-param name="description">Other current assets</xsl:with-param>
                        <xsl:with-param name="total">Total other current assets</xsl:with-param>
                        <xsl:with-param name="indent">2</xsl:with-param>
                    </xsl:apply-templates>
                    <tr>
                        <th class="left in-1">Total current assets</th>
                        <th class="subtotal right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="$bank + $otherCurrentAsset"/>
                            </xsl:call-template>
                        </th>
                    </tr>
                    <tr>
                        <th class="left in-1">Non-current assets</th>
                        <th></th>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">FixedAsset</xsl:with-param>
                        <xsl:with-param name="balance" select="$fixedAsset"/>
                        <xsl:with-param name="description">Fixed assets</xsl:with-param>
                        <xsl:with-param name="total">Total fixed assets</xsl:with-param>
                        <xsl:with-param name="indent">2</xsl:with-param>
                    </xsl:apply-templates>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">OtherAsset</xsl:with-param>
                        <xsl:with-param name="balance" select="$otherAsset"/>
                        <xsl:with-param name="description">Other assets</xsl:with-param>
                        <xsl:with-param name="total">Total other assets</xsl:with-param>
                        <xsl:with-param name="indent">2</xsl:with-param>
                    </xsl:apply-templates>
                    <tr>
                        <th class="left in-1">Total non-current assets</th>
                        <th class="subtotal right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="$fixedAsset + $otherAsset"/>
                            </xsl:call-template>
                        </th>
                    </tr>
                    <tr>
                        <th class="left">Total assets</th>
                        <th class="total right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="$bank + $otherCurrentAsset + $fixedAsset + $otherAsset"/>
                            </xsl:call-template>
                        </th>
                    </tr>
                    <tr>
                        <th class="left">
                            <xsl:text>Liabilities and </xsl:text>
                            <xsl:call-template name="equity">
                                <xsl:with-param name="type" select="company/type"/>
                                <xsl:with-param name="value" select="-$equity"/>
                            </xsl:call-template>
                        </th>
                        <th></th>
                    </tr>
                    <tr>
                        <th class="left in-1">Current assets</th>
                        <th></th>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">Bank</xsl:with-param>
                        <xsl:with-param name="balance" select="$bank"/>
                        <xsl:with-param name="description">Cash and cash equivalents</xsl:with-param>
                        <xsl:with-param name="total">Total cash and cash equivalents</xsl:with-param>
                        <xsl:with-param name="indent">2</xsl:with-param>
                    </xsl:apply-templates>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">OtherCurrentAsset</xsl:with-param>
                        <xsl:with-param name="balance" select="$otherCurrentAsset"/>
                        <xsl:with-param name="description">Other current assets</xsl:with-param>
                        <xsl:with-param name="total">Total other current assets</xsl:with-param>
                        <xsl:with-param name="indent">2</xsl:with-param>
                    </xsl:apply-templates>
                    <tr>
                        <th class="left in-1">Total current assets</th>
                        <th class="subtotal right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="$bank + $otherCurrentAsset"/>
                            </xsl:call-template>
                        </th>
                    </tr>
                    <tr>
                        <th class="left in-1">Non-current assets</th>
                        <th></th>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">FixedAsset</xsl:with-param>
                        <xsl:with-param name="balance" select="$fixedAsset"/>
                        <xsl:with-param name="description">Fixed assets</xsl:with-param>
                        <xsl:with-param name="total">Total fixed assets</xsl:with-param>
                        <xsl:with-param name="indent">2</xsl:with-param>
                    </xsl:apply-templates>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">OtherAsset</xsl:with-param>
                        <xsl:with-param name="balance" select="$otherAsset"/>
                        <xsl:with-param name="description">Other assets</xsl:with-param>
                        <xsl:with-param name="total">Total other assets</xsl:with-param>
                        <xsl:with-param name="indent">2</xsl:with-param>
                    </xsl:apply-templates>
                    <tr>
                        <th class="left in-1">Total non-current assets</th>
                        <th class="subtotal right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="$fixedAsset + $otherAsset"/>
                            </xsl:call-template>
                        </th>
                    </tr>
                    <tr>
                        <th class="left">
                            <xsl:text>Total liabilities and </xsl:text>
                            <xsl:call-template name="equity">
                                <xsl:with-param name="type" select="company/type"/>
                                <xsl:with-param name="value" select="-$equity"/>
                            </xsl:call-template>
                        </th>
                        <th class="total right">
                            <xsl:call-template name="number">
                                <xsl:with-param name="value" select="-$currentLiability -$otherCurrentLiability - $longTermLiability -$equity"/>
                            </xsl:call-template>
                        </th>
                    </tr>
                </tbody>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
