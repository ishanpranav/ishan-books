<?xml version="1.0" encoding="utf-8"?>
<!--
cash-flow-statement.xslt
Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
Licensed under the MIT License.
-->
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:html="http://www.w3.org/1999/xhtml"
    xmlns:liber="urn:liber"
    exclude-result-prefixes="msxsl">
    <xsl:include href="financial-statement.xslt"/>
    <xsl:output method="html" indent="yes"/>
    <xsl:template name="cash-flow-statement">
        <xsl:param name="title"/>
        <table>
            <thead>
                <tr>
                    <th colspan="2" class="subtitle">
                        <xsl:value-of select="company/name"/>
                    </th>
                </tr>
                <tr>
                    <th colspan="2" class="title">
                        <xsl:value-of select="$title"/>
                    </th>
                </tr>
                <tr>
                    <th colspan="2" class="bar dateline">
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
            <xsl:variable name="operating" select="-$netIncome + sum(company/account[cash-flow = 'Operating']/previous) - sum(company/account[cash-flow = 'Operating']/balance) + sum(company/account[cash-flow = 'GainLoss']/balance)"/>
            <xsl:variable name="investing" select="sum(company/account[(cash-flow = 'Investing')]/previous) - sum(company/account[cash-flow = 'Investing']/balance) - sum(company/account[cash-flow = 'GainLoss']/balance)"/>
            <xsl:variable name="financing" select="sum(company/account[(cash-flow = 'Financing')]/previous) - sum(company/account[(cash-flow = 'Financing')]/balance)"/>
            <tbody>
                <tr>
                    <th class="left">
                        <xsl:value-of select="concat(liber:gets('Bank'), ', ', liber:fdates(started))"/>
                    </th>
                    <th class="right">
                        <xsl:value-of select="liber:fm(sum(company/account[cash-flow = 'Cash']/previous))"/>
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
                        <xsl:for-each select="company/account[cash-flow = 'Operating' and (previous - balance != 0)]">
                            <tr>
                                <td class="in-4 left account">
                                    <xsl:value-of select="name"/>
                                </td>
                                <td class="right">
                                    <xsl:value-of select="liber:fm(previous - balance)"/>
                                </td>
                            </tr>
                        </xsl:for-each>
                        <xsl:for-each select="company/account[cash-flow = 'GainLoss' and balance != 0]">
                            <tr>
                                <td class="in-4 left account">
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
                        <xsl:for-each select="company/account[cash-flow = 'Investing' and (previous - balance != 0)]">
                            <tr>
                                <td class="in-3 left account">
                                    <xsl:value-of select="name"/>
                                </td>
                                <td class="right">
                                    <xsl:value-of select="liber:fm(previous - balance)"/>
                                </td>
                            </tr>
                        </xsl:for-each>
                        <xsl:for-each select="company/account[cash-flow = 'GainLoss' and balance != 0]">
                            <tr>
                                <td class="in-3 left account">
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
                        <xsl:for-each select="company/account[cash-flow = 'Financing' and (previous - balance != 0)]">
                            <tr>
                                <td class="in-3 left account">
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
                        <xsl:value-of select="liber:fm(sum(company/account[cash-flow = 'Cash']/balance))"/>
                    </th>
                </tr>
            </tbody>
        </table>
    </xsl:template>
</xsl:stylesheet>
