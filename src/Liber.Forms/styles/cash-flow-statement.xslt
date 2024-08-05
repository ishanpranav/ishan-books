<?xml version="1.0" encoding="utf-8"?>
<!--
cash-flow-statement.xslt
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
            <xsl:with-param name="styles">
                <link rel="stylesheet" type="text/css" href="https://liber.example/styles/cash-flow-statement.css"/>
            </xsl:with-param>
            <xsl:with-param name="table">
                <colgroup>
                    <col class="wide"></col>
                </colgroup>
                <thead>
                    <tr>
                        <th colspan="2">
                            <xsl:value-of select="company/name"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="2" class="title">
                            <xsl:value-of select="title"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="2" class="bar">
                            <xsl:value-of select="liber:ftspanl(started, posted)"/>
                        </th>
                    </tr>
                    <tr>
                        <th></th>
                        <th class="heading">
                            <xsl:value-of select="liber:ftspans(started, posted)"/>
                        </th>
                    </tr>
                </thead>
                <xsl:variable name="operating" select="sum(company/account[type = 'OtherCurrentAsset' or type = 'OtherCurrentLiability' or type = 'CreditCard']/previous) - sum(company/account[type = 'OtherCurrentAsset' or type = 'OtherCurrentLiability' or type = 'CreditCard']/balance) - $netIncome + sum(company/account[type = 'OtherIncomeExpense']/balance)"/>
                <xsl:variable name="investing" select="sum(company/account[(type = 'FixedAsset' or type = 'OtherAsset')]/previous) - sum(company/account[type = 'FixedAsset' or type = 'OtherAsset' or type = 'OtherIncomeExpense' or other-equity = 'true']/balance)"/>
                <xsl:variable name="financing" select="sum(company/account[(type = 'LongTermLiability')]/previous) - sum(company/account[(type = 'LongTermLiability')]/balance)"/>
                <tbody>
                    <tr>
                        <th class="left">
                            <xsl:value-of select="concat(liber:gets('Bank'), ', ', liber:fdates(started))"/>
                        </th>
                        <th class="right">
                            <xsl:value-of select="liber:fm(sum(company/account[type = 'Bank']/previous))"/>
                        </th>
                    </tr>
                    <xsl:choose>
                        <xsl:when test="$operating != 0">
                            <tr>
                                <th class="in-1 left">
                                    <xsl:value-of select="liber:gets('operating')"/>
                                </th>
                            </tr>
                            <tr>
                                <td class="in-2 left">
                                    <xsl:value-of select="liber:pngets('net-income', -$netIncome)"/>
                                </td>
                                <td class="right">
                                    <xsl:value-of select="liber:fm(-$netIncome)"/>
                                </td>
                            </tr>
                            <tr>
                                <td class="in-2 left">
                                    <xsl:value-of select="liber:fgets('adjustments{0}{1}', liber:pngets('net-income', -$netIncome), liber:pngets('operating', $operating))" />
                                </td>
                            </tr>
                            <xsl:for-each select="company/account[(type = 'OtherCurrentAsset' or type = 'OtherCurrentLiability' or type = 'CreditCard') and (previous - balance != 0)]">
                                <tr>
                                    <td class="in-4 left">
                                        <xsl:value-of select="name"/>
                                    </td>
                                    <td class="right">
                                        <xsl:value-of select="liber:fm(previous - balance)"/>
                                    </td>
                                </tr>
                            </xsl:for-each>
                            <xsl:for-each select="company/account[(type = 'OtherIncomeExpense') and balance != 0]">
                                <tr>
                                    <td class="in-4 left">
                                        <xsl:value-of select="name"/>
                                    </td>
                                    <td class="right">
                                        <xsl:value-of select="liber:fm(balance)"/>
                                    </td>
                                </tr>
                            </xsl:for-each>
                        </xsl:when>
                    </xsl:choose>
                    <tr>
                        <th class="in-1 left">
                            <xsl:value-of select="liber:pngets('operating', $operating)"/>
                        </th>
                        <th class="subtotal right">
                            <xsl:value-of select="liber:fm($operating)"/>
                        </th>
                    </tr>
                    <xsl:choose>
                        <xsl:when test="$investing != 0">
                            <tr>
                                <th class="in-1 left">
                                    <xsl:value-of select="liber:gets('investing')"/>
                                </th>
                            </tr>
                            <xsl:for-each select="company/account[(type = 'FixedAsset' or type = 'OtherAsset') and (previous - balance != 0)]">
                                <tr>
                                    <td class="in-3 left">
                                        <xsl:value-of select="name"/>
                                    </td>
                                    <td class="right">
                                        <xsl:value-of select="liber:fm(previous - balance)"/>
                                    </td>
                                </tr>
                            </xsl:for-each>
                            <xsl:for-each select="company/account[(type = 'OtherIncomeExpense' or other-equity = 'true') and balance != 0]">
                                <tr>
                                    <td class="in-3 left">
                                        <xsl:value-of select="name"/>
                                    </td>
                                    <td class="right">
                                        <xsl:value-of select="liber:fm(-balance)"/>
                                    </td>
                                </tr>
                            </xsl:for-each>
                        </xsl:when>
                    </xsl:choose>
                    <tr>
                        <th class="in-1 left">
                            <xsl:value-of select="liber:pngets('investing', $investing)"/>
                        </th>
                        <th class="subtotal right">
                            <xsl:value-of select="liber:fm($investing)"/>
                        </th>
                    </tr>
                    <xsl:choose>
                        <xsl:when test="$financing != 0">
                            <tr>
                                <th class="in-1 left">
                                    <xsl:value-of select="liber:gets('financing')"/>
                                </th>
                            </tr>
                            <xsl:for-each select="company/account[(type = 'LongTermLiability') and (previous - balance != 0)]">
                                <tr>
                                    <td class="in-3 left">
                                        <xsl:value-of select="name"/>
                                    </td>
                                    <td class="right">
                                        <xsl:value-of select="liber:fm(previous - balance)"/>
                                    </td>
                                </tr>
                            </xsl:for-each>
                        </xsl:when>
                    </xsl:choose>
                    <tr>
                        <th class="in-1 left">
                            <xsl:value-of select="liber:pngets('financing', $financing)"/>
                        </th>
                        <th class="subtotal right">
                            <xsl:value-of select="liber:fm($financing)"/>
                        </th>
                    </tr>
                    <tr>
                        <th class="left">
                            <xsl:value-of select="concat(liber:gets('Bank'), ', ', liber:fdates(posted))"/>
                        </th>
                        <th class="total right">
                            <xsl:value-of select="liber:fm(sum(company/account[type = 'Bank']/balance))"/>
                        </th>
                    </tr>
                </tbody>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
